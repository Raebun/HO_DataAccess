using DataAccess.DTO;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
	public class Discount
	{
		string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

		public DiscountDTO ReadSingleRecord(int discountId)
		{
			DiscountDTO result = null;

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM Discount WHERE discountId = @discountIdValues";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("discountIdValues", discountId);
						con.Open();
						SqlDataReader reader = command.ExecuteReader();
						reader.Read();
						
						if(reader.HasRows == true)
						{
							result = new DiscountDTO();
							result.DiscountId = reader.GetInt32(0);
							result.DiscountName = reader.GetString(1);
							result.DueTime = reader.GetDateTime(2);
							result.DiscountPercentage = reader.GetDouble(3);
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public List<DiscountDTO> Read()
		{
			List<DiscountDTO> result = new List<DiscountDTO>();

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM Discount";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						con.Open();
						SqlDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							int discountId = reader.GetInt32(0);
							string discountName = reader.GetString(1);
							DateTime dueTime = reader.GetDateTime(2);
							double discountPercentage = reader.GetDouble(3);

							DiscountDTO discount = new DiscountDTO();
							discount.DiscountId = discountId;
							discount.DiscountName = discountName;
							discount.DueTime = dueTime;
							discount.DiscountPercentage = discountPercentage;

							result.Add(discount);
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public int Create(DiscountDTO discount)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "INSERT INTO Discount (discountName, dueTime, discountPercentage) VALUES (@discountNameValues, @dueTimeValues, @discountPercentageValues)";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@discountNameValues", discount.DiscountName);
						command.Parameters.AddWithValue("@dueTimeValues", discount.DueTime);
						command.Parameters.AddWithValue("@discountPercentageValues", discount.DiscountPercentage);
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

		public int Delete(DiscountDTO discount)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "delete from Discount where discountId = @discountIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@discountIdValues", discount.DiscountId);
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

		public int Update(DiscountDTO discount)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "Update Discount SET discountName=@discountNameValues, dueTimeValues=@dueTimeValues, discountPercentage=@discountPercentageValues WHERE discountId = @discountIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@discountIdValues", discount.DiscountId);
						command.Parameters.AddWithValue("@discountNameValues", discount.DiscountName);
						command.Parameters.AddWithValue("@dueTimeValues", discount.DueTime);
						command.Parameters.AddWithValue("@discountPercentageValues", discount.DiscountPercentage);
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
