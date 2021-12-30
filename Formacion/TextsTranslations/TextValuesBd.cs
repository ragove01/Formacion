using Formacion.Data.Context.Resources;
using Formacion.Controllers;
using Formacion.Data.Models.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Formacion.TextsTranslations
{
    internal class TextValuesBd:TextValuesBase
    {
        internal TextValuesBd()
        {

        }
        internal TextValuesBd(CultureInfo culture):base(culture)
        {

        }
        protected override Dictionary<TextsIndex, string> LoadTexts()
        {
            this.textsValues = new Dictionary<TextsIndex, string>();
            this.LoadTextFromBd().Wait();
            return this.textsValues;
        }

        private async Task<bool> LoadTextFromBd()
        {
            ResourceController controller = new ResourceController(new ResourcesContext());
            bool loadedResources = await this.LoadTextFromBdCultureEspcific(controller);
            if(loadedResources == false)
            {
                await this.LoadTextFromBdNoCulture(controller); 
            }
            return true;
        }

        private async Task<bool> LoadTextFromBdCultureEspcific(ResourceController controller)
        {
            var textsCulture = await controller.GetResources(this.Culture.Name);
            if (textsCulture == null)
            {
                return false;
            }

            foreach (TextResourceCulture item in textsCulture)
            {
                this.AddResourceToDictionary(item.Resource.TextIndex, item.TextValue);
            }

            return true;
        }
        private async Task<bool> LoadTextFromBdNoCulture(ResourceController controller)
        {
            var textsCulture = await controller.GetResources();
            if (textsCulture == null)
            {
                return false;
            }
            foreach (TextResource item in textsCulture)
            {
                this.AddResourceToDictionary(item.TextIndex, item.TextValue);
            }

            return true;
        }
        private void AddResourceToDictionary(string resourceName, string resourceValue)
        {
            object index;
            if (!Enum.TryParse(typeof(TextsIndex), resourceName, out index))
            {
                throw new ApplicationException(string.Format(GetText(TextsIndex.EnumConversionError), resourceName, typeof(TextsIndex).Name));
            }
            this.textsValues.Add((TextsIndex)index, resourceValue);
        }
    }
}
