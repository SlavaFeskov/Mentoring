namespace FileSystemVisitorLib_V2.Models
{
    public abstract class BaseObject<TObject>
    {
        public TObject Info { get; set; }
        public abstract FileSystemObjectType Type { get; }
        public abstract string Name { get; }
        public abstract string Path { get; }
    }
}