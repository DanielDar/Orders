using System.ComponentModel;

namespace HibernatingRhinos.Orders.Backend.Infrastructure
{
    public interface IObservable : INotifyPropertyChanged
    {
        object Value { get; }
    }
}