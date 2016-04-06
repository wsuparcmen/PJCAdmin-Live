using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PJCAdmin.Models;

namespace PJCAdmin.Classes.Helpers.APIModelHelpers
{
    public class JobHelper
    {
        private PJCAdmin.Classes.Helpers.MVCModelHelpers.RoutineHelper routineHelper;
        private DbHelper helper;

        public JobHelper(DbHelper h = null)
        {
            if (h == null)
                helper = new DbHelper();
            else
                helper = h;

            routineHelper = new PJCAdmin.Classes.Helpers.MVCModelHelpers.RoutineHelper(helper);
        }

        public DbHelper getDBHelper()
        {
            return helper;
        }

        public bool createJob(string assigneeUsername, JobModel job)
        {
            if (!routineHelper.routineExists(job.creatorUsername, job.routineTitle, assigneeUsername))
                return false;

            Routine r = routineHelper.getMostRecentRoutineAssignedToByName(job.creatorUsername, job.routineTitle, assigneeUsername);

            Job j = new Job()
            {
                routineID = r.routineID,
                startTime = job.startTime
            };

            for (byte i = 0; i < job.stepEndTimes.Count(); i++)
            {
                Step s = new Step()
                {
                    sequenceNo = (byte) (i + 1),
                    endTime = job.stepEndTimes[i]
                };

                /*foreach (StepNoteModel stepNote in job.stepNotes)
                {
                    if (stepNote.stepNo == i + 1)
                        s.Notes.Add(new Note() { 
                            noteTitle = stepNote.note.noteTitle,
                            noteMessage = stepNote.note.noteMessage
                        });
                }*/

                j.Steps.Add(s);
            }

            /*foreach (NoteModel jobNote in job.jobNotes)
            {
                j.Notes.Add(new Note() { 
                    noteTitle = jobNote.noteTitle,
                    noteMessage = jobNote.noteMessage
                });
            }*/

            j = helper.createJob(j);

            return true;
        }
    }
}