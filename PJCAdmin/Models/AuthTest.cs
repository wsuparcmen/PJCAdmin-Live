//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Runtime.Serialization;
namespace PJCAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    [DataContract]
    public partial class AuthTest
    {
        [DataMember]
        public int AuthTestID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string TestMessage { get; set; }
    
        public virtual UserName UserName1 { get; set; }
    }
}
