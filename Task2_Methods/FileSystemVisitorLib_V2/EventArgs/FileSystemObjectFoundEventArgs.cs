using System.IO;

namespace FileSystemVisitorLib_V2.EventArgs
{
    public class FileSystemObjectFoundEventArgs : FileSystemObjectBaseEventArgs
    {
        public bool Stop { get; set; }

        public FileSystemObjectFoundEventArgs(FileSystemInfo fileSystemObject) : base(fileSystemObject)
        {
        }
    }
}