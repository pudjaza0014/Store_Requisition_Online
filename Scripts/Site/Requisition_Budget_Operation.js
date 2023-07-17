var itemsBudget;


$(document).ready(function () {
    try {


        GetBudgetNameAll();




        $('#txtPeriodName.datepiking').datepicker({
            language: 'en',
            view: 'months',
            minView: 'months',
            dateFormat: 'yyyy mm',
            autoClose: true,
            maxDate: new Date(),
            onSelect: function onSelect(fd, date) {
                $('#txtPeriodName').val(fd.replace(' ', ''));
                $('#txtPeriodName').removeClass("is-invalid");
                $("#spError").html('');
            },
        }).data('datepicker');
        $('#txtBudgetName').on("input", function () {
            var valuesG = $(this).val().toUpperCase();
            $(this).val(valuesG);

        });

        $("#btnSubmit_Period").on("click", () => {
            try {
                if ($('#txtPeriodName').val() == "") {
                    $('#txtPeriodName').addClass("is-invalid");
                    throw "กรุณากรอกข้อมูล Period ";
                }

                if ($('#txtBudgetName').val() == "") {
                    $('#txtBudgetName').addClass("is-invalid");
                    throw "กรุณากรอกข้อมูล BudgetName แล้วเลือกจาก List เพื่อความถูกต้อง ";
                }

                if (itemsBudget == null) {
                    $('#txtBudgetName').addClass("is-invalid");
                    throw "เกิดความผิดปกติบางอย่างเกี่ยวกับ BudgetName กรุณาปิดโปรแกรมแล้วลองใหม่อีกครั้ง";
                } else {
                    var citems = itemsBudget.length;
                    var item = $('#txtBudgetName').val();
                    var indexx = itemsBudget.indexOf(item);
                    debugger
                    if (indexx < 0) {
                        $('#txtBudgetName').addClass("is-invalid");
                        throw "BudgetName: " + item + " ไม่ถูกต้อง กรุณาตรวจสอบแล้ว ลองอีกครั้ง";
                    }
                }

                if ($('#txtBudgetAmt').val() == "") {
                    $('#txtBudgetAmt').addClass("is-invalid");
                    throw "กรุณากรอกข้อมูล Initial budget Amount ";
                }

                var PeriodName = $('#txtPeriodName').val();
                var BudgetName = $('#txtBudgetName').val();
                var BudgetAmt = $('#txtBudgetAmt').val();


                var result = confirm("โปรดยืนยันว่าคุณต้องการดำเนินการนี้โดยกดปุ่ม OK");
                if (result) {
                    submitNewData(PeriodName, BudgetName, BudgetAmt, null, null);
                }
                

                return;
            } catch (e) {
                alert('error! :' + e);
                $("#spError").html('error! :' + e);
            }

        });
        $("input").on("input", function () {
            var value = $(this).val();
            $(this).removeClass("is-invalid");
            $("#spError").html('');
            // Additional actions with $(this) can be performed here
        });
        var params = new URLSearchParams(window.location.search);
        var jobType = params.get("jobType");
        if (jobType != null && jobType == 'Edit') {
            var periodName = params.get("period");
            var BudgetName = params.get("Budget");
            getDataDetial(periodName, BudgetName);
        }

        $("#btnAddSubmit").on("click", () => {
            try {
                var choices = $('input[name="choices"]:checked').val().toUpperCase();
                if ($("#txtaddAmount").val() == "0" || $("#txtaddAmount").val() == "") {
                    return;
                }
                if ($("#txtAddReason").val() == "") {
                    $('#txtAddReason').addClass("is-invalid");
                    throw "กรุณากรอกเหตุผลในการทำ " + choices;
                }

                var addAmount = parseFloat($("#txtaddAmount").val().replace(/,/g, ''));
                var availAmt = parseFloat($("#txtDAvail_Amt").val().replace(/,/g, ''));

                console.log(addAmount, availAmt, $("#txtDAvail_Amt").val().replace(/,/g, ''))
                if (choices == "Reduce") {

                
                if (addAmount > availAmt) {
                    $('#txtaddAmount').addClass("is-invalid");
                    throw "จำนวนที่กรอกเกินจำนวนที่มี กรุณาตรวจสอบแล้วลองอีกครั้ง";
                    }
                } 
                var PeriodName = $('#txtDPeriodName').val();
                var BudgetName = $('#txtDBudgetName').val();
                var BudgetAmt = $('#txtaddAmount').val();
                var D_Remark = $('#txtAddReason').val();





                var result = confirm("โปรดยืนยันว่าคุณต้องการดำเนินการนี้โดยกดปุ่ม OK");
                if (result) {
                    submitNewData(PeriodName, BudgetName, BudgetAmt, choices, D_Remark);
                }
            } catch (e) {
                alert('error! :' + e);
                $("#spError").html('error! :' + e);
            }
        });

    } catch (e) {
        alert('error! :' + e);
        $("#spError").html('error! :' + e);
    }
});

