interval = window.setInterval(getData_M, 3000);

$(document).ready(function () {
    getData_M();
});

function getData_M() {
    try {

      //  var $tbl = $("#tbl");
        $.ajax({
            url: 'Frm_Requisition_monitoring.aspx/MonitorGetData',             
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                 
                if (data.d.ResultStatus == 'OK') {
                    data = data.d;
                    var ProcessGroup = data.processGroups;
                    var requis = data.requisition_Lists;
                    genCardProcess(ProcessGroup);
                    genDatatables(requis); 
                }  
            },
            error: function (ex) {
              //  alert('error!' + ex.responseText);
                console.log(ex.responseText);

                //await delay(5000);
                               
                setTimeout(getData_M, 10000);
              
            }
        });
         
    } catch (e) {
        //alert('error!' + e.responseText);
        console.log(e.responseText);
        return;
    }
};


function genCardProcess(data) {

    try {
        var $divs = $("#card_Process");

        $divs.empty();
        var strHtml = '';

        for (var i = 0; i < data.length; i++) {            
            var ii = data[i];
            strHtml += '            <div class="col-md-2 col-12  mb-1">'
           // strHtml += '                <div class="card border-left-primary shadow  text-light" style="background-color : #' + ii.processColor +'; opacity: 0.9;">'
           // strHtml += '                    <div class="card-body">'
           // strHtml += '                        <div class="row no-gutters align-items-center">'
           // strHtml += '                            <div class="col mr-md-2">'
           // strHtml += '                                <div class="text-xs font-weight-bold text-white text-uppercase mb-1">'
           // strHtml += '                                  <div class="h6">' + ii.ProcessName+'</div>'
           // strHtml += '                                </div>'
           //// strHtml += '                                <div class="h5 mb-0 font-weight-bold text-gray-800"></div>'
           // strHtml += '                            </div>'
           // strHtml += '                            <div class="col-auto"> '
           // strHtml += '                              <div class="h2">' + ii.RequisitionAmount+'</div>'
           // strHtml += '                            </div>'
           // strHtml += '                        </div>'
           // strHtml += '                    </div>'
           // strHtml += '                </div>'




            //strHtml += ' <button class="btn btn-block text-white" style="background-color : #' + ii.processColor + '; ">  <span class="small">' + ii.ProcessName + '</span>  <div class="h4">' + ii.RequisitionAmount +'</div> </button> '
//      ../images/iOS-7-dark-mod-iphone-wallpaper-ar72014.jpeg

            strHtml += ' <button class="btn btn-block" style="border-color:#' + ii.processColor + ';  color :#' + ii.processColor + '; background: rgb(13,40,77); background: linear-gradient(135deg, rgba(13,40,77,1) 70%, #' + ii.processColor + ' 70%);  box-shadow: 1px 1px 300px 1px #' + ii.processColor + ';" >  <span class="small">' + ii.ProcessName + '</span>  <div class="h3"> ' + ii.RequisitionAmount + '</span> </button> '
            strHtml += '            </div> '
             
        }
        $divs.append(strHtml);
        
    } catch (e) {
        alert('error!' + e.responseText);
        console.log(e.responseText);
    }
};

function genDatatables(data) {

    try {
        var $tbl = $("#tbl");
        $tbl.empty();
        $tbl.append('<thead class="text-nowrap" ><tr style="background: rgb(10,20,32);background: linear-gradient(135deg, rgba(10, 20, 32, 1) 3 %, rgba(14, 30, 48, 1) 44 %, rgba(15, 23, 32, 1) 100 %);"><th>#</th><th>Time</th><th>LOCATION</th><th>Transfer to</th><th>Delivery Station</th><th>RequisitionNo</th><th>Status</th> </tr></thead>');
        $tbl.append('<tbody class="table-striped" >')
        if (data.length > 0) {
            var newdata = data;
            var rows = [];            
            for (var i = 0; i < newdata.length; i++) { 
                var vpacking = newdata[i].PACKING; 
                rows.push('<tr style=" color: #' + newdata[i].PROCESS_COLORS + ';  " class="h2"><td> ' + (newdata[i].STATE_NEW == 'new' ? ' <kbd> ' + newdata[i].STATE_NEW + '</kbd> ' : '') + '</td><td>' + newdata[i].REQ_TIME + ' </td><td>' + newdata[i].LOCATION + '</td><td>' + newdata[i].TRANSFER_TO + '</td><td>' + newdata[i].DELIVERY_STATE + '</td><td>' + newdata[i].REQ_NUM+'</td><td style=" color: #' + newdata[i].PROCESS_COLORS + ' "><b>' + newdata[i].STATUS + '</b></td></tr>')
            }
            $tbl.append(rows.join(''));
        } else {
            $tbl.append('<tr><td colspan="6"><i class="far fa-folder"></i> data not found</td></tr>');
        }
        $tbl.append('</tbody>')
    } catch (e) {
        alert('error!' + e.responseText);
        console.log(e.responseText);
    }

}


