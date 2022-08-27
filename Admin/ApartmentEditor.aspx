<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentEditor.aspx.cs" Inherits="Admin.ApartmentEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="alert alert-danger" id="ErrorMessage" role="alert" runat="server" visible="false">
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="form-group">
                <label>Vlasnik</label>
                <asp:DropDownList ID="ddlApartmentOwner" DataValueField="Id" DataTextField="Name" runat="server" AutoPostBack="true"
                    CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <label>Status</label>
                <asp:DropDownList ID="ddlStatus" DataValueField="Id" DataTextField="Name" runat="server"
                    CssClass="form-control" AutoPostBack="true">
                </asp:DropDownList>
            </div>







            <div class="form-group">
                <label>Naziv</label>
                <asp:TextBox ID="tbName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Adresa</label>
                <asp:TextBox ID="tbAddress" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Grad</label>
                <asp:DropDownList ID="ddlCity" DataValueField="Id" DataTextField="Name" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group">
                <label>Cijena</label>
                <div class="input-group">
                    <span class="input-group-addon" id="sizing-addon1">€</span>
                    <asp:TextBox ID="tbPrice" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label>Broj odraslih</label>
                <asp:TextBox ID="tbMaxAdults" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Broj djece</label>
                <asp:TextBox ID="tbMaxChildren" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Broj soba</label>
                <asp:TextBox ID="tbTotalRooms" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Udaljenost od plaže</label>
                <asp:TextBox ID="tbBeachDistance" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>


            <div class="form-group">
                <label>Odabir tagova</label>
                <asp:DropDownList ID="ddlTags" runat="server" CssClass="form-control"
                    DataValueField="Id"
                    DataTextField="Name" OnSelectedIndexChanged="ddlTags_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>



            <div class="container">
                <div class="form-group">
                    <label class="btn btn-default">
                        Upload slika
                <asp:FileUpload ID="uplImages" runat="server" CssClass="hidden" AllowMultiple="true" OnChange="handleFileSelect(this.files)" />

                    </label>
                    <div id="uplImageInfo"></div>
                    <script>
                        function getPicture(files) {
                            $.ajax({
                                type: "POST",
                                url: "ApartmentEditor.aspx/GetPictures",
                                data: '{}',
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: OnSuccess,
                                failure: function (response) {
                                    alert(response.d);
                                },
                                error: function (response) {
                                    alert(response.d);
                                }
                            });
                        }

                        function OnSuccess(response) {
                            alert(response);
                            var table = $("#imagesContainer li").eq(0).clone(true);
                            var imagePictures = response.d;
                            console.log(imagePictures);
                            $("#imagesContainer li").eq(0).remove();
                            $(imagePictures).each(function () {
                                $("input[id$=hidClientField]", table).html(this.Id);
                                $("#txtApartmentPicture", table).html(this.name);
                                $("#imgApartmentPicture", table).attr("src", this.name);
                                $("#cbIsRepresentative", table).prop('checked', false);
                                $("#cbDelete", table).prop('checked', false);
                                $("#imagesContainer").append(table).append("<br />");
                                table = $("#imagesContainer li").eq(0).clone(true);
                            });
                        }

                        function handleFileSelect(files) {
                            // getPicture(files);

                            console.log("HandleFileSelect json is running")
                            $('#uplImageInfo').empty();
                            for (var i = 0; i < files.length; i++) {
                                $span = $("<span class='label label-info'></span>").text(files[i].name);
                                $('#uplImageInfo').append($span);
                                $('#uplImageInfo').append("<br />");
                                console.log(files);
                                console.log(files[0].name)
                                //  OnSuccess(files)
                            }

                        }


                    </script>
                </div>
            </div>
            <hr />




        </div>




        <!--  Tag repeater -->

        <div class="col-md-6">
            <asp:Repeater ID="repTags" runat="server">
                <HeaderTemplate>
                    <ul class="list-group">
                </HeaderTemplate>
                <ItemTemplate>

                    <li class="list-group-item">
                        <asp:HiddenField runat="server" ID="hidTagId" Value='<%# Eval("ID")   %>' />
                        <asp:Label runat="server" ID="txtTagName" Text='<%# Eval("Name") %>' />
                        <asp:LinkButton runat="server" ID="btnDeleteTag" CssClass="btn" OnClick="btnDeleteTag_Click">
                            <span class="glyphicon glyphicon-trash"></span>
                        </asp:LinkButton>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <!--  Image repeater -->
        <div class="col-md-6 ">

            <div id="imagesContainer">

                <asp:Repeater ID="repApartmentPictures" runat="server">
                    <ItemTemplate>
                        <div class="form-group">
                            <div class="row">
                                <asp:HiddenField runat="server" ID="hidApartmentPictureId" Value='<%# Eval("ID") %>' />
                                <div class="col-md-3">
                                    <a href="<%# Eval("Path") %>">
                                        <asp:Image ID="imgApartmentPicture" runat="server" CssClass="img-thumbnail" ImageUrl='<%# Eval("Path") %>' Width="420px" />
                                    </a>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtApartmentPicture" runat="server" CssClass="form-control" Text='<%# Eval("Name") %>' placeholder="Naziv" />
                                    </div>
                                    <div class="form-group">
                                        <label class="btn btn-success">
                                            Glavna slika
                              <asp:CheckBox ID="cbIsRepresentative" runat="server" CssClass="is-representative-picture" Checked='<%# Eval("IsRepresentative") %>' />
                                        </label>
                                    </div>
                                    <div class="form-group">
                                        <label class="btn btn-danger">
                                            <span class="glyphicon glyphicon-trash"></span>
                                            <asp:CheckBox ID="cbDelete" runat="server" Checked="false" />
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <script>
                    $(function () {
                        var repPicCheckboxes = $(".is-representative-picture > input[type=checkbox]");
                        repPicCheckboxes.change(function () {
                            currentCheckbox = this;
                            if (currentCheckbox.checked) {
                                repPicCheckboxes.each(function () {
                                    otherCheckbox = this
                                    if (currentCheckbox != otherCheckbox && otherCheckbox.checked) {
                                        otherCheckbox.checked = false;
                                    }
                                })
                            }
                        });
                    })
                </script>
            </div>
        </div>



    </div>
    <div>
        <asp:LinkButton ID="lblSave" runat="server" Text="Spremi" CssClass="btn btn-primary" OnClick="lblSave_Click" />

        <asp:LinkButton ID="btnApartmentList" runat="server" Text="Natrag na listu" CssClass="btn btn-info" OnClick="btnApartmentList_Click" />
    </div>


</asp:Content>
