using System.Collections.Generic;
using DFC.Composite.Shell.Moc.TwoCol.Data;

namespace DFC.Composite.Shell.Moc.TwoCol.Models
{
    public class SidebarViewModel : BaseViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}
