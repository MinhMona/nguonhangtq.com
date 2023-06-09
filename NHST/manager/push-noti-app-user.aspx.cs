﻿using MB.Extensions;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class push_noti_app_user : System.Web.UI.Page
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
                    string Username = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(Username);
                    if (ac.RoleID == 1)
                    {
                        Response.Redirect("/trang-chu");
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            string username = Session["userLoginSystem"].ToString();
            DateTime currentDate = DateTime.Now;
            string backlink = "/manager/Noti-app-list.aspx";
            var acc = AccountController.GetByUsername(txtUsername.Text.Trim());
            if (acc != null)
            {
                //string Key = "NHTQ-vominhthien";
                var kq = AppPushNotiController.InsertUser(txtTitle.Text, txtMessage.Text, acc.Username, acc.ID, currentDate, username);
                if (kq != null)
                {
                    var l = DeviceTokenController.GetAllByUID(acc.ID);
                    if (l != null)
                    {
                        foreach (var item in l)
                        {
                            string link = "http://nguonhangtq.com/danh-sach-thong-bao-app.aspx?UID=" + acc.ID + "&Key=" + item.UserToken + "";
                            PushAndroidiOS(kq.AppNotiTitle, item.Device, Convert.ToInt32(item.Type), kq.AppNotiMessage, link);

                            //            string NID = NotificationsController.Inser(Convert.ToInt32(acc.ID),
                            //                            AccountController.GetByID(Convert.ToInt32(acc.ID)).Username, 0, kq.AppNotiMessage,
                            //                            15, currentDate, "System", true);

                            //            NotificationSingleController.InsertNoti(NID.ToInt(0), Convert.ToInt32(acc.ID), AccountController.GetByID(Convert.ToInt32(acc.ID)).Username, 0, kq.AppNotiMessage,
                            //1, 15, currentDate, "System");
                        }
                    }
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Thông báo thành công.", "s", true, backlink, Page);
                }
                else
                {
                    PJUtils.ShowMessageBoxSwAlert("Có lỗi trong quá trình tạo thông báo. Vui lòng thử lại.", "e", true, Page);
                }
            }
            else
            {
                lbl_check.Text = "Tên đăng nhập không tồn tại.";
                lbl_check.Visible = true;
            }


        }


        public class SmsResponse
        {
            public string MessageCount { get; set; }
            public List<Message> Messages { get; set; }
        }

        public class Message
        {
            public string To { get; set; }
            public string MessageId { get; set; }
            public string Status { get; set; }
            public string RemainingBalance { get; set; }
            public string MessagePrice { get; set; }
            public string Network { get; set; }
            public string From { get; set; }
        }

        public class FCMResponse
        {
            public long multicast_id { get; set; }
            public int success { get; set; }
            public int failure { get; set; }
            public int canonical_ids { get; set; }
            public List<FCMResult> results { get; set; }
        }
        public class FCMResult
        {
            public string message_id { get; set; }
        }
        protected void PushAndroidiOS(string title, string DeviceToken, int TypeDevice, string Noti, string link)
        {
            try
            {
                string android_firebase_key = "AAAA8v-7c-8:APA91bGO0ze09qR_hTwJmXSJqrfI2g_n9jxU_Xd9fZGfybLI__Z_S6JEHsQB3fr1DqqdHN0hoQAbtsBk1bswu3FgDcuY2eveB-wNpJHkw7vNq74jMwXtsyxponukbjGZcM9uApjnLz_G";
                string SenderIdAndroid = "1043672560623";

                string ios_firebase_key = "AAAA8v-7c-8:APA91bGbMpTlTVJTkAwsm4ivJ9CNVG2h06WFcCNJ66gIocqGoEt3SYU_Fs4_bwd-NNsbsftOA-MuoTr2qdq4CYyG-oN358LzzYkADYorbheSMOzMA1ZMlpBhwDhPRT9-mBPbMGT_ZX0f";
                string SenderIdiOS = "1043672560623";


                var SENDER_ID = "";
                var FirebaseKey = "";

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var objNotificationiOS = new
                {
                    to = DeviceToken,
                    collapse_key = "type_a",
                    notification = new
                    {
                        body = Noti,
                        title = title
                    },
                    data = new
                    {
                        title = title,
                        message = Noti,
                        image = "http://demo.nguonhangtq.com/App_Themes/vominhthien/images/main-logo.png",
                        link = link

                    }
                };
                var objNotificationAndroid = new
                {
                    to = DeviceToken,
                    data = new
                    {
                        title = title,
                        message = Noti,
                        image = "http://demo.nguonhangtq.com/App_Themes/vominhthien/images/main-logo.png",
                        link = link
                    }
                };


                string jsonNotificationFormat = "";
                if (TypeDevice == 1) //1: Android; 2: IOS
                {
                    SENDER_ID = SenderIdAndroid;
                    FirebaseKey = android_firebase_key;
                    jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objNotificationAndroid);
                }
                else
                {
                    SENDER_ID = SenderIdiOS;
                    FirebaseKey = ios_firebase_key;
                    jsonNotificationFormat = Newtonsoft.Json.JsonConvert.SerializeObject(objNotificationiOS);
                }

                Byte[] byteArray = Encoding.UTF8.GetBytes(jsonNotificationFormat);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", FirebaseKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                tRequest.ContentLength = byteArray.Length;
                tRequest.ContentType = "application/json";
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String responseFromFirebaseServer = tReader.ReadToEnd();

                                FCMResponse response = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(responseFromFirebaseServer);
                                if (response.success == 1)
                                {
                                    // thành công
                                }
                                else if (response.failure == 1)
                                {
                                    //thất bại
                                }
                            }
                        }

                    }
                }
            }
            catch
            {

            }
        }
    }
}