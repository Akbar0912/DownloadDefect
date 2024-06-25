using DownloadDefect.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DownloadDefect._Repositories
{
    public class DefectRepository : IDefectRepository
    {
        private readonly string _dbConnection;

        public DefectRepository()
        {
            _dbConnection = ConfigurationManager.ConnectionStrings["LSBUDBConnectionQc"].ConnectionString;
        }

        public IEnumerable<DefectModel> GetAllResult()
        {
            var resultList = new List<DefectModel>();

            try
            {
                using (var connection = new SqlConnection(_dbConnection))
                {
                    connection.Open();

                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            SELECT 
                                DR.Id, 
                                DN.Id AS DefectId, 
                                DR.DateTime, 
                                DR.ModelNumber, 
                                DR.ModelCode, 
                                DR.SerialNumber, 
                                P.PartName AS PartId, 
                                L.LocationName AS LocationId, 
                                DN.DefectName, 
                                U.Name AS InspectorName 
                            FROM 
                                Defect_Results DR 
                            INNER JOIN 
                                LSBU_Auth.dbo.AspNetUsers U ON DR.InspectorId = U.NIK 
                            INNER JOIN 
                                Parts P ON DR.PartId = P.Id 
                            INNER JOIN 
                                LSBU_Common.dbo.Locations L ON DR.LocationId = L.Id 
                            INNER JOIN 
                                Defect_Names DN ON DR.DefectId = DN.Id 
                            WHERE 
                                CONVERT(date, DR.DateTime) = CONVERT(date, GETDATE())
                            ORDER BY 
                                DR.Id DESC";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                var dateTime = (DateTime)reader["DateTime"];
                                var resultModel = new DefectModel
                                {
                                    Id = reader["Id"].ToString(),
                                    Defect = reader["DefectName"].ToString(),
                                    Date = dateTime.ToString("yyyy-MM-dd"),
                                    Time = dateTime.ToString("HH:mm:ss"),
                                    ModelNumber = reader["ModelNumber"].ToString(),
                                    ModelCode = reader["ModelCode"].ToString(),
                                    SerialNumber = reader["SerialNumber"].ToString(),
                                    PartName = reader["PartId"].ToString(),
                                    LocationId = reader["LocationId"].ToString(),
                                    Inspector = reader["InspectorName"].ToString()
                                };
                                resultList.Add(resultModel);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return resultList;
        }

        public IEnumerable<DefectModel> GetFilter(string defectName, DateTime selectedDate)
        {
            List<DefectModel> results = new List<DefectModel>();
            string query = @"
                            SELECT 
                                DR.Id, 
                                DN.Id AS DefectId, 
                                DR.DateTime, 
                                DR.ModelNumber, 
                                DR.ModelCode, 
                                DR.SerialNumber, 
                                P.PartName AS PartId, 
                                L.LocationName AS LocationId, 
                                DN.DefectName, 
                                U.Name AS InspectorName 
                            FROM 
                                Defect_Results DR 
                            INNER JOIN 
                                LSBU_Auth.dbo.AspNetUsers U ON DR.InspectorId = U.NIK 
                            INNER JOIN 
                                Parts P ON DR.PartId = P.Id 
                            INNER JOIN 
                                LSBU_Common.dbo.Locations L ON DR.LocationId = L.Id 
                            INNER JOIN 
                                Defect_Names DN ON DR.DefectId = DN.Id 
                            WHERE 
								WHERE DefectName LIKE @DefectName AND CAST(DateTime AS DATE) = @SelectedDate
                            ORDER BY 
                                DR.Id DESC";



            using (SqlConnection connection = new SqlConnection(_dbConnection))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                command.Parameters.Add("@DefectName", SqlDbType.VarChar).Value = "%" + defectName;
                command.Parameters.Add("@SelectedDate", SqlDbType.Date).Value = selectedDate.Date;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dateTime = (DateTime)reader["DateTime"];
                        var result = new DefectModel
                        {
                            Id = reader["Id"].ToString(),
                            Defect = reader["DefectName"].ToString(),
                            Date = dateTime.ToString("yyyy-MM-dd"),
                            Time = dateTime.ToString("HH:mm:ss"),
                            ModelNumber = reader["ModelNumber"].ToString(),
                            ModelCode = reader["ModelCode"].ToString(),
                            SerialNumber = reader["SerialNumber"].ToString(),
                            PartName = reader["PartId"].ToString(),
                            LocationId = reader["LocationId"].ToString(),
                            Inspector = reader["InspectorName"].ToString()
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
