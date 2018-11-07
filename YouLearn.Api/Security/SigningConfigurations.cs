using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouLearn.Api.Security
{
    public class SigningConfigurations
    {
        private const string SECRET_KEY = "4fcf0afa-4ce1-4f08-b6bb-784513f4084e";

        public SigningCredentials SigningCredentials { get; }
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_KEY));

        public SigningConfigurations()
        {
            SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
