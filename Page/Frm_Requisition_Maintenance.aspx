<%@ Page Title="" Language="C#" MasterPageFile="~/BoostrapSite.Master" AutoEventWireup="true" CodeBehind="Frm_Requisition_Maintenance.aspx.cs" Inherits="StoreRequisition.Page.Frm_Requisition_Maintenance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">  
    <title>Request List</title>
    <style type="text/css">
        .auto-style1 {
            text-align: right;
        }

        .auto-style2 {
            text-align: center;
        }

        .auto-style4 {
            width: 60%;
            height: 26px;
        }

        .auto-style5 {
            width: 100%;
        }

        .auto-style6 {
            text-align: right;
            height: 26px;
        }

        .auto-style7 {
            color: rgb(255, 0, 0);
            font-size: xx-small;
        }

        #tbl > td {
            font-size: larger;
        }

    </style>
    <script src="../Scripts/DataTables/jQuery-3.6.0/jquery-3.6.0.min.js"></script>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../Scripts/Site/Requisition_List.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div id="Div0" runat="server">
                    <fieldset>
                        <legend>Store's Material Requisition List 
                            <label id="lbltest"></label>
                        </legend>
                        <div>
                            <div class="row pb-3">
                                <div class="col">                                   
                                    <a href="Frm_Requisition.aspx" class="btn btn-success"><i class="fas fa-plus"></i>New Requisition</a>
                                </div>
                                <div class="col-auto">
                                    <div class="form-group row">
                                        <label for="ddlLocation" class="col-sm-4 col-form-label">Location</label>
                                        <div class="col-sm-8">
                                            <select class="form-control" id="ddlLocation">
                                                <option value="">ALL</option>
                                                <option value="MMCT">MMCT</option>
                                                <option value="LF">LF</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class=" table-responsive" style="height: 70vh;">
                                <div class="">
                                    <table id="tbl" class="table table-striped bg-white text-center w-100">
                                    </table>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
