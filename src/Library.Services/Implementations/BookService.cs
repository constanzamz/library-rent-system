using AutoMapper;
using Library.Dto;
using Library.Dto.Request;
using Library.Dto.Response;
using Library.Entities.Models;
using Library.Repositories.Abstractions;
using Library.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace Library.Services.Implementations;

public class BookService : IBookService
{
	private readonly IBookRepository repository;
	private readonly ILogger<BookService> logger;
	private readonly IMapper mapper;

	public BookService(
		IBookRepository repository,
		ILogger<BookService> logger,
		IMapper mapper)
	{
		this.repository = repository;
		this.logger = logger;
		this.mapper = mapper;
	}

	public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetAsync()
	{
		var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();

		try
		{
			var books = await repository.GetAsync();
			response.Data = mapper.Map<ICollection<BookResponseDto>>(books);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error getting books");
			response.ErrorMessage = "Error al obtener libros";
		}

		return response;
	}

	public async Task<BaseResponseGeneric<BookResponseDto>> GetByIdAsync(int id)
	{
		var response = new BaseResponseGeneric<BookResponseDto>();

		try
		{
			var book = await repository.GetByIdAsync(id);

			if (book is null)
			{
				response.ErrorMessage = "Libro no encontrado";
				return response;
			}

			response.Data = mapper.Map<BookResponseDto>(book);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error getting book");
			response.ErrorMessage = "Error al obtener libro";
		}

		return response;
	}

	public async Task<BaseResponseGeneric<BookResponseDto>> CreateAsync(BookRequestDto dto)
	{
		var response = new BaseResponseGeneric<BookResponseDto>();

		try
		{
			var book = mapper.Map<Book>(dto);
			//Normalizar ISBN
			book.ISBN = NormalizeIsbn(book.ISBN);

			//Validar longitud ISBN
			if (book.ISBN.Length != 13)
			{
				response.ErrorMessage = "ISBN inválido. Debe contener exactamente 13 dígitos.";
				return response;
			}

			var created = await repository.CreateAsync(book);

			response.Data = mapper.Map<BookResponseDto>(created);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error creating book");
			response.ErrorMessage = "Error al crear libro";
		}

		return response;
	}

	public async Task<BaseResponse> UpdateAsync(int id, BookRequestDto dto)
	{
		var response = new BaseResponse();

		try
		{
			var existing = await repository.GetByIdAsync(id);
			if (existing is null)
			{
				response.ErrorMessage = "Libro no encontrado";
				return response;
			}

			existing.Nombre = dto.Nombre;
			existing.Autor = dto.Autor;
			existing.ISBN = NormalizeIsbn(dto.ISBN);

			if (existing.ISBN.Length != 13)
			{
				response.ErrorMessage = "ISBN inválido. Debe contener exactamente 13 dígitos.";
				return response;
			}

			await repository.UpdateAsync(existing);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error updating book");
			response.ErrorMessage = "Error al actualizar libro";
		}

		return response;
	}

	public async Task<BaseResponse> DeleteAsync(int id)
	{
		var response = new BaseResponse();

		try
		{
			await repository.DeleteAsync(id);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error deleting book");
			response.ErrorMessage = "Error al eliminar libro";
		}

		return response;
	}

	public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetPaginatedAsync(int page, int pageSize)
	{
		var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();

		try
		{
			var books = await repository.GetPaginatedAsync(page, pageSize);
			response.Data = mapper.Map<ICollection<BookResponseDto>>(books);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error getting paginated books");
			response.ErrorMessage = "Error al obtener libros";
		}

		return response;
	}


	private static string NormalizeIsbn(string isbn)
	{
		return new string(isbn.Where(char.IsDigit).ToArray());
	}

}
