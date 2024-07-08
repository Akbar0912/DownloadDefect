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
    public class PackingRespository : IPackingRepository
    {
        private string DBConnection;

        public PackingRespository()
        {
            DBConnection = ConfigurationManager.ConnectionStrings["LSBUDBConnectionPacking"].ConnectionString;
        }

        public IEnumerable<PackingModel> GetAll()
        {
            List<PackingModel> models = new List<PackingModel>();
            string query = @"
                                SELECT 
                                    PR.Id,
                                    PR.Date, 
                                    PR.ModelNumber, 
                                    PR.GlobalCodeId, 
                                    PR.Adjustment,
                                    PR.ModelCodeId, 
                                    PR.ScanResult, 
                                    U.Name AS InspectorId
                                FROM Packing_Results PR
                                INNER JOIN LSBU_Auth.dbo.AspNetUsers U ON PR.InspectorId = U.NIK
                                WHERE CONVERT(DATE, PR.Date) = CONVERT(DATE, GETDATE()) 
                                ORDER BY PR.Id DESC;";

            using (SqlConnection connection = new SqlConnection(DBConnection))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PackingModel caseModel = new PackingModel
                        {
                            Id = reader["Id"].ToString(),
                            Date = Convert.ToDateTime(reader["Date"]).ToString("yyyy-MM-dd"),
                            Time = Convert.ToDateTime(reader["Date"]).ToString("HH:mm:ss"),
                            ModelNumber = reader["ModelNumber"].ToString(),
                            GlobalCodeId = reader["GlobalCodeId"].ToString(),
                            ScanResult = reader["ScanResult"].ToString(),
                            Adjustment = reader["Adjustment"].ToString(),
                            ModelCode = reader["ModelCodeId"].ToString(),
                            Inspector = reader["InspectorId"].ToString()
                        };
                        models.Add(caseModel);
                    }
                }
            }
            return models;
        }

        public IEnumerable<PackingModel> GetFilter(DateTime selectDate)
        {
            List<PackingModel> models = new List<PackingModel>();
            string query = @"
                                SELECT 
                                    PR.Id,
                                    PR.Date, 
                                    PR.ModelNumber, 
                                    PR.GlobalCodeId, 
                                    PR.Adjustment,
                                    PR.ModelCodeId, 
                                    PR.ScanResult, 
                                    U.Name AS InspectorId
                                FROM Packing_Results PR
                                INNER JOIN LSBU_Auth.dbo.AspNetUsers U ON PR.InspectorId = U.NIK
                                WHERE CAST(Date AS DATE) = @SelectedDate
                                ORDER BY PR.Id DESC;";

            using (SqlConnection connection = new SqlConnection(DBConnection))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.Add("@SelectedDate", SqlDbType.Date).Value = selectDate.Date;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PackingModel caseModel = new PackingModel
                        {
                            Id = reader["Id"].ToString(),
                            Date = Convert.ToDateTime(reader["Date"]).ToString("yyyy-MM-dd"),
                            Time = Convert.ToDateTime(reader["Date"]).ToString("HH:mm:ss"),
                            ModelNumber = reader["ModelNumber"].ToString(),
                            GlobalCodeId = reader["GlobalCodeId"].ToString(),
                            ScanResult = reader["ScanResult"].ToString(),
                            Adjustment = reader["Adjustment"].ToString(),
                            ModelCode = reader["ModelCodeId"].ToString(),
                            Inspector = reader["InspectorId"].ToString()
                        };
                        models.Add(caseModel);
                    }
                }
            }
            return models;
        }
    }
}
