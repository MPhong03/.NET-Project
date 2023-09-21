using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNETProject.Shared
{
    public class ReponseDto
    {
        public string message { get; set; }
        public string token { get; set; }
        public long tokenExpired { get; set; }
        public ReponseDto(string message, string token, long tokenExpired)
        {
            this.message = message;
            this.token = token;
            this.tokenExpired = tokenExpired;
        }
    }
}
