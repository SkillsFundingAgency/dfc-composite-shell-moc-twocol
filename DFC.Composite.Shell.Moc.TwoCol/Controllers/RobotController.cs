﻿using System;
using DFC.Composite.Shell.Moc.TwoCol.Models.Robots;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFC.Composite.Shell.Moc.TwoCol.Controllers
{
    public class RobotController : Controller
    {
        private readonly ILogger<RobotController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public RobotController(ILogger<RobotController> logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ContentResult Robot()
        {
            try
            {
                _logger.LogInformation("Generating Robots.txt");

                var robot = GenerateThisSiteRobot();

                // add any dynamic robots data form the Shell app
                //robot.Add("<<add any dynamic text or other here>>");

                _logger.LogInformation("Generated Robots.txt");

                return Content(robot.Data, "text/plain");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(Robot)}: {ex.Message}");
            }

            // fall through from errors
            return Content(null, "text/plain");
        }

        private Robot GenerateThisSiteRobot()
        {
            var robot = new Robot();
            string robotsFilePath = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "StaticRobots.txt");

            if (System.IO.File.Exists(robotsFilePath))
            {
                // output the composite UI default (static) robots data from the StaticRobots.txt file
                string staticRobotsText = System.IO.File.ReadAllText(robotsFilePath);

                if (!string.IsNullOrEmpty(staticRobotsText))
                {
                    robot.Add(staticRobotsText);
                }
            }

            // add any dynamic robots data form the Shell app
            //robot.Add("<<add any dynamic text or other here>>");

            return robot;
        }

    }
}