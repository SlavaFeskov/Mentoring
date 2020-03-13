using System.IO;

namespace FileSystemVisitorLib_V2.EventArgs
{
    public abstract class FileSystemObjectBaseEventArgs
    {
        public FileSystemInfo FileSystemObject { get; set; }

        protected FileSystemObjectBaseEventArgs(FileSystemInfo fileSystemObject)
        {
            FileSystemObject = fileSystemObject;
        }
    }
}