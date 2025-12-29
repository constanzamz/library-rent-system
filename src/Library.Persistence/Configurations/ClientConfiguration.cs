using Library.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
	public void Configure(EntityTypeBuilder<Client> builder)
	{
		builder.ToTable("Clients");

		builder.HasKey(c => c.Id);

		builder.Property(c => c.Nombres)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(c => c.Apellidos)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(c => c.DNI)
			.IsRequired()
			.HasMaxLength(20);

		builder.HasIndex(c => c.DNI)
			.IsUnique();

		builder.Property(c => c.Edad)
			.IsRequired();

		builder.Property(c => c.Status)
			.HasDefaultValue(true);
	}
}
