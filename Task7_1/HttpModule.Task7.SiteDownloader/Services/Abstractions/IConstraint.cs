using System;
using HttpModule.Task7.SiteDownloader.Model.Constraint;

namespace HttpModule.Task7.SiteDownloader.Services.Abstractions
{
    public interface IConstraint
    {
        ConstraintType Type { get; }
        bool IsValid(Uri uri);
    }
}