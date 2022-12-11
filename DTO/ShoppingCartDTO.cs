using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
	public class ShoppingCartDTO
	{
		// Properties
		private int cartId;
		private DateTime dateCreated = DateTime.Now;

		// Getters and setters
		public int CartId { get { return cartId; } set { cartId = value; } }
		public DateTime DateCreated { get { return dateCreated; } set { dateCreated = value; } }
	}
}
