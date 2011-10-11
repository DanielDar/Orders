using System;
using System.Threading.Tasks;

namespace HibernatingRhinos.Orders.Backend.Infrastructure
{
    public static class InvocationExtentions
    {
        public static Task ContinueOnSuccess<T>(this Task<T> parent, Action<T> action)
        {
            return parent.ContinueWith(task => action(task.Result));
        }
    }
}