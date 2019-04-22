<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCustomer.aspx.cs" Inherits="VIEW_GUI.Users.AuthUser.frmCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table border="0">
            <tr align="right">
                <td>
                    שם פרטי
                </td>
                <td>
                    <asp:textbox ID="txtName" runat="server" />
                </td>
            </tr>
            <tr align="right">
                <td>
                    שם משפחה
                </td>
                <td>
                    <asp:textbox ID="txtFamily" runat="server" />
                </td>
            </tr>
                        <tr align="right">
                <td>
                    תעודת זהות
                </td>
                <td>
                    <asp:textbox ID="TextId" runat="server" />
                </td>
            </tr>
                        <tr align="right">
                <td>
                    אזורי חיוד
                </td>
                <td>
                    <asp:DropDownList ID="AreaCodesDropDownList" runat="server" />
                </td>
            </tr>
                        <tr align="right">
                <td>
                    טלפון נייד
                </td>
                <td>
                    <asp:textbox ID="TextPhone" runat="server" />
                </td>
            </tr>
                        <tr align="right">
                <td>
                    כתובת
                </td>
                <td>
                    <asp:textbox ID="TextAdress" runat="server" />
                </td>
            </tr>
             </tr>
                        <tr align="right">
                <td>
                    ערים
                </td>
                <td>
                    <asp:DropDownList ID="CitiesDropDownList" runat="server" />
                </td>
            </tr>

              </tr>
                        <tr align="right">
                <td>
                    מיקוד
                </td>
                <td>
                    <asp:textbox ID="TextPostalCode" runat="server" />
                </td>
            </tr>
              </tr>
                        <tr align="right">
                <td>
                    מייל
                </td>
                <td>
                    <asp:textbox ID="TextEmail" runat="server" />
                </td>
            </tr>
              </tr>
                        <tr align="right">
                <td>
                    סיסמא
                </td>
                <td>
                    <asp:textbox ID="TextPassword" runat="server" />
                </td>
            </tr>
              </tr>
                        <tr align="right">
                <td>
                    תפקידים
                </td>
                <td>
                    <asp:DropDownList ID="RolesDropDownList" runat="server" />
                </td>
            </tr>







        </table>
    </center>
</asp:Content>
