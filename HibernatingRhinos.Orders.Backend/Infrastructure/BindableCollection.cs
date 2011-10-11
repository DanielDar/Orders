﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace HibernatingRhinos.Orders.Backend.Infrastructure
{
    public class BindableCollection<T> : ObservableCollection<T>
    {
        private readonly Dispatcher init = Deployment.Current.Dispatcher;

        private IEqualityComparer<T> comparer;

        public BindableCollection(IEqualityComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public void Execute(Action action)
        {
            if (init.CheckAccess())
                action();
            else
                init.InvokeAsync(action);
        }

        public void Match(ICollection<T> items)
        {
            Execute(() =>
            {
                var toAdd = items.Except(this, comparer).ToArray();
                var toRemove = this.Except(items, comparer).ToArray();

                foreach (var remove in toRemove)
                {
                    Remove(remove);
                }

                foreach (var add in toAdd)
                {
                    Add(add);
                }
            });
        }

        public void Set(IEnumerable<T> enumerable)
        {
            Execute(() =>
            {
                Clear();
                foreach (var v in enumerable)
                {
                    Add(v);
                }
            });
        }
    }
}