using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Request
{
    public class ChangePasswordRequest
    {
        public string NewPassword { get; set; }
        public string CodeResetPassword { get; set; }
        public string Email { get; set; }
    }
}
