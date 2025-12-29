using Library.Dto.Request;
using Library.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
	private readonly IBookService service;

	public BooksController(IBookService service)
	{
		this.service = service;
	}

	[HttpGet]
	public async Task<IActionResult> Get()
	{
		var response = await service.GetAsync();
		return response.Success ? Ok(response) : BadRequest(response);
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var response = await service.GetByIdAsync(id);
		return response.Success ? Ok(response) : NotFound(response);
	}

	[HttpPost]
	public async Task<IActionResult> Post([FromBody] BookRequestDto dto)
	{
		var response = await service.CreateAsync(dto);
		return response.Success ? Ok(response) : BadRequest(response);
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> Put(int id, [FromBody] BookRequestDto dto)
	{
		var response = await service.UpdateAsync(id, dto);
		return response.Success ? Ok(response) : BadRequest(response);
	}

	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var response = await service.DeleteAsync(id);
		return response.Success ? Ok(response) : BadRequest(response);
	}

	[HttpGet("paginated")]
	public async Task<IActionResult> GetPaginated(
	[FromQuery] int page = 1,
	[FromQuery] int pageSize = 10)
	{
		var response = await service.GetPaginatedAsync(page, pageSize);
		return response.Success ? Ok(response) : BadRequest(response);
	}
}
