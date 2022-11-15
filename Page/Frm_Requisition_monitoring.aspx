<%@ Page Title="" Language="C#" MasterPageFile="~/Bootstrap_M.Master" AutoEventWireup="true" CodeBehind="Frm_Requisition_monitoring.aspx.cs" Inherits="StoreRequisition.Page.Frm_Requisition_monitoring" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        body {
            background-color: #1B2430;
        }
    </style>
    <script src="../Scripts/DataTables/jQuery-3.6.0/jquery-3.6.0.min.js"></script>
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="../Scripts/Site/Requisition_M.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
      <%--  <div class="row">
             <div class=" col-12 "> 
               
           </div>
        </div>--%>
       <div class="row mb-2">  
           <div class="col-12">
                <div class="row" id="card_Process">
                </div>
           </div> 
       </div>

        <div class="row">
            <div class="col-12">
                
                        <table id="tbl" class="table table-hover text-center table-dark w-100 shadow-lg " style="font-size: 24px;background: rgb(1,27,56);background: radial-gradient(circle, rgba(1,27,56,1) 0%, rgba(0,6,13,1) 62%); border-collapse: collapse;  border-radius: 0.5em;  overflow: hidden;">
                        </table>
                    
            </div>
           <%-- <div class="col-12 col-lg-3">
                 <h1 class="text-white-50">Progress</h1>
                <div class="row" id="card_Process">
                </div>
            </div> --%>
        </div> 
    </div>
    
</asp:Content> 