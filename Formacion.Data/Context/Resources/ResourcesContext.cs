using Formacion.Data.Migrations.Resources;
using Formacion.Data.Models.Resources;
using Microsoft.EntityFrameworkCore;

namespace Formacion.Data.Context.Resources
{
    public class ResourcesContext:DbContext{
     
        public DbSet<TextResource> TextResources { get; set; }
        public DbSet<ResourceCulture> ResourceCultures { get; set; }
        public DbSet<TextResourceCulture> TextResourcesCulture { get; set; }

        public ResourcesContext():base()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer($@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Formacion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<TextResource>(new TextResourcesConfiguration());
            builder.ApplyConfiguration<ResourceCulture>(new ResourceCulturesConfiguration());
            builder.ApplyConfiguration<TextResourceCulture>(new TextResourcesCultureConfiguration());
            ResourcesInitializer.Initialize(builder);                

        }
    }
}
