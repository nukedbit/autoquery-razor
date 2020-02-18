using ServiceStack.DataAnnotations;
namespace AutoqueryRazor.ServiceModel
{
    public class Category
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

    }

}