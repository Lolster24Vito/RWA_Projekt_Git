<%@ Page Title="User list" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Admin.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="gvUserList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-condensed table-hover">
        <Columns>
            <asp:BoundField HeaderText="Email" DataField="Email" />
            <asp:BoundField HeaderText="Phone number" DataField="PhoneNumber" />
            <asp:BoundField HeaderText="Username" DataField="UserName" />
            <asp:BoundField HeaderText="Address" DataField="Address" />

        </Columns>

    </asp:GridView>
</asp:Content>
