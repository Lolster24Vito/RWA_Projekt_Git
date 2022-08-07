<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentReservationAdd.aspx.cs" Inherits="Admin.ApartmentReservationAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 id="lApartmentName" runat="server"></h2>
    <!-- Reserved  user--->
<div class="alert alert-danger" id="ErrorMessage" role="alert" runat="server" visible="false">
 
</div>
    <div class="thumbnail " id="DivReservedUser" runat="server">

        <h3>Unesite rezervaciju na osobu:</h3>

        <div class="form-group row">
            <div class="col-sm-5">

                <label>Registrirani korisnici</label>
                <asp:CheckBox ID="checkBoxUseRegistered" runat="server" />
                <asp:ListBox ID="lbUsers" runat="server"
                    CssClass="form-control" Width="50%" Rows="15" Font-Size="13" />
            </div>
            <div class="col-sm-6">

                <label>Detalji:</label>
                <asp:TextBox ID="tbDetailsRegisteredUser" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>


        <br />

        <div class="list-group">
            <label>Neregistrirani korisnik:</label>
            <br />
            <div class="list-group-item">
                <div class="row">
                    <div class="col-sm-6">

                        <label>Naziv:</label>
                        <asp:TextBox ID="tbUserName" runat="server" CssClass="form-control"></asp:TextBox>

                        <label>Email:</label>
                        <asp:TextBox ID="tbUserEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>

                        <label>Telefon:</label>
                        <asp:TextBox ID="tbUserPhone" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>

                        <label>Adresa:</label>
                        <asp:TextBox ID="tbUserAdress" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-sm-6">

                        <label>Detalji:</label>
                        <asp:TextBox ID="tbDetailsNotRegisteredUser" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </div>

        </div>




    </div>
    <div>
        <asp:LinkButton ID="lblSave" runat="server" Text="Spremi" CssClass="btn btn-primary" OnClick="lblSave_Click" />

        <asp:LinkButton ID="btnApartmentList" runat="server" Text="Natrag na listu" CssClass="btn btn-info" OnClick="btnApartmentList_Click" />
    </div>
</asp:Content>
