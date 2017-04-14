using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PJCAdmin.Models;
using PJCAdmin.Classes.Helpers.MVCModelHelpers;



namespace PJCAdmin.Classes.Helpers.APIModelHelpers
{
    /* --------------------------------------------------------
     * The RoutineHelper class provides common methods relating 
     * to Routines for the WebAPI service.
     * --------------------------------------------------------
     */
    public class RoutineHelper
    {
        private DbHelper helper;
        private TaskHelper taskHelper;
        private FeedbackHelper feedbackHelper;

        

        public RoutineHelper(DbHelper h = null)
        {
            if (h == null)
                helper = new DbHelper();
            else
                helper = h;
        }

        public DbHelper getDBHelper()
        {
            return helper;
        }

        #region Getters
        /*Returns a list of all routines assigned to the given 
         * user by the user listed as their parent. Returned
         * routines have been passed through the modelcopier
         * and are valid for serialization.
         * @param userName: The username for the child who is 
         * assigned the routines by their parent.
         */
        public List<Routine> getRoutinesAssignedByParentForSerialization(string userName)
        {
            string parentUserName = helper.findUserName(userName).guardianUserName;

            List<Routine> routinesByParent = new List<Routine>();

            foreach (Routine r in helper.findUserName(userName).Routines1)
            {
                if (r.creatorUserName.Equals(parentUserName))
                    routinesByParent.Add(ModelCopier.copyRoutine(r));
            }

            return routinesByParent;
        }
        /*Returns a list of all routines assigned to the given 
         * user by the user listed as their job coach. Returned
         * routines have been passed through the modelcopier
         * and are valid for serialization.
         * @param userName: The username for the user who is 
         * assigned the routines by their job coach.
         */
        public List<Routine> getRoutinesAssignedByJobCoachForSerialization(string userName)
        {
            string jobCoachUserName = helper.findUserName(userName).jobCoachUserName;

            List<Routine> routinesByJobCoach = new List<Routine>();

            foreach (Routine r in helper.findUserName(userName).Routines1)
            {
                if (r.creatorUserName.Equals(jobCoachUserName))
                    routinesByJobCoach.Add(ModelCopier.copyRoutine(r));
            }

            return routinesByJobCoach;
        }
        /*Returns a list of all routines assigned to the given 
         * user by both job coach and parent. Returned routines 
         * have been passed through the modelcopier and are 
         * valid for serialization.
         * @param userName: The username for the user who is 
         * assigned the routines by their job coach and parent.
         */
        public List<Routine> getAllRoutinesAssignedToUserForSerialization(string userName)
        {
            List<Routine> assignedRoutines = new List<Routine>();

            foreach (Routine r in helper.findUserName(userName).Routines1)
            {
                assignedRoutines.Add(ModelCopier.copyRoutine(r));
            }

            return assignedRoutines;
        }
        #endregion

        public void dispose()
        {
            helper.dispose();
        }

        #region create/modify
        public void modifyExistingRoutine(string routineName, RoutineModel model)
        {
            Routine rout = getRoutineAssignedToByName(routineName);

            rout.isTimed = model.isTimed;
            rout.expectedDuration = model.expectedDuration;
            rout.isNotifiable = model.isNotifiable;
            rout.isDisabled = model.isDisabled;
            rout.updatedDate = DateTime.Now;

            taskHelper.modifyExistingTasks(rout.Tasks.ToList(), model.Tasks.ToList(),true);
            feedbackHelper.updateRoutineFeedbacks(rout.routineID, model.Feedbacks.ToList());

            helper.updateRoutine(rout);
        }

        public Routine getRoutineAssignedToByName(string routineName)
        {

            //RoutineModel temp = helper.getAllRoutines().AsQueryable().Where<Routine>(r => r.isDisabled.Equals(0) && r.routineTitle.Equals(routineName));
            
            return (Routine) helper.getAllRoutines().AsQueryable().Where<Routine>(r => r.isDisabled.Equals(0) && r.routineTitle.Equals(routineName));
            // return getRoutinesAssignedTo(creatorUsername, assigneeName).Where(r => r.routineTitle.Equals(routineName)).ToList();
            //    public List<Routine> getRoutinesAssignedTo(string creatorUsername, string assigneeUsername)
            //{
            //    return getRoutines(creatorUsername).Where(r => r.assigneeUserName != null && r.assigneeUserName.Equals(assigneeUsername)).ToList();
            //}

            //public List<Routine> getRoutines(string creatorUsername)
            //{
            //    return helper.findUserName(creatorUsername).Routines.ToList();
            //}

            //getMostRecentRoutineAssignedToByName(creatorUsername, routineName, model.assigneeUserName);
            //return getRoutinesAssignedToByName(creatorUsername, routineName, assigneeName).OrderBy().Last();
        }
        #endregion
    }
}
