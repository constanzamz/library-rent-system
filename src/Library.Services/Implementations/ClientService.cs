using AutoMapper;
using Library.Dto;
using Library.Dto.Request;
using Library.Dto.Response;
using Library.Entities.Models;
using Library.Repositories.Abstractions;
using Library.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace Library.Services.Implementations;

public class ClientService : IClientService
{
	private readonly IClientRepository repository;
	private readonly ILogger<ClientService> logger;
	private readonly IMapper mapper;

	public ClientService(
		IClientRepository repository,
		ILogger<ClientService> logger,
		IMapper mapper)
	{
		this.repository = repository;
		this.logger = logger;
		this.mapper = mapper;
	}

	public async Task<BaseResponseGeneric<ICollection<ClientResponseDto>>> GetAsync()
	{
		var response = new BaseResponseGeneric<ICollection<ClientResponseDto>>();

		try
		{
			var clients = await repository.GetAsync();
			response.Data = mapper.Map<ICollection<ClientResponseDto>>(clients);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error getting clients");
			response.ErrorMessage = "Error al obtener clientes";
		}

		return response;
	}

	public async Task<BaseResponseGeneric<ClientResponseDto>> GetByIdAsync(int id)
	{
		var response = new BaseResponseGeneric<ClientResponseDto>();

		try
		{
			var client = await repository.GetByIdAsync(id);

			if (client is null)
			{
				response.ErrorMessage = "Cliente no encontrado";
				return response;
			}

			response.Data = mapper.Map<ClientResponseDto>(client);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error getting client");
			response.ErrorMessage = "Error al obtener cliente";
		}

		return response;
	}

	public async Task<BaseResponseGeneric<ClientResponseDto>> CreateAsync(ClientRequestDto dto)
	{
		var response = new BaseResponseGeneric<ClientResponseDto>();

		try
		{
			var client = mapper.Map<Client>(dto);
			var created = await repository.CreateAsync(client);

			response.Data = mapper.Map<ClientResponseDto>(created);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error creating client");
			response.ErrorMessage = "Error al crear cliente";
		}

		return response;
	}

	public async Task<BaseResponse> UpdateAsync(int id, ClientRequestDto dto)
	{
		var response = new BaseResponse();

		try
		{
			var client = mapper.Map<Client>(dto);
			client.Id = id;

			await repository.UpdateAsync(client);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error updating client");
			response.ErrorMessage = "Error al actualizar cliente";
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
			logger.LogError(ex, "Error deleting client");
			response.ErrorMessage = "Error al eliminar cliente";
		}

		return response;
	}
}
