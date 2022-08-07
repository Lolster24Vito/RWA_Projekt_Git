<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentReservationsList.aspx.cs" Inherits="Admin.ApartmentReservationsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Rezervacije korisnika</h2>
        <h2 id="lApartmentName" runat="server"></h2>


     <asp:GridView ID="gvApartmentReservationsList" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-condensed table-hover">
        <Columns>
            <asp:BoundField DataField="CreatedAt" HeaderText="Napravljena" />         
            <asp:BoundField DataField="Details" HeaderText="Detalji" />
            <asp:BoundField DataField="UserName" HeaderText="Naziv korisnika" />
            <asp:BoundField DataField="UserEmail" HeaderText="Email korisnika" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="UserPhone" HeaderText="Broj korisnika" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="UserAddress" HeaderText="adresa korisnika" ItemStyle-HorizontalAlign="Right" />

             <asp:TemplateField HeaderText="Registriran">
                <ItemTemplate>
                      <%# Boolean.Parse(Eval("IsRegistered").ToString()) ? "Da" : "Ne" %>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>




</asp:Content>
