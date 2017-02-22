//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template made by Louis-Guillaume Morand.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace USP.Models.ViewModel
{
    
    
    public class SysRolePrivilegeMetaData
    {
        [Required] 
        public virtual long ID
        {
            get;
            set;
        }
        [Required] 
        public virtual long Role
        {
            get;
            set;
        }
        [Required] 
        public virtual long Privilege
        {
            get;
            set;
        }
    	
        [StringLength(250, ErrorMessage="最多可输入250个字符")]
        public virtual string Reserve
        {
            get;
            set;
        }
    	
        [StringLength(250, ErrorMessage="最多可输入250个字符")]
        public virtual string Remark
        {
            get;
            set;
        }
        [Required] 
        public virtual long Creator
        {
            get;
            set;
        }
        [Required] 
        public virtual System.DateTime CreateTime
        {
            get;
            set;
        }
        public virtual Nullable<long> Auditor
        {
            get;
            set;
        }
        public virtual Nullable<System.DateTime> AuditTime
        {
            get;
            set;
        }
        public virtual Nullable<long> Canceler
        {
            get;
            set;
        }
        public virtual Nullable<System.DateTime> CancelTime
        {
            get;
            set;
        }
    }
}
