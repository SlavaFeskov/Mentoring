using System.IO;

namespace FileSystemVisitorLib_V2.Models
{
    public class FileSystemObject : BaseObject<FileSystemInfo>
    {
        public override FileSystemObjectType Type =>
            Info is FileInfo ? FileSystemObjectType.File : FileSystemObjectType.Directory;
        public override string Name => Info.Name;
        public override string Path => Info.FullName;
    }
}