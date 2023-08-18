using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Request
{
    public class LoginModal
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsRemember { get; set; }
        public string LoginType { get; set; }
    }
}
