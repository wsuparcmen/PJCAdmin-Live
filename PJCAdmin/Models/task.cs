//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PJCAdmin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    
    public partial class task
    {
        public task()
        {
            this.prompts = new HashSet<prompt>();
            this.usertasks = new HashSet<usertask>();
            this.usertaskprompts = new HashSet<usertaskprompt>();
            this.jobs = new HashSet<job>();
        }

        [DisplayName("Task ID")]
        public int taskID { get; set; }

        [DisplayName("Task Category ID")]
        public int taskCategoryID { get; set; }
        [DisplayName("Task Name")]
        public string taskName { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }
    
        public virtual ICollection<prompt> prompts { get; set; }

        [DisplayName("Task Category")]
        public virtual taskcategory taskcategory { get; set; }

        public virtual ICollection<usertask> usertasks { get; set; }
        public virtual ICollection<usertaskprompt> usertaskprompts { get; set; }
        public virtual ICollection<job> jobs { get; set; }
    }
}
