using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Negocio.DTO;

namespace Transacciones.Negocio.Services.Interfaces
{
    public interface IJwtService
    {
        public string generateToken(JWT_Values jWT_Values, Dictionary<string, string> customValues);

        public bool validateToken(string token, JWT_Values jWT_Values);

    }
}
