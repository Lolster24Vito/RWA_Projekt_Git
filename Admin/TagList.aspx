<%@ Page Title="Tag list" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TagList.aspx.cs" Inherits="Admin.TagList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="repTags" runat="server">
        <HeaderTemplate>
            <ul class="list-group">
        </HeaderTemplate>
        <ItemTemplate>

            <li class="list-group-item">
                <asp:HiddenField runat="server" ID="hidTagId" Value='<%# Eval("ID")   %>' />
                <asp:Label runat="server" ID="txtTagName" Text='<%# Eval("Name") %>' />
                <asp:Label runat="server" ID="tagCount" Text= '<%# Eval("Count") %>' />
                <%if (Eval("Visible"))
                    { %>
               <asp:HyperLink ID="hlDelete" runat="server"  CssClass="btn btn-primary" Text="DELETE" 
                   NavigateUrl='<%#  Eval("Id", "DeleteTag.aspx?Id={0}") %>'></asp:HyperLink>
                <%} %>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
