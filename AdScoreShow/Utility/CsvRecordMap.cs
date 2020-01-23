using AdScoreShow.Models;
using CsvHelper.Configuration;
using AdScoreShow.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdScoreShow.Utility
{
    public class CsvRecordMap : ClassMap<CsvRecord>
    {
        public CsvRecordMap()
        {
            //AutoMap();

            Map(m => m.Year).TypeConverter<NullInt32FromStringConverter<int>>();
            Map(m => m.Market);
            Map(m => m.Segment);
            Map(m => m.Brand);
            Map(m => m.Copy_Duration).TypeConverter<NullInt32FromStringConverter<int>>();
            Map(m => m.Copy_Name);
            Map(m => m.Score_1).TypeConverter<NullInt32FromStringConverter<int>>();
            Map(m => m.Score_2).TypeConverter<NullInt32FromStringConverter<int>>();
        }
    }
}