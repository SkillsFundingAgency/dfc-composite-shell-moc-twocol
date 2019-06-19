using System.Collections.Generic;
using DFC.Composite.Shell.Moc.TwoCol.Data;

namespace DFC.Composite.Shell.Moc.TwoCol.Models
{
    public class CourseIndexViewModel : BaseViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
    }
}
