//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace USP.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    using System.ComponentModel.DataAnnotations;
    [MetadataType(typeof(USP.Models.ViewModel.SysCorpVocationMetaData))]
    public partial class SysCorpVocation
    {
        public long ID { get; set; }
        public long Parent { get; set; }
        public string Name { get; set; }
        public string Reserve { get; set; }
        public string Remark { get; set; }
        public long Creator { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<long> Auditor { get; set; }
        public Nullable<System.DateTime> AuditTime { get; set; }
        public Nullable<long> Canceler { get; set; }
        public Nullable<System.DateTime> CancelTime { get; set; }
    }
}
