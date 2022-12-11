using DataAccess.DTO;
using System.Configuration;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess
{
	public class ShoppingCart
	{
		string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

		public ShoppingCartDTO ReadSingleRecord(int cartId)
		{
			ShoppingCartDTO result = null;

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM ShoppingCart WHERE cartId = @cartIdValues";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("cartIdValues", cartId);
						con.Open();
						SqlDataReader reader = command.ExecuteReader();
						reader.Read();

						if (reader.HasRows == true)
						{
							result = new ShoppingCartDTO();
							result.CartId = reader.GetInt32(0);
							result.DateCreated = reader.GetDateTime(1);


						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public List<ShoppingCartDTO> Read()
		{
			List<ShoppingCartDTO> result = new List<ShoppingCartDTO>();

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM ShoppingCart";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						con.Open();
						SqlDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							ShoppingCartDTO shoppingCart = new ShoppingCartDTO();
							shoppingCart.CartId = reader.GetInt32(0);
							shoppingCart.DateCreated = reader.GetDateTime(1);

							result.Add(shoppingCart);
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public int Create(ShoppingCartDTO shoppingCart)
		{
			decimal getId = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "INSERT INTO ShoppingCart (dateCreated) VALUES (@dateCreatedValues); SELECT scope_identity()";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@dateCreatedValues", shoppingCart.DateCreated);
						con.Open();

						getId = (decimal)command.ExecuteScalar();
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return (int)getId;
		}

		public int Delete(ShoppingCartDTO shoppingCart)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "delete from ShoppingCart where cartId = @cartIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@cartIdValues", shoppingCart.CartId);
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

		public int Update(ShoppingCartDTO shoppingCart)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "Update ShoppingCart SET dateCreated=@dateCreatedValues WHERE cartId = @cartIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@cartIdValues", shoppingCart.CartId);
						command.Parameters.AddWithValue("@dateCreatedValues", shoppingCart.DateCreated);
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

		public int AddShoppingCartToCustomer(CustomerDTO customer)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "Update Customer SET cartId=@cartIdValues WHERE CustomerId = @customerIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@customerIdValues", customer.CustomerId);
						command.Parameters.AddWithValue("@cartIdValues", customer.ShoppingCart.CartId);
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
