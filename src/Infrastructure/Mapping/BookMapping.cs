using library_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace library_api.Infrastructure.Mapping
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Author)
                            .WithMany(p => p.Books)
                            .HasForeignKey(p => p.AuthorId);
            builder.HasOne(p => p.Publisher)
                            .WithMany(p => p.Books)
                            .HasForeignKey(p => p.PublisherId);
        }
    }
}
