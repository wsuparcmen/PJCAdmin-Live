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
    
    public partial class TaskCategory
    {
        public TaskCategory()
        {
            this.Tasks = new HashSet<Task>();
        }
    
        public byte taskCategoryID { get; set; }
        public string categoryName { get; set; }
    
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
