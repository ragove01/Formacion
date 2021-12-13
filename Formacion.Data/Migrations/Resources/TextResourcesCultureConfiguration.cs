using Formacion.Data.Models.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formacion.Data.Migrations.Resources
{
    public class TextResourcesCultureConfiguration : IEntityTypeConfiguration<TextResourceCulture>
    {
        public void Configure(EntityTypeBuilder<TextResourceCulture> builder)
        {
            builder.ToTable("TextResourcesCulture")
                .HasKey(T => T.TextResourceCultureId);
            builder.HasOne(T => T.Resource)
                .WithMany().HasForeignKey(T => T.TextResourceId)
                .OnDelete(DeleteBehavior.NoAction) ;
            builder.HasOne(T => T.Culture)
                .WithMany().HasForeignKey(T => T.ResourceCultureId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Property(T => T.TextValue)
                .HasMaxLength(255);

        }
    }
}
