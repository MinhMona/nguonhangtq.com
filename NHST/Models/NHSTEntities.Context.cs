﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class NHSTEntities : DbContext
    {
        public NHSTEntities()
            : base("name=NHSTEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tbl_Account> tbl_Account { get; set; }
        public virtual DbSet<tbl_AccountantOutStockPayment> tbl_AccountantOutStockPayment { get; set; }
        public virtual DbSet<tbl_AccountInfo> tbl_AccountInfo { get; set; }
        public virtual DbSet<tbl_AdminSendUserWallet> tbl_AdminSendUserWallet { get; set; }
        public virtual DbSet<tbl_AppPushNoti> tbl_AppPushNoti { get; set; }
        public virtual DbSet<tbl_Bank> tbl_Bank { get; set; }
        public virtual DbSet<tbl_Banner> tbl_Banner { get; set; }
        public virtual DbSet<tbl_Benefits> tbl_Benefits { get; set; }
        public virtual DbSet<tbl_BigPackage> tbl_BigPackage { get; set; }
        public virtual DbSet<tbl_BigPackage1> tbl_BigPackage1 { get; set; }
        public virtual DbSet<tbl_BigPackageHistory> tbl_BigPackageHistory { get; set; }
        public virtual DbSet<tbl_ChinaSite> tbl_ChinaSite { get; set; }
        public virtual DbSet<tbl_Commitment> tbl_Commitment { get; set; }
        public virtual DbSet<tbl_Complain> tbl_Complain { get; set; }
        public virtual DbSet<tbl_Configuration> tbl_Configuration { get; set; }
        public virtual DbSet<tbl_Contact> tbl_Contact { get; set; }
        public virtual DbSet<tbl_CustomerBenefits> tbl_CustomerBenefits { get; set; }
        public virtual DbSet<tbl_DeviceToken> tbl_DeviceToken { get; set; }
        public virtual DbSet<tbl_DinhKhoan> tbl_DinhKhoan { get; set; }
        public virtual DbSet<tbl_ExportRequestTurn> tbl_ExportRequestTurn { get; set; }
        public virtual DbSet<tbl_FeeBuyPro> tbl_FeeBuyPro { get; set; }
        public virtual DbSet<tbl_FeeWeightTQVN> tbl_FeeWeightTQVN { get; set; }
        public virtual DbSet<tbl_Footer> tbl_Footer { get; set; }
        public virtual DbSet<tbl_HistoryAutoBanking> tbl_HistoryAutoBanking { get; set; }
        public virtual DbSet<tbl_HistoryOrderChange> tbl_HistoryOrderChange { get; set; }
        public virtual DbSet<tbl_HistoryPayWallet> tbl_HistoryPayWallet { get; set; }
        public virtual DbSet<tbl_HistoryPayWalletCYN> tbl_HistoryPayWalletCYN { get; set; }
        public virtual DbSet<tbl_HistoryServices> tbl_HistoryServices { get; set; }
        public virtual DbSet<tbl_InWareHousePrice> tbl_InWareHousePrice { get; set; }
        public virtual DbSet<tbl_KyQuy> tbl_KyQuy { get; set; }
        public virtual DbSet<tbl_MainOder> tbl_MainOder { get; set; }
        public virtual DbSet<tbl_MainOrderCode> tbl_MainOrderCode { get; set; }
        public virtual DbSet<tbl_MainOrderRequestShip> tbl_MainOrderRequestShip { get; set; }
        public virtual DbSet<tbl_Menu> tbl_Menu { get; set; }
        public virtual DbSet<tbl_Message> tbl_Message { get; set; }
        public virtual DbSet<tbl_Node> tbl_Node { get; set; }
        public virtual DbSet<tbl_Notification> tbl_Notification { get; set; }
        public virtual DbSet<tbl_Notifications> tbl_Notifications { get; set; }
        public virtual DbSet<tbl_OrderComment> tbl_OrderComment { get; set; }
        public virtual DbSet<tbl_OrderShopTemp> tbl_OrderShopTemp { get; set; }
        public virtual DbSet<tbl_OrderTemp> tbl_OrderTemp { get; set; }
        public virtual DbSet<tbl_OTP> tbl_OTP { get; set; }
        public virtual DbSet<tbl_OutStockSession> tbl_OutStockSession { get; set; }
        public virtual DbSet<tbl_OutStockSessionPackage> tbl_OutStockSessionPackage { get; set; }
        public virtual DbSet<tbl_Page> tbl_Page { get; set; }
        public virtual DbSet<tbl_PageSEO> tbl_PageSEO { get; set; }
        public virtual DbSet<tbl_PageType> tbl_PageType { get; set; }
        public virtual DbSet<tbl_Partners> tbl_Partners { get; set; }
        public virtual DbSet<tbl_PayAllOrderHistory> tbl_PayAllOrderHistory { get; set; }
        public virtual DbSet<tbl_PayHelp> tbl_PayHelp { get; set; }
        public virtual DbSet<tbl_PayhelpDetail> tbl_PayhelpDetail { get; set; }
        public virtual DbSet<tbl_PayHelpTemp> tbl_PayHelpTemp { get; set; }
        public virtual DbSet<tbl_PayOrderHistory> tbl_PayOrderHistory { get; set; }
        public virtual DbSet<tbl_Present> tbl_Present { get; set; }
        public virtual DbSet<tbl_PriceChange> tbl_PriceChange { get; set; }
        public virtual DbSet<tbl_ProductCategory> tbl_ProductCategory { get; set; }
        public virtual DbSet<tbl_ProductLink> tbl_ProductLink { get; set; }
        public virtual DbSet<tbl_Products> tbl_Products { get; set; }
        public virtual DbSet<tbl_Refund> tbl_Refund { get; set; }
        public virtual DbSet<tbl_RequestOutStock> tbl_RequestOutStock { get; set; }
        public virtual DbSet<tbl_RequestShip> tbl_RequestShip { get; set; }
        public virtual DbSet<tbl_Role> tbl_Role { get; set; }
        public virtual DbSet<tbl_SendNotiEmail> tbl_SendNotiEmail { get; set; }
        public virtual DbSet<tbl_Service> tbl_Service { get; set; }
        public virtual DbSet<tbl_ShippingTypeToWareHouse> tbl_ShippingTypeToWareHouse { get; set; }
        public virtual DbSet<tbl_ShippingTypeVN> tbl_ShippingTypeVN { get; set; }
        public virtual DbSet<tbl_SmallPackage> tbl_SmallPackage { get; set; }
        public virtual DbSet<tbl_SmallPackage1> tbl_SmallPackage1 { get; set; }
        public virtual DbSet<tbl_SmsForward> tbl_SmsForward { get; set; }
        public virtual DbSet<tbl_SocialSupport> tbl_SocialSupport { get; set; }
        public virtual DbSet<tbl_StaffIncome> tbl_StaffIncome { get; set; }
        public virtual DbSet<tbl_Step> tbl_Step { get; set; }
        public virtual DbSet<tbl_Support> tbl_Support { get; set; }
        public virtual DbSet<tbl_SupportBuyProduct> tbl_SupportBuyProduct { get; set; }
        public virtual DbSet<tbl_TokenForgotPass> tbl_TokenForgotPass { get; set; }
        public virtual DbSet<tbl_TransportaionOrderDetail> tbl_TransportaionOrderDetail { get; set; }
        public virtual DbSet<tbl_TransportationOrder> tbl_TransportationOrder { get; set; }
        public virtual DbSet<tbl_TransportationOrderNew> tbl_TransportationOrderNew { get; set; }
        public virtual DbSet<tbl_UserLevel> tbl_UserLevel { get; set; }
        public virtual DbSet<tbl_Warehouse> tbl_Warehouse { get; set; }
        public virtual DbSet<tbl_WarehouseFee> tbl_WarehouseFee { get; set; }
        public virtual DbSet<tbl_WarehouseFrom> tbl_WarehouseFrom { get; set; }
        public virtual DbSet<tbl_WebChina> tbl_WebChina { get; set; }
        public virtual DbSet<tbl_Weight> tbl_Weight { get; set; }
        public virtual DbSet<tbl_Withdraw> tbl_Withdraw { get; set; }
        public virtual DbSet<View_OrderDetailExcel> View_OrderDetailExcel { get; set; }
        public virtual DbSet<View_OrderList> View_OrderList { get; set; }
        public virtual DbSet<View_OrderListDamuahang> View_OrderListDamuahang { get; set; }
        public virtual DbSet<View_OrderListFilter> View_OrderListFilter { get; set; }
        public virtual DbSet<View_OrderListFilterWithStatusString> View_OrderListFilterWithStatusString { get; set; }
        public virtual DbSet<View_OrderListFilterYCGiao> View_OrderListFilterYCGiao { get; set; }
        public virtual DbSet<View_OrderListKhoTQ> View_OrderListKhoTQ { get; set; }
        public virtual DbSet<View_OrderListKhoVN> View_OrderListKhoVN { get; set; }
        public virtual DbSet<View_Orderlistwithstatus> View_Orderlistwithstatus { get; set; }
        public virtual DbSet<View_SaleAdminSendUserWallet_List> View_SaleAdminSendUserWallet_List { get; set; }
        public virtual DbSet<View_SaleWithdraw_List> View_SaleWithdraw_List { get; set; }
        public virtual DbSet<View_StaffCustomer> View_StaffCustomer { get; set; }
        public virtual DbSet<View_UserList> View_UserList { get; set; }
        public virtual DbSet<View_UserListExcel> View_UserListExcel { get; set; }
        public virtual DbSet<View_UserListWithWallet> View_UserListWithWallet { get; set; }
        public virtual DbSet<tbl_Order> tbl_Order { get; set; }
    
        public virtual ObjectResult<GetAllByOrderShopTempIDAndUID_Result> GetAllByOrderShopTempIDAndUID(Nullable<int> uID, Nullable<int> shopID)
        {
            var uIDParameter = uID.HasValue ?
                new ObjectParameter("UID", uID) :
                new ObjectParameter("UID", typeof(int));
    
            var shopIDParameter = shopID.HasValue ?
                new ObjectParameter("ShopID", shopID) :
                new ObjectParameter("ShopID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllByOrderShopTempIDAndUID_Result>("GetAllByOrderShopTempIDAndUID", uIDParameter, shopIDParameter);
        }
    
        public virtual ObjectResult<getTotalCartByUID_Result> getTotalCartByUID(Nullable<int> uID)
        {
            var uIDParameter = uID.HasValue ?
                new ObjectParameter("UID", uID) :
                new ObjectParameter("UID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getTotalCartByUID_Result>("getTotalCartByUID", uIDParameter);
        }
    
        public virtual ObjectResult<LoadOrderList_Result> LoadOrderList(Nullable<int> oderType, string txtSearch, Nullable<int> typeSearch, Nullable<double> giatu, Nullable<double> giaden, string startDate, string endDate, string ngayPhathangTu, string ngayPhathangden, Nullable<int> status, Nullable<bool> hasSmallpackage, Nullable<int> roleID, Nullable<int> uID, Nullable<int> mainOrderID, Nullable<int> salerID, Nullable<int> datHangID, Nullable<int> pageSize, Nullable<int> pageIndex)
        {
            var oderTypeParameter = oderType.HasValue ?
                new ObjectParameter("oderType", oderType) :
                new ObjectParameter("oderType", typeof(int));
    
            var txtSearchParameter = txtSearch != null ?
                new ObjectParameter("txtSearch", txtSearch) :
                new ObjectParameter("txtSearch", typeof(string));
    
            var typeSearchParameter = typeSearch.HasValue ?
                new ObjectParameter("typeSearch", typeSearch) :
                new ObjectParameter("typeSearch", typeof(int));
    
            var giatuParameter = giatu.HasValue ?
                new ObjectParameter("giatu", giatu) :
                new ObjectParameter("giatu", typeof(double));
    
            var giadenParameter = giaden.HasValue ?
                new ObjectParameter("giaden", giaden) :
                new ObjectParameter("giaden", typeof(double));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            var ngayPhathangTuParameter = ngayPhathangTu != null ?
                new ObjectParameter("ngayPhathangTu", ngayPhathangTu) :
                new ObjectParameter("ngayPhathangTu", typeof(string));
    
            var ngayPhathangdenParameter = ngayPhathangden != null ?
                new ObjectParameter("ngayPhathangden", ngayPhathangden) :
                new ObjectParameter("ngayPhathangden", typeof(string));
    
            var statusParameter = status.HasValue ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(int));
    
            var hasSmallpackageParameter = hasSmallpackage.HasValue ?
                new ObjectParameter("hasSmallpackage", hasSmallpackage) :
                new ObjectParameter("hasSmallpackage", typeof(bool));
    
            var roleIDParameter = roleID.HasValue ?
                new ObjectParameter("roleID", roleID) :
                new ObjectParameter("roleID", typeof(int));
    
            var uIDParameter = uID.HasValue ?
                new ObjectParameter("UID", uID) :
                new ObjectParameter("UID", typeof(int));
    
            var mainOrderIDParameter = mainOrderID.HasValue ?
                new ObjectParameter("MainOrderID", mainOrderID) :
                new ObjectParameter("MainOrderID", typeof(int));
    
            var salerIDParameter = salerID.HasValue ?
                new ObjectParameter("SalerID", salerID) :
                new ObjectParameter("SalerID", typeof(int));
    
            var datHangIDParameter = datHangID.HasValue ?
                new ObjectParameter("DatHangID", datHangID) :
                new ObjectParameter("DatHangID", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LoadOrderList_Result>("LoadOrderList", oderTypeParameter, txtSearchParameter, typeSearchParameter, giatuParameter, giadenParameter, startDateParameter, endDateParameter, ngayPhathangTuParameter, ngayPhathangdenParameter, statusParameter, hasSmallpackageParameter, roleIDParameter, uIDParameter, mainOrderIDParameter, salerIDParameter, datHangIDParameter, pageSizeParameter, pageIndexParameter);
        }
    
        public virtual ObjectResult<LoadlistRevenueByDate_Result> LoadlistRevenueByDate(Nullable<int> minDate, Nullable<int> maxDate, string txtSearch, string startDate, string endDate, Nullable<int> salerID, Nullable<int> datHangID, Nullable<int> pageSize, Nullable<int> pageIndex)
        {
            var minDateParameter = minDate.HasValue ?
                new ObjectParameter("minDate", minDate) :
                new ObjectParameter("minDate", typeof(int));
    
            var maxDateParameter = maxDate.HasValue ?
                new ObjectParameter("maxDate", maxDate) :
                new ObjectParameter("maxDate", typeof(int));
    
            var txtSearchParameter = txtSearch != null ?
                new ObjectParameter("txtSearch", txtSearch) :
                new ObjectParameter("txtSearch", typeof(string));
    
            var startDateParameter = startDate != null ?
                new ObjectParameter("startDate", startDate) :
                new ObjectParameter("startDate", typeof(string));
    
            var endDateParameter = endDate != null ?
                new ObjectParameter("endDate", endDate) :
                new ObjectParameter("endDate", typeof(string));
    
            var salerIDParameter = salerID.HasValue ?
                new ObjectParameter("SalerID", salerID) :
                new ObjectParameter("SalerID", typeof(int));
    
            var datHangIDParameter = datHangID.HasValue ?
                new ObjectParameter("DatHangID", datHangID) :
                new ObjectParameter("DatHangID", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<LoadlistRevenueByDate_Result>("LoadlistRevenueByDate", minDateParameter, maxDateParameter, txtSearchParameter, startDateParameter, endDateParameter, salerIDParameter, datHangIDParameter, pageSizeParameter, pageIndexParameter);
        }
    }
}
