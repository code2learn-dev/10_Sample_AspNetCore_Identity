using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Identity.WebAppRazorPage.TagHelpers
{
    [HtmlTargetElement("addlink")]
    public class LinkSectionTagHelper : TagHelper
    { 
        public string Caption { get; set; } = "ذخیره داده چدید";

        [HtmlAttributeName("page")]
        public string PageName { get; set; } = "";

        [HtmlAttributeName("area")]
        public string AreaName { get; set; } = "Admin";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("class", "card mt-2");

            TagBuilder cardBody = new TagBuilder("div");
            cardBody.Attributes.Add("class", "card-body text-start");

            TagBuilder linkTag = new TagBuilder("a");
            linkTag.Attributes.Add("class", "btn btn-primary");
            linkTag.Attributes.Add("href", $"/{AreaName}/{PageName}/add");
            linkTag.InnerHtml.Append(Caption);

            cardBody.InnerHtml.AppendHtml(linkTag); 
            output.Content.AppendHtml(cardBody);
        }
    }
}
