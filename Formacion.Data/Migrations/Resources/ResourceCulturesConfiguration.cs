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
    public class ResourceCulturesConfiguration : IEntityTypeConfiguration<ResourceCulture>
    {
        public void Configure(EntityTypeBuilder<ResourceCulture> builder)
        {
            builder.ToTable("ResourceCultures")
                .HasKey(T => T.ResourceCultureId);
            builder.Property(T => T.CultureName)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
