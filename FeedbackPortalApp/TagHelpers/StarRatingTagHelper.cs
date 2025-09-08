using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FeedbackPortalApp.TagHelpers
{
    [HtmlTargetElement("star-rating")]
    public class StarRatingTagHelper : TagHelper
    {
        public int RatingValue { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            var stars = string.Empty;
            for (int i = 1; i <= 5; i++)
            {
                if (i <= RatingValue)
                    stars += "★ ";
                else
                    stars += "☆ ";
            }
            output.Content.SetHtmlContent($"<span style='font-size:24px;color:gold;'>{stars}</span>");
        }
    }
}
