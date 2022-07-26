namespace ClientManagement.Models
{
    public interface IClientStoreDatabaseSettings
    {
        string ClientsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
