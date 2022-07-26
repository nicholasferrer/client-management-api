using ClientManagement.Controllers;
using ClientManagement.Models;
using ClientManagement.Services;
using FakeItEasy;
using FakeItEasy.Configuration;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Xunit;

namespace ClientManagement.Tests
{
    public class ClientControllerTests
    {
        private readonly Mock<IClientService> _clientServiceStub = new();

        private Client CreateClientMock()
        {
            return new Client
            {
                Id = "62dd97e8c2212fb7c54f2959",
                Name = "Teste",
                Email = "teste@bancoparana.com.br"
            };
        }

        [Fact]
        public void GetClients_ReturnsOkResult()
        {
            //Arrange
            var clients = new List<Client>{ CreateClientMock(), CreateClientMock(), CreateClientMock() };

            _clientServiceStub.Setup(service => service.GetClients())
                .Returns(clients);

            var clientController = new ClientController(_clientServiceStub.Object);

            //Act
            var result = clientController.Get();

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetClientById_WithExistingParam_ReturnsOkResult()
        {
            //Arrange
            _clientServiceStub.Setup(service => service.GetClientById("teste@bancoparana.com.br"))
                .Returns(CreateClientMock());

            var clientController = new ClientController(_clientServiceStub.Object);

            //Act
            var result = clientController.Get("teste@bancoparana.com.br");

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void GetClientById_WithUnexistingParam_ReturnsNotFound()
        {
            //Arrange
            _clientServiceStub.Setup(service => service.GetClientById(It.IsAny<string>()))
                .Returns<IClientService>(null);

            var clientController = new ClientController(_clientServiceStub.Object);

            //Act
            var result = clientController.Get("teste@bancoparana.com.br");

            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void CreateClient_WithExistingParam_ReturnsOkResult()
        {
            //Arrange
            var client = new Client { Name = "Nicholas", Email = "nicholasferrer@hotmail.com" };

            _clientServiceStub.Setup(service => service.CreateClient(client))
                .Returns(client);

            var clientController = new ClientController(_clientServiceStub.Object);

            //Act
            var result = clientController.Post(client);

            //Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void CreateClient_WithUnexistingParam_ReturnsOkResult()
        {
            //Arrange
            var client = CreateClientMock();

            _clientServiceStub.Setup(service => service.CreateClient(client))
                .Returns(client);

            var clientController = new ClientController(_clientServiceStub.Object);

            //Act
            var result = clientController.Post(client);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateClient_WithExistingParam_ReturnsOkResult()
        {
            //Arrange
            var client = CreateClientMock();

            _clientServiceStub.Setup(service => service.GetClientById(client.Email))
                .Returns(CreateClientMock());

            _clientServiceStub.Setup(service => service.Update(client.Email, client))
                .Verifiable();

            var clientController = new ClientController(_clientServiceStub.Object);

            //Act
            var result = clientController.Put(client.Email, client);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void UpdateClient_WithUnexistingParam_ReturnsOkResult()
        {
            //Arrange
            var client = CreateClientMock();

            _clientServiceStub.Setup(service => service.GetClientById(client.Email))
                .Returns(CreateClientMock());

            _clientServiceStub.Setup(service => service.Update(client.Email, client))
                .Verifiable();

            var clientController = new ClientController(_clientServiceStub.Object);

            //Act
            var result = clientController.Put("teste2@bancoparana.com.br", client);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteClient_WithExistingParam_ReturnsOkResult()
        {
            //Arrange
            var client = CreateClientMock();

            _clientServiceStub.Setup(service => service.GetClientById(client.Email))
                .Returns(CreateClientMock());

            _clientServiceStub.Setup(service => service.Delete(client.Email))
                .Verifiable();

            var clientController = new ClientController(_clientServiceStub.Object);

            //Act
            var result = clientController.Delete(client.Email);

            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public void DeleteClient_WithUnexistingParam_ReturnsOkResult()
        {
            //Arrange
            var client = CreateClientMock();

            _clientServiceStub.Setup(service => service.Delete(client.Email))
                .Verifiable();

            var clientController = new ClientController(_clientServiceStub.Object);

            //Act
            var result = clientController.Delete("teste2@bancoparana.com.br");

            //Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}