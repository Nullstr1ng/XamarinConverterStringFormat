using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ConverterStringFormat.Converter
{
    public class Converter_StringFormat : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string reformat = string.Empty;

            if (parameter is string && value != null)
            {
                string p = parameter.ToString();

                List<string> parameters = GetParameters(p);

                var classProp = value.GetType().GetProperties();
                //foreach(var prop in classProp) // no, we're not going to use foreach for better perfomance
                for (int i = 0; i <= classProp.Length - 1; i++)
                {
                    var prop = classProp[i];

                    if (prop.CanWrite)
                    {
                        if (parameters.Contains(prop.Name))
                        {
                            var v = prop.GetValue(value, null);

                            p = p.Replace($"{{{prop.Name}}}", v.ToString());
                        }
                    }
                }
                classProp = null;

                reformat = p;
            }

            return reformat;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        List<string> GetParameters(string input)
        {
            bool start = false;
            string param_name = string.Empty;
            List<string> parameters = new List<string>();

            for (int i = 0; i <= input.Length - 1; i++)
            {
                char[] c = input.ToCharArray();

                if (start)
                {
                    param_name += c[i];
                }

                if (c[i] == '{')
                {
                    start = true;
                }
                else if (c[i] == '}')
                {
                    start = false;
                    parameters.Add(param_name.Substring(0, param_name.Length - 1));
                    param_name = string.Empty;
                }
            }

            param_name = null;

            return parameters;
        }
    }
}
