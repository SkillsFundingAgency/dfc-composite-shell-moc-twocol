using Microsoft.AspNetCore.Html;

namespace DFC.Composite.Shell.Moc.TwoCol.Models
{
    public class BaseViewModel
    {
        public string Title { get; set; } = "Unknown Course title";

        public HtmlString Contents { get; set; } = new HtmlString("Unknown Help content");
    }
}
