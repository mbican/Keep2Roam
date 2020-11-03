using System.Collections.Generic;

namespace keep2roam
{
    public class GoogleKeepCardModel
    {
        public string Color { get; set; }
        public bool IsTrashed { get; set; }
        public bool IsPinned { get; set; }
        public bool IsArchived { get; set; }
        public List<object> Annotations { get; set; }
        public List<object> ListContent { get; set; }
        public string Title { get; set; }
        public long UserEditedTimestampUsec { get; set; }
        public List<object> Labels { get; set; }
    }
}