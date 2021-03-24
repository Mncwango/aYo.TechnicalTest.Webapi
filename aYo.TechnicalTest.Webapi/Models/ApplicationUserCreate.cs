using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aYo.TechnicalTest.Webapi.Models
{
    public class ApplicationUserCreate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TimeSpan CreatedDate { get; set; }
    }
}
