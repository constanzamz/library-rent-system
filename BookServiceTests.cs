using AutoMapper;
using Library.Dto.Request;
using Library.Services.Implementations;
using Library.Repositories.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Library.Tests;

public class BookServiceTests
{
	private static IMapper CreateMapper()
	{
		var config = new MapperConfiguration(cfg =>
		{
			cfg.AddMaps(typeof(Library.Services.Profiles.BookProfile).Assembly);
		});

		return config.CreateMapper();
	}

	[Fact]
	public async Task CreateAsync_ShouldFail_WhenIsbnIsInvalid()
	{
		// Arrange
		var repositoryMock = new Mock<IBookRepository>();
		var loggerMock = new Mock<ILogger<BookService>>();
		var mapper = CreateMapper();

		var service = new BookService(
			repositoryMock.Object,
			loggerMock.Object,
			mapper);

		var dto = new BookRequestDto(
			"Libro de prueba",
			"Autor Test",
			"123"
		);

		// Act
		var result = await service.CreateAsync(dto);

		// Assert
		Assert.False(result.Success);
		Assert.Equal(
			"ISBN inválido. Debe contener exactamente 13 dígitos.",
			result.ErrorMessage);
	}
}
