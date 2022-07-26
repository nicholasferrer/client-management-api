using ClientManagement.Models;
using ClientManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;

        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;
        }

        // GET: api/<ClientController>
        [HttpGet]
        public ActionResult<List<Client>> Get()
        {
            return Ok(clientService.GetClients());
        }

        // GET api/<ClientController>/5
        [HttpGet("{email}")]
        public ActionResult<Client> Get(string email)
        {
            var client = clientService.GetClientById(email);

            if (client == null)
            {
                return NotFound($"Cliente com o email: {email} não encontrado.");
            }

            return Ok(client);
        }

        // POST api/<ClientController>
        [HttpPost]
        public ActionResult<Client> Post([FromBody] Client client)
        {
            if(client.Id != "")
            {
                return BadRequest("Enviar somente nome e email na requisição.");
            }

            clientService.CreateClient(client);

            return CreatedAtAction(nameof(Get), client);
        }

        // PUT api/<ClientController>/5
        [HttpPut("{email}")]
        public ActionResult<Client> Put(string email, [FromBody] Client client)
        {
            var existingClient = clientService.GetClientById(email);

            if(existingClient == null)
            {
                return NotFound($"Cliente com o email: {email} não encontrado.");
            }

            clientService.Update(email, client);

            return Ok(existingClient);

        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{email}")]
        public ActionResult<Client> Delete(string email)
        {
            var client = clientService.GetClientById(email);

            if (client == null)
            {
                return NotFound($"Cliente com o email: {email} não encontrado.");
            }

            clientService.Delete(email);

            return Ok($"Cliente com o email: {email} removido.");
        }
    }
}
