using System.Collections.Generic;

namespace Keep2Roam.Models.RoamResearch
{
    public class PageModel
    {
        public string CreateEmail { get; set; }
        public long CreateTime { get; set; }
        public string Title { get; set; }
        public List<NodeModel> Children { get; set; }
        public long EditTime { get; set; }
        public string EditEmail { get; set; }
    }
}