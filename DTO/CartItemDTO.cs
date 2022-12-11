using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
	public class CartItemDTO
	{
		// Properties
		private int cartItemId;
		private int quantity;
		private ProductDTO product;
		private ShoppingCartDTO shoppingCart;

		// Getters and setters
		public int CartItemId { get { return cartItemId; } set { cartItemId = value; } }
		public int Quantity { get { return quantity; } set { quantity = value; } }
		public ProductDTO Product { get { return product; } set { product = value; } }
		public ShoppingCartDTO ShoppingCart { get { return shoppingCart; } set { shoppingCart = value; } }
	}
}
