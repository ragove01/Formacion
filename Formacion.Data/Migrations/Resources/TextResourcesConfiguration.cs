using Formacion.Data.Models.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Formacion.Data.Migrations.Resources
{
    public class TextResourcesConfiguration : IEntityTypeConfiguration<TextResource>
    {
        public void Configure(EntityTypeBuilder<TextResource> builder)
        {
            builder.ToTable("TextResources")
                 .HasKey(T => T.TextResourceId);
            builder.Property(T => T.TextIndex)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(T=> T.TextValue)
                .HasMaxLength(255);
        }
    }
}
