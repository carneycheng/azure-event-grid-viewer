using Microsoft.AspNetCore.Builder;
using viewer.Hubs;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddSignalR();

var app = builder.Build();
app.UseCors(policyBuilder =>
{
    policyBuilder.WithOrigins("http://localhost:3000").AllowAnyHeader().WithMethods("GET", "POST").AllowCredentials();
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<GridEventsHub>("/hubs/gridevents");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();