<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="WEB_VIEW_GUI.frmLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h2><b><u>כניסה למערכת</u></b></h2>
        <br /><br />
        <table width="25%" border="0" cellSpacing="5">
            <tr>
                <td>שם משתמש</td>
                <td><asp:textbox ID="txtUserName" runat="server" Width="100%" /></td>
            </tr>
            <tr>
                <td>סיסמא</td>
                <td><asp:textbox ID="txtPassword" runat="server" TextMode="Password" Width="100%" /></td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td> 
            </tr>
            <tr>
                <td align="right"><asp:button text=" כניסה " id="btnSubmit" runat="server" OnClick="btnSubmit_Click" Width="80px" /></td>
                <td align="left"><asp:button Text=" ביטול " ID="btnCancel" runat="server" OnClick="btnCancel_Click" Width="80px" /></td>
            </tr>
        </table>

        <br />
        <asp:label ID="lblMessage" runat="server" ForeColor="Red" Font-Bold="True" Visible="False" Text="שם משתמש או סיסמא שגויים" />
    </center>
</asp:Content>
