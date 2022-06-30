using Application.Validators;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Newtonsoft.Json.Converters;

namespace StatPlantAPI.Configurations
{
    public static class ValidatorRegister
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(OptionsBuilderConfigurationExtensions => OptionsBuilderConfigurationExtensions.SerializerSettings.Converters.Add(new StringEnumConverter())).AddFluentValidation(c =>
            {
                c.DisableDataAnnotationsValidation = true;
                c.RegisterValidatorsFromAssemblyContaining<IValidator>();
                c.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
            }
            );

            services.AddFluentValidationRulesToSwagger();
        }
    }
}
