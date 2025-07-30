namespace EmploRecrutingProject.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
            });

        builder.Services.AddEndpointsApiExplorer();
    }
}
