namespace Library.Entities.Base;

public abstract class EntityBase
{
	public int Id { get; set; }
	public bool Status { get; set; } = true;
}