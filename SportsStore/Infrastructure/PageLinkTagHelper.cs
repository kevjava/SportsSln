using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper: TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContext { get; set; }

        public PagingInfo? PageModel { get; set; }

        public string? PageAction { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                TagBuilder result = new TagBuilder("div");
                for (int i = 1; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");
                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
                    tag.AddCssClass("p-2");
                    tag.AddCssClass("border-2");
                    tag.AddCssClass("border-black");

                    if (PageModel.CurrentPage == i) 
                    {
                        tag.AddCssClass("bg-blue-50");
                    }
                    else
                    {
                        tag.AddCssClass("bg-blue-800");
                        tag.AddCssClass("text-white");
                    }

                    // Round the outsides of the box containing the links.
                    if (i == 1) 
                        tag.AddCssClass("rounded-l");
                    else if (i == PageModel.TotalPages)
                        tag.AddCssClass("rounded-r");

                    tag.InnerHtml.Append(i.ToString());
                    result.InnerHtml.AppendHtml(tag);
                }
                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
