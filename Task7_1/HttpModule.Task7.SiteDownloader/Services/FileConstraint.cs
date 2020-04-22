using System;
using System.Collections.Generic;
using System.Linq;
using HttpModule.Task7.SiteDownloader.Model.Constraint;
using HttpModule.Task7.SiteDownloader.Services.Abstractions;

namespace HttpModule.Task7.SiteDownloader.Services
{
    public class FileConstraint : IConstraint
    {
        private readonly IEnumerable<string> _allowedExtensions;

        public FileConstraint(IEnumerable<string> allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        public ConstraintType Type => ConstraintType.File;

        public bool IsValid(Uri uri)
        {
            var ext = uri.Segments.Last();

            return ext != null && _allowedExtensions.Any(e => ext.EndsWith(e));
        }
    }
}