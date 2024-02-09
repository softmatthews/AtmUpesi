using Asp.Versioning.ApiExplorer;

namespace API.Setup.Configurations
{
    internal static class DevConfiguration
    {
        internal static void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
            app.UseDeveloperExceptionPage();
        }
    }
}
