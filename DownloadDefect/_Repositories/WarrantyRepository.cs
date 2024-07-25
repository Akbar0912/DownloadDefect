using DownloadData.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadData._Repositories
{
    public class WarrantyRepository : IWarrantyRepository
    {
        private readonly string _dbConnection;

        public WarrantyRepository()
        {
            _dbConnection = ConfigurationManager.ConnectionStrings["LSBUDBConnection"].ConnectionString;
        }

        public IEnumerable<WarrantyModel> GetAll()
        {
            List<WarrantyModel> models = new List<WarrantyModel>();

            string query = @"
                    SELECT 
                        Result_Warranty_Cards.Id,
                        Result_Warranty_Cards.JenisProduk,
                        Result_Warranty_Cards.ModelCode,
                        Result_Warranty_Cards.ModelNumber,
                        Result_Warranty_Cards.SerialNumber,
                        Result_Warranty_Cards.Register,
                        CONVERT(DATE, Result_Warranty_Cards.ScanningDate) AS ScanningDate,
                        Result_Warranty_Cards.ScanningTime,
                        Locations.LocationName AS Location,
                        AspNetUsers.Name AS OperatorId
                    FROM 
                        Result_Warranty_Cards
                    INNER JOIN 
                        AspNetUsers 
                    ON 
                        Result_Warranty_Cards.OperatorId = AspNetUsers.NIK
                    INNER JOIN 
                        Locations 
                    ON 
                        Result_Warranty_Cards.Location = Locations.Id
                    WHERE 
                        CONVERT(DATE, ScanningDate) = @date
                    ORDER BY 
                        Id DESC;
                ";

            using (SqlConnection connection = new SqlConnection(_dbConnection))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@date", Convert.ToDateTime(DateTime.Today).Date);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        models.Add(new WarrantyModel
                        {
                            Id = reader["Id"].ToString(),
                            JenisProduk = reader["JenisProduk"].ToString(),
                            ModelCode = reader["ModelCode"].ToString(),
                            ModelNumber = reader["ModelNumber"].ToString(),
                            NoSeri = reader["SerialNumber"].ToString(),
                            NoReg = reader["Register"].ToString(),
                            Date = Convert.ToDateTime(reader["ScanningDate"]).ToString("yyyy-MM-dd"),
                            ScanTime = reader["ScanningTime"].ToString(),
                            Location = reader["Location"].ToString(),
                            Inspector = reader["OperatorId"].ToString()
                        });
                    }
                }
                connection.Close();
            }
            return models;
        }

        public IEnumerable<WarrantyModel> GetFilter(string serialNumberd, DateTime selectDate)
        {
            List<WarrantyModel> results = new List<WarrantyModel>();

            string query = @"
                    SELECT 
                        Result_Warranty_Cards.Id, 
                        Result_Warranty_Cards.JenisProduk, 
                        Result_Warranty_Cards.ModelCode, 
                        Result_Warranty_Cards.ModelNumber, 
                        Result_Warranty_Cards.SerialNumber, 
                        Result_Warranty_Cards.Register, 
                        Result_Warranty_Cards.ScanningDate, 
                        Result_Warranty_Cards.ScanningTime, 
                        Locations.LocationName AS Location, 
                        AspNetUsers.Name AS OperatorId 
                    FROM 
                        Result_Warranty_Cards 
                    INNER JOIN 
                        AspNetUsers 
                    ON 
                        Result_Warranty_Cards.OperatorId = AspNetUsers.NIK 
                    INNER JOIN 
                        Locations 
                    ON 
                        Result_Warranty_Cards.Location = Locations.Id 
                    WHERE 
                        SerialNumber LIKE @SerialNumber 
                        AND CAST(ScanningDate AS DATE) = @SelectedDate";

            using (SqlConnection connection = new SqlConnection(_dbConnection))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = "%" + serialNumberd;
                command.Parameters.Add("@SelectedDate", SqlDbType.Date).Value = selectDate.Date;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = new WarrantyModel
                        {
                            Id = reader["Id"].ToString(),
                            JenisProduk = reader["JenisProduk"].ToString(),
                            ModelCode = reader["ModelCode"].ToString(),
                            ModelNumber = reader["ModelNumber"].ToString(),
                            NoSeri = reader["SerialNumber"].ToString(),
                            NoReg = reader["Register"].ToString(),
                            Date = Convert.ToDateTime(reader["ScanningDate"]).ToString("yyyy-MM-dd"),
                            ScanTime = reader["ScanningTime"].ToString(),
                            Location = reader["Location"].ToString(),
                            Inspector = reader["OperatorId"].ToString()
                        };
                        results.Add(result);
                    }
                }
                connection.Close();
            }

            return results;
        }
    }
}
