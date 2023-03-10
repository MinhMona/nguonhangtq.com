﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NHST.Models;
using NHST.Bussiness;
using NHST.Controllers;
using Telerik.Web.UI;
using MB.Extensions;

namespace NHST.manager
{
    public partial class ProductDetails : System.Web.UI.Page
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
                    if (ac.RoleID != 0)
                        Response.Redirect("/trang-chu");
                }
                LoadData();

            }
        }

        public void LoadData()
        {
            var id = Request.QueryString["i"].ToInt(0);
            if (id > 0)
            {
                var news = ProductController.GetByID(id);
                if (news != null)
                {
                    ViewState["NID"] = id;
                    txtWebName.Text = news.WebName;
                    txtProductname.Text = news.Productname;
                    txtWebLink.Text = news.WebLink;
                    if (news.ProductIMG != null)
                        imgDaiDien.ImageUrl = news.ProductIMG;
                    chkIsHidden.Checked = Convert.ToBoolean(news.IsHidden);
                    chkIshot.Checked = Convert.ToBoolean(news.IsHot);
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string Username = Session["userLoginSystem"].ToString();

            int ID = ViewState["NID"].ToString().ToInt(0);
            string IMG = "";
            string KhieuNaiIMG = "/Uploads/ProductIMG/";
            string BackLink = "/manager/ProductList.aspx";
            if (pIcon.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in pIcon.UploadedFiles)
                {
                    var o = KhieuNaiIMG + Guid.NewGuid() + f.GetExtension();
                    try
                    {
                        f.SaveAs(Server.MapPath(o));
                        IMG = o;
                    }
                    catch { }
                }
            }
            else
                IMG = imgDaiDien.ImageUrl;
            string kq = ProductController.Update(ID, txtWebName.Text, IMG, txtProductname.Text, txtWebLink.Text, chkIshot.Checked, chkIsHidden.Checked,
                DateTime.Now, Username);
            if (Convert.ToInt32(kq) > 0)
            {
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thành công.", "s", true, BackLink, Page);
            }
            else
            {
                PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình Cập nhật. Vui lòng thử lại.", "e", true, Page);
            }
        }
    }
}