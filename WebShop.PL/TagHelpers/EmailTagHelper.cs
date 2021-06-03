using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebShop.PL.TagHelpers
{

    public class EmailTagHelper : TagHelper
    {
        public string Address { get; set; }
        public string Content { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            //output.Attributes.SetAttribute("class", "btn btn-success");
            output.Attributes.SetAttribute("href", "mailto:" + Address);
            output.Attributes.SetAttribute("class", "text-primary");
            output.Content.SetContent(Content);
        }
    }
}
