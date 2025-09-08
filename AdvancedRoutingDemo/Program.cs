using Microsoft.AspNetCore.Routing.Constraints;
using AdvancedRoutingDemo.Constraints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    // Complex Route: Products
    endpoints.MapControllerRoute(
        name: "products",
        pattern: "Products/{category}/{id:int}",
        defaults: new { controller = "Products", action = "Details" });

    // Complex Route: User Orders
    endpoints.MapControllerRoute(
        name: "userorders",
        pattern: "Users/{username}/Orders",
        defaults: new { controller = "Orders", action = "UserOrders" });

    // Dynamic Route based on Role
    endpoints.MapControllerRoute(
        name: "dashboard",
        pattern: "Dashboard/{role}",
        defaults: new { controller = "Dashboard", action = "Index" });

    // Custom Constraint: GUID Example
    endpoints.MapControllerRoute(
        name: "guidRoute",
        pattern: "Docs/{docId:guid}",
        defaults: new { controller = "Docs", action = "View" });

    // Default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapControllerRoute(
    name: "categoryFilter",
    pattern: "Products/Filter/{category}/{priceRange}",
    defaults: new { controller = "Products", action = "Filter" },
    constraints: new { category = new CategoryConstraint() });

        
});

app.Run();
