using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTableWebApi.Models
{
    public class GetFieldsModel
    {
        public String id { get; set; }
        public String title { get; set; }
        public String Text { get; set; }
        public DateTime receivedAt { get; set; }
    }
}
