﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using ZLADIPJ.Business;
using Telerik.Web.UI;
using System.Data;
using System.Text;
namespace NHST
{
    public partial class kien_la : System.Web.UI.Page
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

                    if (ac.RoleID != 0 && ac.RoleID != 4 && ac.RoleID != 5 && ac.RoleID != 8 && ac.RoleID != 1)
                    {

                    }
                    else
                    {
                        //ltraddminre.Text = "<a type=\"button\" class=\"btn btn-success m-b-sm\" href=\"/Admin/Add-smallpackage.aspx\">Thêm mã vận đơn</a>";
                    }
                }
            }
        }
        #region grid event
        protected void r_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var la = SmallPackageController.GetAllTroinoi(tSearchName.Text.Trim().ToLower());
            if (la != null)
            {
                if (la.Count > 0)
                {
                    gr.DataSource = la;
                }
            }

        }
        
        protected void r_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var g = e.Item as GridDataItem;
            if (g == null) return;
        }

        protected void gr_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {

        }
        #endregion

        #region button event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gr.Rebind();
        }
        #endregion
    }
}