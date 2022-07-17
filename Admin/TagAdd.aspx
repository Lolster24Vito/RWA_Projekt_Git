<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TagAdd.aspx.cs" Inherits="Admin.TagAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Tip:</label>
                <asp:DropDownList ID="ddlTagType" DataValueField="Id" DataTextField="Name" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>

            <div class="form-group">
                <label>Naziv</label>
                <asp:TextBox ID="tbName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Engleski naziv:</label>
                <asp:TextBox ID="tbNameEng" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
    </div>
    <div>
         <asp:LinkButton ID="lblSave" runat="server" Text="Spremi" CssClass="btn btn-primary" OnClick="lblSave_Click" />

        <asp:LinkButton ID="btnTagList" runat="server" Text="Natrag na listu" CssClass="btn btn-info" OnClick="btnTagList_Click" />
    </div>

</asp:Content>
