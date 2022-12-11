using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
	public class DiscountDTO
	{
		// Properties
		private int discountId;
		private string discountName;
		private DateTime dueTime;
		private double discountPercentage;

		// Getters and setters
		public int DiscountId { get { return discountId; } set { discountId = value; } }
		public string DiscountName { get { return discountName; } set { discountName = value; } }
		public DateTime DueTime { get { return dueTime; } set { dueTime = value; } }
		public double DiscountPercentage { get { return discountPercentage; } set { discountPercentage = value; } }
	}
}
