using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Fort.Models
{
    public partial class City
    {
        public int CityId { get; set; }


        [Required(ErrorMessage = "City Name is required")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "City Country Id is required")]
        public int? Countryid { get; set; }
        public int? CityUserId { get; set; }

        public virtual User CityUser { get; set; }
    }
}
