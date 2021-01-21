using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Fort.Models
{
    public partial class User
    {
        public User()
        {
            Cities = new HashSet<City>();
        }

        public int UserId { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 4)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password is required")]
       // [DataType(DataType.Password)]
        public string Password { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
