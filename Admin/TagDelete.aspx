<%@ Page Title="Delete tag" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TagDelete.aspx.cs" Inherits="Admin.TagDelete" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

   <div class="form-group">
      <label>Ime:</label>
      <asp:Label ID="lblTagName" runat="server" CssClass="form-control"></asp:Label>
  </div>
 
  <div>
      <asp:LinkButton ID="lbConfirmDelete" runat="server" Text="Potvrdi brisanje" CssClass="btn btn-danger" OnClick="lbConfirmDelete_Click" />
      <asp:LinkButton ID="lbBack" runat="server" Text="Odustani" CssClass="btn btn-primary" OnClick="lbBack_Click" />
  </div>

</asp:Content>
