using Application.IServices;
using Infrastructure.Repositories.Base;

namespace StatPlantAPI.Configurations
{
    public static class ScrutorRegister
    {
        public static void RegisterScrutor(this IServiceCollection services)
        {
            //TODO: Try to simplify
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(Repository<>))
                .AddClasses(publicOnly: true)
                .AsMatchingInterface((repository, filter) =>
                    filter.Where(implementation => implementation.Name.Equals($"I{repository.Name}", StringComparison.OrdinalIgnoreCase)))
                .WithTransientLifetime());

            services.Scan(scan => scan
                            .FromAssemblyOf<IService>()
                            .AddClasses(publicOnly: true)
                            .AsMatchingInterface((service, filter) =>
                                filter.Where(implementation => implementation.Name.Equals($"I{service.Name}", StringComparison.OrdinalIgnoreCase)))
                            .WithTransientLifetime());
        }
    }
}
