using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTableWebApi.Models
{
    public class Records
    {
        public Fields fields { get; set; }
    }

    public class RecordsList
    {
        public List<Records> records { get; set; }
    }
}
