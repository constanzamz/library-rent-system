using Library.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.ToTable("Orders");

		builder.HasKey(o => o.Id);

		builder.Property(o => o.FechaPedido)
			.IsRequired();

		builder.Property(o => o.Status)
			.HasDefaultValue(true);

		builder.Property(o => o.IsReturned)
			.HasDefaultValue(false)
		.IsRequired();

		builder.HasOne(o => o.Client)
			.WithMany()
			.HasForeignKey(o => o.ClientId)
			.OnDelete(DeleteBehavior.Restrict);

		
	

	}

}
