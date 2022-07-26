using ClientManagement.Models;
using MongoDB.Driver;

namespace ClientManagement.Services
{
    public class ClientService : IClientService
    {
        private readonly IMongoCollection<Client> _clients;

        public ClientService(IClientStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _clients = database.GetCollection<Client>(settings.ClientsCollectionName);
        }

        public Client CreateClient(Client client)
        {
            _clients.InsertOne(client);
            return client;
        }

        public List<Client> GetClients()
        {
            return _clients.Find(_ => true).ToList();
        }

        public Client GetClientById(string email)
        {
            return _clients.Find(client => client.Email == email).FirstOrDefault();
        }

        public void Update(string email, Client client)
        {
            _clients.ReplaceOne(client => client.Email == email, client);
        }

        public void Delete(string email)
        {
            _clients.DeleteOne(client => client.Email == email);
        }
    }
}
