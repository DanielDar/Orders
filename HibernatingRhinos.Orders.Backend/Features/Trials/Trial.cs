using System;

namespace HibernatingRhinos.Orders.Backend.Features.Trials
{
    public class Trial
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Company { get; set; }

        public DateTime LicenseEndDate { get; set; }
    }
}
