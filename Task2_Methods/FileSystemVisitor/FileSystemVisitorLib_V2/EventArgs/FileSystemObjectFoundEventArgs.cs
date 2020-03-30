using FileSystemVisitorLib_V2.Models;

namespace FileSystemVisitorLib_V2.EventArgs
{
    public class FileSystemObjectFoundEventArgs<T> : FileSystemObjectBaseEventArgs<T>
    {
        public bool Stop { get; set; }

        public FileSystemObjectFoundEventArgs(BaseObject<T> fileSystemObject) : base(fileSystemObject)
        {
        }
    }
}