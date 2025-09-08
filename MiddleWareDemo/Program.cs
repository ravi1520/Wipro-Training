using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Middleware 1: Log requests
app.Use(async (context, next) =>
{
    Console.WriteLine($"[Request] {context.Request.Method} {context.Request.Path}");
    await next.Invoke();
    Console.WriteLine($"[Response] {context.Response.StatusCode}");
});

// Middleware 2: Error handling
app.UseExceptionHandler("/error");

// Middleware 3: HTTPS redirection
app.UseHttpsRedirection();

// Middleware 4: Static files with basic security
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Content-Security-Policy",
            "default-src 'self'; script-src 'self'; style-src 'self'");
    }
});

// Default route
app.MapGet("/", () => "Hello from Middleware Demo!");

// Custom error page
app.MapGet("/error", () => Results.Problem("Something went wrong!"));

app.Run();
