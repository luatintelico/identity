using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
    public interface IFileUpload
    {
        Task<string> UploadAsync(IFileListEntry file);
    }
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _environment;
        public FileUpload(IWebHostEnvironment env)
        {
            _environment = env;
        }
        public async Task<string> UploadAsync(IFileListEntry fileEntry)
        {
            var path = Path.Combine(_environment.ContentRootPath, "Upload", fileEntry.Name);
            var ms = new MemoryStream();
            await fileEntry.Data.CopyToAsync(ms);
            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                ms.WriteTo(file);
            }
            return path;
        }
    }
}
