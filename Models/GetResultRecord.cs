using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTableWebApi.Models
{
    public class GetResultRecord
    {
        public String id { get; set; }
        public GetFields fields { get; set; }
        public String createdTime { get; set; }
    }

    public class GetResultRecordList
    {
        public List<GetResultRecord> records { get; set; }
    }
}
