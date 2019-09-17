namespace Asp.NetCoreJWTAuthentication.Models
{
    using System.Collections.Generic;

    public class RegisterResponse
    {
        public RegisterResponse()
        {
            this.Errors = new HashSet<string>();
        }
        
        public ICollection<string> Errors { get; set; }

        public bool Succeeded { get; set; }
    }
}
