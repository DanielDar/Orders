using System;
using System.Linq;
using System.Windows;
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

        public static string GetParamAfter(string urlPrefix)
        {
            var url = Application.Current.Host.NavigationState;
            if (url.StartsWith(urlPrefix) == false)
                return null;

            return url.Substring(urlPrefix.Length);
        }

        public string GetQueryParam(string name)
        {
            string url = ApplicationModel.NavigationState;
            var indexOf = url.IndexOf('?');
            if (indexOf == -1)
                return null;

            var options = url.Substring(indexOf + 1).Split(new[] { '&', }, StringSplitOptions.RemoveEmptyEntries);

            return (from option in options
                    where option.StartsWith(name) && option.Length > name.Length && option[name.Length] == '='
                    select option.Substring(name.Length + 1)
                    ).FirstOrDefault();
        }
    }
}