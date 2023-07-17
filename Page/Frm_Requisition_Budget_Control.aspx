<%@ Page Title="" Language="C#" MasterPageFile="~/BoostrapSite.Master" AutoEventWireup="true" CodeBehind="Frm_Requisition_Budget_Control.aspx.cs" Inherits="StoreRequisition.Page.Frm_Requisition_Budget_Control" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Content/air_datepicker/air_datepickier.css" rel="stylesheet" />

    <script src="../Scripts/DataTables/jQuery-3.6.0/jquery-3.6.0.min.js"></script>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../Scripts/code.jquery.com_ui_1.12.1_jquery-ui.js"></script>  
    <script src="../Scripts/air-Datapicker/air_datepicker.js"></script>
    <script src="../Scripts/Site/Requisition_Budget_Control.js"></script>
    <style>
        .ui-autocomplete {
            /*list-style: none;*/
            max-height: 200px;
            overflow-y: auto;
            background-color: #f2f2f2;
            border: 1px solid #ccc;
            width: fit-content;
            /* padding: 0;
            margin: 0;*/
        }

            .ui-autocomplete .ui-menu-item {
                padding: 5px 10px;
                padding-left: 0;
                /*width: max-content;*/
            }

                .ui-autocomplete .ui-menu-item:hover {
                    background-color: #e0e0e0;
                }

            .ui-autocomplete .ui-state-active {
                background-color: #c0c0c0;
            }


        div[role="status"] {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="col-12">
            <div id="Div0" runat="server">
                <fieldset>
                    <legend>Material Request Budget Control
                           <kbd class="bg-info h6 text-uppercase">
                               <asp:Label ID="lblpageName" runat="server" Text="Label"></asp:Label>
                           </kbd>
                    </legend>
                    <div class="row mt-1">
                        <div class="col">
                            <div class="row">
                                <div class="col">
                                    <a href="Frm_Requisition_Budget_Operation.aspx?jobType=New" class="btn btn-success"><i class="fas fa-plus"></i>New Period</a>
                                </div>
                                <div class="col-auto">
                                    <div class="form-group row">
                                        <label for="ddlLocation" class="col-sm-4 col-form-label text-right">Period Name</label>
                                        <div class="col-sm-8">
                                            <div class="input-group mb-3"> 
                                                <input type="text" id="txtPeriodName" class="form-control" maxlength="6" placeholder="choose period" /> 
                                            </div>
                                            <small id="emailHelp" class="form-text text-muted">Format Period (YYYYMM)</small>                                           
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                     <div class=" table-responsive" style="height: 70vh;"> 
                             <table id="tbl" class="table table-sm table-bordered small" style=" ">
                                 <thead class="text-center  thead-dark" >
                                      <tr class=" m-5 text-nowrap">
                                         <th rowspan="2">PERIODNAME</th>
                                         <th rowspan="2">BUDGETNAME</th>
                                         <th colspan="3">MANAGE BUDGET AMOUNT</th> 
                                         <th colspan="2">USE BUDGET AMOUNT</th>
                                         
                                         <th rowspan="2">AVAILABLE BUDGET</th>
                                         <th rowspan="2">REMARK</th>
                                         <th rowspan="2">Edit</th> 
                                     </tr>
                                     <tr>
                                         <th>INITIAL AMOUNT</th>
                                         <th>ADDITIONAL AMOUNT</th>
                                         <th>REDUCE AMOUNT</th>
                                         <th>REQUISITION</th>
                                         <th>ISSUE SLIP</th>
                                     </tr>
                                    <%-- <tr class=" m-5 text-nowrap">
                                         <th>PERIODNAME</th>
                                         <th>BUDGETNAME</th>
                                         <th>INITIALAMOUNT</th>
                                         <th>ADDITIONALAMOUNT</th>
                                         <th>REDUCEAMOUNT</th>
                                         <th>Requisition</th>
                                         <th>Issue</th>
                                         <th>Available Budget</th>
                                         <th>REMARK</th>
                                         <th>Edit</th> 
                                     </tr>--%>
                                 </thead>
                                 <tbody class="text-center">
                                     <tr><td colspan="10" class ="text-center">Please input period</td></tr>
                                 </tbody> 
                             </table>                                 
                            </div>
                </fieldset>
            </div>
        </div>

    </div>
</asp:Content>
