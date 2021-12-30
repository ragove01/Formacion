using Formacion.Data.Context.Resources;
using Formacion.Data.Models.Resources;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Formacion.Controllers
{
    public class ResourceController
    {
        private ResourcesContext context;
        public ResourceController(ResourcesContext contextArgs)
        {
            this.context = contextArgs; 
        }

        public async Task<IEnumerable<TextResource>>  GetResources()
        {
            return await this.context.TextResources.ToArrayAsync(); 
        }
        public async Task<IEnumerable<TextResourceCulture>> GetResources(string culture)
        {
            return await this.GetResoucesCulture(await this.GetCulture(culture)); 
        }

        private async Task<ResourceCulture> GetCulture(string culture)
        {
            return await this.context.ResourceCultures.FirstOrDefaultAsync(C => C.CultureName == culture); 
        }

        private async Task<IEnumerable<TextResourceCulture>> GetResoucesCulture(ResourceCulture culture)
        {
            if(culture == null)
            {
                return null;
            }
            return await this.context.TextResourcesCulture
                .Include(T => T.Resource)
                .Include(T=> T.Culture)
                .Where(T => T.ResourceCultureId == culture.ResourceCultureId).ToArrayAsync();
        }

        
    }
}
