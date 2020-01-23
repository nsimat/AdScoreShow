using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdScoreShow.Utility
{
    public class NullInt32FromStringConverter<T> : DefaultTypeConverter where T : struct
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            T? result = null;

            if (!string.IsNullOrWhiteSpace(text) & text != "N/A" & text != "NA")
            {
                if (text.EndsWith("%"))
                {
                    text = text.Substring(0, 2);
                }

                var converter = TypeDescriptor.GetConverter(typeof(T));
                result = (T)converter.ConvertFrom(text);
            }
            return result;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            var result = (T?)value;
            return result.ToString();
        }
    }
}