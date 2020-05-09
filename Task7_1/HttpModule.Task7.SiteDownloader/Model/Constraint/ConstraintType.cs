using System;

namespace HttpModule.Task7.SiteDownloader.Model.Constraint
{
    [Flags]
    public enum ConstraintType
    {
        File = 1,
        Uri = 2
    }
}