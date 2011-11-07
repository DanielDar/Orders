using System;
using System.Collections.Generic;
using HibernatingRhinos.Orders.Backend.Features.Products;

namespace HibernatingRhinos.Orders.Backend.Features.Orders
{
	public class Order : IEditable, IEnd
	{
		public ProductTypes Type { get; set; }

		public string Id { get; set; }

		public string OrderNumber { get; set; }

		public int Quantity { get; set; }

		public string ProductId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string CompanyName { get; set; }

		public string PhoneNumber { get; set; }

		public string Email { get; set; }

		public Address Address { get; set; }

		public List<string> Log { get; set; }

		public DateTime OrderedAt { get; set; }

	    private DateTime endsAt;
	    public DateTime EndsAt
	    {
	        get
	        {
	            switch (Type)
	            {
	                    case ProductTypes.LifeTime:
	                    return DateTime.MaxValue;
                        break;
	                    
                        case ProductTypes.Monthly:
	                    return OrderedAt.AddMonths(1);
	                    break;

                        case ProductTypes.Yearly:
	                    return OrderedAt.AddYears(1);
	                    break;

                    default:
	                    return DateTime.MinValue;
	            }
	        }
            set { endsAt = value; } 
        }

		public string LicenseFor { get; set; }

		public string LinkForReceipt { get; set; }

		public string IpAddress { get; set; }

		public List<Payment> Payments { get; set; }

		public List<Guid> LicenseIds { get; set; }

		public Order()
		{
			Address = new Address();
			Payments = new List<Payment>();
			LicenseIds = new List<Guid>();
			Log = new List<string>();
		}
	}

	public class Payment
	{
		public Money Total { get; set; }
		public Money VAT { get; set; }
		public DateTime At { get; set; }
	}

	public class Money
	{
		public string Currency { get; set; }
		public decimal Amount { get; set; }
	}
}
