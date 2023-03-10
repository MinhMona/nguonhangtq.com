using MB.Extensions;
using Microsoft.AspNet.SignalR;
using NHST.Bussiness;
using NHST.Controllers;
using NHST.Hubs;
using NHST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHST.manager
{
    public partial class ProductEdit : System.Web.UI.Page
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
                    //string username_current = Session["userLoginSystem"].ToString();
                    //tbl_Account ac = AccountController.GetByUsername(username_current);
                    //if (ac.RoleID != 0 && ac.RoleID != 3)
                    //    Response.Redirect("/trang-chu");
                    Loaddata();
                }

            }

        }
        public void Loaddata()
        {
            int MainOrderID = 0;
            var id = Request.QueryString["id"].ToInt(0);
            if (id > 0)
            {
                var o = OrderController.GetAllByID(id);
                if (o != null)
                {
                    var mainorder = MainOrderController.GetAllByID(o.MainOrderID.ToString().ToInt());

                    int khachhangID = Convert.ToInt32(mainorder.UID);
                    var khachhang = AccountController.GetByID(khachhangID);
                    double khachhangCurrency = 0;
                    if (khachhang != null)
                    {
                        if (!string.IsNullOrEmpty(khachhang.Currency))
                        {
                            if (khachhang.Currency.ToFloat(0) > 0)
                            {
                                khachhangCurrency = Convert.ToDouble(khachhang.Currency);
                            }
                        }
                    }
                    var config = ConfigurationController.GetByTop1();
                    double currency = 0;
                    if (config != null)
                    {
                        hdfcurrent.Value = mainorder.CurrentCNYVN.ToString();
                        currency = Convert.ToDouble(mainorder.CurrentCNYVN);
                    }
                    if (khachhangCurrency > 0)
                    {
                        hdfcurrent.Value = khachhangCurrency.ToString();
                        currency = khachhangCurrency;
                    }
                    string username_current = Session["userLoginSystem"].ToString();
                    tbl_Account ac = AccountController.GetByUsername(username_current);
                    if (ac.RoleID != 0 && ac.RoleID != 3 && ac.RoleID != 2 && ac.RoleID != 6)
                    {
                        Response.Redirect("/manager/OrderDetail.aspx?id=" + o.MainOrderID + "");
                    }

                    if (mainorder.Status > 0 && ac.RoleID == 6)
                    {
                        Response.Redirect("/manager/OrderDetail.aspx?id=" + o.MainOrderID + "");
                    }
                    //else
                    //{
                    //    if (ac.RoleID == 3)
                    //    {
                    //        if (mainorder.Status >= 5)
                    //            btncreateuser.Visible = false;
                    //        pProductPriceOriginal.Enabled = false;
                    //        pRealPrice.Enabled = true;
                    //    }
                    //}
                    lblBrandname.Text = o.brand;
                    double price = 0;
                    double pricepromotion = 0;
                    double priceorigin = 0;
                    if (!string.IsNullOrEmpty(o.price_promotion))
                        pricepromotion = Convert.ToDouble(o.price_promotion);
                    if (!string.IsNullOrEmpty(o.price_origin))
                        priceorigin = Convert.ToDouble(o.price_origin);

                    if (pricepromotion > 0)
                    {
                        if (priceorigin > pricepromotion)
                        {
                            price = pricepromotion;
                        }
                        else
                        {
                            price = priceorigin;
                        }
                    }
                    else
                    {
                        price = priceorigin;
                    }
                    ViewState["productprice"] = price;
                    pProductPriceOriginal.Value = price;
                    if (!string.IsNullOrEmpty(o.quantity))
                        pQuanity.Value = Convert.ToDouble(o.quantity);
                    else pQuanity.Value = 0;
                    pRealPrice.Value = Convert.ToDouble(o.RealPrice);
                    txtproducbrand.Text = o.brand;
                    ltrback.Text += "<a href=\"/manager/OrderDetail.aspx?id=" + o.MainOrderID + "\" class=\"btn primary-btn\">Trở về</a>";
                    string productstatus = "";
                    if (!string.IsNullOrEmpty(o.ProductStatus.ToString()))
                        ddlStatus.SelectedValue = o.ProductStatus.ToString();
                    else
                        ddlStatus.SelectedValue = "1";
                }
            }
        }

        protected void btncreateuser_Click(object sender, EventArgs e)
        {
            string username = Session["userLoginSystem"].ToString();
            var obj_user = AccountController.GetByUsername(username);
            DateTime currentDate = DateTime.Now;
            int status = ddlStatus.SelectedValue.ToString().ToInt(1);

            //Update lại giá sản phẩm
            var id = Request.QueryString["id"].ToInt(0);
            if (obj_user.RoleID == 3)
            {
                var o = OrderController.GetAllByID(id);
                var mo = MainOrderController.GetAllByID(Convert.ToInt32(o.MainOrderID));
                if (mo.IsPaying == true && mo.Status >= 2)
                {
                    Response.Redirect("/manager/orderlist?t=1");
                }
                else
                {
                    int MainOrderID = 0;
                    if (id > 0)
                    {
                        if (o != null)
                        {
                            MainOrderID = Convert.ToInt32(o.MainOrderID);
                            double pprice = Convert.ToDouble(ViewState["productprice"].ToString());
                            double price = 0;
                            double pricepromotion = 0;
                            double priceorigin = 0;
                            if (!string.IsNullOrEmpty(o.price_promotion))
                                pricepromotion = Convert.ToDouble(o.price_promotion);

                            if (!string.IsNullOrEmpty(o.price_origin))
                                priceorigin = Convert.ToDouble(o.price_origin);

                            if (pricepromotion > 0)
                            {
                                if (priceorigin > pricepromotion)
                                {
                                    price = pricepromotion;
                                }
                                else
                                {
                                    price = priceorigin;
                                }
                            }
                            else
                            {
                                price = priceorigin;
                            }

                            double quantity = 0;
                            if (status == 2)
                            {
                                price = 0;
                                quantity = 0;
                                var od = MainOrderController.GetAllByID(MainOrderID);
                                if (od != null)
                                {
                                    int userdathangID = Convert.ToInt32(od.UID);
                                    var userdathang = AccountController.GetByID(userdathangID);
                                    if (userdathang != null)
                                    {
                                        NotificationController.Inser(obj_user.ID, obj_user.Username, userdathang.ID, userdathang.Username, MainOrderID,
                                                               "Đơn hàng: " + MainOrderID + " có sản phẩm bị hết hàng.", 0,
                                                               1, DateTime.Now, obj_user.Username, false);
                                    }
                                }
                                if (price.ToString() != pProductPriceOriginal.Value.ToString())
                                {
                                    HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                                    " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                                    + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                                }
                                if (o.quantity != pQuanity.Value.ToString())
                                {
                                    HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                                    " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", o.price_origin) + ", sang: "
                                                    + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                                }
                                OrderController.UpdateQuantity(id, quantity.ToString());
                                OrderController.UpdateProductStatus(id, status);
                                OrderController.UpdatePricePriceReal(id, "0", "0");
                                OrderController.UpdatePricePromotion(id, "0");
                            }
                            else
                            {
                                quantity = Convert.ToDouble(pQuanity.Value);
                                if (price.ToString() != pProductPriceOriginal.Value.ToString())
                                {
                                    HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                                    " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                                    + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                                }
                                if (o.quantity != pQuanity.Value.ToString())
                                {
                                    HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                                    " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", o.price_origin) + ", sang: "
                                                    + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                                }
                                OrderController.UpdateQuantity(id, quantity.ToString());
                                OrderController.UpdateProductStatus(id, status);
                                OrderController.UpdatePricePriceReal(id, pProductPriceOriginal.Value.ToString(), pRealPrice.Value.ToString());
                                OrderController.UpdatePricePromotion(id, pProductPriceOriginal.Value.ToString());
                            }
                            OrderController.UpdateBrand(id, txtproducbrand.Text.Trim());
                        }
                    }

                    //Update lại giá của đơn hàng, lấy từng sản phẩm thuộc đơn hàng để lấy giá xác định rồi tổng lại rồi cộng các phí
                    var listorder = OrderController.GetByMainOrderID(MainOrderID);
                    var mainorder = MainOrderController.GetAllByID(MainOrderID);

                    if (mainorder != null)
                    {
                        double current = Convert.ToDouble(mainorder.CurrentCNYVN);
                        int khachhangID = Convert.ToInt32(mainorder.UID);
                        var khachhang = AccountController.GetByID(khachhangID);
                        double khachhangCurrency = 0;
                        if (khachhang != null)
                        {
                            if (!string.IsNullOrEmpty(khachhang.Currency))
                            {
                                if (khachhang.Currency.ToFloat(0) > 0)
                                {
                                    khachhangCurrency = Convert.ToDouble(khachhang.Currency);
                                }
                            }
                        }
                        if (khachhangCurrency > 0)
                        {
                            current = khachhangCurrency;
                        }
                        if (listorder != null)
                        {
                            if (listorder.Count > 0)
                            {
                                double pricevnd = 0;
                                double pricecyn = 0;
                                foreach (var item in listorder)
                                {
                                    double originprice = Convert.ToDouble(item.price_origin);
                                    double promotionprice = Convert.ToDouble(item.price_promotion);
                                    double oprice = 0;
                                    if (promotionprice > 0)
                                    {
                                        if (promotionprice < originprice)
                                        {
                                            pricecyn += promotionprice;
                                            oprice = promotionprice * Convert.ToDouble(item.quantity) * current;
                                        }
                                        else
                                        {
                                            pricecyn += originprice;
                                            oprice = originprice * Convert.ToDouble(item.quantity) * current;
                                        }
                                    }
                                    else
                                    {
                                        pricecyn += originprice;
                                        oprice = originprice * Convert.ToDouble(item.quantity) * current;
                                    }
                                    //var oprice = Convert.ToDouble(item.price_origin) * Convert.ToDouble(item.quantity) * Convert.ToDouble(item.CurrentCNYVN) + Convert.ToDouble(item.PriceChange);

                                    //pricecyn += item.price_origin.ToFloat();
                                    //var oprice = Convert.ToDouble(item.price_origin) * Convert.ToDouble(item.quantity) * current;
                                    pricevnd += oprice;
                                }
                                MainOrderController.UpdatePriceNotFee(MainOrderID, pricevnd.ToString());
                                MainOrderController.UpdatePriceCYN(MainOrderID, pricecyn.ToString());
                                double Deposit = Convert.ToDouble(mainorder.Deposit);
                                double FeeShipCN = Convert.ToDouble(mainorder.FeeShipCN);
                                double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
                                if (FeeBuyPro < 0)
                                    FeeBuyPro = 0;
                                double FeeWeight = Convert.ToDouble(mainorder.FeeWeight);
                                //double FeeShipCNToVN = Convert.ToDouble(mainorder.FeeShipCNToVN);

                                double IsCheckProductPrice = 0;
                                if (mainorder.IsCheckProduct == true)
                                {
                                    double total = 0;
                                    double counpros = 0;
                                    if (listorder.Count > 0)
                                    {
                                        foreach (var item in listorder)
                                        {
                                            counpros += item.quantity.ToInt(1);
                                        }
                                    }
                                    //var count = listpro.Count;
                                    if (counpros >= 1 && counpros <= 2)
                                    {
                                        total = total + (5000 * counpros);
                                    }
                                    else if (counpros > 2 && counpros <= 10)
                                    {
                                        total = total + (3500 * counpros);
                                    }
                                    else if (counpros > 10 && counpros <= 100)
                                    {
                                        total = total + (2000 * counpros);
                                    }
                                    else if (counpros > 100 && counpros <= 500)
                                    {
                                        total = total + (1500 * counpros);
                                    }
                                    else if (counpros > 500)
                                    {
                                        total = total + (1000 * counpros);
                                    }
                                    IsCheckProductPrice = total;
                                }
                                else
                                    IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);

                                double IsPackedPrice = 0;
                                IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);

                                double IsFastDeliveryPrice = 0;
                                IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);


                                double TotalPriceVND = FeeShipCN + FeeBuyPro
                                                        + FeeWeight + IsCheckProductPrice
                                                        + IsPackedPrice + IsFastDeliveryPrice
                                                        + Convert.ToDouble(mainorder.IsFastPrice) + pricevnd;
                                double newdeposit = 0;


                                #region phần chỉnh sửa giá
                                double totalo = 0;
                                var ui = AccountController.GetByID(mainorder.UID.ToString().ToInt());
                                double UL_CKFeeBuyPro = 0;
                                double UL_CKFeeWeight = 0;
                                double LessDeposito = 0;
                                if (ui != null)
                                {
                                    UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeBuyPro);
                                    UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeWeight);
                                    LessDeposito = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).LessDeposit);
                                }
                                double fastprice = 0;
                                double pricepro = pricevnd;
                                double servicefee = 0;
                                bool getFeeFromUser = false;
                                if (!string.IsNullOrEmpty(ui.FeeBuyPro))
                                {
                                    if (ui.FeeBuyPro.ToFloat(0) > 0)
                                    {
                                        servicefee = Convert.ToDouble(ui.FeeBuyPro) / 100;
                                        getFeeFromUser = true;
                                    }
                                    else
                                    {
                                        var adminfeebuypro = FeeBuyProController.GetAll();
                                        if (adminfeebuypro.Count > 0)
                                        {
                                            foreach (var item in adminfeebuypro)
                                            {
                                                if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                                {
                                                    servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                                    //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    var adminfeebuypro = FeeBuyProController.GetAll();
                                    if (adminfeebuypro.Count > 0)
                                    {
                                        foreach (var item in adminfeebuypro)
                                        {
                                            if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                            {
                                                servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                                //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                                                break;
                                            }
                                        }
                                    }
                                }

                                double feebpnotdc = pricepro * servicefee;
                                double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                                //double feebp = feebpnotdc - subfeebp;
                                //feebp = Math.Round(feebp, 0);
                                double feebp = 0;
                                //double feebuyproUser = 0;
                                //if (!string.IsNullOrEmpty(ui.FeeBuyPro))
                                //{
                                //    if (ui.FeeBuyPro.ToFloat(0) > 0)
                                //    {
                                //        feebuyproUser = Convert.ToDouble(ui.FeeBuyPro);
                                //    }
                                //    feebp = feebuyproUser;
                                //}
                                //else
                                //{
                                //    feebp = feebpnotdc - subfeebp; ;
                                //}
                                feebp = feebpnotdc - subfeebp;
                                feebp = Math.Round(feebp, 0);
                                if (feebp < 10000)
                                    feebp = 10000;
                                if (mainorder.IsFast == true)
                                {
                                    fastprice = (pricepro * 5 / 100);
                                }
                                totalo = fastprice + pricepro;
                                double FeeCNShip = FeeShipCN;
                                double FeeBuyPros = feebp;
                                double FeeCheck = IsCheckProductPrice;
                                //totalo = totalo + FeeCNShip + FeeBuyPros + FeeCheck;
                                totalo = fastprice + pricepro + FeeCNShip + FeeBuyPros + FeeCheck + FeeWeight + IsFastDeliveryPrice;
                                double AmountDeposit = Math.Floor((totalo * LessDeposito) / 100);
                                //cập nhật lại giá phải deposit của đơn hàng
                                MainOrderController.UpdateAmountDeposit(MainOrderID, AmountDeposit.ToString());

                                //giá hỏa tốc, giá sản phẩm, phí mua sản phẩm, phí ship cn, phí kiểm tra hàng
                                newdeposit = AmountDeposit;

                                //nếu đã đặt cọc rồi thì trả phí lại cho người ta
                                if (Deposit > 0)
                                {
                                    if (Deposit > newdeposit)
                                    {
                                        double drefund = Deposit - newdeposit;
                                        double userwallet = 0;
                                        if (ui.Wallet.ToString() != null)
                                            userwallet = Convert.ToDouble(ui.Wallet.ToString());

                                        double wallet = userwallet + drefund;
                                        AccountController.updateWallet(ui.ID, wallet, currentDate, obj_user.Username);
                                        PayOrderHistoryController.Insert(MainOrderID, obj_user.ID, 12, drefund, 2, currentDate, obj_user.Username);
                                        // HistoryOrderChangeController.Insert(mainorder.ID, obj_user.ID, username, username +
                                        //" đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Đã xong.", 1, currentDate);
                                        if (status == 2)
                                            HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " hết hàng.", wallet, 2, 2, currentDate, obj_user.Username);
                                        else
                                            HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " giảm giá.", wallet, 2, 2, currentDate, obj_user.Username);

                                        NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(mainorder.UID),
                                            AccountController.GetByID(Convert.ToInt32(mainorder.UID)).Username, mainorder.ID, "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem", 0,
                                            1, currentDate, obj_user.Username, false);

                                        var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                        hubContext.Clients.All.addNewMessageToPage("", "");

                                        var setNoti = SendNotiEmailController.GetByID(19);
                                        if (setNoti != null)
                                        {
                                            if (setNoti.IsSentNotiUser == true)
                                            {
                                                NotificationsController.Inser(Convert.ToInt32(mainorder.UID),
                                                        AccountController.GetByID(Convert.ToInt32(mainorder.UID)).Username, mainorder.ID, "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem",
                                                        1, currentDate, obj_user.Username, false);
                                            }

                                            if (setNoti.IsSendEmailUser == true)
                                            {
                                                try
                                                {
                                                    PJUtils.SendMailGmail("NguonhangTQ.com@gmail.com", "jjzzwqhglchstxkc", AccountInfoController.GetByUserID(Convert.ToInt32(mainorder.UID)).Email,
                                                        "Thông báo tại Nguồn Hàng TQ", "Đã có cập nhật mới về đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem", "");
                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (Deposit < newdeposit)
                                        {
                                            MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                                        }
                                        else if (Deposit == newdeposit)
                                        {
                                            //MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);
                                        }
                                        newdeposit = Deposit;

                                    }
                                }
                                else
                                {
                                    MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                                    newdeposit = 0;
                                }
                                if (totalo == 0)
                                {
                                    MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                                }

                                #endregion


                                MainOrderController.UpdateFee(MainOrderID, newdeposit.ToString(), FeeCNShip.ToString(), FeeBuyPros.ToString(), FeeWeight.ToString(),
                                    FeeCheck.ToString(), IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), totalo.ToString());
                            }
                        }
                    }
                    PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thông tin thành công.", "s", true, "/manager/OrderDetail.aspx?id=" + MainOrderID, Page);
                }
            }
            else
            {
                int MainOrderID = 0;
                if (id > 0)
                {
                    var o = OrderController.GetAllByID(id);
                    if (o != null)
                    {
                        MainOrderID = Convert.ToInt32(o.MainOrderID);

                        double pprice = Convert.ToDouble(ViewState["productprice"].ToString());
                        double price = 0;
                        double pricepromotion = 0;
                        double priceorigin = 0;
                        if (!string.IsNullOrEmpty(o.price_promotion))
                            pricepromotion = Convert.ToDouble(o.price_promotion);

                        if (!string.IsNullOrEmpty(o.price_origin))
                            priceorigin = Convert.ToDouble(o.price_origin);

                        if (pricepromotion > 0)
                        {
                            if (priceorigin > pricepromotion)
                            {
                                price = pricepromotion;
                            }
                            else
                            {
                                price = priceorigin;
                            }
                        }
                        else
                        {
                            price = priceorigin;
                        }


                        double quantity = 0;
                        if (status == 2)
                        {
                            price = 0;
                            quantity = 0;
                            var od = MainOrderController.GetAllByID(MainOrderID);
                            if (od != null)
                            {
                                int userdathangID = Convert.ToInt32(od.UID);
                                var userdathang = AccountController.GetByID(userdathangID);
                                if (userdathang != null)
                                {
                                    NotificationController.Inser(obj_user.ID, obj_user.Username, userdathang.ID, userdathang.Username, MainOrderID,
                                                           "Đơn hàng: " + MainOrderID + " có sản phẩm bị hết hàng.", 0,
                                                           1, DateTime.Now, obj_user.Username, false);
                                }
                            }
                            if (price.ToString() != pProductPriceOriginal.Value.ToString())
                            {
                                HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                                " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                                + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                            }
                            if (o.quantity != pQuanity.Value.ToString())
                            {
                                HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                                " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", o.price_origin) + ", sang: "
                                                + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                            }
                            OrderController.UpdateQuantity(id, quantity.ToString());
                            OrderController.UpdateProductStatus(id, status);
                            OrderController.UpdatePricePriceReal(id, "0", "0");
                            OrderController.UpdatePricePromotion(id, "0");
                        }
                        else
                        {
                            quantity = Convert.ToDouble(pQuanity.Value);
                            if (price.ToString() != pProductPriceOriginal.Value.ToString())
                            {
                                HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                                " đã đổi giá sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", price) + ", sang: "
                                                + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                            }
                            if (o.quantity != pQuanity.Value.ToString())
                            {
                                HistoryOrderChangeController.Insert(MainOrderID, obj_user.ID, obj_user.Username, obj_user.Username +
                                                " đã đổi số lượng sản phẩm của Sản phẩm ID là: " + o.ID + ", của đơn hàng ID là: " + MainOrderID + ", từ: " + string.Format("{0:N0}", o.price_origin) + ", sang: "
                                                + string.Format("{0:N0}", Convert.ToDouble(pProductPriceOriginal.Value)) + "", 1, currentDate);
                            }
                            OrderController.UpdateQuantity(id, quantity.ToString());
                            OrderController.UpdateProductStatus(id, status);
                            OrderController.UpdatePricePriceReal(id, pProductPriceOriginal.Value.ToString(), pRealPrice.Value.ToString());
                            OrderController.UpdatePricePromotion(id, pProductPriceOriginal.Value.ToString());
                        }
                        OrderController.UpdateBrand(id, txtproducbrand.Text.Trim());
                    }
                }

                //Update lại giá của đơn hàng, lấy từng sản phẩm thuộc đơn hàng để lấy giá xác định rồi tổng lại rồi cộng các phí
                var listorder = OrderController.GetByMainOrderID(MainOrderID);
                var mainorder = MainOrderController.GetAllByID(MainOrderID);

                if (mainorder != null)
                {
                    double current = Convert.ToDouble(mainorder.CurrentCNYVN);
                    int khachhangID = Convert.ToInt32(mainorder.UID);
                    var khachhang = AccountController.GetByID(khachhangID);
                    double khachhangCurrency = 0;
                    if (khachhang != null)
                    {
                        if (!string.IsNullOrEmpty(khachhang.Currency))
                        {
                            if (khachhang.Currency.ToFloat(0) > 0)
                            {
                                khachhangCurrency = Convert.ToDouble(khachhang.Currency);
                            }
                        }
                    }
                    if (khachhangCurrency > 0)
                    {
                        current = khachhangCurrency;
                    }
                    if (listorder != null)
                    {
                        if (listorder.Count > 0)
                        {
                            double pricevnd = 0;
                            double pricecyn = 0;
                            foreach (var item in listorder)
                            {
                                double originprice = Convert.ToDouble(item.price_origin);
                                double promotionprice = Convert.ToDouble(item.price_promotion);
                                double oprice = 0;
                                if (promotionprice > 0)
                                {
                                    if (promotionprice < originprice)
                                    {
                                        pricecyn += promotionprice;
                                        oprice = promotionprice * Convert.ToDouble(item.quantity) * current;
                                    }
                                    else
                                    {
                                        pricecyn += originprice;
                                        oprice = originprice * Convert.ToDouble(item.quantity) * current;
                                    }
                                }
                                else
                                {
                                    pricecyn += originprice;
                                    oprice = originprice * Convert.ToDouble(item.quantity) * current;
                                }
                                //var oprice = Convert.ToDouble(item.price_origin) * Convert.ToDouble(item.quantity) * Convert.ToDouble(item.CurrentCNYVN) + Convert.ToDouble(item.PriceChange);

                                //pricecyn += item.price_origin.ToFloat();
                                //var oprice = Convert.ToDouble(item.price_origin) * Convert.ToDouble(item.quantity) * current;
                                pricevnd += oprice;
                            }
                            MainOrderController.UpdatePriceNotFee(MainOrderID, pricevnd.ToString());
                            MainOrderController.UpdatePriceCYN(MainOrderID, pricecyn.ToString());
                            double Deposit = Convert.ToDouble(mainorder.Deposit);
                            double FeeShipCN = Convert.ToDouble(mainorder.FeeShipCN);
                            double FeeBuyPro = Convert.ToDouble(mainorder.FeeBuyPro);
                            if (FeeBuyPro < 0)
                                FeeBuyPro = 0;
                            double FeeWeight = Convert.ToDouble(mainorder.FeeWeight);
                            //double FeeShipCNToVN = Convert.ToDouble(mainorder.FeeShipCNToVN);

                            double IsCheckProductPrice = 0;
                            var counpros_more10 = 0;
                            var counpros_les10 = 0;

                            if (mainorder.IsCheckProduct == true)
                            {
                                double total = 0;
                                double counpros = 0;
                                if (listorder.Count > 0)
                                {
                                    foreach (var item in listorder)
                                    {
                                        // counpros += item.quantity.ToInt(1);

                                        double countProduct = item.quantity.ToInt(1);
                                        if (Convert.ToDouble(item.price_origin) >= 10)
                                        {
                                            counpros_more10 += item.quantity.ToInt(1);
                                        }
                                        else
                                        {
                                            counpros_les10 += item.quantity.ToInt(1);
                                        }
                                    }
                                }
                                //var count = listpro.Count;
                                //if (counpros >= 1 && counpros <= 2)
                                //{
                                //    total = total + (5000 * counpros);
                                //}
                                //else if (counpros > 2 && counpros <= 10)
                                //{
                                //    total = total + (3500 * counpros);
                                //}
                                //else if (counpros > 10 && counpros <= 100)
                                //{
                                //    total = total + (2000 * counpros);
                                //}
                                //else if (counpros > 100 && counpros <= 500)
                                //{
                                //    total = total + (1500 * counpros);
                                //}
                                //else if (counpros > 500)
                                //{
                                //    total = total + (1000 * counpros);
                                //}
                                if (counpros_more10 > 0)
                                {
                                    if (counpros_more10 >= 1 && counpros_more10 <= 2)
                                    {
                                        total = total + (7000 * counpros_more10);
                                    }
                                    else if (counpros_more10 > 2 && counpros_more10 <= 10)
                                    {
                                        total = total + (5000 * counpros_more10);
                                    }
                                    else if (counpros_more10 > 10 && counpros_more10 <= 100)
                                    {
                                        total = total + (3000 * counpros_more10);
                                    }
                                    else if (counpros_more10 > 100 && counpros_more10 <= 500)
                                    {
                                        total = total + (2000 * counpros_more10);
                                    }
                                    else if (counpros_more10 > 500)
                                    {
                                        total = total + (1500 * counpros_more10);
                                    }
                                }
                                if (counpros_les10 > 0)
                                {
                                    if (counpros_les10 >= 1 && counpros_les10 <= 2)
                                    {
                                        total = total + (1500 * counpros_les10);
                                    }
                                    else if (counpros_les10 > 2 && counpros_les10 <= 10)
                                    {
                                        total = total + (1000 * counpros_les10);
                                    }
                                    else if (counpros_les10 > 10 && counpros_les10 <= 100)
                                    {
                                        total = total + (700 * counpros_les10);
                                    }
                                    else if (counpros_les10 > 100 && counpros_les10 <= 500)
                                    {
                                        total = total + (700 * counpros_les10);
                                    }
                                    else if (counpros_les10 > 500)
                                    {
                                        total = total + (700 * counpros_les10);
                                    }
                                }

                                IsCheckProductPrice = total;
                            }
                            else
                                IsCheckProductPrice = Convert.ToDouble(mainorder.IsCheckProductPrice);

                            double IsPackedPrice = 0;
                            IsPackedPrice = Convert.ToDouble(mainorder.IsPackedPrice);

                            double IsFastDeliveryPrice = 0;
                            IsFastDeliveryPrice = Convert.ToDouble(mainorder.IsFastDeliveryPrice);


                            double TotalPriceVND = FeeShipCN + FeeBuyPro
                                                    + FeeWeight + IsCheckProductPrice
                                                    + IsPackedPrice + IsFastDeliveryPrice
                                                    + Convert.ToDouble(mainorder.IsFastPrice) + pricevnd;
                            double newdeposit = 0;


                            #region phần chỉnh sửa giá
                            double totalo = 0;
                            var ui = AccountController.GetByID(mainorder.UID.ToString().ToInt());
                            double UL_CKFeeBuyPro = 0;
                            double UL_CKFeeWeight = 0;
                            double LessDeposito = 0;
                            if (ui != null)
                            {
                                UL_CKFeeBuyPro = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeBuyPro);
                                UL_CKFeeWeight = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).FeeWeight);
                                LessDeposito = Convert.ToDouble(UserLevelController.GetByID(ui.LevelID.ToString().ToInt()).LessDeposit);
                            }
                            double fastprice = 0;
                            double pricepro = pricevnd;
                            double servicefee = 0;
                            bool getFeeFromUser = false;
                            if (!string.IsNullOrEmpty(ui.FeeBuyPro))
                            {
                                if (ui.FeeBuyPro.ToFloat(0) > 0)
                                {
                                    servicefee = Convert.ToDouble(ui.FeeBuyPro) / 100;
                                    getFeeFromUser = true;
                                }
                                else
                                {
                                    var adminfeebuypro = FeeBuyProController.GetAll();
                                    if (adminfeebuypro.Count > 0)
                                    {
                                        foreach (var item in adminfeebuypro)
                                        {
                                            if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                            {
                                                servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                                //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var adminfeebuypro = FeeBuyProController.GetAll();
                                if (adminfeebuypro.Count > 0)
                                {
                                    foreach (var item in adminfeebuypro)
                                    {
                                        if (pricepro >= item.AmountFrom && pricepro < item.AmountTo)
                                        {
                                            servicefee = item.FeePercent.ToString().ToFloat(0) / 100;
                                            //serviceFeeMoney = Convert.ToDouble(item.FeeMoney);
                                            break;
                                        }
                                    }
                                }
                            }

                            double feebpnotdc = pricepro * servicefee;
                            double subfeebp = feebpnotdc * UL_CKFeeBuyPro / 100;
                            //double feebp = feebpnotdc - subfeebp;
                            //feebp = Math.Round(feebp, 0);
                            double feebp = 0;
                            //double feebuyproUser = 0;
                            //if (!string.IsNullOrEmpty(ui.FeeBuyPro))
                            //{
                            //    if (ui.FeeBuyPro.ToFloat(0) > 0)
                            //    {
                            //        feebuyproUser = Convert.ToDouble(ui.FeeBuyPro);
                            //    }
                            //    feebp = feebuyproUser;
                            //}
                            //else
                            //{
                            //    feebp = feebpnotdc - subfeebp; ;
                            //}
                            feebp = feebpnotdc - subfeebp;
                            feebp = Math.Round(feebp, 0);
                            if (feebp < 10000)
                                feebp = 10000;
                            if (mainorder.IsFast == true)
                            {
                                fastprice = (pricepro * 5 / 100);
                            }
                            totalo = fastprice + pricepro;
                            double FeeCNShip = FeeShipCN;
                            double FeeBuyPros = feebp;
                            double FeeCheck = IsCheckProductPrice;
                            //totalo = totalo + FeeCNShip + FeeBuyPros + FeeCheck;
                            totalo = fastprice + pricepro + FeeCNShip + FeeBuyPros + FeeCheck + FeeWeight + IsFastDeliveryPrice;
                            double AmountDeposit = Math.Floor((totalo * LessDeposito) / 100);
                            //cập nhật lại giá phải deposit của đơn hàng
                            MainOrderController.UpdateAmountDeposit(MainOrderID, AmountDeposit.ToString());

                            //giá hỏa tốc, giá sản phẩm, phí mua sản phẩm, phí ship cn, phí kiểm tra hàng
                            newdeposit = AmountDeposit;

                            //nếu đã đặt cọc rồi thì trả phí lại cho người ta
                            if (Deposit > 0)
                            {
                                if (Deposit > newdeposit)
                                {
                                    double drefund = Deposit - newdeposit;
                                    double userwallet = 0;
                                    if (ui.Wallet.ToString() != null)
                                        userwallet = Convert.ToDouble(ui.Wallet.ToString());

                                    double wallet = userwallet + drefund;
                                    AccountController.updateWallet(ui.ID, wallet, currentDate, obj_user.Username);
                                    PayOrderHistoryController.Insert(MainOrderID, obj_user.ID, 12, drefund, 2, currentDate, obj_user.Username);
                                    // HistoryOrderChangeController.Insert(mainorder.ID, obj_user.ID, username, username +
                                    //" đã đổi trạng thái của đơn hàng ID là: " + o.ID + ", từ: Chờ thanh toán, sang: Đã xong.", 1, currentDate);
                                    if (status == 2)
                                        HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " hết hàng.", wallet, 2, 2, currentDate, obj_user.Username);
                                    else
                                        HistoryPayWalletController.Insert(ui.ID, ui.Username, mainorder.ID, drefund, "Sản phẩm đơn hàng: " + mainorder.ID + " giảm giá.", wallet, 2, 2, currentDate, obj_user.Username);

                                    NotificationController.Inser(obj_user.ID, obj_user.Username, Convert.ToInt32(mainorder.UID),
                                        AccountController.GetByID(Convert.ToInt32(mainorder.UID)).Username, mainorder.ID, "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem", 0,
                                        1, currentDate, obj_user.Username, false);

                                    var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
                                    hubContext.Clients.All.addNewMessageToPage("", "");

                                    var setNoti = SendNotiEmailController.GetByID(19);
                                    if (setNoti != null)
                                    {
                                        if (setNoti.IsSentNotiUser == true)
                                        {
                                            NotificationsController.Inser(Convert.ToInt32(mainorder.UID),
                                                    AccountController.GetByID(Convert.ToInt32(mainorder.UID)).Username, mainorder.ID, "Đã có cập nhật mới về sản phẩm cho đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem",
                                                    1, currentDate, obj_user.Username, false);
                                        }

                                        if (setNoti.IsSendEmailUser == true)
                                        {
                                            try
                                            {
                                                PJUtils.SendMailGmail("NguonhangTQ.com@gmail.com", "jjzzwqhglchstxkc", AccountInfoController.GetByUserID(Convert.ToInt32(mainorder.UID)).Email,
                                                    "Thông báo tại Nguồn Hàng TQ", "Đã có cập nhật mới về đơn hàng #" + mainorder.ID + " của bạn. CLick vào để xem", "");
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                    //try
                                    //{
                                    //    PJUtils.SendMailGmail("cskh@1688pgs.vn", "1688pegasus", AccountInfoController.GetByUserID(Convert.ToInt32(mainorder.UID)).Email,
                                    //        "Thông báo tại 1688PGS", "Đã có cập nhật mới về cân nặng cho đơn hàng #" + id + " của bạn. CLick vào để xem", "");
                                    //}
                                    //catch
                                    //{

                                    //}
                                    //newdeposit = Deposit;
                                    //MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);
                                }
                                else
                                {
                                    if (Deposit < newdeposit)
                                    {
                                        MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                                    }
                                    else if (Deposit == newdeposit)
                                    {
                                        //MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 2);
                                    }
                                    newdeposit = Deposit;

                                }
                            }
                            else
                            {
                                MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                                newdeposit = 0;
                            }
                            if (totalo == 0)
                            {
                                MainOrderController.UpdateStatus(mainorder.ID, ui.ID, 0);
                            }
                            //if (status == 2)
                            //{

                            //}
                            //else
                            //{
                            //    newdeposit = Deposit;
                            //}
                            #endregion


                            MainOrderController.UpdateFee(MainOrderID, newdeposit.ToString(), FeeCNShip.ToString(), FeeBuyPros.ToString(), FeeWeight.ToString(),
                                FeeCheck.ToString(), IsPackedPrice.ToString(), IsFastDeliveryPrice.ToString(), totalo.ToString());
                        }
                    }
                }
                PJUtils.ShowMessageBoxSwAlertBackToLink("Cập nhật thông tin thành công.", "s", true, "/manager/OrderDetail.aspx?id=" + MainOrderID, Page);
            }

        }
    }
}