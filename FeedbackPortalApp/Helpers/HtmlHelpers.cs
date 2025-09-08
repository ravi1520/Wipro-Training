using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FeedbackPortalApp.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent StyledInput(this IHtmlHelper htmlHelper, string name, string placeholder)
        {
            return new HtmlString($"<input name='{name}' placeholder='{placeholder}' class='form-control' />");
        }
    }
}
