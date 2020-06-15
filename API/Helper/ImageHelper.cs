using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public static class ImageHelper
    {
        private static string _imagesFolder = Startup.StaticConfig["ImagesFolder:Path"];
        public static async Task<string> Upload(IFormFile formFile)
        {
            string FilePath = string.Empty;
            string FileName = Path.GetRandomFileName();
            if (!string.IsNullOrWhiteSpace(formFile.FileName))
            {
                FilePath = Path.Combine(_imagesFolder, FileName + Path.GetExtension(formFile.FileName));
                using (var stream = System.IO.File.Create(FilePath))
                {
                    await formFile.CopyToAsync(stream);
                }
            }
            return FileName + Path.GetExtension(formFile.FileName);
        }

        public static bool Remove(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                File.Delete(Path.Combine(_imagesFolder, fileName));
                return true;
            }
            return false;
        }
    }
}
