using DataAccess.DTO;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
	public class Order
	{
		string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

		public OrderDTO ReadSingleRecord(int orderId)
		{
			OrderDTO result = null;

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM Order WHERE orderId = @orderIdValues";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("orderIdValues", orderId);
						con.Open();
						SqlDataReader reader = command.ExecuteReader();
						reader.Read();

						if (reader.HasRows == true)
						{
							result = new OrderDTO();
							result.OrderId = reader.GetInt32(0);

							if (reader.IsDBNull(1) == false)
							{
								int orderDetailId = reader.GetInt32(1);
								OrderDetail orderDetail = new OrderDetail();
								result.OrderDetail = orderDetail.ReadSingleRecord(orderDetailId);
							}

							result.DateCreated = reader.GetDateTime(2);
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public List<OrderDTO> Read()
		{
			List<OrderDTO> result = new List<OrderDTO>();

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM Order";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						con.Open();
						SqlDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							OrderDTO order = new OrderDTO();
							order.OrderId = reader.GetInt32(0);
							if (reader.IsDBNull(1) == false)
							{
								int orderDetailId = reader.GetInt32(1);
								OrderDetail orderDetail = new OrderDetail();
								order.OrderDetail = orderDetail.ReadSingleRecord(orderDetailId);
							}
							order.DateCreated = reader.GetDateTime(2);


							result.Add(order);
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public int Create(OrderDTO order)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "INSERT INTO Order (orderDetailId, dateCreated) VALUES (@orderDetailIdValues, @dateCreatedValues)";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@orderDetailIdValues", order.OrderDetail.OrderDetailId);
						command.Parameters.AddWithValue("@dateCreatedValues", order.DateCreated);
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

		public int Delete(OrderDTO order)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "delete from Order where orderId = @orderIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@orderIdValues", order.OrderId);
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

		public int Update(OrderDTO order)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "Update Order SET orderDetailId=@orderDetailIdValues, dateCreated=@dateCreatedValues WHERE orderId = @orderIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@orderIdValues", order.OrderId);
						command.Parameters.AddWithValue("@orderDetailIdValues", order.OrderDetail.OrderDetailId);
						command.Parameters.AddWithValue("@dateCreatedValues", order.DateCreated);
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
