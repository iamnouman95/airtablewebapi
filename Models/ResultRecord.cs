using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTableWebApi.Models
{
    public class ResultRecord
    {
        public String id { get; set; }
        public Fields fields { get; set; }
        public String createdTime { get; set; }
    }

    public class ResultRecordList
    {
        public List<ResultRecord> records { get; set; }
    }
}
