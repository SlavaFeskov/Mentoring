using System.Collections.Generic;
using FileSystemVisitorLib_V2.Models;

namespace FileSystemVisitorLib_V2.Services.Abstractions
{
    public interface IFileSystemObjectsProvider<TObject>
    {
        IEnumerable<BaseObject<TObject>> GetFileSystemObjects(string path);
    }
}