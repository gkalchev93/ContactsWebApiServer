using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiServer.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255,MinimumLength =1)]
        public string Name { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string Egn { get; set; }
        [StringLength(255, MinimumLength = 1)]
        public string Address { get; set; }
        [StringLength(32, MinimumLength = 1)]
        public string Telephone { get; set; }
    }
}