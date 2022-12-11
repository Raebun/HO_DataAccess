using DataAccess.DTO;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
	public class CartItem
	{
		string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

		public CartItemDTO ReadSingleRecord(int cartItemId)
		{
			CartItemDTO result = null;

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM CartItem WHERE cartItemId = @cartItemIdValues";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("cartItemIdValues", cartItemId);
						con.Open();
						SqlDataReader reader = command.ExecuteReader();
						reader.Read();

						if (reader.HasRows == true)
						{
							result = new CartItemDTO();
							result.CartItemId = reader.GetInt32(0);

							if (!reader.IsDBNull(1))
							{
								int productId = reader.GetInt32(1);
								Product product = new Product();
								result.Product = product.ReadSingleRecord(productId);
							}

							result.Quantity = reader.GetInt32(2);

							if (!reader.IsDBNull(3))
							{
								int cartId = reader.GetInt32(3);
								ShoppingCart shoppingCart = new ShoppingCart();
								result.ShoppingCart = shoppingCart.ReadSingleRecord(cartId);
							}
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public List<CartItemDTO> Read()
		{
			List<CartItemDTO> result = new List<CartItemDTO>();

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM CartItem";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						con.Open();
						SqlDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							CartItemDTO cartItem = new CartItemDTO();
							cartItem.CartItemId = reader.GetInt32(0);
							if (reader.IsDBNull(1) == false)
							{
								int productId = reader.GetInt32(1);
								Product product = new Product();
								cartItem.Product = product.ReadSingleRecord(productId);
							}

							cartItem.Quantity = reader.GetInt32(2);

							if (!reader.IsDBNull(3))
							{
								int cartId = reader.GetInt32(3);
								ShoppingCart shoppingCart = new ShoppingCart();
								cartItem.ShoppingCart = shoppingCart.ReadSingleRecord(cartId);
							}

							result.Add(cartItem);
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public int Create(CartItemDTO cartItem)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "INSERT INTO CartItem (productId, quantity, cartId) VALUES (@productIdValues, @quantityValues, @cartIdValues)";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@productIdValues", cartItem.Product.ProductId);
						command.Parameters.AddWithValue("@quantityValues", cartItem.Quantity);
						command.Parameters.AddWithValue("@cartIdValues", cartItem.ShoppingCart.CartId);
						con.Open();

						affectedRows = command.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return affectedRows;
		}

		public int Delete(CartItemDTO cartItem)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "delete from CartItem where cartItemId = @cartItemIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@cartItemIdValues", cartItem.CartItemId);
						con.Open();

						affectedRows = command.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return affectedRows;
		}

		public int Update(CartItemDTO cartItem)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "Update CartItem SET productId=@productIdValues, quantity=@quantityValues WHERE cartItemId = @cartItemIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@cartItemIdValues", cartItem.CartItemId);
						command.Parameters.AddWithValue("@productIdValues", cartItem.Product.ProductId);
						command.Parameters.AddWithValue("@quantityValues", cartItem.Quantity);
						con.Open();

						affectedRows = command.ExecuteNonQuery();
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return affectedRows;
		}
	}
}
