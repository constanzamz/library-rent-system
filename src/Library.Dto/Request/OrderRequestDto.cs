namespace Library.Dto.Request;

public record OrderRequestDto(
	int ClientId,
	List<int> BookIds
);
