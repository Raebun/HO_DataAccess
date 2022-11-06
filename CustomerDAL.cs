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
							int customerId = reader.GetInt32(0);
							string firstName = reader.GetString(1);
							string lastName = reader.GetString(2);
							string address = reader.GetString(3);
							string city = reader.GetString(4);
							string postalCode = reader.GetString(5);
							string phone = reader.GetString(6);

							CustomerDTO customer = new CustomerDTO();
							customer.CustomerId = customerId;
							customer.FirstName = firstName;
							customer.LastName = lastName;
							customer.Address = address;
							customer.City = city;
							customer.PostalCode = postalCode;
							customer.Phone = phone;

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

						if (affectedRows > 0)
						{
							// created
						}
						else
						{
							// not created
						}
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

						if (affectedRows > 0)
						{
							// deleted
						}
						else
						{
							// not deleted
						}
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
					string sqlQuery = "Update Customer SET firstName=@firstNameValues, lastName=@lastNameValues, homeAddress=@homeAddressValues, city=@cityValues, postalCode=@postalCodeValues, phone=@phoneValues  WHERE CustomerId = @customerIdValues";
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

						if (affectedRows > 0)
						{
							// updated
						}
						else
						{
							// not updated
						}
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
