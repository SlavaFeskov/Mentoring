using FileSystemVisitorLib_V2.Models;

namespace FileSystemVisitorLib_V2.EventArgs
{
    public abstract class FileSystemObjectBaseEventArgs<T>
    {
        public BaseObject<T> FileSystemObject { get; set; }

        protected FileSystemObjectBaseEventArgs(BaseObject<T> fileSystemObject)
        {
            FileSystemObject = fileSystemObject;
        }
    }
}