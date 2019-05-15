using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DFC.Composite.Shell.Moc.TwoCol.Models.Registration
{
    public class Region
    {

        public Api.RegionApiDto.PageRegions PageRegion { get; set; }

        public string RegionEndpoint { get; set; }

        public bool HeathCheckRequired { get; set; }

        public string OfflineHtml { get; set; }

    }
}
