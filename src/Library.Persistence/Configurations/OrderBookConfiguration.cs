using Library.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistence.Configurations;

public class OrderBookConfiguration : IEntityTypeConfiguration<OrderBook>
{
	public void Configure(EntityTypeBuilder<OrderBook> builder)
	{
		builder.ToTable("OrderBooks");

		builder.HasKey(ob => new { ob.OrderId, ob.BookId });

		builder.HasOne(ob => ob.Order)
			.WithMany(o => o.OrderBooks)
			.HasForeignKey(ob => ob.OrderId);

		builder.HasOne(ob => ob.Book)
			.WithMany()
			.HasForeignKey(ob => ob.BookId);
	}
}
