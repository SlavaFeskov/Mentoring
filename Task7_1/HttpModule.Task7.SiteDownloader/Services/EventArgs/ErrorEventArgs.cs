namespace HttpModule.Task7.SiteDownloader.Services.EventArgs
{
    public class ErrorEventArgs
    {
        public string Message { get; set; }

        public ErrorEventArgs(string message)
        {
            Message = message;
        }
    }
}