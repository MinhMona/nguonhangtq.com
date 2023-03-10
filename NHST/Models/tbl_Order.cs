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
    
    public partial class tbl_Order
    {
        public int ID { get; set; }
        public Nullable<int> UID { get; set; }
        public string title_origin { get; set; }
        public string title_translated { get; set; }
        public string price_origin { get; set; }
        public string price_promotion { get; set; }
        public string property_translated { get; set; }
        public string property { get; set; }
        public string data_value { get; set; }
        public string image_model { get; set; }
        public string image_origin { get; set; }
        public string shop_id { get; set; }
        public string shop_name { get; set; }
        public string seller_id { get; set; }
        public string wangwang { get; set; }
        public string quantity { get; set; }
        public string stock { get; set; }
        public string location_sale { get; set; }
        public string site { get; set; }
        public string comment { get; set; }
        public string item_id { get; set; }
        public string link_origin { get; set; }
        public string link { get; set; }
        public string outer_id { get; set; }
        public string error { get; set; }
        public string weight { get; set; }
        public string step { get; set; }
        public string brand { get; set; }
        public string category_name { get; set; }
        public string category_id { get; set; }
        public string tool { get; set; }
        public string version { get; set; }
        public Nullable<bool> is_translate { get; set; }
        public Nullable<bool> IsForward { get; set; }
        public string IsForwardPrice { get; set; }
        public Nullable<bool> IsFastDelivery { get; set; }
        public string IsFastDeliveryPrice { get; set; }
        public Nullable<bool> IsCheckProduct { get; set; }
        public string IsCheckProductPrice { get; set; }
        public Nullable<bool> IsPacked { get; set; }
        public string IsPackedPrice { get; set; }
        public Nullable<bool> IsFast { get; set; }
        public string IsFastPrice { get; set; }
        public string PriceVND { get; set; }
        public string PriceCNY { get; set; }
        public string FeeShipCN { get; set; }
        public string FeeBuyPro { get; set; }
        public string FeeWeight { get; set; }
        public string Note { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<int> Status { get; set; }
        public string Deposit { get; set; }
        public string CurrentCNYVN { get; set; }
        public string TotalPriceVND { get; set; }
        public string PriceChange { get; set; }
        public string RealPrice { get; set; }
        public Nullable<int> MainOrderID { get; set; }
        public string ProductNote { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> ProductStatus { get; set; }
        public string stepprice { get; set; }
        public string OrderShopCode { get; set; }
        public Nullable<bool> isGetLink { get; set; }
    }
}
