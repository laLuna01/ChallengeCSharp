using Xunit;
using Moq;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Tests.Services
{
    public class PaisServiceTests
    {
        [Fact]
        public async Task AdicionarPais_DeveChamarRepositorioComDadosCorretos()
        {
            // Arrange
            var pais = new Pais { NOME = "Brasil" };

            var mockRepo = new Mock<IPaisRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Pais>())).Returns(Task.CompletedTask);

            var service = new PaisService(mockRepo.Object);

            // Act
            await service.AddAsync(pais);

            // Assert
            mockRepo.Verify(r => r.AddAsync(It.Is<Pais>(p => p.NOME == "Brasil")), Times.Once);
        }
    }
}