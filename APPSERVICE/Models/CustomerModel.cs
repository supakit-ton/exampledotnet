using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPSERVICE.Models
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }

    public class CustomerResponse
    {
        public bool UpdateStatus { get; set; }
        public string Messagelabel { get; set; }
    }
}
