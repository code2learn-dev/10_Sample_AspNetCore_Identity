using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Identity.WebAppRazorPage.TagHelpers
{
    [HtmlTargetElement("formlink")]
    public class FormLinkSectionTagHelper : TagHelper
    {
        [HtmlAttributeName("link-title")]
        public string LinkCaption { get; set; } = "لیست اصلی";

        [HtmlAttributeName("page")]
        public string PageName { get; set; } = string.Empty;

        [HtmlAttributeName("area")]
        public string AreaName { get; set; } = "Admin";

        [HtmlAttributeName("btn-title")]
        public string ButtonCaption { get; set; } = "ذخیره";

        [HtmlAttributeName("btn-color")]
        public string ButtonCssClass { get; set; } = "info";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.Attributes.Add("class", "card mt-3 text-start");

            TagBuilder link = new TagBuilder("a");
            link.Attributes.Add("class", "btn btn-warning ml-1");
            link.InnerHtml.Append($"بازگشت به لیست {LinkCaption}");
            link.Attributes.Add("href", $"/{AreaName}/{PageName}/Index");

            TagBuilder button = new TagBuilder("button");
            button.Attributes.Add("type", "submit");
            button.Attributes.Add("class", $"btn btn-{ButtonCssClass}");
            button.InnerHtml.Append(ButtonCaption);

            TagBuilder cardBody = new TagBuilder("div");
            cardBody.Attributes.Add("class", "card-body");
            cardBody.InnerHtml.AppendHtml(link);
            cardBody.InnerHtml.AppendHtml(button);

            output.Content.AppendHtml(cardBody); 
        }
    }
}
