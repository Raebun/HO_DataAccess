using DataAccess.DTO;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
	public class CustomerDAL
	{
		string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

		public List<CustomerDTO> Read()
		{
			List<CustomerDTO> result = new List<CustomerDTO>();

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM Customer";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						con.Open();
						SqlDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							CustomerDTO customer = new CustomerDTO();
							customer.CustomerId = reader.GetInt32(0);
							customer.FirstName = reader.GetString(1);
							customer.LastName = reader.GetString(2);
							customer.Address = reader.GetString(3);
							customer.City = reader.GetString(4);
							customer.PostalCode = reader.GetString(5);
							customer.Phone = reader.GetString(6);
							if (!reader.IsDBNull(7))
							{
								int orderId = reader.GetInt32(7);
								Order order = new Order();
								customer.Order = order.ReadSingleRecord(orderId);
							}
							if (!reader.IsDBNull(8))
							{
								int cartId = reader.GetInt32(8);
								ShoppingCart shoppingCart = new ShoppingCart();
								customer.ShoppingCart = shoppingCart.ReadSingleRecord(cartId);
							}

							result.Add(customer);
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public int Create(CustomerDTO customer)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "INSERT INTO Customer (firstName, lastName, homeAddress, city, postalCode, phone) VALUES (@firstNameValues, @lastNameValues, @homeAddressValues, @cityValues, @postalCodeValues, @phoneValues)";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@firstNameValues", customer.FirstName);
						command.Parameters.AddWithValue("@lastNameValues", customer.LastName);
						command.Parameters.AddWithValue("@homeAddressValues", customer.Address);
						command.Parameters.AddWithValue("@cityValues", customer.City);
						command.Parameters.AddWithValue("@postalCodeValues", customer.PostalCode);
						command.Parameters.AddWithValue("@phoneValues", customer.Phone);
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

		public int Delete(CustomerDTO customer)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "delete from Customer where CustomerId = @customerIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@customerIdValues", customer.CustomerId);
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

		public int Update(CustomerDTO customer)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "Update Customer SET firstName=@firstNameValues, lastName=@lastNameValues, homeAddress=@homeAddressValues, city=@cityValues, postalCode=@postalCodeValues, phone=@phoneValues WHERE CustomerId = @customerIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@customerIdValues", customer.CustomerId);
						command.Parameters.AddWithValue("@firstNameValues", customer.FirstName);
						command.Parameters.AddWithValue("@lastNameValues", customer.LastName);
						command.Parameters.AddWithValue("@homeAddressValues", customer.Address);
						command.Parameters.AddWithValue("@cityValues", customer.City);
						command.Parameters.AddWithValue("@postalCodeValues", customer.PostalCode);
						command.Parameters.AddWithValue("@phoneValues", customer.Phone);
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
