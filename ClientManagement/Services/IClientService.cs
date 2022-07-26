using ClientManagement.Models;

namespace ClientManagement.Services
{
    public interface IClientService
    {
        Client CreateClient(Client client);
        List<Client> GetClients();
        Client GetClientById(string email);
        void Update(string email, Client client);
        void Delete(string email);
    }
}
