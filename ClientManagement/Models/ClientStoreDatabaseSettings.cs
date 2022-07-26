namespace ClientManagement.Models
{
    public class ClientStoreDatabaseSettings : IClientStoreDatabaseSettings
    {
        public string ClientsCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
