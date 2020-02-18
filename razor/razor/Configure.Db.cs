using AutoqueryRazor.ServiceModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace AutoqueryRazor
{
    public class MyTable
    {
        [AutoIncrement]
        public int Id { get; set; }        
        public string Name { get; set; }
    }
        
    public class ConfigureDb : IConfigureServices, IConfigureAppHost
    {
        IConfiguration Configuration { get; }
        public ConfigureDb(IConfiguration configuration) => Configuration = configuration;

        public void Configure(IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                Configuration.GetConnectionString("DefaultConnection") 
                    ?? "Server=localhost;User Id=postgres;Password=ASec@Pwd123;Database=testautoqueryrazor;Pooling=true;MinPoolSize=0;MaxPoolSize=200",
                PostgreSqlDialect.Provider));
        }

        public void Configure(IAppHost appHost)
        {
            appHost.GetPlugin<SharpPagesFeature>()?.ScriptMethods.Add(new DbScriptsAsync());

            using (var db = appHost.Resolve<IDbConnectionFactory>().Open())
            {
                if (db.CreateTableIfNotExists<MyTable>())
                {
                    db.Insert(new MyTable { Name = "Seed Data for new MyTable" });
                }

                if (db.CreateTableIfNotExists<Category>())
                {
                    db.InsertAll(new []{ new Category() { Name = "Bar" } , 
                        new Category() { Name = "Bar2" } }); 
                }

            }
        }
    }    
}
