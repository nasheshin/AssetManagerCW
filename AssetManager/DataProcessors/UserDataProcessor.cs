using System.Collections.Generic;
using System.Data.SqlClient;
using AssetManager.Models;

namespace AssetManager.DataProcessors
{
    internal class UserDataProcessor : DataProcessor
    {
        private const string Table = "dbo.Users";
        private string[] _columns = { "Name", "Password", "BrokerId" };
        
        public UserDataProcessor() : base()
        {
            
        }

        public override IEnumerable<DataModel> Select()
        {
            var users = new List<DataModel>();
            
            var conn = DataConnector.Connection;
            var sqlQuery = $"SELECT * FROM {Table}";
            var command = new SqlCommand(sqlQuery, conn);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var password = reader.GetString(2);
                    var brokerId = reader.GetInt32(3);
                
                    users.Add(new UserDataModel(id, name, password, brokerId));
                }
            }
            
            return users;
        }
    }
}