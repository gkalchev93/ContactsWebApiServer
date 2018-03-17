using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCommunication.DTO
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Egn { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
    }
}
