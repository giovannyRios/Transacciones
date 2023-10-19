using AutoMapper;

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
