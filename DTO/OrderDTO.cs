using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
	public class OrderDTO
	{
		// Properties
		private int orderId;
		private OrderDetailDTO orderDetail;
		private DateTime dateCreated;

		// Getters and setters
		public int OrderId { get { return orderId; } set { orderId = value; } }
		public OrderDetailDTO OrderDetail { get { return orderDetail;  } set { orderDetail = value; } }
		public DateTime DateCreated { get { return dateCreated; } set { dateCreated = value; } }
	}
}
