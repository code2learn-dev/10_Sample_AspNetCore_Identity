using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Identity.WebAppRazorPage.TagHelpers
{
    [HtmlTargetElement("htext")]
    public class HeaderTagHelper : TagHelper
    {
        [HtmlAttributeName("alert")]
        public Alert Alert { get; set; } = Alert.info;

        [HtmlAttributeName("text")]
        public string Text { get; set; } = "لیست داده";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "h5";
            output.Attributes.Add("class", $"alert alert-{Alert}");

            var childContent = Task.Run(async() => await output.GetChildContentAsync());
            var innerText = childContent.Result.GetContent();
            output.Content.Append(innerText);
            //output.Content.SetHtmlContent(innerText);
        }
    }
}
