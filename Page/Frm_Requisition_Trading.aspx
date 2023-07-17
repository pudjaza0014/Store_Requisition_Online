<%@ Page Title="" Language="C#" MasterPageFile="~/BoostrapSite.Master" AutoEventWireup="true" CodeBehind="Frm_Requisition_Trading.aspx.cs" Inherits="StoreRequisition.Page.Frm_Requisition_Trading" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


    <link href="../Content/air_datepicker/air_datepickier.css" rel="stylesheet" />
    <script src="../Scripts/DataTables/jQuery-3.6.0/jquery-3.6.0.min.js"></script>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../Scripts/air-Datapicker/air_datepicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">

                <div class="card">
                    <div class="card-header"><a class="btn btn-outline-dark" href="Frm_Requisition_List.aspx?jobType=ActOwner"><span class="ml-3 mr-3"><i class="fas fa-backward"></i>GO BACK</span></a> </div>
                    <div class="card-body">
                        <div id="Div0" runat="server">
                            <fieldset>
                                <legend>Trading Requisition </legend>
                            </fieldset>
                        </div>
                        <form>
                        </form>
                        <form id="form2" runat="server">
                            <div class="form-group">
                                <div class="row mb-1">
                                    <div class="col-md-3">
                                        <div class="text-right">
                                            <div class="col">
                                                <label for="DatePick">Deu Date : </label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <input type="text" name="DatePick" id="DatePick" value="" placeholder="Click for choose" autocomplete="off" class="form-control" />

                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="DatePick2" name="DatePick2" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-plaintext"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="offset-md-3 col-md-3">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Generate" OnClick="Button1_Click" />
                                    </div>
                                </div>

                            </div>
                        </form>

                    </div>
                </div>
                <script>
                    $('#DatePick').datepicker({
                        language: 'en',
                        dateFormat: 'yyyy/mm/dd',
                        //timeFormat: 'hh:ii',
                        minView: 'days',
                        view: 'days',
                        // timepicker: true,
                        autoClose: false,
                        minDate: new Date(),
                        onSelect: function onSelect(fd, date) {
                          //  $('#DatePick2').val(date);
                            document.getElementById('<%=DatePick2.ClientID %>').value = $("#DatePick").val();
                        },
                    }).data('datepicker'); 

                    $('#datetimep').datepicker({
                        language: 'en',
                        dateFormat: 'yyyy/mm/dd',
                        //timeFormat: 'hh:ii',
                        minView: 'days',
                        view: 'days',
                        // timepicker: true,
                        autoClose: false,
                        minDate: new Date(),
                        onSelect: function onSelect(fd, date) {
                            //  $('#DatePick2').val(date);
                            document.getElementById('<%=DatePick2.ClientID %>').value = $("#DatePick").val();
                        },
                    }).data('datepicker'); 
                </script>
            </div>
        </div>
    </div>
</asp:Content>
