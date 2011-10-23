using System;

namespace HibernatingRhinos.Orders.Backend.Features.Orders
{
    public interface IEnd
    {
        DateTime EndsAt { get; set; }
    }
}