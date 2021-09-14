using System;
using System.Collections.Generic;
using System.Linq;
using AssetManager.Models;

namespace AssetManager.DataUtils
{
    public class DataProcessorBrokers : DataProcessorBase
    {
        public DataProcessorBrokers(DataContext database) : base(database)
        {
        }

        public List<Broker> Brokers => Database.Brokers.ToList();

        public override void AddElement(object element)
        {
            if (!(element is Broker broker))
                throw new ArgumentException("Element type is not broker");

            Database.Brokers.Add(broker);
            Save();
        }

        public override void RemoveElement(object element)
        {
            throw new NotImplementedException();
        }

        public Broker FindBrokerByName(string name)
        {
            return Brokers.FirstOrDefault(br => br.Name.ToLower() == name.ToLower());
        }
    }
}