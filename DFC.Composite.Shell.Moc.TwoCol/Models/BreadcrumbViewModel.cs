using System.Collections.Generic;

namespace DFC.Composite.Shell.Moc.TwoCol.Models
{
    public class BreadcrumbViewModel
    {
        public string Title { get; set; } = "Unknown course";
        public IList<BreadcrumbPathViewModel> Paths { get; set; }
    }
}
