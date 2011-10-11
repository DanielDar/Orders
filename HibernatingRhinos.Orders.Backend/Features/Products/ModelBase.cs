using HibernatingRhinos.Orders.Backend.Infrastructure;
using Raven.Client;

namespace HibernatingRhinos.Orders.Backend.Features.Products
{
    public class ModelBase : NotifyPropertyChangedBase
    {
        private IAsyncDocumentSession session;
        protected IAsyncDocumentSession Session
        {
            get { return session ?? (session = RavenStoreHolder.Store.OpenAsyncSession()); }
        }

        protected void InitializeSession()
        {
            if (session == null)
                return;
            session = null;
        }
    }
}