using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesDemo.Pages
{
    public class ItemsModel : PageModel
    {
        private static List<string> _items = new() { "Apple", "Banana", "Orange" };

        [BindProperty]
        public string NewItem { get; set; }

        public List<string> Items => _items;

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(NewItem))
                _items.Add(NewItem);

            return RedirectToPage();
        }
    }
}
