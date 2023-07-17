<%@ Page Title="" Language="C#" MasterPageFile="~/BoostrapSite.Master" AutoEventWireup="true" CodeBehind="Frm_Storage_Suggestion.aspx.cs" Inherits="StoreRequisition.Page.Frm_Storage_Suggestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="../Scripts/DataTables/jQuery-3.6.0/jquery-3.6.0.min.js"></script>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../Scripts/code.jquery.com_ui_1.12.1_jquery-ui.js"></script>
    <script src="../Scripts/Site/Storage_Suggestion.js"></script>
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
                width: max-content;
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

        .bg-wanted {
            background-color: #FAE392;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="">
        <div class="col-12">
            <div id="Div0" runat="server">
                <fieldset>
                    <legend>Store's Material Storage Suggestion
                           <kbd class="bg-info h6">
                               <asp:Label ID="lblpageName" runat="server" Text="Label"></asp:Label>
                           </kbd>
                        <button class="ml-lg-2 btn btn-outline-secondary" id="btnRefresh">refresh</button>
                    </legend>
                    <%--<form id="form1" runat="server" class="container-fluid container-lg">--%>


                    <div>
                        <%  
                            string autor = getAuthority();

                            switch (autor)
                            {
                                case "store":
                                case "isc":
                        %>
                        <div class="row">
                            <div class="col-12 col-sm-6">
                                <div class="form-group">
                                    <div class="row">
                                        <div class=" col-12 col-sm-5">
                                            <label for="txtLotNo">Lot number</label>
                                        </div>
                                        <div class="col-12 col-sm-7">
                                            <input id="txtLotNo" type="text" class="form-control form-control-sm" autocomplete="off" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-12 col-sm-5">
                                            <label for="txtInvNo">Invoice Number</label>
                                        </div>
                                        <div class="col-12 col-sm-7">
                                            <input id="txtInvNo" type="text" class="form-control form-control-sm disabled" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-12 col-sm-5">
                                            <label for="txtItemCode">Item Code</label>
                                        </div>
                                        <div class="col-12 col-sm-7">
                                            <input id="txtItemCode" type="text" class="form-control form-control-sm disabled" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-12 col-sm-5">
                                            <label for="txtTotal">Total</label>
                                        </div>
                                        <div class="col-12 col-sm-7">
                                            <input id="txtTotal" type="text" class="form-control form-control-sm disabled" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-12 col-sm-5">
                                            <label for="txtShelfPackCount">Shelf Pack Count</label>
                                        </div>
                                        <div class="col-12 col-sm-7">
                                            <input id="txtShelfPackCount" type="text" class="form-control form-control-sm disabled" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-12 col-sm-6">
                                
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-12 col-sm-5">
                                            <label for="txtShelfName">Shelf Name</label>
                                        </div>
                                        <div class="col-12 col-sm-7">
                                            <input id="txtShelfName" type="text" class="form-control form-control-sm disabled" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-12 col-sm-5">
                                            <label for="txtShelfCap">Shelf Pack Capacity</label>
                                        </div>
                                        <div class="col-12 col-sm-7">
                                            <input id="txtShelfCap" type="text" class="form-control form-control-sm disabled" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-12 col-sm-5">
                                            <label for="txtCountToTemp">Scan to ST-TEMP</label>
                                        </div>
                                        <div class="col-12 col-sm-7">
                                            <input id="txtCountToTemp" type="text" class="form-control form-control-sm disabled" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-12 col-sm-5">
                                            <label for="txtScanToTemp">Total Count Pack</label>
                                        </div>
                                        <div class="col-12 col-sm-7">
                                            <input id="txtTotalCountPack" type="text" class="form-control form-control-sm disabled" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-12 col-sm-5">
                                            <label for="txtShelfStatus">Shelf Status</label>
                                        </div>
                                        <div class="col-12 col-sm-7">
                                            <input id="txtShelfStatus" type="text" class="form-control form-control-sm disabled" readonly="readonly" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <%
                                break;
                            case "warehouse":
                        %>
                        <%
                                break;
                            } %>                       
                        <div class="row">
                            <div class="col">
                                <div class="row pb-2 text-success">
                                    <div class=" col-12 ">
                                        <div class="row">
                                            <div class="col-12 col-sm text-right">
                                                <label for="txtSubFrom">SubInventoryForm</label>
                                            </div>
                                            <div class="col-12 col-sm">
                                                <input type="text" id="txtSubFrom" class="form-control form-control-sm bg-wanted" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 ">
                                        <div class="row">
                                            <div class="  col-12 col-sm text-right">
                                                <label for="txtLocatorForm">LocatorForm</label>
                                            </div>
                                            <div class="col-12 col-sm">
                                                <input type="text" id="txtLocatorForm" class="form-control form-control-sm " autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col">
                                <div class="row pb-2 text-danger">
                                    <div class=" col-12 ">
                                        <div class="row">
                                            <div class="col-12 col-sm text-right">
                                                <label for="txtSubTo">SubInventoryTo</label>
                                            </div>
                                            <div class="col-12 col-sm">
                                                <input type="text" id="txtSubTo" class="form-control form-control-sm bg-wanted" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-12 ">
                                        <div class="row">
                                            <div class="col text-right">
                                                <label for="txtLocatorTo">LocatorTo</label>
                                            </div>
                                            <div class="col-12 col-sm">
                                                <input type="text" id="txtLocatorTo" class="form-control form-control-sm bg-wanted" autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div><span id="spError" class="text-danger"></span></div> 
                        <div class="row">
                            <div class="offset-md-6 col-md-6 col">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 col-md">
                                <div class="form-group">
                                    <div class="row">

                                        <div class=" col-12  ">  </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row pb-2">
                            <% 
                                switch (autor)
                            {
                                case "store":
                                case "isc":
                            %>
                            <div class="col-12 pb-3 col-sm-6"> 
                                <div class="card">
                                    <div class="card-header">
                                        Lot list form invoice
                                    </div>
                                    <div class="card-body"> 
                                         <button class="btn  btn-secondary mb-1" id="btnSelectAll"><i class="fas fa-check-double"></i> SelectAll</button>
                                        <div class="table-responsive " style="max-height: 70vh;">
                                            <table id="tbl" class="table table-sm table-striped table-hover bg-white text-center w-100 ">
                                                <thead class="thead-dark">
                                                    <tr class="table-info">
                                                        <%--<th>select</th>--%>
                                                        <th>No</th>
                                                        <th>Lot number</th>
                                                        <th>Qty</th>
                                                        <th>Expired</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <!-- Table rows will be added dynamically here -->
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="col-12 col-sm-6 pb-2">
                            <%
                                         break;

                                    default:
                                        %>


                                  <div class="col-12  py-1">
                                 <%
                                               break;
                                } %>
                           
                                 
                                <div class="card">
                                    <div class="card-header border border-primary">
                                        <div class="row">
                                            <div class="col-auto text-nowrap">
                                                <span class="d-none d-sm-block">Lot list for transfer</span>
                                                <span id="totalCount"></span>
                                            </div>
                                            <div class="col-12 col-sm ">
                                                <div class="form-group form-inline">
                                                    <span class="col-form-label">OK : </span>
                                                    <span id="lblCntOK" class="text-success">0</span>
                                                </div>
                                            </div>
                                            <div class="col-12 col-sm ">
                                                <div class="form-group form-inline">
                                                    <span class="col-form-label">NG : </span>
                                                    <span id="lblCntError" class="text-danger">0</span>
                                                </div>
                                            </div>
                                            <div class="col-12 col-lg text-right">
                                                <button class="btn btn-sm btn-danger mb-1" id="btndelError"><i class="far fa-trash-alt"></i>Delete error</button>
                                                <button class="btn btn-sm btn-primary mb-1" id="btnSubmit"><i class="fa fa-arrow-up" aria-hidden="true"></i>Transfer</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="input-group">
                                            <input id="txtLotNoToTemp" type="text" class="form-control bg-wanted" placeholder="Scan lotNo for transfer" />
                                            <div class="input-group-append">
                                               <button class="btn btn-secondary hide" >options </button>
                                            </div> 
                                        </div> 
                                        <div class="table-responsive" style="max-height: 70vh;">
                                            <table id="tbl_transfer" class="table table-sm table-striped bg-white text-center w-100 ">
                                                <thead class="thead-light">
                                                    <tr class="table-danger">
                                                        <th>No</th>
                                                        <th>Lot number</th>
                                                        <th>Qty</th>
                                                        <th>Expired</th>
                                                        <th>Locator</th> 
                                                        <th>Status</th> 
                                                        <th>OP</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <!-- Table rows will be added dynamically here -->
                                                </tbody>
                                            </table>
                                        </div>

                                    </div>
                                </div>
                                        
                            </div>
                        </div>
                    </div>
                    <%--</form>--%>
                </fieldset>
            </div>
        </div>
    </div>

</asp:Content>
