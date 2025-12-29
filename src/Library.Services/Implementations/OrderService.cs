using AutoMapper;
using Library.Dto;
using Library.Dto.Request;
using Library.Dto.Response;
using Library.Repositories.Abstractions;
using Library.Services.Abstractions;
using Microsoft.Extensions.Logging;

namespace Library.Services.Implementations;

public class OrderService : IOrderService
{
	private readonly IOrderRepository orderRepository;
	private readonly ILogger<OrderService> logger;
	private readonly IMapper mapper;

	public OrderService(
		IOrderRepository orderRepository,
		ILogger<OrderService> logger,
		IMapper mapper)
	{
		this.orderRepository = orderRepository;
		this.logger = logger;
		this.mapper = mapper;
	}

	public async Task<BaseResponseGeneric<OrderResponseDto>> CreateAsync(OrderRequestDto dto)
	{
		var response = new BaseResponseGeneric<OrderResponseDto>();

		try
		{
			var order = mapper.Map<Entities.Models.Order>(dto);
			var created = await orderRepository.CreateAsync(order);

			response.Data = mapper.Map<OrderResponseDto>(created);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error creating order");
			response.ErrorMessage = "Error al registrar pedido";
		}

		return response;
	}

	public async Task<BaseResponseGeneric<ICollection<BookResponseDto>>> GetBooksByClientDniAsync(string dni)
	{
		var response = new BaseResponseGeneric<ICollection<BookResponseDto>>();

		try
		{
			var books = await orderRepository.GetBooksByClientDniAsync(dni);
			response.Data = mapper.Map<ICollection<BookResponseDto>>(books);
			response.Success = true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error getting books by DNI");
			response.ErrorMessage = "Error al obtener libros por DNI";
		}

		return response;
	}
}
