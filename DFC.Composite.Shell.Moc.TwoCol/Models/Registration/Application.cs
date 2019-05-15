using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFC.Composite.Shell.Moc.TwoCol.Models.Registration
{
    public class Application
    {
        public string Path { get; set; }

        public string TopNavigationText { get; set; }

        public int TopNavigationOrder { get; set; }

        public Api.PathApiDto.PageLayouts Layout { get; set; }

        public string OfflineHtml { get; set; }

        public string SitemapUrl { get; set; }

        public IEnumerable<Region> Regions { get; set; }

    }
}