function GetBudgetNameAll() {
    try {
        $.ajax({
            url: 'Frm_Requisition_Budget_Operation.aspx/getBudgetNameAll',
            data: null,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                var item = data.d;
                itemsBudget = item;
                $("#txtBudgetName").autocomplete({
                    source: item
                });
            },
            error: function (ex) {
                alert('error! :' + ex);
                $("#spError").html('error! :' + ex);
            }
        });
    } catch (e) {
        alert('error! :' + e)
        $("#spError").html('error! :' + e);
    }
}
function formatNumber(input) {
    try {
        calTotalBudget()
        var value = input.value.replace(/,/g, '').replace(/\D/g, '');
        value = value.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
        input.value = value;


    } catch (e) {
        alert('error! :' + e);
        $("#spError").html('error! :' + e);
    }
}


function formatNumberAdd(input) {
    try {

        var value = input.value.replace(/,/g, '').replace(/\D/g, '');
        value = value.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
        input.value = value;


    } catch (e) {
        alert('error! :' + e);
        $("#spError").html('error! :' + e);
    }
}
function calTotalBudget() {

    var initAmt = parseInt($("#txtBudgetAmt").val().replace(/,/g, '').replace(/\D/g, ''));
    var AddAmt = parseInt($("#txtAddAmt").val().replace(/,/g, '').replace(/\D/g, ''));
    var ReduceAmt = parseInt($("#txtReduceAmt").val().replace(/,/g, '').replace(/\D/g, ''));

    try {
        var values = initAmt + AddAmt - ReduceAmt;
        var value = values.toString().replace(/,/g, '').replace(/\D/g, '');
        value = value.replace(/\B(?=(\d{3})+(?!\d))/g, ',');
        $("#txtTotalBudget").val(value);
    } catch (e) {
        alert('error! :' + e);
        $("#spError").html('error! :' + e);
    }
};

function submitNewData(p1, p2, p3, p4 , p5) {
    try {
        var periodName = p1,
            budgetName = p2,
            budgetAmount = p3,
            Operation = p4
            D_remark = p5;

        debugger
        var obj = {
            "param1": periodName,
            "param2": budgetName,
            "param3": budgetAmount,
            "param4": Operation,
            "param5": D_remark,
        };


        $.ajax({
            url: 'Frm_Requisition_Budget_Operation.aspx/SaveNewData',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                var result = confirm("Updateข้อมูลสำเร็จ ");
                location.reload();
            },
            error: function (ex) {

                alert('error! :' + ex.responseJSON.Message);
                $("#spError").html('error! :' + ex.responseJSON.Message);
            }
        });
    } catch (e) {
        alert('error! :' + e);
        $("#spError").html('error! :' + e);
    }


}


function getDataDetial(periodName, BudgetName) {
    try {
        $("#spError").html('Data Loading...');

        var obj = {
            "PeriodName": periodName,
            "BudgetName": BudgetName,
        };
        $.ajax({
            url: 'Frm_Requisition_Budget_Operation.aspx/GetDataBudget_Detail',
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                debugger
                var budget = data.d;  
                var item = budget.Period;
                var itemtrans = budget.transection;


                debugger
                console.log(budget, item, itemtrans);
                 

                $("#txtDPeriodName").val(item.PerioldName);
                $("#txtDBudgetName").val(item.BudgetName);
                $("#txtDBudgetAmt").val(item.InitialBudgetAmount);
                $("#txtDAddAmt").val(item.AdditionalBudgetAmount);
                $("#txtDReduceAmt").val(item.ReduceBudgetAmount);
                $("#txtDTotalBudget").val(item.totalBudgetAmount);
                $("#txtDReqUseAmt").val(item.RequisitionUseAmount);
                $("#txtDIssUseAmt").val(item.IssueUseAmount);
                $("#txtDTotalUse").val(item.totaluseAmount);
                $("#txtDbudgetAvail").val(item.AvailableBudget);
                $("#txtDAvail_Amt").val(item.AvailableBudget);


                var $tbl = document.getElementById("tbl");
                var rowCount = $tbl.rows.length;
                var tbody = $tbl.tBodies[0];
                while (tbody.firstChild) {
                    tbody.removeChild(tbody.firstChild);
                }
                
                $.each(itemtrans, function (index, element) {
                    var newRow = $tbl.tBodies[0].insertRow(0);
                    var cellDocumentTransDate = newRow.insertCell();
                    var cellDocumentNumber = newRow.insertCell();
                    var cellAmount = newRow.insertCell();
                    var cellDocument_Type = newRow.insertCell();
                    var cellTransaction_Type = newRow.insertCell();
                    var cellAction = newRow.insertCell();
                    var cellReferDocumentNumber = newRow.insertCell();

                    cellDocumentTransDate.innerHTML = element.DocumentTransDate
                    cellDocumentNumber.innerHTML = element.DocumentNumber
                    cellAmount.innerHTML = element.Amount
                    
                    cellDocument_Type.innerHTML = element.Document_Type
                    cellTransaction_Type.innerHTML = element.Transaction_Type
                    cellAction.innerHTML = element.Action
                    cellReferDocumentNumber.innerHTML = element.ReferDocumentNumber 

                    var dang = element.Amount.indexOf('-') !== -1 ? "text-danger" : "text-success";
                    cellAmount.classList.add("text-right", dang, "font-weight-bold");
                     
                })


                $("#spError").html('');
            },
            error: function (ex) {

                alert('error! :' + ex.responseJSON.Message);
                $("#spError").html('error! :' + ex.responseJSON.Message);
            }
        });



    } catch (e) {
        throw e;
    }



}