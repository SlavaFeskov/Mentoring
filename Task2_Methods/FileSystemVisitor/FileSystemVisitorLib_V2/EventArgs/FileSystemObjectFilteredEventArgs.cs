using FileSystemVisitorLib_V2.Models;

namespace FileSystemVisitorLib_V2.EventArgs
{
    public class FileSystemObjectFilteredEventArgs<T> : FileSystemObjectBaseEventArgs<T>
    {
        public bool Skip { get; set; }

        public FileSystemObjectFilteredEventArgs(BaseObject<T> fileSystemObject) : base(fileSystemObject)
        {
        }
    }
}