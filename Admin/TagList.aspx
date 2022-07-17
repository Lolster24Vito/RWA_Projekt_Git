<%@ Page Title="Tag list" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TagList.aspx.cs" Inherits="Admin.TagList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="repTags" runat="server" OnItemDataBound="repTags_ItemDataBound">
        <HeaderTemplate>
            <ul class="list-group list-group-flush align-items-stretch">
        </HeaderTemplate>
        <ItemTemplate>

            <li class="list-group-item ">

                    <asp:HiddenField runat="server" ID="hidTagId" Value='<%# Eval("ID")   %>' />
                    <asp:Label runat="server" ID="txtTagName" Text='<%# Eval("Name") %>' />
                    <asp:Label runat="server" ID="tagCount" Text=' <%# "("+ Eval("Count")+")" %>' />

                    <asp:HyperLink ID="hlDelete" runat="server" CssClass="btn btn-danger  " Text="DELETE"
                        NavigateUrl='<%#  Eval("Id", "TagDelete.aspx?Id={0}") %>'></asp:HyperLink>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>

    <div>
        <asp:LinkButton ID="lbTagAdd" runat="server" Text="Add tag" CssClass="btn btn-primary" OnClick="lbTagAdd_Click" /></div>


</asp:Content>
