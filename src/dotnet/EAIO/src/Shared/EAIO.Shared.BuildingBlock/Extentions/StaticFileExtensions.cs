using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAIO.Shared.BuildingBlock.Extentions
{
    public static class StaticFileExtensions
    {
        public static void UseSharedStaticFiles(this WebApplication app)
        {
            var path = Path.GetFullPath(
                Path.Combine(app.Environment.ContentRootPath,
                             "..",
                             "Shared",
                             "EAIO.Shared.UI.Static",
                             "wwwroot"));

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = "/public"
            });
        }
    }
}
