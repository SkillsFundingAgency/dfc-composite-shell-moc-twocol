using System;
using System.Collections.Generic;
using System.Linq;
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
        [Route("course/head/{**data}")]
        public IActionResult Head(string data)
        {
            var vm = new HeadViewModel();

            return View(vm);
        }

        [HttpGet]
        [Route("course/bodytop/{**data}")]
        public IActionResult BodyTop(string data)
        {
            var vm = new BodyTopViewModel();

            return View(vm);
        }

        [HttpGet]
        [Route("course/breadcrumb/{**data}")]
        public IActionResult Breadcrumb(string data)
        {
            var vm = new BreadcrumbViewModel();

            if (!string.IsNullOrWhiteSpace(data))
            {
                vm.Paths = new List<BreadcrumbPathViewModel>() {
                    new BreadcrumbPathViewModel()
                    {
                        Route = "/",
                        Title = "Home"
                    },
                    new BreadcrumbPathViewModel()
                    {
                        Route = "/course/index",
                        Title = "Courses"
                    },
                    new BreadcrumbPathViewModel()
                    {
                        Route = $"/course/{data}",
                        Title = data
                    }
                };

                vm.Paths.Last().IsLastItem = true;
            }

            return View(vm);
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
        [Route("course/sidebarleft/{**data}")]
        public IActionResult SidebarLeft(string data)
        {
            var vm = new SidebarViewModel
            {
                Categories = _courseService.GetCategories()
            };

            return View(vm);
        }

        [HttpGet]
        [Route("course/sidebarright/{**data}")]
        public IActionResult SidebarRight(string data)
        {
            var vm = new SidebarViewModel
            {
                Categories = _courseService.GetCategories()
            };

            return View(vm);
        }

        [HttpGet]
        [Route("course/bodyfooter/{**data}")]
        public IActionResult BodyFooter(string data)
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