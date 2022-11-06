<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentList.aspx.cs" Inherits="Admin.ApartmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <asp:Label ID="lblStatus" runat="server" Text="Status:"></asp:Label>
        <asp:DropDownList ID="ddlStatus" DataValueField="Id" DataTextField="Name" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <div>
        <asp:Label ID="lblCity" runat="server" Text="Grad:"></asp:Label>
        <asp:DropDownList ID="ddlCity" DataValueField="Id" DataTextField="Name" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <div>
        <asp:Label ID="lblOrder" runat="server" Text="Sortiranje:"></asp:Label>
        <asp:DropDownList ID="ddlOrder" DataValueField="Id" DataTextField="Name" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged"></asp:DropDownList>
    </div>
    <hr />


    <asp:GridView ID="gvApartmentsList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-condensed table-hover">
        <Columns>
            <asp:BoundField DataField="OwnerName" HeaderText="Vlasnik" />
            <asp:BoundField DataField="StatusName" HeaderText="Status" />
            <asp:TemplateField HeaderText="">

                <ItemTemplate>  <!-- todo fix this if 1 and 2 status true -->

                    <asp:HyperLink ID="hlReservation" runat="server" CssClass="btn btn-warning" Text="Rezerviraj" Visible='<%# (Convert.ToInt32(Eval("StatusId")).Equals(3))%>' NavigateUrl='<%# Eval("Id", "ApartmentReservationAdd.aspx?Id={0}") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:HyperLink ID="hlReservationList" runat="server" Text="Sve rezervacije" NavigateUrl='<%# Eval("Id", "ApartmentReservationsList.aspx?Id={0}") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CityName" HeaderText="Grad" />
            <asp:BoundField DataField="Address" HeaderText="Adresa" />
            <asp:BoundField DataField="Name" HeaderText="Naziv" />
            <asp:BoundField DataField="MaxAdults" HeaderText="Broj odraslih" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="MaxChildren" HeaderText="Broj djece" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="TotalRooms" HeaderText="Broj soba" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="BeachDistance" HeaderText="Udaljenost od plaže" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="Price" HeaderText="Cijena" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:HyperLink ID="hlEditor" runat="server" CssClass="btn btn-primary" Text="Uredi" NavigateUrl='<%# Eval("Id", "ApartmentEditor.aspx?Id={0}") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:HyperLink ID="hlDelete" runat="server" CssClass="btn btn-danger" Text="Briši" NavigateUrl='<%# Eval("Id", "ApartmentDelete.aspx?Id={0}") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <div>
        <asp:LinkButton ID="lbApartmentEditor" runat="server" Text="Add apartment" CssClass="btn btn-primary" OnClick="lbApartmentEditor_Click" />
    </div>


</asp:Content>
