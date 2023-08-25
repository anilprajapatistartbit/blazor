using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.Request
{
    public class PasswordChange
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
    public class ForgotPwd : PasswordChange
    {
        public string Otp { get; set; }
        public string Email { get; set; }
    }
  
}
