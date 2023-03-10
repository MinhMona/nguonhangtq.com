using MB.Extensions;
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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace NHST
{
    public partial class Default11 : System.Web.UI.Page
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
                string timework = confi.TimeWork;
                ltrAddress.Text = confi.Address;
                ltrhotline.Text = "<a href=\"tel:" + hotline + "\">" + hotline + "</a>";
                ltrTimework.Text = timework;
                ltrEmail.Text = "<a href=\"mailto:" + email + "\">" + email + "</a>";
            }
            #region Lấy thông tin trang chủ
            ///Dịch vụ
            ///
            var services = ServiceController.GetAll();
            if (services.Count > 0)
            {
                foreach (var s in services)
                {
                    ltrService.Text += "";
                    ltrService.Text += "<li class=\"serv__item\">";
                    ltrService.Text += "    <div class=\"serv-card\">";
                    ltrService.Text += "        <div class=\"img\"><img src=\"" + s.ServiceIMG + "\" alt=\"\"></div>";
                    ltrService.Text += "        <h4 class=\"hd\">" + s.ServiceName + "</h4>";
                    ltrService.Text += "        <p>" + s.ServiceContent + "</p>";
                    ltrService.Text += "    </div>";
                    ltrService.Text += "</li>";
                }
            }

            ///Quy trình
            ///
            var steps = StepController.GetAll("");
            if (steps.Count > 0)
            {
                int count = 0;
                foreach (var s in steps)
                {
                    string name = s.StepName;
                    string namenotdau = LeoUtils.ConvertToUnSign(name);
                    ltrStep1.Text += "";
                    if (count == 0)
                        ltrStep1.Text += "<li class=\"step__item active\" step-nav=\"#step-" + namenotdau + "\">";
                    else
                        ltrStep1.Text += "<li class=\"step__item\" step-nav=\"#step-" + namenotdau + "\">";

                    ltrStep1.Text += "<div class=\"step-block\">";
                    ltrStep1.Text += "<div class=\"icon\"><img src=\"" + s.StepIMG + "\" alt=\"\"></div>";
                    ltrStep1.Text += "<span class=\"check\"><span class=\"check-box\"></span></span>";
                    ltrStep1.Text += "<div class=\"hd\">" + name + "</div>";
                    ltrStep1.Text += "</div>";
                    ltrStep1.Text += "</li>";


                    ltrStep2.Text += "<div id=\"step-" + namenotdau + "\">";
                    ltrStep2.Text += "  <div class=\"stepct-block\">";
                    ltrStep2.Text += "      <div class=\"img\">";
                    ltrStep2.Text += "          <img src=\"/App_Themes/TruongThanh/images/step-img.png\" alt=\"\">";
                    ltrStep2.Text += "      </div>";
                    ltrStep2.Text += "      <div class=\"detail\">";
                    ltrStep2.Text += "          <h3 class=\"hd\">" + name + "</h3>";
                    ltrStep2.Text += "          <p>" + s.StepContent + "</p>";
                    if (!string.IsNullOrEmpty(s.StepLink))
                        ltrStep2.Text += "          <div class=\"button\"><a href=\"" + s.StepLink + "\" class=\"mn-btn btn-2\">" + name + "</a></div>";
                    ltrStep2.Text += "      </div>";
                    ltrStep2.Text += "  </div>";
                    ltrStep2.Text += "</div>";
                    count++;
                }
            }

            ///Quyền lợi khách hàng
            ///
            var ql = CustomerBenefitsController.GetAll("");
            if (ql.Count > 0)
            {
                var ql1 = ql.Where(q => q.Position < 4).ToList();
                var ql2 = ql.Where(q => q.Position > 3).ToList();
                if (ql1.Count > 0)
                {
                    foreach (var q in ql1)
                    {
                        ltrQL1.Text += "<li class=\"righ-ensu__item\">";
                        ltrQL1.Text += "<div class=\"left-icon-card\">";
                        ltrQL1.Text += "<div class=\"img\"><img src=\"" + q.Icon + "\" alt=\"\"></div>";
                        ltrQL1.Text += "<div class=\"ct\">";
                        ltrQL1.Text += "<h4 class=\"hd\">" + q.CustomerBenefitName + "</h4>";
                        ltrQL1.Text += "<p>" + q.CustomerBenefitDescription + "</p>";
                        ltrQL1.Text += "</div>";
                        ltrQL1.Text += "</div>";
                        ltrQL1.Text += "</li>";
                    }
                }
                if (ql2.Count > 0)
                {
                    foreach (var q in ql2)
                    {
                        ltrQL2.Text += "<li class=\"righ-ensu__item\">";
                        ltrQL2.Text += "<div class=\"left-icon-card\">";
                        ltrQL2.Text += "<div class=\"img\"><img src=\"" + q.Icon + "\" alt=\"\"></div>";
                        ltrQL2.Text += "<div class=\"ct\">";
                        ltrQL2.Text += "<h4 class=\"hd\">" + q.CustomerBenefitName + "</h4>";
                        ltrQL2.Text += "<p>" + q.CustomerBenefitDescription + "</p>";
                        ltrQL2.Text += "</div>";
                        ltrQL2.Text += "</div>";
                        ltrQL2.Text += "</li>";
                    }
                }
            }
            #endregion
        }
        protected void btnsearchpro_Click(object sender, EventArgs e)
        {
            string text = txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                string a = PJUtils.TranslateText(text, "vi|zh");
                string page = ddlWebsearch.SelectedValue;
                SearchPage(page, PJUtils.RemoveHTMLTags(a));
            }
        }
        #region Translate And Run
        public string TranslateText(string input, string languagePair)
        {
            try
            {
                string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36";
                request.Method = "GET";
                var content = String.Empty;
                HttpStatusCode statusCode;
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    var contentType = response.ContentType;
                    Encoding encoding = null;
                    if (contentType != null)
                    {
                        var match = Regex.Match(contentType, @"(?<=charset\=).*");
                        if (match.Success)
                            encoding = Encoding.GetEncoding(match.ToString());
                    }

                    encoding = encoding ?? Encoding.UTF8;

                    statusCode = ((HttpWebResponse)response).StatusCode;
                    using (var reader = new StreamReader(stream, encoding))
                        content = reader.ReadToEnd();
                }
                var doc = Dcsoup.Parse(content);
                var scoreDiv = doc.Select("html").Select("span[id=result_box]").Html;
                return scoreDiv;
            }
            catch
            {
                return "";
            }

        }

        public void SearchPage(string page, string text)
        {
            string linkgo = "";
            if (page == "tmall")
            {
                string a = text;
                string textsearch_tmall = GetHashString(a);
                //string fullLinkSearch_tmall = "https://list.tmall.com/search_product.htm?q=" + textsearch_tmall + "&type=p&vmarket=&spm=875.7931836%2FB.a2227oh.d100&from=mallfp..pc_1_searchbutton";
                linkgo = "https://list.tmall.com/search_product.htm?q=" + textsearch_tmall + "&type=p&vmarket=&spm=875.7931836%2FB.a2227oh.d100&from=mallfp..pc_1_searchbutton";
            }
            else if (page == "taobao")
            {
                string a = text;
                string textsearch_taobao = GetHashString(a);
                //string fullLinkSearch_taobao = "https://world.taobao.com/search/search.htm?q=" + textsearch_taobao + "&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1";
                linkgo = "https://world.taobao.com/search/search.htm?q=" + textsearch_taobao + "&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1";
                //https://world.taobao.com/search/search.htm?q=%B9%AB%BC%A6&navigator=all&_input_charset=&spm=a21bp.7806943.20151106.1
            }
            else if (page == "1688")
            {
                string a = text;
                string textsearch_1688 = GetHashString(a);
                //string fullLinkSearch_1688 = "https://s.1688.com/selloffer/offer_search.htm?keywords=" + textsearch_1688 + "&button_click=top&earseDirect=false&n=y";
                linkgo = "https://s.1688.com/selloffer/offer_search.htm?keywords=" + textsearch_1688 + "&button_click=top&earseDirect=false&n=y";
            }
            Response.Redirect(linkgo);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "redirect('" + linkgo + "')", true);
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "redirect('" + linkgo + "');", true);
        }
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            byte[] bytes = Encoding.GetEncoding("gb2312").GetBytes(inputString);
            return bytes;
        }
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append("%" + b.ToString("X2"));

            return sb.ToString();
        }
        #endregion
        [WebMethod]
        public static string getPopup()
        {
            if (HttpContext.Current.Session["notshowpopup"] == null)
            {
                var conf = ConfigurationController.GetByTop1();
                string popup = conf.NotiPopup;
                if (!string.IsNullOrEmpty(popup))
                {
                    NotiInfo n = new NotiInfo();
                    n.NotiTitle = conf.NotiPopupTitle;
                    n.NotiEmail = conf.NotiPopupEmail;
                    n.NotiContent = conf.NotiPopup;
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(n);
                }
                else
                    return "null";
            }
            else
                return null;

        }
        [WebMethod]
        public static void setNotshow()
        {
            HttpContext.Current.Session["notshowpopup"] = "1";
        }
        public class NotiInfo
        {
            public string NotiTitle { get; set; }
            public string NotiContent { get; set; }
            public string NotiEmail { get; set; }
        }
    }
}