using DataAccess.DTO;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
	public class Product
	{
		string connectionString = ConfigurationManager.ConnectionStrings["sql"].ConnectionString;

		public ProductDTO ReadSingleRecord(int productId)
		{
			ProductDTO result = null;

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM Product WHERE productId = @productIdValues";

					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("productIdValues", productId);
						con.Open();
						SqlDataReader reader = command.ExecuteReader();
						reader.Read();

						if (reader.HasRows == true)
						{
							result = new ProductDTO();
							result.ProductId = reader.GetInt32(0);
							result.ProductName = reader.GetString(1);
							result.ProductDescription = reader.GetString(2);
							result.UnitCost = reader.GetDouble(3);

							if (reader.IsDBNull(1) == false)
							{
								int discountId = reader.GetInt32(4);
								Discount discount = new Discount();
								result.Discount = discount.ReadSingleRecord(discountId);
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

		public List<ProductDTO> Read()
		{
			List<ProductDTO> result = new List<ProductDTO>();

			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "SELECT * FROM Product";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						con.Open();
						SqlDataReader reader = command.ExecuteReader();

						while (reader.Read())
						{
							ProductDTO product = new ProductDTO();
							product.ProductId = reader.GetInt32(0);
							product.ProductName = reader.GetString(1);
							product.ProductDescription = reader.GetString(2);
							product.UnitCost = reader.GetDouble(3);

							if (reader.IsDBNull(1) == false)
							{
								int discountId = reader.GetInt32(4);
								Discount discount = new Discount();
								product.Discount = discount.ReadSingleRecord(discountId);
							}

							result.Add(product);
						}
					}
				}
			}
			catch (SqlException ex)
			{

			}

			return result;
		}

		public int Create(ProductDTO product)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "INSERT INTO Product (productName, productDescription, unitCost, discountId) VALUES (@productNameValues, @productDescriptionValues, @unitCostValues, @discountIdValues)";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@productNameValues", product.ProductName);
						command.Parameters.AddWithValue("@productDescriptionValues", product.ProductDescription);
						command.Parameters.AddWithValue("@unitCostValues", product.UnitCost);
						command.Parameters.AddWithValue("@discountIdValues", product.Discount.DiscountId);
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

		public int Delete(ProductDTO product)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "delete from Product where productId = @productIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@productIdValues", product.ProductId);
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

		public int Update(ProductDTO product)
		{
			int affectedRows = 0;
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string sqlQuery = "Update Product SET productName=@productNameValues, productDescription=@productDescriptionValues, unitCost=@unitCostValues discountId=@discountIdValues WHERE productId = @productIdValues";
					using (SqlCommand command = new SqlCommand(sqlQuery, con))
					{
						command.Parameters.AddWithValue("@productIdValues", product.ProductId);
						command.Parameters.AddWithValue("@productNameValues", product.ProductName);
						command.Parameters.AddWithValue("@productDescriptionValues", product.ProductDescription);
						command.Parameters.AddWithValue("@unitCostValues", product.UnitCost);
						command.Parameters.AddWithValue("@discountIdValues", product.Discount.DiscountId);
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
