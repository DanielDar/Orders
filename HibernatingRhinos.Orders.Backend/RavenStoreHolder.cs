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

            IndexCreation.CreateIndexesAsync(typeof(Orders_Search).Assembly, Store);
        }

        public static IDocumentStore Store { get; private set; }
    }
}
