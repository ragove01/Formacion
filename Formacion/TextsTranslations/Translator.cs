using System;
using System.Globalization;

namespace Formacion.TextsTranslations
{
    public class Translator
    {
        
        private static TextValuesBase textsValues = new TextValuesBase();
        public static string GetText(TextsIndex index)
        {
            InitialiceTexts();
            return textsValues.GetText(index);
        }

        public static string GetText(string indexString)
        {
            object index;
            if (!Enum.TryParse(typeof(TextsIndex), indexString, out index))
            {
                throw new ApplicationException(string.Format(GetText(TextsIndex.EnumConversionError), indexString, typeof(TextsIndex).Name));
            }
            return GetText((TextsIndex)index);
        }

        private static void InitialiceTexts()
        {
            if(CultureInfo.CurrentCulture == textsValues.Culture)
            {
                return;
            }
            textsValues = new TextValuesBd(CultureInfo.CurrentCulture);
            
        }

      

    }
}
