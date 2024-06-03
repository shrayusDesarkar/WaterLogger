using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public List<DrinkingWaterModel> Records { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            Records = GetAllRecords();
            ViewData["Total"] = Records.AsEnumerable().Sum(x => x.Quantity);
        }

        private List<DrinkingWaterModel> GetAllRecords()
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM drinking_water";

                var tableData = new List<DrinkingWaterModel>();
                SqliteDataReader sqliteDataReader = tableCmd.ExecuteReader();

                while (sqliteDataReader.Read())
                {
                    tableData.Add(
                        new DrinkingWaterModel
                        {
                            Id = sqliteDataReader.GetInt32(0),
                            Date = DateTime.Parse(sqliteDataReader.GetString(1), CultureInfo.CurrentUICulture.DateTimeFormat),
                            Quantity = sqliteDataReader.GetInt32(2),
                        }); ;
                }

                return tableData;
            }            
        }
    }
}
