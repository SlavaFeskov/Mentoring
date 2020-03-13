using System.IO;

namespace FileSystemVisitorLib_V2.EventArgs
{
    public class FileSystemObjectFilteredEventArgs : FileSystemObjectBaseEventArgs
    {
        public bool Skip { get; set; }

        public FileSystemObjectFilteredEventArgs(FileSystemInfo fileSystemObject) : base(fileSystemObject)
        {
        }
    }
}