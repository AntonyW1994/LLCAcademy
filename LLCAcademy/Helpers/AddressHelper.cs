using LLCAcademy.Models;
using Dapper;
using System.Data.SqlClient;
using System.Linq;

namespace LLCAcademy.Helpers
{
    public static class AddressHelper
    {
        public static Address GetAddressByID(int addressID)
        {
            var connectionString = System.Configuration.ConfigurationManager.AppSettings["DatabaseConnection"];
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string sql = "SELECT Id, Line1, Line2, Line3, Town, County, Postcode, GoogleMapsUrl FROM Address WHERE Id = @Id";
                var addresses = connection.Query<Address>(sql, new { Id = addressID });
                return addresses.FirstOrDefault();
            }
        }
    }
}