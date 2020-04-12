using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSystemVisitorLib_V2.Models;
using FileSystemVisitorLib_V2.Services.Abstractions;

namespace FileSystemVisitorLib_V2.Services
{
    public class FileSystemObjectProvider : IFileSystemObjectsProvider<FileSystemInfo>
    {
        public IEnumerable<BaseObject<FileSystemInfo>> GetFileSystemObjects(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFileSystemInfos().Select(o => new FileSystemObject {Info = o}).ToList();
        }
    }
}