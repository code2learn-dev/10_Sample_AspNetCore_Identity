using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Identity.WebAppRazorPage.TagHelpers
{
    [HtmlTargetElement("editlinks")]
    public class DisplayLinkSectionTagHelper : TagHelper
    {
        [HtmlAttributeName("area")]
        public string AreaName { get; set; } = "Admin";

		[HtmlAttributeName("page")]
		public string PageName { get; set; } = "";

        [HtmlAttributeName("id")]
        public long EntityId { get; set; } = 0;

        [HtmlAttributeName("back-link-caption")]
        public string BackLinkCaption { get; set; } = "";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.Attributes.Add("class", "card mt-3");

            TagBuilder cardBody = new("div");
            cardBody.Attributes.Add("class", "card-body text-start");

            TagBuilder editLink = new("a");
            editLink.Attributes.Add("class", "btn btn-primary");
            editLink.InnerHtml.Append("ویرایش");
            editLink.Attributes.Add("href", $"/{AreaName}/{PageName}/edit/{EntityId}");

            TagBuilder listLink = new("a");
            listLink.Attributes.Add("class", "btn btn-secondary ml-1");
            listLink.Attributes.Add("href", $"/{AreaName}/{PageName}/Index");
            listLink.InnerHtml.Append($"بازگشت به لیست {BackLinkCaption}");

            cardBody.InnerHtml.AppendHtml(listLink);
            cardBody.InnerHtml.AppendHtml(editLink);

            output.Content.AppendHtml(cardBody);
        }
    }
}
