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
                mc.AddProfile(new DeviceMappingProfile());
                mc.AddProfile(new HubMappingProfile());
                mc.AddProfile(new NotificationMappingProfile());
                mc.AddProfile(new SensorTypeMappingProfile());
                mc.AddProfile(new SensorDataMappingProfile());
                mc.AddProfile(new SensorMappingProfile());
                mc.AddProfile(new ConditionMappingProfile());
                mc.AddProfile(new TriggerMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
