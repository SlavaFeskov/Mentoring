using System;
using HttpModule.Task7.SiteDownloader.Model.Constraint;
using HttpModule.Task7.SiteDownloader.Services.Abstractions;

namespace HttpModule.Task7.SiteDownloader.Services
{
    public class UriConstraint : IConstraint
    {
        private readonly Uri _parentUri;
        private readonly CrossDomainConstraintType _crossDomainConstraintType;

        public UriConstraint(string parentUri, CrossDomainConstraintType crossDomainConstraintType)
        {
            _parentUri = new Uri(parentUri);
            _crossDomainConstraintType = crossDomainConstraintType;
        }

        public ConstraintType Type => ConstraintType.Uri | ConstraintType.File;

        public bool IsValid(Uri uri)
        {
            return _crossDomainConstraintType switch
            {
                CrossDomainConstraintType.All => true,
                CrossDomainConstraintType.OnlyCurrentDomain => _parentUri.DnsSafeHost == uri.DnsSafeHost,
                CrossDomainConstraintType.OnlyDescendantUrls => _parentUri.IsBaseOf(uri),
                _ => throw new ArgumentException()
            };
        }
    }
}