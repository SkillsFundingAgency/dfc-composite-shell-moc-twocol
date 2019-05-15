using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DFC.Composite.Shell.Moc.TwoCol.Data;

namespace DFC.Composite.Shell.Moc.TwoCol.Services
{
    public interface ICourseService
    {
        Course GetCourse(int id);
        List<Course> GetCourses(string category = null, bool filterThisMonth = false, bool filterNextMonth = false, string searchClue = null);
        List<Category> GetCategories();
    }
}
