using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fort.Models
{
    public class CityViewModel
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? Countryid { get; set; }
        public string CountryName { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
}
