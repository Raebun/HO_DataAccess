using DataAccess.DTO;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
	public class OrderDetail
	{
		string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

		public OrderDetailDTO ReadSingleRecord(int orderDetailId)
		{
			OrderDetailDTO result = null;

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM OrderDetail WHERE orderDetailId = @orderDetailIdValues";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("productIdValues", orderDetailId);
						con.Open();
						SqlDataReader reader = command.ExecuteReader();
						reader.Read();

						if (reader.HasRows == true)
						{
							result = new OrderDetailDTO();
							result.OrderDetailId = reader.GetInt32(0);

							if (reader.IsDBNull(1) == false)
							{
								int productId = reader.GetInt32(1);
								Product product = new Product();
								result.Product = product.ReadSingleRecord(productId);
							}

							result.Quantity = reader.GetInt32(2);
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public List<OrderDetailDTO> Read()
		{
			List<OrderDetailDTO> result = new List<OrderDetailDTO>();

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM OrderDetail";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						con.Open();
						SqlDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							OrderDetailDTO orderDetail = new OrderDetailDTO();
							orderDetail.OrderDetailId = reader.GetInt32(0);
							if (reader.IsDBNull(1) == false)
							{
								int productId = reader.GetInt32(1);
								Product product = new Product();
								orderDetail.Product = product.ReadSingleRecord(productId);
							}
							orderDetail.Quantity = reader.GetInt32(2);

							result.Add(orderDetail);
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public int Create(OrderDetailDTO orderDetail)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "INSERT INTO OrderDetail (productId, quantity) VALUES (@productIdValues, @quantityValues)";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@productIdValues", orderDetail.Product.ProductId);
						command.Parameters.AddWithValue("@quantityValues", orderDetail.Quantity);
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

		public int Delete(OrderDetailDTO orderDetail)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "delete from OrderDetail where orderDetailId = @orderDetailIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@orderDetailIdValues", orderDetail.OrderDetailId);
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

		public int Update(OrderDetailDTO orderDetail)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "Update OrderDetail SET productId=@productIdValues quantity=@quantityValues WHERE orderDetailId = @orderDetailIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@orderDetailIdValues", orderDetail.OrderDetailId);
						command.Parameters.AddWithValue("@productIdValues", orderDetail.Product.ProductId);
						command.Parameters.AddWithValue("@quantityValues", orderDetail.Quantity);
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
