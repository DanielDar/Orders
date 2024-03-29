﻿using System;
using HibernatingRhinos.Orders.Backend.Features.Orders;

namespace HibernatingRhinos.Orders.Backend.Features.Trials
{
    public class Trial : IEnd, IEditable
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Company { get; set; }

        public DateTime EndsAt { get; set; }

        public DateTime StartedAt { get; set; }

    	public string Email { get; set; }

		public Guid TrackingId { get; set; }

		public string ProductId { get; set; }
    }
}
