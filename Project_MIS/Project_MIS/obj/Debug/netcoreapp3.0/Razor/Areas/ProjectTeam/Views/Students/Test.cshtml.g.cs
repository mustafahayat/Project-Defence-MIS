#pragma checksum "E:\My Repo\Project-Defence-MIS\Project_MIS\Project_MIS\Areas\ProjectTeam\Views\Students\Test.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "53a80201bdb63171fb7c6f5c0d96e9b2176239b0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_ProjectTeam_Views_Students_Test), @"mvc.1.0.view", @"/Areas/ProjectTeam/Views/Students/Test.cshtml")]
namespace AspNetCore
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
#line 1 "E:\My Repo\Project-Defence-MIS\Project_MIS\Project_MIS\Areas\ProjectTeam\Views\_ViewImports.cshtml"
using Project_MIS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\My Repo\Project-Defence-MIS\Project_MIS\Project_MIS\Areas\ProjectTeam\Views\_ViewImports.cshtml"
using Project_MIS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"53a80201bdb63171fb7c6f5c0d96e9b2176239b0", @"/Areas/ProjectTeam/Views/Students/Test.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2a2ef7da8beb913ce9d37d44bf03051a5c9bbf16", @"/Areas/ProjectTeam/Views/_ViewImports.cshtml")]
    public class Areas_ProjectTeam_Views_Students_Test : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "E:\My Repo\Project-Defence-MIS\Project_MIS\Project_MIS\Areas\ProjectTeam\Views\Students\Test.cshtml"
  
    ViewData["Title"] = "Test";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Test</h1>
<table class=""table table-bordered"">
    <thead>
    <tr>
        <th>Name</th>
        <th>Name</th>
        <th>Name</th>
        <th>Name</th>
        <th>Name</th>
    </tr>
    </thead>
    <tbody>
        <tr>
            <td>Mustafa</td>
            <td>Mustafa</td>
            <td>Mustafa</td>
            <td>Mustafa</td>
            <td>Mustafa</td>
        </tr>
    </tbody>
</table>


");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    \r\n    <script>\r\n        $(document).ready(function() {\r\n            $(\".table\").DataTable();\r\n        })\r\n    </script>\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
