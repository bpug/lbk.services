using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsLBKLibrary
{
    public class Event
    {
        public DateTime ActivatedAt { get; set; }
        public string Date { get; set; }
        public DateTime DateOrder { get; set; }
        public string Description { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsActivated { get; set; }
        public string ReservationLink { get; set; }
        public string ThumbnailLink { get; set; }
        public string Title { get; set; }
    }
}
