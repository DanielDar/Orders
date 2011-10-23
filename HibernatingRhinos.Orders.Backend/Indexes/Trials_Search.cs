using System;
using System.Linq;
using HibernatingRhinos.Orders.Backend.Features.Trials;
using Raven.Client.Indexes;

namespace HibernatingRhinos.Orders.Backend.Indexes
{
    public class Trials_Search : AbstractIndexCreationTask<Trial>
    {
        public class ReduceResult
        {
            public string Query { get; set; }
            public DateTime StartedAt { get;set; }
        }
        public Trials_Search()
        {
            Map = trials => from trial in trials
                            select new
                            {
                                Query = new[]
                                {
                                    trial.Company,
                                    trial.Name,
                                    trial.Id
                                },
                                trial.StartedAt
                            };
        }
    }
}