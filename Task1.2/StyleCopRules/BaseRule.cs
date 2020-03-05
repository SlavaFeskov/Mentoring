using StyleCop;
using StyleCop.CSharp;

namespace StyleCopRules
{
    [SourceAnalyzer(typeof(CsParser))]
    public abstract class BaseRule : SourceAnalyzer
    {
        public override void AnalyzeDocument(CodeDocument document)
        {
            var csDocument = (CsDocument) document;
            csDocument.WalkDocument(VisitElement);
        }

        protected abstract bool VisitElement(CsElement element, CsElement parentElement, object context);
    }
}