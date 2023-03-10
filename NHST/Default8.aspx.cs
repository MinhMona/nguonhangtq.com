using NHST.Bussiness;
using NHST.Controllers;
using Supremes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Default7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
        public void LoadData()
        {
            var confi = ConfigurationController.GetByTop1();
            if (confi != null)
            {
                string email = confi.EmailSupport;
                string hotline = confi.Hotline;
              
                ltrTimework.Text = confi.TimeWork;
                ltrEmail.Text += "<p><a href=\"mailto:" + email + "\">" + email + "</a></p>";
                ltrHotline.Text += "<p><a href=\"tel:" + hotline+ "\">" + hotline + "</a></p>";

                //ltrTopLeft.Text += "<p>Tỷ giá ¥: <span>" + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></p>"
                //                + "  <a href=\"\"><i class=\"fas fa-phone\"></i>" + hotline + "</a>"
                //                + "  <p>(Thời gian làm việc: " + confi.TimeWork + ")</p>";
                //ltrWebname.Text = confi.Websitename;
                //ltrHotline2.Text = "<a href=\"tel:" + hotline + "\">" + hotline + "</a>";
                //ltrEmail2.Text = "<a href=\"mailto:" + email + "\">" + email + "</a>";
                //ltrAddress.Text = confi.Address; ;
                //ltrSocial.Text += "<div class=\"social__item fb\"><a href=\"" + confi.Facebook + "\" target=\"_blank\"><i class=\"fab fa-facebook-f\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"social__item tw\"><a href=\"" + confi.Twitter + "\" target=\"_blank\"><i class=\"fab fa-twitter\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"social__item ins\"><a href=\"" + confi.Instagram + "\" target=\"_blank\"><i class=\"fab fa-instagram\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"social__item sky\"><a href=\"" + confi.Skype + "\" target=\"_blank\"><i class=\"fab fa-skype\"></i></a></div>";

                //ltrConfig.Text += "<p class=\"info\">Tỷ giá: <span class=\"hl-txt\"> " + string.Format("{0:N0}", Convert.ToDouble(confi.Currency)) + "</span></p>";
                //ltrConfig.Text += "<p class=\"info\"><i class=\"fas fa fa-clock-o\"></i> Giờ làm việc: " + confi.TimeWork + "</p>";
                //ltrConfig.Text += "<a href=\"mailto:" + email + "\" class=\"info\"><i class=\"fas fa-fw fa-envelope\"></i> Email: " + email + "</a>";
                //ltrConfig.Text += "<a href=\"tel:" + hotline + "\" class=\"info\"><i class=\"fa fa-phone-square\"></i> " + hotline + "</a>";

                //ltrSocial.Text += "<div class=\"icon-fb\"><a href=\"" + confi.Facebook + "\" target=\"_blank\"><i class=\"fab fa-fw fa-facebook-f\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-tw\"><a href=\"" + confi.Twitter + "\" target=\"_blank\"><i class=\"fab fa-fw fa-twitter\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-gg\"><a href=\"" + confi.GooglePlus + "\" target=\"_blank\"><i class=\"fab fa-fw fa-google-plus-g\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-pi\"><a href=\"" + confi.Pinterest + "\" target=\"_blank\"><i class=\"fab fa-fw fa-pinterest-p\"></i></a></div>";
                //ltrSocial.Text += "<div class=\"icon-ib\"><a href=\"mailto:" + email + "\"><i class=\"fas fa-fw fa-envelope\"></i></a></div>";               
            }
        }

    }
}