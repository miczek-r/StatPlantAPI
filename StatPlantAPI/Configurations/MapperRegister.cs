using Application.Mappers;
using AutoMapper;

namespace StatPlantAPI.Configurations
{
    public static class MapperRegister
    {
        public static void RegisterMapper(this IServiceCollection services)
        {
            //TODO: Try to find any autoregistration for mappers
            MapperConfiguration mapperConfig = new(mc =>
            {
                mc.AddProfile(new UserMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
