$(document).ready(function () {
   /* GetBudgetNameAll();*/

    $('#txtPeriodName').datepicker({
        language: 'en',
        view: 'months',
        minView: 'months',
        dateFormat: 'yyyy mm',
        autoClose: true,
        maxDate: new Date(),
        onSelect: function onSelect(fd, date) {
            //  $('#DatePick2').val(date);
            debugger
            $('#txtPeriodName').val(fd.replace(' ', ''));


            getData_PeriodList($('#txtPeriodName').val());

        },
    }).data('datepicker'); 

    $('#txtPeriodName').on('change', function () {
        debugger

        $(this).val($(this).val().replace(' ', '')); // Remove non-numeric characters
    });  
    $('#txtPeriod').on('input', function () {
        $(this).val($(this).val().replace(/[^0-9]/g, '')); // Remove non-numeric characters
    }); 
    $("#txtPeriod").on("change",()=> {
        alert(this.val());
    });
});

function getData_PeriodList(i) {
    try {
        var obj = {
            "param1": i,
        }
        $.ajax({
            url: 'Frm_Requisition_Budget_Control.aspx/getData_PeriodList',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                var $tbl = document.getElementById("tbl");
                var rowCount = $tbl.rows.length;
                var tbody = $tbl.tBodies[0];
                while (tbody.firstChild) {
                    tbody.removeChild(tbody.firstChild);
                }
                var it = data.d;
                debugger
                $.each(it, function (index, element) {

                    console.log(element);
                    debugger
                    var newRow = $tbl.tBodies[0].insertRow();
                    var cellPerioldName = newRow.insertCell();
                    var cellBudgetName = newRow.insertCell();
                    var cellInitialBudgetAmount = newRow.insertCell();
                    var cellAdditionalBudgetAmount = newRow.insertCell();
                    var cellReduceBudgetAmount = newRow.insertCell();
                    var cellRequisitionUseAmount = newRow.insertCell();
                    var cellIssueUseAmount = newRow.insertCell();
                    var cellAvailableBudget = newRow.insertCell();
                    var cellRemark = newRow.insertCell();
                    var cellOperation = newRow.insertCell();


                    cellPerioldName.innerHTML = element.PerioldName;
                    cellBudgetName.innerHTML = element.BudgetName;
                    cellInitialBudgetAmount.innerHTML = element.InitialBudgetAmount;
                    cellAdditionalBudgetAmount.innerHTML = element.AdditionalBudgetAmount;
                    cellReduceBudgetAmount.innerHTML = element.ReduceBudgetAmount;
                    cellRequisitionUseAmount.innerHTML = element.RequisitionUseAmount;
                    cellIssueUseAmount.innerHTML = element.IssueUseAmount;
                    cellAvailableBudget.innerHTML = element.AvailableBudget;
                    cellRemark.innerHTML = element.Remark;
                    cellOperation.innerHTML = "<a href='Frm_Requisition_Budget_Operation.aspx?jobType=Edit&period=" + element.PerioldName + "&Budget=" + element.BudgetName +"' class='btn btn-sm small btn-secondary py-0' >Edit</a>";



                    cellInitialBudgetAmount.innerHTML = element.InitialBudgetAmount;
                    cellAdditionalBudgetAmount.innerHTML = element.AdditionalBudgetAmount;
                    cellReduceBudgetAmount.innerHTML = element.ReduceBudgetAmount;
                    cellRequisitionUseAmount.innerHTML = element.RequisitionUseAmount;
                    cellIssueUseAmount.innerHTML = element.IssueUseAmount;
                    cellAvailableBudget.innerHTML = element.AvailableBudget;

                    cellInitialBudgetAmount.classList.add("text-right", "text-success", "font-weight-bold");
                    cellAdditionalBudgetAmount.classList.add("text-right", "text-success", "font-weight-bold");
                    cellReduceBudgetAmount.classList.add("text-right", "text-danger", "font-weight-bold");
                    cellRequisitionUseAmount.classList.add("text-right", "text-danger", "font-weight-bold");
                    cellIssueUseAmount.classList.add("text-right", "text-danger", "font-weight-bold");
                    cellAvailableBudget.classList.add("text-right", "text-primary","font-weight-bold");


                    //cellLocatorID.innerHTML = element.LocatorID;
                    //cellOP.innerHTML = "<button class=\"btn btn-sm btn-danger\" value=\"" + element.LotNo + "\"  onclick=\"RemovedScan(this)\"  >x</button>";

                });

                //$("#lblCntLotScan").text($tbl.tBodies[0].rows.length);
                //$("#txtLotNoToTemp").val("");
                //$("#txtLotNoToTemp").focus();
            },
            error: function (ex) {

                alert('error! :' + ex.responseJSON.Message);
                $("#spError").html('error! :' + ex.responseJSON.Message);
                console.log(ex.responseJSON.Message);
                //$("#txtLotNoToTemp").val("");
                //$("#txtLotNoToTemp").focus();
            }
        });


    } catch (e) {

        alert('error! :' + ex);
        $("#spError").html('error! :' + ex);

    } 



};