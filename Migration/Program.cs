using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HibernatingRhinos.Orders.Backend.Features.Orders;
using HibernatingRhinos.Orders.Backend.Features.Products;
using Raven.Client.Document;

namespace Migration
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var store = new DocumentStore
			{
				Url = "http://daniel-pc:8080",
				DefaultDatabase = "Orders2"
			}.Initialize())
			using (var entities = new Entities())
			{
				//var x = from trial in entities.Trials.ToArray()
				//        select new HibernatingRhinos.Orders.Backend.Features.Trials.Trial
				//        {
				//            Company = trial.Company,
				//            Email = trial.Email,
				//            Name = trial.Name,
				//            EndsAt = trial.Expiry.Value,
				//            StartedAt = trial.Expiry.Value.AddMonths(-1),
				//            TrackingId = trial.TrackingId.Value,
				//            ProductId = "products/" + trial.Profile
				//        };


				//using(var session = store.OpenSession())
				//{
				//    foreach (var trial in x)
				//    {
				//        session.Store(trial);
				//    }

				//    session.SaveChanges();
				//}


				var q = from order in entities.Orders
							.Include("Buyer")
							.Where(x => x.SWRegOrderNumber.StartsWith("E") &&
								(x.OrderType == 1 || x.OrderType == 2 || x.OrderType == 3)) // initial orders - SWReg
							.ToArray()
						select new HibernatingRhinos.Orders.Backend.Features.Orders.Order
						{
							Address = new Address
							{
								Address1 = order.Buyer.Address1,
								Address2 = order.Buyer.Address2,
								City = order.Buyer.City,
								Country = order.Buyer.Country,
								State = order.Buyer.StateProvince,
								ZipCode = order.Buyer.ZipPostalCode,
							},
							CompanyName = order.Buyer.CompanyName,
							Email = order.Buyer.ContactEmail,
							FirstName = order.Buyer.ContactFirstName,
							LastName = order.Buyer.ContactLastName,
							LicenseFor = order.Buyer.CompanyName ?? order.Buyer.ContactFirstName + " " + order.Buyer.ContactLastName,
							OrderedAt = order.OrderDate.Value,
							OrderNumber = order.UniqueOrderId,
							PhoneNumber = order.Buyer.ContactPhone,
							Quantity = order.LicenseQuantity ?? 0,
							LinkForReceipt = order.PlimusLinkForReceipt ?? order.SWRegLinkForReceipt,
							IpAddress = order.Buyer.IpAddress,
							Type = GetOrderType(order),
							Payments = new List<Payment>(
								from payment in entities.Orders
								where payment.SWRegOrderNumber.EndsWith(order.SWRegOrderNumber.Substring(1))					 
								select new Payment
								{
									Total = new Money
									{
										Amount = payment.Total ?? 0,
										Currency = payment.Currency
									},
									VAT = new Money
									{
										Amount = payment.Vat ?? 0,
										Currency = payment.Currency
									},
									At = payment.OrderDate.Value
								}),
							LicenseIds = new List<Guid>(order.LicenseFiles.Select(x=>x.Id)),
							ProductId = "products/" + order.Profile
						};


				using (var session = store.OpenSession())
				{
					foreach (var order in q)
					{
						session.Store(order);
					}

					session.SaveChanges();
				}

			}
		}

		private static ProductTypes GetOrderType(Order order)
		{
			switch (order.OrderType.Value)
			{
				case 1:
					return ProductTypes.LifeTime;
				case 2:
					return ProductTypes.Monthly;
				case 3:
					return ProductTypes.Yearly;
				default:
					throw new ArgumentException(order.OrderType.Value.ToString());
			}
		}
	}
}
