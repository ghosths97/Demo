#pragma checksum "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "534455edfe1a37efd420f5ad6758e3fa5d4fdcf4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Demo.Web.Pages.Pages_Index), @"mvc.1.0.razor-page", @"/Pages/Index.cshtml")]
namespace Demo.Web.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\_ViewImports.cshtml"
using Demo.Web;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"534455edfe1a37efd420f5ad6758e3fa5d4fdcf4", @"/Pages/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b884b425dfa100d986d995503641027902a9c1e", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\Index.cshtml"
  
    ViewData["Title"] = "Home page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"text-center\">\r\n    <h1 class=\"display-4\">\r\n        ");
#nullable restore
#line 10 "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\Index.cshtml"
   Write(Html.LabelFor(m => m.pageName));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

    </h1>
    

    <table class=""table table-striped"">

        <thead>

            <tr>
                <td>
                    Name
                </td>
                <td>
                    Company id
                </td>
                <td>
                    Production Date
                </td>
                <td>
                    ExpiresInDays
                </td>
                <td>
                    Expiry
                </td>
            </tr>

        </thead>


");
#nullable restore
#line 40 "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\Index.cshtml"
         foreach (var item in Model.Products)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\r\n\r\n        <td>");
#nullable restore
#line 44 "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\Index.cshtml"
       Write(item.name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n        <td>");
#nullable restore
#line 45 "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\Index.cshtml"
       Write(item.companyId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n        <td>");
#nullable restore
#line 47 "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\Index.cshtml"
       Write(item.production);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n        <td>");
#nullable restore
#line 49 "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\Index.cshtml"
       Write(item.expiresInDays);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n        <td>");
#nullable restore
#line 51 "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\Index.cshtml"
       Write(item.ExpiryDate());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n    </tr>\r\n");
#nullable restore
#line 54 "C:\Users\Daffolap-917\Desktop\Demo\Demo.Web\Pages\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </table>\r\n\r\n</div>\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IndexModel>)PageContext?.ViewData;
        public IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
