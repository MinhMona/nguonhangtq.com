﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="them-khieu-nai.aspx.cs" Inherits="NHST.them_khieu_nai1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-50 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Thêm khiếu nại</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot2">
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-row marbot1">
                                        Mã đơn hàng
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox ID="txtOrderID" runat="server" CssClass="form-control full-width" Enabled="false"></asp:TextBox>
                                    </div>

                                    <div class="form-row marbot1">
                                        Số tiền bồi thường
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control full-width" Skin="MetroTouch"
                                            ID="pAmount" NumberFormat-DecimalDigits="0" MinValue="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                        <asp:RequiredFieldValidator ID="re" runat="server" ControlToValidate="pAmount" Display="Dynamic"
                                            ErrorMessage="Không để trống" ForeColor="Red" ValidationGroup="khieunai"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-row marbot1">
                                        Ảnh đối chiếu
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="hinhDaiDien" ChunkSize="0"
                                            Localization-Select="Chọn ảnh" AllowedFileExtensions=" .jpeg,.jpg,.png"
                                            MultipleFileSelection="Automatic" OnClientFileSelected="OnClientFileSelected">
                                        </telerik:RadAsyncUpload>
                                        <asp:Image runat="server" ID="imgDaiDien" Width="200" />
                                        <asp:HiddenField runat="server" ID="listImg" ClientIDMode="Static" />
                                    </div>
                                    <div class="form-row marbot1">
                                        Loại khiếu nại:
                                    </div>
                                    <div class="form-row marbot2">
                                       <asp:DropDownList ID="ddlComplainType" runat="server">
                                           <asp:ListItem Value="Hàng thiếu" Text="Hàng thiếu"></asp:ListItem>
                                           <asp:ListItem Value="Hàng sai mẫu mã" Text="Hàng sai mẫu mã"></asp:ListItem>
                                           <asp:ListItem Value="Hàng sai quy cách chất lượng" Text="Hàng sai quy cách chất lượng"></asp:ListItem>
                                           <asp:ListItem Value="Hàng bị vỡ, hỏng, gãy xước" Text="Hàng bị vỡ, hỏng, gãy xước"></asp:ListItem>
                                           <asp:ListItem Value="Hàng bị hỏng do ngấm nước" Text="Hàng bị hỏng do ngấm nước"></asp:ListItem>
                                           <asp:ListItem Value="Hàng đổi trả" Text="Hàng đổi trả"></asp:ListItem>
                                           <asp:ListItem Value="Mất hàng" Text="Mất hàng"></asp:ListItem>
                                           <asp:ListItem Value="Yêu cầu hoàn tiền" Text="Yêu cầu hoàn tiền"></asp:ListItem>
                                           <asp:ListItem Value="Kiểm đếm hàng sai" Text="Kiểm đếm hàng sai"></asp:ListItem>
                                           <asp:ListItem Value="Tính sai cân nặng" Text="Tính sai cân nặng"></asp:ListItem>
                                           <asp:ListItem Value="Trừ ví chưa chính xác" Text="Trừ ví chưa chính xác"></asp:ListItem>
                                           <asp:ListItem Value="Đơn hàng sai lệch về tiền" Text="Đơn hàng sai lệch về tiền"></asp:ListItem>
                                           <asp:ListItem Value="Khiếu nại dịch vụ" Text="Khiếu nại dịch vụ"></asp:ListItem>
                                           <asp:ListItem Value="Lý do khác" Text="Lý do khác"></asp:ListItem>
                                       </asp:DropDownList>
                                    </div>
                                    <div class="form-row marbot1">
                                        Nội dung
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox ID="txtNote" runat="server" CssClass="form-control full-width" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNote" Display="Dynamic"
                                            ErrorMessage="Không để trống" ForeColor="Red" ValidationGroup="khieunai"></asp:RequiredFieldValidator>
                                    </div>
                                     <div class="form-row marbot1" style="display:none" >
                                        Ghi chú
                                    </div>
                                    <div class="form-row marbot2" style="display:none" >
                                        <asp:TextBox ID="UserNote" runat="server"  CssClass="form-control full-width" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                       
                                    </div>
                                    <div class="form-row no-margin center-txt">
                                        <asp:Button ID="btnSend" runat="server" Text="Tạo khiếu nại"
                                            CssClass="btn pill-btn primary-btn admin-btn mar-top3 main-btn hover" OnClick="btnSend_Click" ValidationGroup="khieunai" />
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>

    </main>    
    <style>
        .width-not-full {
            float: left;
            width: auto;
            margin: 10px 20px 0 0;
        }

        .btn.pill-btn {
            font-weight: bold;
            text-transform: uppercase;
        }
    </style>
    <telerik:RadAjaxLoadingPanel ID="rxLoading" runat="server" Skin="">
        <div class="loading1">
            <asp:Image ID="Image1" runat="server" ImageUrl="/App_Themes/NHST/loading1.gif" AlternateText="loading" />
        </div>
    </telerik:RadAjaxLoadingPanel>
    <!-- END CONTENT -->
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSend">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Button1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery.min.js"></script>
        <script>
            
        </script>
        <script>
            function DelRow(that, link) {

                $(that).parent().parent().remove();
                var myHidden = $("#<%= listImg.ClientID %>");
                var tempF = myHidden.value;
                myHidden.value = tempF.replace(link, '');
            }
            (function (global, undefined) {
                var textBox = null;

                function textBoxLoad(sender) {
                    textBox = sender;
                }

                function OpenFileExplorerDialog() {
                    global.radopen("/Dialogs/Dialog.aspx", "ExplorerWindow");
                }

                //This function is called from a code declared on the Explorer.aspx page
                function OnFileSelected(fileSelected) {
                    if (textBox) {
                        {
                            var myHidden = document.getElementById('<%= listImg.ClientID %>');
                            var tempF = myHidden.value;

                            tempF = tempF + '#' + fileSelected;
                            myHidden.value = tempF;

                            $('.hidImage').append('<tr><td><img height="100px" src="' + fileSelected + '"/></td><td style="text-align:center"><a class="btn btn-success" onclick="DelRow(this,\'' + fileSelected + '\')">Xóa</a></td></li>');
                            //alert(fileSelected);
                            textBox.set_value(fileSelected);
                        }
                    }
                }

                global.OpenFileExplorerDialog = OpenFileExplorerDialog;
                global.OnFileSelected = OnFileSelected;
                global.textBoxLoad = textBoxLoad;
            })(window);
        </script>
        <script type="text/javascript">
            function keypress(e) {
                var keypressed = null;
                if (window.event) {
                    keypressed = window.event.keyCode; //IE
                }
                else {
                    keypressed = e.which; //NON-IE, Standard
                }
                if (keypressed < 48 || keypressed > 57) {
                    if (keypressed == 8 || keypressed == 127) {
                        return;
                    }
                    return false;
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <style>
        .RadUpload_Metro .ruFakeInput {
            float: left;
            width: 60%;
        }

        .page.account-management .right-content .right-side {
            padding-left: 20px;
        }

        div.RadUploadSubmit, div.RadUpload_Metro .ruButton {
            padding: 0;
        }
    </style>
</asp:Content>
