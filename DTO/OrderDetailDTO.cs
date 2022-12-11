using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
	public class OrderDetailDTO
	{
		// Properties
		private int orderDetailId;
		private int quantity;
		private ProductDTO product;

		// Getters and setters
		public int OrderDetailId { get { return orderDetailId; } set { orderDetailId = value; } }
		public int Quantity { get { return quantity; } set { quantity = value; } }
		public ProductDTO Product { get { return product; } set { product = value; } }
	}
}
