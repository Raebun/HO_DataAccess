using DataAccess.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
	public class CustomerDAL
	{
		//string connectionString = ConfigurationManager.ConnectionStrings['sql'].ConnectionString;
		string connectionString;

		public List<CustomerDTO> Read()
		{
			List<CustomerDTO> result = new List<CustomerDTO>();

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "select * from TABLE";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						con.Open();
						SqlDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							int customerId = reader.GetInt32(0);
							string firstName = reader.GetString(0);
							string lastName = reader.GetString(1);
							string address = reader.GetString(2);
							string city = reader.GetString(3);
							string postalCode = reader.GetString(4);
							string phone = reader.GetString(5);

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
					string sqlQuery = "insert into TABLE (COLUMN, COLUMN) VALUES (@columnValues, @columnValues)";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@firstNameValues", customer.FirstName);
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
					string sqlQuery = "delete from TABLE where COLUMN = @columnValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@firstNameValues", customer.FirstName);
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
					string sqlQuery = "Update TABLE set COLUMN = @columnValues WHERE column = @columnValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@firstNameValues", customer.FirstName);
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
