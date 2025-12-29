using Library.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.ToTable("Books");

		builder.HasKey(b => b.Id);

		builder.Property(b => b.Nombre)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(b => b.Autor)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(b => b.ISBN)
			.IsRequired()
			.HasMaxLength(50);

		builder.HasIndex(b => b.ISBN)
			.IsUnique();

		builder.Property(b => b.Status)
			.HasDefaultValue(true);
	}
}
