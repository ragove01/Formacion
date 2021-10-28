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
            if (CultureInfo.CurrentCulture.Name.Equals("en-GB"))
            {
                SetCulture_en_GB();
                return;
            }
            if (CultureInfo.CurrentCulture.Name.Equals("en-US"))
            {
                SetCulture_en_US();
                return;
            }
            SetCultureBase();
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
