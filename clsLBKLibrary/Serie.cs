using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsLBKLibrary
{
    public class Serie
    {
        public DateTime ActivatedAt { get; set; }
        public string Description { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsActivated { get; set; }
    }
}
