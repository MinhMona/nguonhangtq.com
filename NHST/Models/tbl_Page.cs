//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NHST.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Page
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string PageContent { get; set; }
        public Nullable<int> PageTypeID { get; set; }
        public Nullable<bool> IsHidden { get; set; }
        public string IMG { get; set; }
        public Nullable<int> NodeID { get; set; }
        public string NodeAliasPath { get; set; }
        public string ogurl { get; set; }
        public string ogtitle { get; set; }
        public string ogdescription { get; set; }
        public string ogimage { get; set; }
        public string metatitle { get; set; }
        public string metadescription { get; set; }
        public string metakeyword { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
