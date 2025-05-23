using Xunit;
using Moq;
using System.Threading.Tasks;
using ChallengeCSharp.Domain.Entities;
using ChallengeCSharp.Application.Services;
using ChallengeCSharp.Domain.Interfaces;

namespace ChallengeCSharp.Tests.Services
{
    public class EstadoServiceTests
    {
        [Fact]
        public async Task AdicionarEstado_DeveChamarRepositorioComDadosCorretos()
        {
            // Arrange
            var estado = new Estado
            {
                NOME_ESTADO = "São Paulo",
                COD_PAIS = 1
            };

            var mockEstadoRepo = new Mock<IEstadoRepository>();
            var mockPaisRepo = new Mock<IPaisRepository>();

            // Simula que o país existe, se o serviço verificar isso
            mockPaisRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Pais { COD_PAIS = 1, NOME = "Brasil" });

            mockEstadoRepo.Setup(r => r.AddAsync(It.IsAny<Estado>())).Returns(Task.CompletedTask);

            var service = new EstadoService(mockEstadoRepo.Object, mockPaisRepo.Object);

            // Act
            await service.AddAsync(estado);

            // Assert
            mockEstadoRepo.Verify(r => r.AddAsync(It.Is<Estado>(e =>
                e.NOME_ESTADO == "São Paulo" && e.COD_PAIS == 1
            )), Times.Once);
        }
    }
}