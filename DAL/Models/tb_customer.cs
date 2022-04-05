using System;
using System.ComponentModel.DataAnnotations;


namespace DAL.Models
{
    public class tb_customer
    {
        [Key]
        public int CustomerID { get; set; }
        public string FirstName_TH { get; set; }
        public string LastName_TH { get; set; }
        public string FirstName_EN { get; set; }
        public string LastName_EN { get; set; }
        public string Age { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
