using Newtonsoft.Json;
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

        [JsonConstructor]
        public Contact(string n, string e, string a, string t)
        {
            Name = n;
            Egn = e;
            Address = a;
            Telephone = t;
        }

        public Contact(int i, string n, string e, string a, string t)
        {
            Id = i;
            Name = n;
            Egn = e;
            Address = a;
            Telephone = t;
        }
    }
}
