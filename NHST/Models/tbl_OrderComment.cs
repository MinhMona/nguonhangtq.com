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
    
    public partial class tbl_OrderComment
    {
        public int ID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CommentIMG { get; set; }
        public Nullable<int> TypeOrder { get; set; }
        public string Link { get; set; }
        public Nullable<int> TypeContent { get; set; }
    }
}
