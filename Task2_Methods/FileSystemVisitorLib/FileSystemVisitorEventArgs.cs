using System.IO;

namespace FileSystemVisitorLib
{
    public class FileSystemVisitorEventArgs
    {
        public FileSystemInfo FileSystemInfo { get; set; }

        public FileSystemVisitorEventArgs()
        {
        }

        public FileSystemVisitorEventArgs(FileSystemInfo fileSystemInfo)
        {
            FileSystemInfo = fileSystemInfo;
        }
    }
}