namespace AssetManager.Models
{
    internal class UserDataModel : DataModel
    {
        public UserDataModel(int id, string name, string password, int brokerId)
        {
            Id = id;
            Name = name;
            Password = password;
            BrokerId = brokerId;
        }

        public int Id { get; }
        public string Name { get; }
        public string Password { get; }
        public int BrokerId { get; }
    }
}