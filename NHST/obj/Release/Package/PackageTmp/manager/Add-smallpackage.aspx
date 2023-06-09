﻿<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Add-smallpackage.aspx.cs" Inherits="NHST.manager.Add_smallpackage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                                <h3 class="lb">Cấu hình phí vận chuyển TQ - VN</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row marbot1">
                                        Mã vận đơn
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtOrderTransactionCode" CssClass="form-control has-validate" placeholder="Mã vận đơn"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOrderTransactionCode" ErrorMessage="Không để trống" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Bao hàng
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-row marbot1">
                                        Loại hàng
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox runat="server" ID="txtProductType" CssClass="form-control has-validate" placeholder="Loại hàng"></asp:TextBox>
                                        <span class="error-info-show">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="txtProductType" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </span>
                                    </div>
                                    <div class="form-row marbot1">
                                        Phí ship (Tệ)
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pShip" MinValue="0" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSizes="3" Width="100%" placeholder="Phí ship" Value="0">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Cân (kg)
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSizes="3" Width="100%" placeholder="Cân (kg)" Value="0">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Khối (m3)
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pVolume" MinValue="0" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSizes="3" Width="100%" placeholder="Khối (m3)" Value="0">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="form-row no-margin center-txt">
                                        <asp:Button ID="Button1" runat="server" Text="Tạo mới" CssClass="btn primary-btn"
                                            OnClick="btncreateuser_Click" />
                                        <asp:Button ID="btnBack" runat="server" Text="Trở về" CssClass="btn primary-btn"
                                            OnClick="btnBack_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
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
</asp:Content>
