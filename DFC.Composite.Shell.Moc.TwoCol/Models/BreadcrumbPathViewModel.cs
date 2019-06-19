﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFC.Composite.Shell.Moc.TwoCol.Models
{
    public class BreadcrumbPathViewModel
    {
        public string Route { get; set; }
        public string Title { get; set; }
        public bool IsLastItem { get; set; } = false;
    }
}
