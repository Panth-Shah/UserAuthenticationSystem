using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationUserAuthSystem.Models
{
    public class UserInformationViewEdit
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserFamilyName { get; set; }
        public string EmailID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
    }
}