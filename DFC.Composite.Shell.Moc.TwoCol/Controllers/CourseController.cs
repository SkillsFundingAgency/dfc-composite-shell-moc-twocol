using System;
using System.Collections.Generic;
using DFC.Composite.Shell.Moc.TwoCol.Data;
using DFC.Composite.Shell.Moc.TwoCol.Models;
using DFC.Composite.Shell.Moc.TwoCol.Services;
using Microsoft.AspNetCore.Mvc;

namespace DFC.Composite.Shell.Moc.TwoCol.Controllers
{
    public class CourseController : Controller
    {
        public const string FilterThisMonth = "ThisMonth";
        public const string FilterNextMonth = "NextMonth";
        public static readonly List<string> Filters = new List<string> { FilterThisMonth, FilterNextMonth };

        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult Head()
        {
            var vm = new HeadViewModel();

            return View(vm);
        }

        [HttpGet]
        public IActionResult BodyTop()
        {
            var vm = new BodyTopViewModel();

            return View(vm);
        }

        [HttpGet]
        public IActionResult Breadcrumb(string data)
        {
            string[] paths = null;
            string thisLocation = null;

            if (!string.IsNullOrEmpty(data))
            {
                paths = data.Split('/');

                if (paths.Length > 0)
                {
                    thisLocation = paths[paths.Length - 1];
                    paths = new ArraySegment<string>(paths, 0, paths.Length - 1).ToArray();
                }
            }

            var viewModel = new BreadcrumbViewModel()
            {
                Paths = paths,
                ThisLocation = thisLocation
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Index(string category, string filter, string searchClue)
        {
            var vm = new CourseIndexViewModel();
            bool filterThisMonth = (string.Compare(filter, FilterThisMonth, true) == 0);
            bool filterNextMonth = (string.Compare(filter, FilterNextMonth, true) == 0);

            vm.Courses = _courseService.GetCourses(category, filterThisMonth, filterNextMonth, searchClue);

            return View(vm);
        }

        [HttpGet]
        public IActionResult SidebarLeft()
        {
            var vm = new SidebarViewModel
            {
                Categories = _courseService.GetCategories()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult SidebarRight()
        {
            var vm = new SidebarViewModel
            {
                Categories = _courseService.GetCategories()
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult BodyFooter()
        {
            var vm = new BodyFooterViewModel();

            return View(vm);
        }

        [HttpGet]
        public IActionResult Search(string searchClue)
        {
            if (!string.IsNullOrEmpty(searchClue))
            {
                return RedirectToAction(nameof(Index), new { searchClue });
            }

            var vm = new SearchViewModel();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchViewModel search)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(search.Clue))
                {
                    return RedirectToAction(nameof(Index), new { searchClue = search.Clue });
                }
            }

            return View(search);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _courseService.GetCourse(id);
            var vm = new CourseEditViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Start = model.Start,
                MaxAttendeeCount = model.MaxAttendeeCount,
                Category = model.Category
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CourseEditViewModel course)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

    }
}