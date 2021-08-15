namespace AssetManager.Models
{
    internal class BrokerDataModel : DataModel
    {
        public BrokerDataModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}