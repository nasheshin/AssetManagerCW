using System.Collections.Generic;
using System.Data.SqlClient;
using AssetManager.Models;

namespace AssetManager.DataProcessors
{
    internal class BrokerDataProcessor : DataProcessor
    {
        private const string Table = "dbo.Brokers";
        private string[] _columns = { "Name" };
        
        public BrokerDataProcessor() : base()
        {
            
        }

        public override IEnumerable<DataModel> Select()
        {
            var brokers = new List<DataModel>();
            
            var conn = DataConnector.Connection;
            var sqlQuery = $"SELECT * FROM {Table}";
            var command = new SqlCommand(sqlQuery, conn);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);

                    brokers.Add(new BrokerDataModel(id, name));
                }
            }

            return brokers;
        }
    }
}