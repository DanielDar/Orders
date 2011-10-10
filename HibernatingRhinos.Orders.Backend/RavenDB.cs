using Raven.Client;
using Raven.Client.Document;

namespace HibernatingRhinos.Orders.Backend
{
    public static class RavenDB
    {
        static RavenDB()
        {
            Store = new DocumentStore
            {
                Url = "http://localhost:8080"
            }.Initialize();
        }

        public static IDocumentStore Store { get; private set; }
    }
}
