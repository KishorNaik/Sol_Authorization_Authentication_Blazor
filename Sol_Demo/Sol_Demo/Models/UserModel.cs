using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo.Models
{
    public class UserModel
    {
        [Required]
        [EmailAddress]
        public String EmailId { get; set; }

        [Required]
        public String Password { get; set; }

        public String Role { get; set; }
    }
}