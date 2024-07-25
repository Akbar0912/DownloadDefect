using DownloadData.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DownloadData._Repositories
{
    public class DefectRepository : IDefectRepository
    {
        private readonly string _dbConnection;

        public DefectRepository()
        {
            _dbConnection = ConfigurationManager.ConnectionStrings["LSBUDBConnection"].ConnectionString;
        }

        public IEnumerable<DefectModel> GetAllResult()
        {
            var resultList = new List<DefectModel>();
            using (var connection = new SqlConnection(_dbConnection))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "SELECT " +
                    "    DR.Id, " +
                    "    DN.Id AS DefectId, " +
                    "    DR.DateTime, " +
                    "    DR.ModelNumber, " +
                    "    DR.ModelCode, " +
                    "    DR.SerialNumber, " +
                    "    L.LocationName AS LocationId, " +
                    "    DN.DefectName, " +
                    "    U.Name AS InspectorName " +
                    "FROM " +
                    "    Defect_Results DR " +
                    "INNER JOIN " +
                    "    AspNetUsers U ON DR.InspectorId = U.NIK " +
                    "INNER JOIN " +
                    "   Locations L ON DR.LocationId = L.Id " +
                    "INNER JOIN " +
                    "    Defect_Names DN ON DR.DefectId = DN.Id " +
                    "WHERE " +
                    "    CONVERT(date, DR.DateTime) = CONVERT(date, GETDATE())" +
                    "ORDER BY " +
                    "    DR.Id DESC";

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
                            LocationId = reader["LocationId"].ToString(),
                            Inspector = reader["InspectorName"].ToString()
                        };
                        resultList.Add(resultModel);
                    }
                }
            }

            return resultList;
        }

        public IEnumerable<DefectModel> GetFilter(string defectName, DateTime selectedDate)
        {
            var resultList = new List<DefectModel>();
            using (var connection = new SqlConnection(_dbConnection))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText =
                    "SELECT " +
                    "    DR.Id, " +
                    "    DN.Id AS DefectId, " +
                    "    DR.DateTime, " +
                    "    DR.ModelNumber, " +
                    "    DR.ModelCode, " +
                    "    DR.SerialNumber, " +
                    "    L.LocationName AS LocationId, " +
                    "    DN.DefectName, " +
                    "    U.Name AS InspectorName " +
                    "FROM " +
                    "    Defect_Results DR " +
                    "INNER JOIN " +
                    "    AspNetUsers U ON DR.InspectorId = U.NIK " +
                    "INNER JOIN " +
                    "   Locations L ON DR.LocationId = L.Id " +
                    "INNER JOIN " +
                    "    Defect_Names DN ON DR.DefectId = DN.Id " +
                    "WHERE " +
                    "    DefectName LIKE @DefectName AND CAST(DR.DateTime AS DATE) = @SelectedDate " +
                    "ORDER BY " +
                    "    DR.Id DESC";
                command.Parameters.Add("@DefectName", SqlDbType.VarChar).Value = "%" + defectName + "%";
                command.Parameters.Add("@SelectedDate", SqlDbType.Date).Value = selectedDate.Date;

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
                            LocationId = reader["LocationId"].ToString(),
                            Inspector = reader["InspectorName"].ToString()
                        };
                        resultList.Add(resultModel);
                    }
                }
            }

            return resultList;
        }
    }
}
