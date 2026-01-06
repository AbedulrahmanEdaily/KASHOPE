using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOPE.DAL.DTO.Response.AccountResponse
{
    public class LoginResponse: BaseResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
