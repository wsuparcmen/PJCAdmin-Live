using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PJCAdmin.Models
{
    public class JobModel
    {
        public JobModel()
        {
            this.stepEndTimes = new DateTime[0];
            this.jobNotes = new HashSet<NoteModel>();
            this.stepNotes = new HashSet<StepNoteModel>();
        }

        public string creatorUsername { get; set; }
        public string routineTitle { get; set; }
        public DateTime startTime { get; set; }
        public DateTime[] stepEndTimes { get; set; }
        public virtual ICollection<NoteModel> jobNotes { get; set; }
        public virtual ICollection<StepNoteModel> stepNotes { get; set; }
    }

    public class NoteModel
    {
        public string noteTitle { get; set; }
        public string noteMessage { get; set; }
    }

    public class StepNoteModel
    {
        public NoteModel note { get; set; }
        public int stepNo { get; set; }
    }
}