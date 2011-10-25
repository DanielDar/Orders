using System.Reflection;
using HibernatingRhinos.Orders.Backend.Indexes;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace HibernatingRhinos.Orders.Backend
{
    public static class RavenStoreHolder
    {
        static RavenStoreHolder()
        {
            Store = new DocumentStore
            {
                Url = "http://localhost:8080"
            }.Initialize();

            //IndexCreation.CreateIndexesAsync(typeof(Orders_Search).Assembly, Store);
            var productsStats = new Products_Stats();
            productsStats.ExecuteAsync(Store.AsyncDatabaseCommands, Store.Conventions)
                .ContinueWith(task =>
                {
                    var indexDefinition = productsStats.CreateIndexDefinition();
                    var map = indexDefinition.Map;
                    var reduce = indexDefinition.Reduce;
                });
        }

        public static IDocumentStore Store { get; private set; }
    }
}
