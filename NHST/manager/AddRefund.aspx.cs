﻿using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MB.Extensions;

namespace NHST.manager
{
    public partial class AddRefund : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userLoginSystem"] == null)
                {
                    Response.Redirect("/trang-chu");
                }
                else
                {
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac != null)
                    {
                        if (ac.RoleID == 1 || ac.RoleID == 3)
                            Response.Redirect("/trang-chu");
                    }
                    loaddata();
                }
            }
        }
        public void loaddata()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                string username_current = Session["userLoginSystem"].ToString();
                tbl_Account ac = AccountController.GetByUsername(username_current);
                int role = ac.RoleID.ToString().ToInt();


                ViewState["UID"] = id;
                var a = AccountController.GetByID(id);
                if (a != null)
                {
                    lblUsername.Text = a.Username;
                    txtNote.Text = a.Username + " đã được nạp tiền vào tài khoản.";
                }
                else
                {
                    Response.Redirect("/manager/userlist.aspx");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            string uReceive = lblUsername.Text.Trim().ToLower();
            var admin = AccountController.GetByUsername(username);
            var u = AccountController.GetByUsername(uReceive);
            string content = txtNote.Text;
            double money = Convert.ToDouble(pAmount.Value);
            DateTime currentdate = DateTime.Now;
            if (u != null)
            {
                int UID = u.ID;
                if (money > 0)
                {
                    int status = ddlStatus.SelectedValue.ToInt(0);
                    string kq = RefundController.Insert(UID, u.Username, Convert.ToDouble(pAmount.Value), txtNote.Text, status, DateTime.Now, username);
                    if (kq.ToInt(0) > 0)
                    {
                        if (status == 2)
                        {
                            double walletCYN = Convert.ToDouble(u.WalletCYN);
                            walletCYN = walletCYN + money;
                            AccountController.updateWalletCYN(u.ID, walletCYN);
                            HistoryPayWalletCYNController.Insert(u.ID, u.Username, money, walletCYN, 2, 2,
                                u.Username + " đã được hoàn lại tiền mua hộ vào tài khoản.", currentdate, username);
                        }
                        PJUtils.ShowMessageBoxSwAlert("Tạo lệnh hoàn tiền thành công", "s", true, Page);
                    }
                }

            }
            else
            {
                lbl_check.Text = "Tên đăng nhập không tồn tại.";
                lbl_check.Visible = true;
            }
        }
    }
}