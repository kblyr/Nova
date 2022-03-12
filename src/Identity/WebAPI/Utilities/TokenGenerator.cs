using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Nova.Identity.Utilities;

public sealed class TokenGenerator
{
    public string Generate()
    {
        var privateKeyXmlString = File.ReadAllText(@"C:\Crypto Keys\Nova\private_key.xml");
        using var rsa = RSA.Create();
        rsa.FromXmlString(privateKeyXmlString);
        var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = signingCredentials
        };
        tokenDescriptor.Claims = new Dictionary<string, object>
        {
            {
                "CustomData", new Dictionary<string, object>
                {
                    { "Field1", "Value for Field 1" },
                    { "Field2", "Value for Field 2" },
                    { "Field3", "Value for Field 3" },
                }
            }
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }
}