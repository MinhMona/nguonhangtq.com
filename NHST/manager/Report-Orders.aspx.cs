﻿using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ZLADIPJ.Business;

namespace NHST.manager
{
    public partial class Report_Orders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/manager/Login.aspx");
                }
                else
                {
                    string Username = Session["userLoginSystem"].ToString();
                    var obj_user = AccountController.GetByUsername(Username);
                    if (obj_user != null)
                    {
                        if (obj_user.RoleID != 0 && obj_user.RoleID != 2 && obj_user.RoleID != 7)
                        {
                            Response.Redirect("/trang-chu");
                        }
                    }
                }
                LoadData();
            }
        }
        public void LoadData()
        {
            rdatefrom.SelectedDate = DateTime.Now;
            rdateto.SelectedDate = DateTime.Now.AddDays(30);
        }
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            var acc = Session["userLoginSystem"].ToString();
            var ListOrder = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
            //ltrHistory.Text = "";

            if (ListOrder.Count > 0)
            {
                List<ReportOrder> ro = new List<ReportOrder>();
                double NotDeposit = 0;
                double OrderDeposit = 0;
                double Waiting = 0;
                double OrderChecked = 0;
                double OrderIsOrder = 0;
                double OrderTQ = 0;
                double OrderVN = 0;
                double OrderWaitingPayment = 0;
                double OrderSuccess = 0;
                double OrderCancel = 0;
                double OrderTotal = ListOrder.Count;

                foreach (var o in ListOrder)
                {
                    double or_TotalPriceVND = o.TotalPriceVND.ToFloat();
                    double or_Deposit = o.Deposit.ToFloat();
                    double or_IsFastPrice = o.IsFastPrice.ToFloat(0);
                    double or_FeeShipCN = o.FeeShipCN.ToFloat(0);
                    double or_FeeBuyPro = o.FeeBuyPro.ToFloat(0);
                    double or_FeeWeight = o.FeeWeight.ToFloat(0);
                    double or_IsCheckProductPrice = o.IsCheckProductPrice.ToFloat(0);
                    double or_IsPackedPrice = o.IsPackedPrice.ToFloat(0);
                    double or_IsFastDeliveryPrice = o.IsFastDeliveryPrice.ToFloat(0);
                    double currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;

                    int stt = Convert.ToInt32(o.Status);
                    if (stt == 0)
                    {
                        NotDeposit += 1;
                    }
                    else if (stt == 1)
                    {
                        OrderCancel += 1;
                    }
                    else if (stt == 2)
                    {
                        OrderDeposit += 1;
                    }
                    else if (stt == 3)
                    {
                        Waiting += 1;
                    }
                    else if (stt == 4)
                    {
                        OrderChecked += 1;
                    }
                    else if (stt == 5)
                    {
                        OrderIsOrder += 1;
                    }
                    else if (stt == 6)
                    {
                        OrderTQ += 1;
                    }
                    else if (stt == 7)
                    {
                        OrderVN += 1;
                    }
                    else if (stt == 8)
                    {
                        OrderWaitingPayment += 1;
                    }
                    else
                    {
                        OrderSuccess += 1;
                    }


                    ReportOrder r = new ReportOrder();
                    r.OrderID = o.ID;
                    r.ShopID = o.ShopID;
                    r.ShopName = o.ShopName;
                    r.FullName = o.FullName;
                    r.Email = o.Email;
                    r.Phone = o.Phone;
                    r.ShipCN = string.Format("{0:N0}", or_FeeShipCN);
                    r.BuyPro = string.Format("{0:N0}", or_FeeBuyPro);
                    r.FeeWeight = string.Format("{0:N0}", or_FeeWeight);
                    r.ShipHome = string.Format("{0:N0}", or_IsFastDeliveryPrice);
                    r.CheckProduct = string.Format("{0:N0}", or_IsCheckProductPrice);
                    r.Package = string.Format("{0:N0}", or_IsPackedPrice);
                    r.IsFast = string.Format("{0:N0}", or_IsFastPrice);
                    r.Total = string.Format("{0:N0}", or_TotalPriceVND);
                    r.Deposit = string.Format("{0:N0}", or_Deposit);
                    r.PayLeft = string.Format("{0:N0}", currentOrderPriceLeft);
                    r.Status = PJUtils.IntToRequestAdmin(stt);
                    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                    ro.Add(r);
                }
                lblNotDeposit.Text = string.Format("{0:N0}", NotDeposit);
                lblOrderDeposit.Text = string.Format("{0:N0}", OrderDeposit);
                lblWaiting.Text = string.Format("{0:N0}", Waiting);
                lblOrderChecked.Text = string.Format("{0:N0}", OrderChecked);
                lblOrderIsOrder.Text = string.Format("{0:N0}", OrderIsOrder);
                lblOrderTQ.Text = string.Format("{0:N0}", OrderTQ);
                lblOrderVN.Text = string.Format("{0:N0}", OrderVN);
                lblOrderWaitingPayment.Text = string.Format("{0:N0}", OrderWaitingPayment);
                lblOrderSuccess.Text = string.Format("{0:N0}", OrderSuccess);
                lblOrderCancel.Text = string.Format("{0:N0}", OrderCancel);
                lblOrderTotal.Text = string.Format("{0:N0}", OrderTotal);

                pninfo.Visible = true;
                gr.DataSource = ro;
                gr.DataBind();
            }
        }
        public void LoadGrid()
        {
            var ListOrder = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
            //ltrHistory.Text = "";

            if (ListOrder.Count > 0)
            {
                List<ReportOrder> ro_gr = new List<ReportOrder>();
                foreach (var o in ListOrder)
                {
                    double or_TotalPriceVND = o.TotalPriceVND.ToFloat(0);
                    double or_Deposit = o.Deposit.ToFloat(0);
                    double or_IsFastPrice = o.IsFastPrice.ToFloat(0);
                    double or_FeeShipCN = o.FeeShipCN.ToFloat(0);
                    double or_FeeBuyPro = o.FeeBuyPro.ToFloat(0);
                    double or_FeeWeight = o.FeeWeight.ToFloat(0);
                    double or_IsCheckProductPrice = o.IsCheckProductPrice.ToFloat(0);
                    double or_IsPackedPrice = o.IsPackedPrice.ToFloat(0);
                    double or_IsFastDeliveryPrice = o.IsFastDeliveryPrice.ToFloat(0);
                    double currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;



                    int stt = Convert.ToInt32(o.Status);

                    ReportOrder r = new ReportOrder();
                    r.OrderID = o.ID;
                    r.ShopID = o.ShopID;
                    r.ShopName = o.ShopName;
                    r.FullName = o.FullName;
                    r.Email = o.Email;
                    r.Phone = o.Phone;
                    r.ShipCN = string.Format("{0:N0}", or_FeeShipCN);
                    r.BuyPro = string.Format("{0:N0}", or_FeeBuyPro);
                    r.FeeWeight = string.Format("{0:N0}", or_FeeWeight);
                    r.ShipHome = string.Format("{0:N0}", or_IsFastDeliveryPrice);
                    r.CheckProduct = string.Format("{0:N0}", or_IsCheckProductPrice);
                    r.Package = string.Format("{0:N0}", or_IsPackedPrice);
                    r.IsFast = string.Format("{0:N0}", or_IsFastPrice);
                    r.Total = string.Format("{0:N0}", or_TotalPriceVND);
                    r.Deposit = string.Format("{0:N0}", or_Deposit);
                    r.PayLeft = string.Format("{0:N0}", currentOrderPriceLeft);
                    r.Status = PJUtils.IntToRequestAdmin(stt);
                    r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                    ro_gr.Add(r);
                }

                gr.DataSource = ro_gr;
                //gr.DataBind();
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LoadGrid();
        }

        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            LoadGrid();
            gr.Rebind();
        }
        protected void gr_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            LoadGrid();
            gr.Rebind();
        }
        #endregion


        public class ReportOrder
        {
            public int OrderID { get; set; }
            public string ShopID { get; set; }
            public string ShopName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string ShipCN { get; set; }
            public string BuyPro { get; set; }
            public string FeeWeight { get; set; }
            public string ShipHome { get; set; }
            public string CheckProduct { get; set; }
            public string Package { get; set; }
            public string IsFast { get; set; }
            public string Total { get; set; }
            public string Deposit { get; set; }
            public string PayLeft { get; set; }
            public string Status { get; set; }
            public string CreatedDate { get; set; }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string Username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(Username);
            if (obj_user.RoleID == 0)
            {
                var ListOrder = MainOrderController.GetFromDateToDate(Convert.ToDateTime(rdatefrom.SelectedDate), Convert.ToDateTime(rdateto.SelectedDate));
                //ltrHistory.Text = "";

                if (ListOrder.Count > 0)
                {
                    List<ReportOrder> ro_gr = new List<ReportOrder>();
                    foreach (var o in ListOrder)
                    {
                        double or_TotalPriceVND = o.TotalPriceVND.ToFloat(0);
                        double or_Deposit = o.Deposit.ToFloat(0);
                        double or_IsFastPrice = o.IsFastPrice.ToFloat(0);
                        double or_FeeShipCN = o.FeeShipCN.ToFloat(0);
                        double or_FeeBuyPro = o.FeeBuyPro.ToFloat(0);
                        double or_FeeWeight = o.FeeWeight.ToFloat(0);
                        double or_IsCheckProductPrice = o.IsCheckProductPrice.ToFloat(0);
                        double or_IsPackedPrice = o.IsPackedPrice.ToFloat(0);
                        double or_IsFastDeliveryPrice = o.IsFastDeliveryPrice.ToFloat(0);
                        double currentOrderPriceLeft = or_TotalPriceVND - or_Deposit;



                        int stt = Convert.ToInt32(o.Status);

                        ReportOrder r = new ReportOrder();
                        r.OrderID = o.ID;
                        r.ShopID = o.ShopID;
                        r.ShopName = o.ShopName;
                        r.FullName = o.FullName;
                        r.Email = o.Email;
                        r.Phone = o.Phone;
                        r.ShipCN = string.Format("{0:N0}", or_FeeShipCN);
                        r.BuyPro = string.Format("{0:N0}", or_FeeBuyPro);
                        r.FeeWeight = string.Format("{0:N0}", or_FeeWeight);
                        r.ShipHome = string.Format("{0:N0}", or_IsFastDeliveryPrice);
                        r.CheckProduct = string.Format("{0:N0}", or_IsCheckProductPrice);
                        r.Package = string.Format("{0:N0}", or_IsPackedPrice);
                        r.IsFast = string.Format("{0:N0}", or_IsFastPrice);
                        r.Total = string.Format("{0:N0}", or_TotalPriceVND);
                        r.Deposit = string.Format("{0:N0}", or_Deposit);
                        r.PayLeft = string.Format("{0:N0}", currentOrderPriceLeft);
                        r.Status = PJUtils.IntToRequestAdmin(stt);
                        r.CreatedDate = string.Format("{0:dd/MM/yyyy}", o.CreatedDate);
                        ro_gr.Add(r);
                    }

                    StringBuilder StrExport = new StringBuilder();
                    StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                    StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                    StrExport.Append("<DIV  style='font-size:12px;'>");
                    StrExport.Append("<table border=\"1\">");
                    StrExport.Append("  <tr>");
                    StrExport.Append("      <th><strong>Mã đơn hàng</strong></th>");
                    StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>ShopID</strong></th>");
                    StrExport.Append("      <th><strong>Tổng tiền</strong></th>");
                    StrExport.Append("      <th><strong>Đặt cọc</strong></th>");
                    StrExport.Append("      <th style=\"mso-number-format:'\\@'\" ><strong>Còn lại</strong></th>");
                    StrExport.Append("      <th><strong>Trạng thái</strong></th>");
                    StrExport.Append("      <th><strong>Ngày tạo</strong></th>");
                    StrExport.Append("  </tr>");
                    foreach (var item in ro_gr)
                    {
                        StrExport.Append("  <tr>");
                        StrExport.Append("      <td>" + item.OrderID + "</td>");
                        StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.ShopID + "</td>");
                        StrExport.Append("      <td>" + item.Total + "</td>");
                        StrExport.Append("      <td>" + item.Deposit + "</td>");
                        StrExport.Append("      <td style=\"mso-number-format:'\\@'\" >" + item.PayLeft + "</td>");
                        StrExport.Append("      <td>" + item.Status + "</td>");
                        StrExport.Append("      <td>" + item.CreatedDate + "</td>");
                        StrExport.Append("  </tr>");
                    }
                    StrExport.Append("</table>");
                    StrExport.Append("</div></body></html>");
                    string strFile = "bao-cao-don-hang-" + string.Format("{0:dd/MM/yyyy}", rdatefrom.SelectedDate) + "-" + string.Format("{0:dd/MM/yyyy}", rdateto.SelectedDate) + ".xls";
                    string strcontentType = "application/vnd.ms-excel";
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.BufferOutput = true;
                    Response.ContentType = strcontentType;
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                    Response.Write(StrExport.ToString());
                    Response.Flush();
                    //Response.Close();
                    Response.End();


                    //gr.DataSource = ro_gr;
                    //gr.DataBind();
                }
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Bạn không có quyền xuất file excel!", "e", false, Page);
            }
               
        }
    }
}