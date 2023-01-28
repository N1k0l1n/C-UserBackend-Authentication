using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;


namespace UserBackend.Helpers
{
    public class JwtService
    {
        //The key that we want to encode our Jwt with the id
        //And it should be a long string
        private string secureKey = "this is a very secure key";


        //Generating Jwt Token
        public string Generate(int id) 
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            //Creating the Credentials
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //JWT Header
            var header = new JwtHeader(credentials);


            //Creating the Payload (the data that we want to encode)
            //From standart this payload gets as parameter (issuer: id, audience : null,
            //    claims : null, notBefore : null, expires: DateTime)

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1)); //the token expires in 1 day

            //Add payload to the header
            var securityToken = new JwtSecurityToken(header, payload);

            //We have to return it as a String
            return new JwtSecurityTokenHandler().WriteToken(securityToken);

        }

        public JwtSecurityToken Verify(string jwt)
        {
            //Creating the Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //Getting the Key
            var key = Encoding.ASCII.GetBytes(secureKey);

            //Validate the Token
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience=false
            }, out SecurityToken validatedToken);

            //Getting the validated Token
            return (JwtSecurityToken) validatedToken;
        }
    }
}
