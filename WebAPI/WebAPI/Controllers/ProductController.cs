using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebAPI.Models.DTOS;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        public IConfiguration _configuration { get; }
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns>Products</returns>
        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            string connectionstring = _configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string sql = "Select * From Products";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ProductDTO product = new ProductDTO();
                        product.IdProduct = Convert.ToInt32(dataReader["IdProduct"]);
                        product.Nombre = Convert.ToString(dataReader["Nombre"]);
                        product.Costo = Convert.ToDecimal(dataReader["Costo"]);
                        product.CantidadInventario = Convert.ToInt32(dataReader["CantidadInventario"]);
                        products.Add(product);
                    }
                }
                connection.Close();
            }
            return products;
        }
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true o false</returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<bool>> Login([FromBody] UserDTO user)
        {
            bool login = false;
            string connectionstring = _configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM Users WHERE Nombre = @Nombre AND Password = @password";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Nombre", user.Nombre);
                cmd.Parameters.AddWithValue("@Password", user.Password);

                int count = Convert.ToInt32(await cmd.ExecuteScalarAsync()); //devuelve la fila afectada

                if (count == 0)
                    login = false;
                else
                    login = true;
            }
            return await Task.FromResult(login);
        }
    }
}
