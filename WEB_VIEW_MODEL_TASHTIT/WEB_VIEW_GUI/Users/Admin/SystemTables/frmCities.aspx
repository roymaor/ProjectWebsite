<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCities.aspx.cs" Inherits="VIEW_GUI.Users.Admin.SystemTables.frmCities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">
        function checkCity(source, args) {
            args.IsValid = checkName(args.Value);
        }
    </script>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table width="70%" border="0">
            <tr>
                <td align="center" width="70%">
      <asp:Gridview ID="grdCities" runat="server" CellPadding="4" Width="20%" OnRowDeleting="grdCities_RowDeleting" OnRowEditing="grdCities_RowEditing" OnRowDataBound="grdCities_RowDataBound" />
                </td>
                <td align="right" width="30%">
        <asp:Button ID="btnAdd" runat="server" Text="הוספה" OnClick="btnAdd_Click" />
        <asp:Panel ID="pnlCity" runat="server" Visible="false">
            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCity" Text="יש למלא ישוב"></asp:RequiredFieldValidator>
            <asp:CustomValidator        ID="CustomValidator1"        runat="server" ControlToValidate="txtCity" ClientValidationFunction="checkCity" Text="שם ישוב שגוי"></asp:CustomValidator>

            <br />
            <asp:button ID="btnOk" text="אישור" runat="server" OnClick="btnOk_Click" />
            <asp:button ID="btnCancel" text="ביטול" runat="server" CausesValidation="False" OnClick="btnCancel_Click" />
        </asp:Panel>
                </td>
            </tr>
        </table>
        <br />
    </center>
</asp:Content>
