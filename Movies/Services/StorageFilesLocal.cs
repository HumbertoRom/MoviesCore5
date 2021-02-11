using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Services
{
    public class StorageFilesLocal : IStorageFiles
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public StorageFilesLocal(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task DeleteFile(string rute, string container)
        {
            if (!string.IsNullOrEmpty(rute))
            {
                string fileName = Path.GetFileName(rute);
                string fileDirectory = Path.Combine(env.WebRootPath, container, fileName);

                if (File.Exists(fileDirectory))
                {
                    File.Delete(fileDirectory);
                }
            }

            return Task.FromResult(0);
        }

        public async Task<string> EditFile(byte[] content, string extension, string container, string contentType, string rute)
        {
            await DeleteFile(rute, container);
            return await SaveFile(content, extension, container, contentType);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            string fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string rute = Path.Combine(folder, fileName);
            await File.WriteAllBytesAsync(rute, content);
            string currentUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            string dbUrl = Path.Combine(currentUrl, container, fileName).Replace("\\", "/");

            return dbUrl;
        }
    }
}
