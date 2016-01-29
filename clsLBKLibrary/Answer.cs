using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsLBKLibrary
{
    public class Answer
    {
        public int  Id { get; set; }
	    public string Description { get; set; }
	    public string Explanation { get; set; }
	    public bool IsRight { get; set; }
        public int  QuestionId { get; set; }
    }
}
