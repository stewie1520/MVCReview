using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCReview.Models;

namespace MVCReview.Data.Config
{
    public class BookModelConfiguration : IEntityTypeConfiguration<BookModel>
    {
        public void Configure(EntityTypeBuilder<BookModel> builder)
        {
            builder.HasIndex(x => x.Title);
        }
    }
}
