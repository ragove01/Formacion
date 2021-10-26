using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Formacion.TextsTranslations
{
    public class Translator
    {
        private static CultureInfo culture = CultureInfo.CurrentCulture;
        private static TextValuesBase textsValues = new TextValuesBase();
        public static string GetText(TextsIndex index)
        {
            if(CultureInfo.CurrentCulture != culture)
            {
                SetValuesTexts(CultureInfo.CurrentCulture);
            }
            return textsValues.GetText(index); 
        }

        private static void SetValuesTexts(CultureInfo TheCulture)
        {
            culture = TheCulture;

            if(TheCulture == CultureInfo.InvariantCulture)
            {
                SetCultureBase();
            }
            if(TheCulture.Name.Equals("en-GB"))
            {
                SetCulture_en_GB(); 
            }
            if(TheCulture.Name.Equals("en-US"))
            {
                SetCulture_en_US(); 
            }

        }

        private static void SetCultureBase()
        {
            textsValues = new TextValuesBase();
        }
        private static void SetCulture_en_GB()
        {
            textsValues = new TextsValues_en_GB(); 
        }

        private static void SetCulture_en_US()
        {
            textsValues = new TextsVaues_en_US(); 
        }


    }
}
