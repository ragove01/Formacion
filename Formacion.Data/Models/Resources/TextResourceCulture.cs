using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formacion.Data.Models.Resources
{
    public class TextResourceCulture
    {
        public int TextResourceCultureId { get; set; }
        public int TextResourceId { get; set; }
        public int ResourceCultureId { get; set; }
        public string TextValue { get; set; }
        public ResourceCulture Culture{get;set;}
        public TextResource Resource { get; set; }

    }
}
