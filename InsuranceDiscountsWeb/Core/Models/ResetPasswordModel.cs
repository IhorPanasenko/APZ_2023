using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmationNewPassword { get; set; }

        public string Code { get; set; }
    }
}
