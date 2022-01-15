using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAPI.Models.Responses
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool isSuccessful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
