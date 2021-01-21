using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Fort.Models
{
    public partial class Country
    {
        public int Countryid { get; set; }
        [Required(ErrorMessage = "Country Name is required")]
        public string Countryname { get; set; }
    }
}
