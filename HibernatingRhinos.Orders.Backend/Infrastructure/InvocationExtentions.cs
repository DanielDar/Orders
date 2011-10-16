using System;
using System.Threading.Tasks;
using System.Windows;
using Raven.Client.Extensions;

namespace HibernatingRhinos.Orders.Backend.Infrastructure
{
    public static class InvocationExtentions
    {
        public static Task ContinueOnSuccess<T>(this Task<T> parent, Action<T> action)
        {
            return parent.ContinueWith(task => action(task.Result));
        }

        public static Task ContinueOnSuccess(this Task parent, Action action)
        {
            return parent.ContinueWith(task =>
            {
                if (task.IsFaulted)
                    return task;

                return TaskEx.Run(action);
            }).Unwrap();
        }

        public static Task ContinueOnSuccessInTheUiThread(this Task parent, Action action)
        {
            return parent.ContinueOnSuccess(() =>
            {
                if (Deployment.Current.Dispatcher.CheckAccess())
                    action();
                Deployment.Current.Dispatcher.InvokeAsync(action)
                    .Catch();
            });
        }

        public static Task Catch(this Task parent)
        {
            return parent.Catch(e => { });
        }

        public static Task Catch(this Task parent, Action<Exception> action)
        {
            parent.ContinueWith(task =>
            {
                if (task.IsFaulted == false)
                    return;

                Deployment.Current.Dispatcher.InvokeAsync(() => new ErrorWindow(task.Exception.ExtractSingleInnerException()).Show())
                    .ContinueWith(_ => action(task.Exception));
            });

            return parent;
        }
    }
}