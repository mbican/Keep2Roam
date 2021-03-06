﻿using System.Collections.Generic;

namespace Keep2Roam.Models.GoogleKeep
{
    public class CardModel
    {
        public List<AttachmentModel> Attachments { get; set; }
        public string Color { get; set; }
        public bool IsTrashed { get; set; }
        public bool IsPinned { get; set; }
        public bool IsArchived { get; set; }
        public List<AnnotationModel> Annotations { get; set; }
        public List<ListItemModel> ListContent { get; set; }
        public string TextContent { get; set; }
        public string Title { get; set; }
        public long UserEditedTimestampUsec { get; set; }
        public List<LabelModel> Labels { get; set; }
    }
}