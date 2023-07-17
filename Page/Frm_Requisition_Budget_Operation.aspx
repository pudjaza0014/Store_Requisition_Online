<%@ Page Title="" Language="C#" MasterPageFile="~/BoostrapSite.Master" AutoEventWireup="true" CodeBehind="Frm_Requisition_Budget_Operation.aspx.cs" Inherits="StoreRequisition.Page.Frm_Requisition_Budget_Operation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link href="../Content/air_datepicker/air_datepickier.css" rel="stylesheet" />
    <script src="../Scripts/DataTables/jQuery-3.6.0/jquery-3.6.0.min.js"></script>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../Scripts/code.jquery.com_ui_1.12.1_jquery-ui.js"></script>
    <script src="../Scripts/air-Datapicker/air_datepicker.js"></script>
    <script src="../Scripts/Site/Requisition_Budget_Operation.js"></script>

    <style>
        .ui-autocomplete {
            /*list-style: none;*/
            max-height: 200px;
            overflow-y: auto;
            /*background-color: #f2f2f2;*/
            background-color: #fff;
            border: 1px solid #ccc;
            width: fit-content;
            padding: 0 0 0 1vw;
            /*margin: 0;*/
            list-style-type: none;
        }

            .ui-autocomplete .ui-menu-item {
                padding: 5px 10px;
                padding-left: 0;
                /*width: max-content;*/
            }

                .ui-autocomplete .ui-menu-item:hover {
                    background-color: #c0c0c0;
                }

            .ui-autocomplete .ui-state-active {
                /*background-color: #c0c0c0;*/
                cursor: pointer;
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
                    <legend>Material Request Budget Operation
                           <kbd class="bg-info h6">
                               <asp:Label ID="lblpageName" runat="server" Text="Label"></asp:Label>
                           </kbd>
                    </legend>

                    <div class="row">
                        <% string Operation = getjobtype(); %>

                        <%  switch (Operation)
                            {
                                case "New":
                        %>
                        <div class="col-12 col-lg-5  " aria-disabled="true">
                            <div><span id="spError" class="text-danger"></span></div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtPeriodName">PERIOD NAME</label>
                                    </div>
                                    <div class="col-12 col-lg-6 ">
                                        <input id="txtPeriodName" type="text" class="datepiking form-control text-lg-right  " autocomplete="off" required />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtBudgetName">BUDGET NAME</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtBudgetName" type="text" class="form-control text-lg-right " autocomplete="off" required />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtBudgetAmt">INITIAL BUDGET AMOUNT</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtBudgetAmt" type="text" class="form-control text-right  " oninput="formatNumber(this)" autocomplete="off" required />
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtAddAmt">ADDITIONAL BUDGET AMOUNT</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtAddAmt" type="text" class="form-control  form-control-plaintext text-right" value="0" readonly autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtReduceAmt">REDUCE BUDGET AMOUNT</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtReduceAmt" type="text" class="form-control  form-control-plaintext text-right" value="0" readonly autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtTotalBudget" class="font-weight-bold">TOTAL BUDGET</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtTotalBudget" type="text" class="form-control  font-weight-bold form-control-plaintext text-right" value="0" disabled autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtReqUseAmt">REQUISITION USE AMOUNT</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtReqUseAmt" type="text" class="form-control  form-control-plaintext text-right" value="0" readonly autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtIssUseAmt">ISSUESLIP USE AMOUNT</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtIssUseAmt" type="text" class="form-control  form-control-plaintext text-right" value="0" readonly autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtTotalUse" class="font-weight-bold">TOTAL USE</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtTotalUse" type="text" class="form-control  font-weight-bold form-control-plaintext text-right" value="0" disabled autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <button class="btn btn-primary" id="btnSubmit_Period">submit</button>
                        </div>
                        <%
                                break;
                            case "Edit":
                        %>
                        <div class="col-12 col-sm-5  " aria-disabled="true">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtDPeriodName">PERIOD NAME</label>
                                    </div>
                                    <div class="col-12 col-lg-6 ">
                                        <input id="txtDPeriodName" type="text" class="form-control  form-control-plaintext text-lg-right" readonly autocomplete="off" required />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtDBudgetName">BUDGET NAME</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtDBudgetName" type="text" class="form-control  form-control-plaintext text-lg-right" readonly autocomplete="off" required />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtDBudgetAmt">INITIAL BUDGET AMOUNT</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtDBudgetAmt" type="text" class="form-control  form-control-plaintext text-right" readonly oninput="formatNumber(this)" autocomplete="off" required />
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtDAddAmt">ADDITIONAL BUDGET AMOUNT</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtDAddAmt" type="text" class="form-control  form-control-plaintext text-right" value="0" readonly autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtDReduceAmt">REDUCE BUDGET AMOUNT</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtDReduceAmt" type="text" class="form-control  form-control-plaintext text-right" value="0" readonly autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtDTotalBudget" class="font-weight-bold">TOTAL BUDGET</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtDTotalBudget" type="text" class="form-control  font-weight-bold form-control-plaintext text-right" value="0" disabled autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtDReqUseAmt">REQUISITION USE AMOUNT</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtDReqUseAmt" type="text" class="form-control  form-control-plaintext text-right" value="0" readonly autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtDIssUseAmt">ISSUESLIP USE AMOUNT</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtDIssUseAmt" type="text" class="form-control  form-control-plaintext text-right" value="0" readonly autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtDTotalUse" class="font-weight-bold">TOTAL USE</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtDTotalUse" type="text" class="form-control  font-weight-bold form-control-plaintext text-right" value="0" disabled autocomplete="off" />
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 col-lg-6">
                                        <label for="txtDbudgetAvail" class="font-weight-bold">BUDGET AVAILABLE</label>
                                    </div>
                                    <div class="col-12 col-lg-6">
                                        <input id="txtDbudgetAvail" type="text" class="form-control  font-weight-bold form-control-plaintext text-right" value="0" disabled autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                            <%--<button class="btn btn-primary">submit</button>--%>
                        </div>
                        <div class="col-12 col-sm offset-xl-3 ">
                            <div class="card">
                                <div class="card-header">
                                    Additional / Reduce Budget
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col">
                                            <div><span id="spError" class="text-danger"></span></div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-12 col-lg-5">
                                                        <label for="txtLotNo" class="font-weight-bold">Mode</label>
                                                    </div>
                                                    <div class="col-12 col-lg-7">
                                                        <div class="row">
                                                            <div class="col-12 ">
                                                                <input type="radio" id="option1" name="choices" value="Additional" checked>
                                                                <label for="option1">Additional</label>
                                                            </div>
                                                            <div class="col-12 ">
                                                                <input type="radio" id="option2" name="choices" value="Reduce">
                                                                <label for="option2">Reduce</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-12 col-lg-5">
                                                        <label for="txtDAvail_Amt" class="font-weight-bold text-nowrap">Avaiable Budget</label>
                                                    </div>
                                                    <div class="col-12 col-lg-7">
                                                        <input id="txtDAvail_Amt" type="text" class="form-control text-right font-weight-bold form-control-plaintext" disabled autocomplete="off" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-12 col-lg-5">
                                                        <label for="txtaddAmount" class="font-weight-bold">Amount</label>
                                                    </div>
                                                    <div class="col-12 col-lg-7">
                                                        <input id="txtaddAmount" type="text" oninput="formatNumberAdd(this)" class="form-control text-right" autocomplete="off" placeholder="0" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-12 col-lg-5">
                                                        <label for="txtAddReason" class="font-weight-bold">Reason</label>
                                                    </div>
                                                    <div class="col-12 col-lg-7">
                                                        <textarea id="txtAddReason" class="form-control" rows="3" placeholder="กรุณากรอกเหตุผลที่ทำการ Addition/reduce"></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                            <button class="btn btn-primary" id="btnAddSubmit">Submit</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="col-12">
                            <hr />
                            <h3>Tracnsection</h3>
                            <div class="row">
                                <div class="col-12">
                                    <div class="table-responsive">
                                        <table id="tbl" class="table table-sm table-bordered small" style="">
                                            <thead class="text-center  thead-dark">
                                                <tr> 
                                                    <th>Transaction Date</th>
                                                    <th>Document number</th>
                                                    <th>Amount</th>
                                                    <th>Document Type</th>
                                                    <th>Transaction Type</th>
                                                    <th>Action</th>
                                                    <th>Refer/remark</th>
                                                </tr>
                                            </thead>
                                            <tbody class="text-center">
                                                <tr>
                                                    <td colspan="10" class="text-center">Please input period</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%
                                break;
                            } %>
                    </div>
                </fieldset>

            </div>
        </div>
    </div>
</asp:Content>
