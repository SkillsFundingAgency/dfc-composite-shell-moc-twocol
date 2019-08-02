﻿using DFC.Composite.Shell.Moc.TwoCol.Models;
using DFC.Composite.Shell.Moc.TwoCol.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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
            var vm = new HeadViewModel
            {
                Title = string.IsNullOrWhiteSpace(data) ? "Index" : data,
                Contents = null
            };

            PopulateVisits(vm);

            return View(vm);
        }

        [HttpGet]
        [Route("course/bodytop/{**data}")]
        public IActionResult BodyTop(string data)
        {
            var vm = new BodyTopViewModel
            {
                Title = nameof(BodyTop),
                Contents = null
            };

            return View(vm);
        }

        [HttpGet]
        [Route("course/breadcrumb/{**data}")]
        public IActionResult Breadcrumb(string data)
        {
            var vm = new BreadcrumbViewModel()
            {
                Title = data,
                Paths = new List<BreadcrumbPathViewModel>() {
                    new BreadcrumbPathViewModel()
                    {
                        Route = "/",
                        Title = "Home"
                    },
                    new BreadcrumbPathViewModel()
                    {
                        Route = "/course/index",
                        Title = "Courses"
                    }
                }
            };

            if (!string.IsNullOrWhiteSpace(data))
            {
                vm.Paths.Add(
                    new BreadcrumbPathViewModel()
                    {
                        Route = $"/course/{data}",
                        Title = data
                    }
                );

                vm.Paths.Last().IsLastItem = true;
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Index(string category, string filter, string searchClue)
        {
            bool filterThisMonth = (string.Compare(filter, FilterThisMonth, true) == 0);
            bool filterNextMonth = (string.Compare(filter, FilterNextMonth, true) == 0);
            var vm = new CourseIndexViewModel()
            {
                Title = nameof(Index)
            };

            vm.Courses = _courseService.GetCourses(category, filterThisMonth, filterNextMonth, searchClue);

            PopulateVisits(vm);

            return View(vm);
        }

        [HttpGet]
        public IActionResult Health()
        {
            return Ok();
        }

        [HttpGet]
        [Route("course/sidebarleft/{**data}")]
        public IActionResult SidebarLeft(string data)
        {
            var vm = new SidebarViewModel
            {
                Title = nameof(SidebarLeft),
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
                Title = nameof(SidebarRight),
                Categories = _courseService.GetCategories()
            };

            return View(vm);
        }

        [HttpGet]
        [Route("course/bodyfooter/{**data}")]
        public IActionResult BodyFooter(string data)
        {
            var vm = new BodyFooterViewModel
            {
                Title = nameof(BodyFooter),
                Contents = null
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Search(string searchClue)
        {
            if (!string.IsNullOrEmpty(searchClue))
            {
                return RedirectToAction(nameof(Index), new { searchClue });
            }

            var vm = new SearchViewModel()
            {
                Title = nameof(Search)
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("course/search")]
        [Route("/search")]
        public IActionResult Search(SearchViewModel search)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(search.Clue))
                {
                    return RedirectToAction(nameof(Index), new { searchClue = search.Clue });
                }
            }

            search.Title = nameof(Search);

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
                City = model.City,
                Category = model.Category
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("course/edit")]
        [Route("/edit")]
        public IActionResult Edit(CourseEditViewModel course)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

        private void PopulateVisits(BaseViewModel baseViewModel)
        {
            var visitsBodyKey = "VisitsBody";
            var visitsBody = HttpContext.Session.GetInt32(visitsBodyKey) ?? 0;
            HttpContext.Session.SetInt32(visitsBodyKey, visitsBody + 1);
            baseViewModel.VisitsBody = visitsBody + 1;

            var visitsFooterKey = "VisitsFooter";
            var visitsFooter = HttpContext.Session.GetInt32(visitsFooterKey) ?? 0;
            HttpContext.Session.SetInt32(visitsFooterKey, visitsFooter + 1);
            baseViewModel.VisitsFooter = visitsFooter + 1;
        }

    }
}