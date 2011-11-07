using System;

namespace HibernatingRhinos.Orders.Backend.Features.Orders
{
    public interface IEditable
    {
        string Id { get; set; }
    }
}