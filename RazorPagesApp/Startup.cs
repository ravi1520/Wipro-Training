app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();

    // Custom route for product details
    endpoints.MapFallbackToPage("/Products/{id:int}", "/Products/Details");
});
