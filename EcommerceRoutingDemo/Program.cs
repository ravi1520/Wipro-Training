var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
endpoints.MapControllerRoute(
    name: "productDetails",
    pattern: "Products/{category}/{id:int}",
    defaults: new { controller = "Products", action = "Details" });

endpoints.MapControllerRoute(
    name: "productFilter",
    pattern: "Products/Filter/{category}/{priceRange}",
    defaults: new { controller = "Products", action = "Filter" },
    constraints: new { category = new CategoryConstraint() });

endpoints.MapControllerRoute(
    name: "checkout",
    pattern: "Checkout",
    defaults: new { controller = "Cart", action = "Checkout" });

endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
