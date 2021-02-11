using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Services
{
    public interface IStorageFiles
    {
        Task<string> EditFile(byte[] content, string extension, string container, string contentType, string rute);
        Task DeleteFile(string rute, string container);
        Task<string> SaveFile(byte[] content, string extension, string container, string contentType);
    }
}
