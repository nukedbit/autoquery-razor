using AutoqueryRazor.ServiceModel;
using ServiceStack;

namespace razor.ServiceInterface
{
    [Route("/api/categories")]
    public class QueryCategories : QueryDb<Category> { }
}