using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transacciones.Negocio.MappingEntities
{
    public class MapperConfig
    {
        private MapperConfiguration getConfiguration()
        {
            var config = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
                m.RecognizePostfixes("_reference");
            });

            return config;
            
        }

        public IMapper getMappper()
        {
            var configuration = getConfiguration();
            return configuration.CreateMapper();
        }
    }
}
