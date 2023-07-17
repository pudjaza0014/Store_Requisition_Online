<%@ Page Title="" Language="C#" MasterPageFile="~/BoostrapSite.Master" AutoEventWireup="true" CodeBehind="Frm_Report_R.aspx.cs" Inherits="StoreRequisition.Page.Frm_Report_R" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script src="../Scripts/DataTables/jQuery-3.6.0/jquery-3.6.0.min.js"></script>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <%--<script src="../Scripts/Site/Requisition_List.js"></script>--%>
    <link href="../css/MMCT_Report.css" rel="stylesheet" />
    <script>
        //$('.numberonly').keypress(function (e) {
        //    var charCode = (e.which) ? e.which : event.keyCode
        //    if (String.fromCharCode(charCode).match(/[^0-9]/g))
        //        return false;
        //});
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid pt-md-1">
        <div class="card">
            <div class="card-header"><a class="btn btn-outline-dark" href="Frm_Requisition_List.aspx?jobType=ActOwner"><span class="ml-3 mr-3"><i class="fas fa-backward"></i> GO BACK</span></a> </div>
            <div class="card-body">
                <form id="form1" runat="server" style="width: 100%;">
                    <table style="width: 100%;" class="table table-borderless">
                        <tr>
                            <td class="auto-style11">&nbsp;</td>
                            <td class="auto-style6 text-center">
                                <strong>
                                    <asp:Label ID="Label43" runat="server" Font-Names="Century Gothic" Text="Store's Material Requisition" Font-Size="Large" CssClass="auto-style8" Font-Italic="False" Font-Overline="False" Font-Underline="True"></asp:Label>
                                </strong>
                            </td>
                            <td style="width: 25%;" class="auto-style6"></td>
                        </tr>
                        <tr>
                            <td colspan="2" rowspan="2">
                                <strong>
                                    <asp:Label ID="Label44" runat="server" Font-Names="Century Gothic" Text="Mektec Manufacturing Corporation" Font-Size="Medium" CssClass="auto-style8"></asp:Label>
                                </strong>
                            </td>
                            <td class="auto-style19">
                                <strong>
                                    <asp:Label ID="Label45" runat="server" Font-Names="Century Gothic" Text="Req Number :" Font-Size="Small"></asp:Label>
                                    &nbsp;</strong><asp:Label ID="vlRequisitionNumber" runat="server" Font-Names="Century Gothic" Font-Size="Small">2018007201</asp:Label>
                            </td>
                        </tr>
                        
                    </table>
                    <div class="row">
                        <div class="col-12">
                            <div class="auto-style29 text-center text-lg-right">
                                <%=getReqNumber()%>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 text-md-right  ">
                            <asp:Label ID="lbRequestDate27" runat="server" Font-Names="Century Gothic" Text="Issue Type : " Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="vlIssueType" runat="server" Font-Names="Century Gothic" Text="OS Issue" Font-Size="Small" Font-Bold="False"></asp:Label>
                        </div>
                        <div class="col-md-2  text-md-right">
                            <asp:Label ID="lbRequestDate21" runat="server" Font-Names="Century Gothic" Text="Delivery Status : " Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="vlDeliveryStation" runat="server" Font-Names="Century Gothic" Text="B15" Font-Size="Small" Font-Bold="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 text-md-right">
                            <asp:Label ID="lbRequestDate18" runat="server" Font-Names="Century Gothic" Text="Requisition :   " Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-2 ">
                            <asp:Label ID="vlLocation" runat="server" Font-Names="Century Gothic" Text="MMCT" Font-Size="Small" Font-Bold="False"></asp:Label>
                        </div>
                        <div class="col-md-2 text-md-right">
                            <asp:Label ID="lbRequestDate22" runat="server" Font-Names="Century Gothic" Text="Date  : " Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-2 ">
                            <asp:Label ID="vlRequestDate" runat="server" Font-Names="Century Gothic" Text="30/01/2018" Font-Size="Small" Font-Bold="False"></asp:Label>
                        </div>
                        <div class="col-md-2 text-md-right">
                            <asp:Label ID="lbRequestDate24" runat="server" Font-Names="Century Gothic" Text="Time : " Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-2 ">
                            <asp:Label ID="vlRequestTime" runat="server" Font-Names="Century Gothic" Text="30/01/2018" Font-Size="Small" Font-Bold="False"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 text-md-right">
                            <asp:Label ID="lbRequestDate19" runat="server" Font-Names="Century Gothic" Text="Request by : " Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-2 ">
                            <asp:Label ID="vlRequestBy" runat="server" Font-Names="Century Gothic" Text="B-446 Somjit" Font-Size="Small" Font-Bold="False" Font-Strikeout="False"></asp:Label>
                        </div>
                        <div class="col-md-2 text-md-right">
                            <asp:Label ID="lbRequestDate25" runat="server" Font-Names="Century Gothic" Text="Tel  : " Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-2 ">
                            <asp:Label ID="vlRequestTel" runat="server" Font-Names="Century Gothic" Text="B-446 Somjit" Font-Size="Small" Font-Bold="False" Font-Strikeout="False"></asp:Label>
                        </div>
                        <div class="col-md-2 text-md-right">
                            <asp:Label ID="lbRequestDate26" runat="server" Font-Names="Century Gothic" Text="Approve by : " Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-2 ">
                            <asp:Label ID="vlApprovedLicense" runat="server" Font-Names="Century Gothic" Text="____________" Font-Size="Small" Font-Bold="False"></asp:Label><br />
                            <asp:Label ID="vlApprovedBy" runat="server" Font-Names="Century Gothic" Text="____________" Font-Size="Small" Font-Bold="False"></asp:Label>
                            
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-2 text-md-right">
                            <asp:Label ID="lbRequestDate20" runat="server" Font-Names="Century Gothic" Text="Sub Inventory : " Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-2 ">
                             <asp:Label ID="vlSubInventory" runat="server" Font-Names="Century Gothic" Text="AS8-MP" Font-Size="Small" Font-Bold="False"></asp:Label>
                        </div>
                        <div class="col-md-2 text-md-right">
                          <asp:Label ID="Label1" runat="server" Font-Names="Century Gothic" Text="Current Status : "   Font-Size="Small" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-2 ">
                          <asp:Label ID="vStatus" runat="server" Font-Names="Century Gothic" Text="AS8-MP" Font-Size="Small" Font-Bold="False"></asp:Label>
                        </div>
                       
                    </div>
                     <div class="row">
                        <div class="col-md-2 text-md-right"> 
                        </div>
                        <div class="col-md-2 "> 
                        </div>
                        <div class="col-md-2 text-md-right"> 
                        </div>
                        <div class="col-md-2 "> 
                        </div>
                       
                    </div>
                     <div class="row pb-md-3">
                        <div class="col-md-2 text-md-right">
                              <asp:Label ID="lbRequestDate28" runat="server" Font-Names="Century Gothic" Text="Remark : " Font-Size="Larger" CssClass="font-weight-bold"></asp:Label>
                        </div>
                        <div class="col-md-10 ">
                           <asp:Label ID="vlRemark" runat="server" Font-Names="Century Gothic" Text="Testing New Web Store's Material Requisition" Font-Size="X-Large" Font-Bold="true" CssClass="text-danger"></asp:Label>
                        </div>
                       
                       
                    </div>
                    <%@ Register Src="~/Page/UserControls/ucRegularDetail.ascx" TagPrefix="uc1" TagName="ucRegularDetail" %>
                    <%@ Register Src="~/Page/UserControls/ucPickingCenters.ascx" TagPrefix="uc1" TagName="ucPickingCenters" %>
                    <%@ Register Src="~/Page/UserControls/ucSupvApproveds.ascx" TagPrefix="uc1" TagName="ucSupvApproveds" %>
                    <%@ Register Src="~/Page/UserControls/ucStoreIssue.ascx" TagPrefix="uc1" TagName="ucStoreIssue" %>
                    <%@ Register Src="~/Page/UserControls/ucUserReceived.ascx" TagPrefix="uc1" TagName="ucUserReceived" %>
                    <% string strAuthor = getAuthority();

                        if (strAuthor != "")
                        {
                            if (strAuthor == "approved")
                            { %>
                            <uc1:ucSupvApproveds runat="server" ID="ucSupvApproveds" />
                         <% }
                       else if (strAuthor == "issue")
                            { %>
                            <uc1:ucUserReceived runat="server" ID="ucUserReceived" />
                         <% }
                       else if (strAuthor == "packing")
                            { %>
                            <uc1:ucPickingCenters runat="server" ID="ucPickingCenters" />
                         <% }
                       else if (strAuthor == "store")
                            { %>
                            <uc1:ucStoreIssue runat="server" ID="ucStoreIssue" />
                         <% }
                        else
                            { %>
                            <uc1:ucRegularDetail runat="server" ID="ucRegularDetail" />
                         <% }
                        }
                        else
                        { %>
                            <uc1:ucRegularDetail runat="server" ID="ucRegularDetail1" />
                     <% } %>
                </form>
            </div>
            <div class="card-footer">
                <div class="row">
                    <div class="col-12 text-center">
                         <%--<a href="Frm_Requisition_List.aspx?jobType=ActOwner" class="btn btn-dark"><i class="fas fa-backward"></i> back</a>--%>
                        <a href="Frm_Report_R_Print.aspx?RequestNo=<%=getREQ_NUM()%>" class="btn btn-warning" target="_blank"><i class="fas fa-print"></i> Print</a>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
