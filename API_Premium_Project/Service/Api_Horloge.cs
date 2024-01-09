using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Premium_Project.Service
{
    public class Api_Horloge
    {
       public Api_Horloge()
        {

        }
        public class WorldTimeApiResponse
        {
            public string abbreviation { get; set; }
            public string client_ip { get; set; }
            public string datetime { get; set; }
            public string day_of_week { get; set; }
            public int day_of_year { get; set; }
            public bool dst { get; set; }
            public string dst_from { get; set; }
            public int dst_offset { get; set; }
            public string dst_until { get; set; }
            public int raw_offset { get; set; }
            public string timezone { get; set; }
            public string unixtime { get; set; }
            public string utc_datetime { get; set; }
            public string utc_offset { get; set; }
            public int week_number { get; set; }
        }

    }


}
