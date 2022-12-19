using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCenter
{
    class ClassApplications
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> DateOfSubmission { get; set; }
        public Nullable<System.DateTime> DateSOSW { get; set; }
        public System.DateTime DateOfCompletion { get; set; }
        public string Title { get; set; }
    }
}
