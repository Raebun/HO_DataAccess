using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
	public class ProductDTO
	{
		// Properties
		private int productId;
		private string productName;
		private string productDescription;
		private double unitCost;
		private DiscountDTO discount;

		// Getters & Setters
		public int ProductId { get { return productId; } set { productId = value; } }
		public string ProductName { get { return productName; } set { productName = value; } }
		public string ProductDescription { get { return productDescription; } set { productDescription = value; } }
		public double UnitCost { get { return unitCost; } set { unitCost = value; } }
		public DiscountDTO Discount { get { return discount;  } set { discount = value; } }
	}
}
