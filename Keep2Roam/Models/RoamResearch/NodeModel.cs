using System.Collections.Generic;

namespace Keep2Roam.Models.RoamResearch
{
    public class NodeModel
    {
        public string String { get; set; }
        public string CreateEmail { get; set; }
        public long CreateTime { get; set; }
        public List<NodeModel> Children { get; set; }
        public string Uid { get; set; }
        public long EditTime { get; set; }
        public string EditEmail { get; set; }
    }
}