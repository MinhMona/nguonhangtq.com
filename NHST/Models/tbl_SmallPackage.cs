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
    
    public partial class tbl_SmallPackage
    {
        public int ID { get; set; }
        public Nullable<int> MainOrderID { get; set; }
        public Nullable<int> BigPackageID { get; set; }
        public string OrderTransactionCode { get; set; }
        public string ProductType { get; set; }
        public Nullable<double> FeeShip { get; set; }
        public Nullable<double> Weight { get; set; }
        public Nullable<double> Volume { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<bool> IsTemp { get; set; }
        public Nullable<System.DateTime> DateInLasteWareHouse { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<bool> IsLost { get; set; }
        public string PackageCodeTemp { get; set; }
        public Nullable<bool> IsHelpMoving { get; set; }
        public Nullable<int> TransportationOrderID { get; set; }
        public Nullable<double> WarehouseFee { get; set; }
        public string IMGPackage { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> DateOutWarehouse { get; set; }
        public Nullable<int> UID { get; set; }
        public string Username { get; set; }
        public Nullable<double> Length { get; set; }
        public Nullable<double> Height { get; set; }
        public Nullable<double> Width { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public string ListIMG { get; set; }
        public Nullable<System.DateTime> DateInTQWarehouse { get; set; }
        public string StaffTQWarehouse { get; set; }
        public string StaffVNOutWarehouse { get; set; }
        public string StaffVNWarehouse { get; set; }
    }
}