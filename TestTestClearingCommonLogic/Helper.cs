using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using uStore.Common.BLL.Datasource;

namespace TestTestClearingCommonLogic
{
    public static class CustomHtmlClearingCommonLogic
    {
        static readonly string dbConfDataSourceId = @"DatasourceID";
        static readonly string dbConfName = @"Name";
        static readonly string dbConfData = @"Data";

        /// <summary>
        /// Get data source configuration witch sets in uStore Presets.
        /// </summary>
        /// <returns>
        /// Data source configuration dictionary <ConfigId, ConfigValue>.
        /// </returns>
        public static Dictionary<string, string> GetDataSourceList()
        {
            try
            {
                Dictionary<string, string> dataSourceList = new Dictionary<string, string>();
                DataSet set = uStore.Common.BLL.Datasource.Datasource.ListDS();
                foreach (DataRow dr in set.Tables[0].Rows)
                {
                    if (dr[dbConfDataSourceId] != null
                        && dr[dbConfData] != null)
                    {
                        dataSourceList.Add(dr[dbConfDataSourceId].ToString(), dr[dbConfName].ToString().Trim());
                    }
                }
                return dataSourceList;
            }
            catch (Exception ex)
            {
                throw new Exception("GetDataSourceList()", ex);
            }
        }

        /// <summary>
        /// Get data source configuration witch sets in uStore Presets by ID.
        /// </summary>
        /// <param name="datasourceID">
        /// Data source identificator.
        /// </param>
        /// <returns>
        /// Data source configuration <ConfigName, ConfigData>.
        /// </returns>
        public static string GetConnectionStringByDataSourceId(string datasourceID)
        {
            foreach (DataRow row in Datasource.ListDS().Tables[0].Rows)
            {
                if (string.Compare(row[dbConfDataSourceId].ToString(), datasourceID, true) == 0)
                {
                    var ds = new DatasourceSQL();
                    ds.InitializeDatasource(row);
                    XmlSerializer serial = new XmlSerializer(typeof(DatasourceSQL));

                    if (row[dbConfData] != null)
                    {
                        return (serial.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(row[dbConfData].ToString().Trim()))) as DatasourceSQL).ConnectionString;
                    }
                }

            }
            throw new InvalidOperationException("Data source doesn't exist.");
        }

        public static SqlResult FetchData(string connectionStringId, string sqlQuery, SqlParameter parameters)
        {
            try
            {
                var resultDataset = new DataSet();
                if (connectionStringId != null && !String.IsNullOrWhiteSpace(connectionStringId))
                {
                    string connectionString = CustomHtmlClearingCommonLogic.GetConnectionStringByDataSourceId(connectionStringId);
                    if (sqlQuery != null && !String.IsNullOrWhiteSpace(sqlQuery))
                    {
                        using (var sqlConnection = new SqlConnection(connectionString))
                        {
                            sqlConnection.Open();
                            using (var sqlCommand = new SqlCommand(sqlQuery, sqlConnection))
                            {
                                sqlCommand.Parameters.AddWithValue(parameters.EmailParameter.Key, parameters.EmailParameter.Value);
                                sqlCommand.Parameters.AddWithValue(parameters.UserIdParameter.Key, parameters.UserIdParameter.Value);
                                sqlCommand.Parameters.AddWithValue(parameters.UserExternalIdParameter.Key, parameters.UserExternalIdParameter.Value);
                                sqlCommand.Parameters.AddWithValue(parameters.StoreIdParameter.Key, parameters.StoreIdParameter.Value);
                                sqlCommand.Parameters.AddWithValue(parameters.OrderIdParameter.Key, parameters.OrderIdParameter.Value);
                                sqlCommand.Parameters.AddWithValue(parameters.CurrencyIdParameter.Key, parameters.CurrencyIdParameter.Value);
                                sqlCommand.Parameters.AddWithValue(parameters.CustomParameter.Key, parameters.CustomParameter.Value);
                                using (var adapter = new SqlDataAdapter(sqlCommand))
                                {
                                    adapter.Fill(resultDataset);
                                }
                            }
                        }
                    }
                }
                return new SqlResult() { ExecutioResult = resultDataset };
            }
            catch (System.Exception ex)
            {
                return new SqlResult() { IsError = true, Message = ex.Message };
            }
        }
    }
}
