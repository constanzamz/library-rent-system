using Library.Dto.Request;
using Library.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
	private readonly IOrderService service;

	public OrdersController(IOrderService service)
	{
		this.service = service;
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromBody] OrderRequestDto dto)
	{
		var response = await service.CreateAsync(dto);
		return response.Success ? Ok(response) : BadRequest(response);
	}

	[HttpGet("books-by-dni/{dni}")]
	public async Task<IActionResult> GetBooksByDni(string dni)
	{
		var response = await service.GetBooksByClientDniAsync(dni);
		return response.Success ? Ok(response) : BadRequest(response);
	}

	[HttpPut("{id:int}/return")]
	public async Task<IActionResult> Return(int id)
	{
		var response = await service.ReturnAsync(id);
		return response.Success ? Ok(response) : BadRequest(response);
	}

	

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var response = await service.GetAllAsync();
		return response.Success ? Ok(response) : BadRequest(response);
	}
}
