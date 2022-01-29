using System.Diagnostics;

using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//app.Run();
app.Start();

var server = app.Services.GetRequiredService<IServer>();
var addressFeature = server.Features.Get<IServerAddressesFeature>()!;

foreach (var address in addressFeature.Addresses)
{
    Debug.WriteLine("Kestrel is listening on address: " + address);
}

app.WaitForShutdown();
