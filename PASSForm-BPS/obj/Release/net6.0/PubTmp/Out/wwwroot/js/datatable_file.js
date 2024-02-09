
function loadPartial(btnid, inputid, chemistcode, brickValue, chemistpanelId) {
    ;
    var inputValue = $("#" + inputid).val();
    var fromDate = new Date($('#startdate').val());
    var toDate = new Date($('#enddate').val());
    var trackingid = document.getElementById('hcpid').value;

    ;
    var selectedMonthsAndYear = [];
    while (fromDate <= toDate) {
        selectedMonthsAndYear.push({
            month: fromDate.toLocaleString('default', { month: 'long' }),
            year: fromDate.getFullYear()
        });
        fromDate.setMonth(fromDate.getMonth() + 1);
    }
    $.ajax({
        url: "/ListView/_Accordion_PartialView", // Replace with your controller and action names
        method: "POST", // Use GET or POST based on your server's requirements
        data: { inputValue: inputValue, selectedMonthsAndYear: selectedMonthsAndYear, fromDate: fromDate, toDate: toDate, chemistId: chemistcode, brickValue: brickValue, trackingid: trackingid, chemistpanelId: chemistpanelId }, // Send the unique identifier as data
        success: function (data) {
            // Handle the server's response here
            $("#abc-" + btnid).html(data);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}

function enableSearchButton(inputElement, checkboxindex) {
    const submitButton = document.getElementById(`search-${checkboxindex}`);

    if (inputElement.value.trim() !== "") {
        submitButton.removeAttribute("disabled");
    } else {
        submitButton.setAttribute("disabled", "true");
    }
}


function getchembybrickcode() {


    var searchButtons = document.getElementsByClassName('searchbutton');
    if (searchButtons.length > 0) {
        var searchbutton = searchButtons[0];
        searchbutton.removeAttribute('disabled');
    }
    var selectedbrick = document.getElementById('selectedbrick').value;
    var parts = selectedbrick.split('-');

    // Extract the left part (the part before the '-')
    var leftValue = parts[0].trim();

    $.ajax({
        url: "/ListView/CreateAccordion", // Replace with your controller and action names
        method: "GET", // Use GET or POST based on your server's requirements
        data: { brickValue: leftValue }, // Send the unique identifier as data
        success: function (data) {
            document.getElementById('selectedchemist').innerHTML = data.macChemMappings;
            $('#tableAcc').empty();
            // Handle the server's response here
            // $("#tableAcc").html(data);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}

function SubmittedForm() {
    debugger;
    var Dis = document.getElementById("distributer");
    var selectedDistributorValue = Dis.value || "";
    var discode = selectedDistributorValue.split('-');
    var DistributorCode = discode.shift();
    var Brick = document.getElementById("selectedbrick");
    var selectedBrickValue = Brick.options[Brick.selectedIndex].value;
    var brickcode = selectedBrickValue.split('-');
    var MacroBrickCode = brickcode.shift();

    var HCPREQID = document.getElementById('hcpid').value;
    //var startDateInput = document.getElementById('startdatepost-0').value;
    //var endDateInput = document.getElementById('enddatepost-0').value;
    var Comment = document.getElementById('createcomments').value;
    var currentDate = new Date();
    var HeaderData = {

        DistributerCode: DistributorCode,
        MacroBrickCode: MacroBrickCode,
        FromDate: currentDate,
        ToDate: currentDate,
        HCPREQID: HCPREQID,
        trackingid: HCPREQID,
        Comments: Comment,
    };


    var Salesarr = [];
    var salesArrString;

    var chemistCount = $("#tableAcc").find("button").length;
    $("#tableAcc").find("button").each(function (index) {
        var buttonId = $(this).attr("id");
        var parts = buttonId.split('-');

        var button1rightPart = parts[1];

       /* for (var i = 0; i < chemistCount; i++) {*/


            var preContribution = document.getElementById("Contribution-" + button1rightPart).value;
            var div = document.getElementById("sku-" + button1rightPart);

            var table = div.querySelector("table");
            var thElements = table.getElementsByTagName("th");
            var thCount = thElements.length;

            var PreMon = (thCount - 6) / 2;

            var preactualfromDate = new Date($('#startdatepre-' + button1rightPart).val());
            var prefromDate = preactualfromDate.getFullYear() + '-' +
                (preactualfromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
                preactualfromDate.getDate().toString().padStart(2, '0');
            var preactualtoDate = new Date($('#enddatepre-' + button1rightPart).val());
            var pretoDate = preactualtoDate.getFullYear() + '-' +
                (preactualtoDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
                preactualtoDate.getDate().toString().padStart(2, '0');
            var preselectedMonthsAndYear = [];
            while (preactualfromDate <= preactualtoDate) {
                preselectedMonthsAndYear.push({
                    month: preactualfromDate.toLocaleString('default', { month: 'long' }),
                    year: preactualfromDate.getFullYear()
                });
                preactualfromDate.setMonth(preactualfromDate.getMonth() + 1);
            }

            var numberOfPreMonths = preselectedMonthsAndYear.length;
            var uniquePreYears = [...new Set(preselectedMonthsAndYear.map(item => item.year))];
            var numberOfPreYears = uniquePreYears.length;



            var postactualfromDate = new Date($('#startdatepost-' + button1rightPart).val());
            var postfromDate = postactualfromDate.getFullYear() + '-' +
                (postactualfromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
                postactualfromDate.getDate().toString().padStart(2, '0');
            var postactualtoDate = new Date($('#enddatepost-' + button1rightPart).val());
            var posttoDate = postactualtoDate.getFullYear() + '-' +
                (postactualtoDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
                postactualtoDate.getDate().toString().padStart(2, '0');

            ;
            var postselectedMonthsAndYear = [];
            while (postactualfromDate <= postactualtoDate) {
                postselectedMonthsAndYear.push({
                    month: postactualfromDate.toLocaleString('default', { month: 'long' }),
                    year: postactualfromDate.getFullYear()
                });
                postactualfromDate.setMonth(postactualfromDate.getMonth() + 1);
            }

            var numberOfPostMonths = postselectedMonthsAndYear.length;
            var uniquePostYears = [...new Set(postselectedMonthsAndYear.map(item => item.year))];
            var numberOfPreYears = uniquePostYears.length


            //var chemistCode = document.getElementById("C" + i + "-AccText" + i).innerText;
            var chemist = document.getElementById("chemist-" + button1rightPart).innerText;
            /*      var Contribution = document.getElementById("contribution-" + i).value;*/
            var chemcodeparts = chemist.split('-');
            var ChemistCode = chemcodeparts[0].trim();
            var productPreSkuCount = $("#tbl-pre-sku-" + button1rightPart).find("tr").length;
            var productPostCount = $("#tbl-post-" + button1rightPart).find("tr").length;
            var pretotal = document.getElementById("total-" + button1rightPart).value;
            var posttotal = document.getElementById("total-post-" + button1rightPart).value;
            var discountpercentage = document.getElementById("discountpercentage-pre-" + button1rightPart).value;
            var discountpostcentage = document.getElementById("discountpercentage-post-" + button1rightPart).value;
            var totalroipercentage = document.getElementById("totalroipercentage").value;
            var bpspercentage = document.getElementById("bpspercentage").value;
            var grandtotal = document.getElementById("grandtotal").value;

            var prdArr = [];

            //Products For Pre Sales Against Each Chemist
            for (var j = 0; j < productPreSkuCount; j++) {

                var productName = document.getElementById("sku-pre-product-" + button1rightPart + "-" + j).innerHTML;
                var productCode = document.getElementById("sku-pre-pakcode-" + button1rightPart + "-" + j).innerHTML;
                var postproductDescription = document.getElementById("sku-pre-description-" + button1rightPart + "-" + j).innerHTML;
                /*            var preproductActualDiscount = document.getElementById("sku-pre-column-" + i + "-" + j).innerText;*/
                /*            var preproductContribution = document.getElementById("sku-pre-actdis-" + i + "-" + j).value;*/


                var salArr = [];
                for (var k = 0; k < PreMon; k++) {
                    var monthyearnames = document.getElementById("value-pre-coloum-" + (k + 3)).innerHTML;
                    var YearPart = monthyearnames.split(' ');
                    var Year = YearPart[0];
                    var Month = YearPart[1];

                    var skuSales = document.getElementById("sku-pre-column-" + button1rightPart + "-" + j + "-" + k).innerHTML;
                    var valueSales = document.getElementById("value-pre-column-" + button1rightPart + "-" + j + "-" + k).innerHTML;

                    salArr.push({ skuSales: skuSales, valueSales: valueSales, Year: Year, Month: Month });
                }
                prdArr.push({ ProductName: productName, productCode: productCode, postproductDescription: postproductDescription, Contribution: preContribution, SalesType: "Pre", Sale: salArr, pretotal: pretotal, discountpercentage: discountpercentage })
            }
            //Products For Post Sales Against Each Chemist
            for (var j = 0; j < productPostCount; j++) {

                var productName = document.getElementById("post-product-" + button1rightPart + "-" + j).innerText;
                var productCode = document.getElementById("post-pakcode-" + button1rightPart + "-" + j).innerText;
                var postproductDescription = document.getElementById("post-description-" + button1rightPart + "-" + j).innerText;
                //var postproductContribution = document.getElementById("sku-post-actdis-" + i + "-" + j).value;
                var salArr = [];
                for (var k = 0; k < numberOfPostMonths; k++) {
                    var monthyearnames = document.getElementById("value-post-coloum-" + button1rightPart + "-" + k).innerHTML;

                    // Remove brackets if they exist
                    monthyearnames = monthyearnames.replace('[', '').replace(']', '');

                    var parts = monthyearnames.split("/"); // Split the string at "/"
                    if (parts.length === 2) {
                        var PostMonth = parts[0];
                        var PostYear = parts[1];
                    } else {
                        console.log("Invalid input string format");
                    }


                    var skuSales = document.getElementById("sku-post-input-column-" + button1rightPart + "-" + j + "-" + k).value;
                    var valueSales = document.getElementById("value-post-input-column-" + button1rightPart + "-" + j + "-" + k).value;


                    salArr.push({ skuSales: skuSales, valueSales: valueSales, Month: PostMonth, Year: PostYear });
                }
                prdArr.push({ ProductName: productName, productCode: productCode, postproductDescription: postproductDescription, Contribution: preContribution, SalesType: "Post", Sale: salArr, posttotal: posttotal, discountpostcentage: discountpostcentage })
            }
            
            Salesarr.push({ ChemistCode: ChemistCode, ProductArr: prdArr, postfromDate: postfromDate, posttoDate: posttoDate, prefromDate: prefromDate, pretoDate: pretoDate, grandtotal: grandtotal, bpspercentage: bpspercentage, totalroipercentage: totalroipercentage });


            




        
    });
    var salesArrString = JSON.stringify(Salesarr);
        $.ajax({

            url: "/ListView/CreateBpsRecord", // Replace with the URL of your controller action
            method: "POST", // Use POST since you are sending data
            data: { Salesarr1: salesArrString, HeaderData1: HeaderData },
            success: function (data) {
                console.log("Data sent to server:", { Salesarr1: Salesarr, HeaderData1: HeaderData });

                if (data = true) {

                    /*var modelvisisble = document.getElementById('myModal').style.visibility = 'visible';*/
                    Swal.fire({
                        icon: "success",
                        title: 'Record Created Successfully!',
                        showConfirmButton: false,
                        timer: 3600,
                        width: 680,
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });

                } else {

                }
                setTimeout(function () {
                    window.location.href = "/ListView/ApprovedView/1"; // you can pass true to reload function to ignore the client cache and reload from the server
                }, 3500);

            },
            error: function (xhr, status, error) {
                console.error("Error:", status, error);
            }
        });

    
    
}

function UpdateSubmittedForm() {

    var Dis = document.getElementById("distributer");
    var selectedDistributorValue = Dis.value || "";
    var discode = selectedDistributorValue.split('-');
    var DistributorCode = discode.shift();
    var Brick = document.getElementById("selectedbrick").value;
 
    var brickcode = Brick.split('-');
    var MacroBrickCode = brickcode.shift();

    var HCPREQID = document.getElementById('hcpid').value;
    //var startDateInput = document.getElementById('startdatepost-0').value;
    //var endDateInput = document.getElementById('enddatepost-0').value;
/*    var Comment = document.getElementById('createcomments').value;*/
    var currentDate = new Date();
    var HeaderData = {

        DistributerCode: DistributorCode,
        MacroBrickCode: MacroBrickCode,
        FromDate: currentDate,
        ToDate: currentDate,
        HCPREQID: HCPREQID,
        trackingid: HCPREQID,
       /* Comments: Comment,*/
    };



    var Updatearr = [];
    var UpdatesalesArrString;

    var chemistCount = $("#tableAcc").find("button").length;
    $("#UpdateTableAcc").find("button").each(function (index) {
        var buttonId = $(this).attr("id");
        var parts = buttonId.split('-');

        var button1rightPart = parts[1];

        /* for (var i = 0; i < chemistCount; i++) {*/


        var preContribution = document.getElementById("Contribution-" + button1rightPart).value;
        var div = document.getElementById("sku-" + button1rightPart);

        var table = div.querySelector("table");
        var thElements = table.getElementsByTagName("th");
        var thCount = thElements.length;

        var PreMon = (thCount - 3);

        var preactualfromDate = new Date($('#startdatepre-' + button1rightPart).val());
        var prefromDate = preactualfromDate.getFullYear() + '-' +
            (preactualfromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
            preactualfromDate.getDate().toString().padStart(2, '0');
        var preactualtoDate = new Date($('#enddatepre-' + button1rightPart).val());
        var pretoDate = preactualtoDate.getFullYear() + '-' +
            (preactualtoDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
            preactualtoDate.getDate().toString().padStart(2, '0');
        var preselectedMonthsAndYear = [];
        while (preactualfromDate <= preactualtoDate) {
            preselectedMonthsAndYear.push({
                month: preactualfromDate.toLocaleString('default', { month: 'long' }),
                year: preactualfromDate.getFullYear()
            });
            preactualfromDate.setMonth(preactualfromDate.getMonth() + 1);
        }

        var numberOfPreMonths = preselectedMonthsAndYear.length;
        var uniquePreYears = [...new Set(preselectedMonthsAndYear.map(item => item.year))];
        var numberOfPreYears = uniquePreYears.length;



        var postactualfromDate = new Date($('#startdatepost-' + button1rightPart).val());
        var postfromDate = postactualfromDate.getFullYear() + '-' +
            (postactualfromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
            postactualfromDate.getDate().toString().padStart(2, '0');
        var postactualtoDate = new Date($('#enddatepost-' + button1rightPart).val());
        var posttoDate = postactualtoDate.getFullYear() + '-' +
            (postactualtoDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
            postactualtoDate.getDate().toString().padStart(2, '0');

        ;
        var postselectedMonthsAndYear = [];
        while (postactualfromDate <= postactualtoDate) {
            postselectedMonthsAndYear.push({
                month: postactualfromDate.toLocaleString('default', { month: 'long' }),
                year: postactualfromDate.getFullYear()
            });
            postactualfromDate.setMonth(postactualfromDate.getMonth() + 1);
        }

        var numberOfPostMonths = postselectedMonthsAndYear.length;
        var uniquePostYears = [...new Set(postselectedMonthsAndYear.map(item => item.year))];
        var numberOfPreYears = uniquePostYears.length


        //var chemistCode = document.getElementById("C" + i + "-AccText" + i).innerText;
        var chemist = document.getElementById("chemist-" + button1rightPart).innerText;
        /*      var Contribution = document.getElementById("contribution-" + i).value;*/
        var chemcodeparts = chemist.split('-');
        var ChemistCode = chemcodeparts[0].trim();
        var productPreSkuCount = $("#tbl-pre-sku-" + button1rightPart).find("tr").length;
        var productPostCount = $("#tbl-post-" + button1rightPart).find("tr").length;
        var pretotal = document.getElementById("total-" + button1rightPart).value;
        var posttotal = document.getElementById("total-post-" + button1rightPart).value;
        var discountpercentage = document.getElementById("discountpercentage-pre-" + button1rightPart).value;
        var discountpostcentage = document.getElementById("discountpercentage-post-" + button1rightPart).value;
        var totalroipercentage = document.getElementById("totalroipercentage").value;
        var bpspercentage = document.getElementById("bpspercentage").value;
        var grandtotal = document.getElementById("grandtotal").value;

        var prdArr = [];

        //Products For Pre Sales Against Each Chemist
        for (var j = 0; j < productPreSkuCount; j++) {

            var productName = document.getElementById("sku-pre-product-" + button1rightPart + "-" + j).innerHTML;
            var productCode = document.getElementById("sku-pre-pakcode-" + button1rightPart + "-" + j).innerHTML;
            var postproductDescription = document.getElementById("sku-pre-description-" + button1rightPart + "-" + j).innerHTML;
            /*            var preproductActualDiscount = document.getElementById("sku-pre-column-" + i + "-" + j).innerText;*/
            /*            var preproductContribution = document.getElementById("sku-pre-actdis-" + i + "-" + j).value;*/


            var salArr = [];
            for (var k = 0; k < PreMon; k++) {
                var monthyearnames = document.getElementById("value-pre-coloum-" + (k)).innerHTML;
                var YearPart = monthyearnames.split(' ');
                var Year = YearPart[0];
                var Month = YearPart[1];

                var skuSales = document.getElementById("sku-pre-column-" + button1rightPart + "-" + j + "-" + k).innerHTML;
                var valueSales = document.getElementById("value-pre-column-" + button1rightPart + "-" + j + "-" + k).innerHTML;

                salArr.push({ skuSales: skuSales, valueSales: valueSales, Year: Year, Month: Month });
            }
            prdArr.push({ ProductName: productName, productCode: productCode, postproductDescription: postproductDescription, Contribution: preContribution, SalesType: "Pre", Sale: salArr, pretotal: pretotal, discountpercentage: discountpercentage })
        }
        //Products For Post Sales Against Each Chemist
        for (var j = 0; j < productPostCount; j++) {

            var productName = document.getElementById("post-product-" + button1rightPart + "-" + j).innerText;
            var productCode = document.getElementById("post-pakcode-" + button1rightPart + "-" + j).innerText;
            var postproductDescription = document.getElementById("post-description-" + button1rightPart + "-" + j).innerText;
            //var postproductContribution = document.getElementById("sku-post-actdis-" + i + "-" + j).value;
            var salArr = [];
            for (var k = 0; k < numberOfPostMonths; k++) {
                var monthyearnames = document.getElementById("value-post-coloum-" + k).innerHTML;

                var YearPart = monthyearnames.split(' ');
                var PostYear = YearPart[0];
                var PostMonth = YearPart[1];
                // Remove brackets if they exist
                //monthyearnames = monthyearnames.replace('[', '').replace(']', '');

                //var parts = monthyearnames.split("/"); // Split the string at "/"
                //if (parts.length === 2) {
                //    var PostMonth = parts[0];
                //    var PostYear = parts[1];
                //} else {
                //    console.log("Invalid input string format");
                //}


                var skuSales = document.getElementById("sku-post-input-column-" + button1rightPart + "-" + j + "-" + k).value;
                var valueSales = document.getElementById("value-post-input-column-" + button1rightPart + "-" + j + "-" + k).value;


                salArr.push({ skuSales: skuSales, valueSales: valueSales, Month: PostMonth, Year: PostYear });
            }
            prdArr.push({ ProductName: productName, productCode: productCode, postproductDescription: postproductDescription, Contribution: preContribution, SalesType: "Post", Sale: salArr, posttotal: posttotal, discountpostcentage: discountpostcentage })
        }

        Updatearr.push({ ChemistCode: ChemistCode, ProductArr: prdArr, postfromDate: postfromDate, posttoDate: posttoDate, prefromDate: prefromDate, pretoDate: pretoDate, grandtotal: grandtotal, bpspercentage: bpspercentage, totalroipercentage: totalroipercentage });




    });



    var UpdatesalesArrString = JSON.stringify(Updatearr);
    $.ajax({
        url: "/ListView/UpdateBpsRecords",
        method: "POST",

        data: { Updatedatalist: UpdatesalesArrString, trackingid: HCPREQID, HeaderData1: HeaderData }, // Convert arr to JSON and send it as "datalist"
        success: function (data) {
            
            if (data = true) {
               /* var modelvisisble = document.getElementById('myModal').style.visibility = 'visible';*/
                Swal.fire({
                    icon: "success",
                    title: 'Record Updated Successfully!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
            } else {
       /*         var modelvisisble = document.getElementById('myModal').style.visibility = 'hidden';*/

            }
            setTimeout(function () {
                window.location.href = "/ListView/ApprovedView/1"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 3500);

        },
        error: function (xhr, status, error) {
            console.error("Error:", status, error);
        }

    });

}

function ApprovedForm() {
    ;
    var trackingid = document.getElementById("hcpid").value;

    $.ajax({
        url: "/ListView/Approved", // Replace with the URL of your controller action
        method: "POST", // Use POST since you are sending data
        //contentType: "application/json", // Set content type to JSON

        data: { trackingid: trackingid }, // Convert arr to JSON and send it as "datalist"
        success: function (data) {


        },
        error: function (xhr, status, error) {
            console.error("Error:", status, error);
        }
    });


}

function RejectedForm() {
    ;
    var trackingid = document.getElementById("hcpid").value;
    $.ajax({
        url: "/ListView/Rejected", // Replace with the URL of your controller action
        method: "POST", // Use POST since you are sending data
        //contentType: "application/json", // Set content type to JSON

        data: { trackingid: trackingid }, // Convert arr to JSON and send it as "datalist"
        success: function (data) {


        },
        error: function (xhr, status, error) {
            console.error("Error:", status, error);
        }
    });


}

function ObjectionForm() {
    ;
    var trackingid = document.getElementById("hcpid").value;
    var comments = document.getElementById("comments").value;
    $.ajax({
        url: "/ListView/Objection", // Replace with the URL of your controller action
        method: "POST", // Use POST since you are sending data
        //contentType: "application/json", // Set content type to JSON

        data: { trackingid: trackingid, comments: comments }, // Convert arr to JSON and send it as "datalist"
        success: function (data) {


        },
        error: function (xhr, status, error) {
            console.error("Error:", status, error);
        }
    });


}






function PreActivitySales() {
    ;
    var fromDate = new Date($('#startdatepre').val());
    var toDate = new Date($('#enddatepre').val());


    ;
    var preselectedMonthsAndYear = [];
    while (fromDate <= toDate) {
        preselectedMonthsAndYear.push({
            month: fromDate.toLocaleString('default', { month: 'long' }),
            year: fromDate.getFullYear()
        });
        fromDate.setMonth(fromDate.getMonth() + 1);
    }


}

var checkedItems;

function teamsChange(val) {



        var checkboxes = document.querySelectorAll(".dropdown-content input[type='checkbox']");
        checkedItems = [];
        var checkboxindex = 0;
        var html = '';

        var checkbox = null;
        var attrid;
        checkboxes.forEach(function (checkboxS) {
            if (checkboxS.checked && $('#tableAcc').find('[_id="' + checkboxS.id +'"]').length == 0) {

                checkbox = checkboxS.value
                attrid = checkboxS.id
                checkboxindex = checkboxS.id;
              //  checkedItems.push(checkbox.value);
               
            }
            if (checkboxS.checked) {
                
            }
            else {
                $('#tableAcc').find('[_id="' + checkboxS.id + '"]').remove();
                $('#tableAcc').find('[_idbtn="' + checkboxS.id + '"]').remove();
            }
           
        });


          //  if (existingHtml.indexOf(`id="chemist-${checkboxindex}"`) === -1) {
        if (checkbox != null) {
            html += `
  
 
    <button style="margin-top:2%;" id="chemist-${checkboxindex}" onclick="togglePanel(this)" _idbtn=${attrid} class="accordion">${checkbox}</button>
    <div class="panel" id="ChemistPanelID-${checkboxindex}" _id=${attrid} style="height:auto;">
        <p style="text-align: center; font-weight: bold; font-size: 20px;">Pre-Sales</p>
        <div style="text-align: center;">
            <div class="container">
              <div class="row">
                 <div class="col-4">
                    <p style="text-align:right; color: #d71921;">Sale Units</p>
                 </div>
                 <div class="col-4">
                    <div id="toggle-${checkboxindex}" class="toggle-switch" onclick="toggleSwitchAction(this,${checkboxindex})">
                      <div class="toggle-slider"></div>
                     </div>  
                 </div>
                 <div class="col-4">
                    <p style="text-align:left; color: #d71921;">Sale Values</p>
                 </div>
              </div>
            </div>
     
        </div>

        <div style="padding-top:15px; padding-left:15px;">
            <div class="container">
                <div class="row">
                    <div class="col-3">
                        <label style="font-weight:bold">Pre Activity Sales</label>
                    </div>
                    <div class="col-3">
                        <label>From:</label>
                        <input type="month" id="startdatepre-${checkboxindex}" value="" style="margin-left: 2%;" />
                    </div>
                    <div class="col-3">
                        <label>To:</label>
                        <input type="month" id="enddatepre-${checkboxindex}" value="" style="margin-left: 2%;" />
                    </div>
                    <div class="col-3">
                     <label>Contribution % :</label>
                      <input id="Contribution-${checkboxindex}"  type="number"  Style=" text-align:right;text-align: right; width: 30%; margin-left: 5%;"></br>
                      <div>
                         <input style="margin-top:2px;" id="search-${checkboxindex}" onclick="PreActivitySales(${checkboxindex})" type="button" class="searchbutton" value="Search"  />  </div>
    
                    </div>
                </div>
            </div>
        </div>
        <div id="pre-${checkboxindex}"> </div>

        <p style="text-align: center; font-weight: bold; font-size: 20px; margin-top:5%;">Post-Sales</p>
                <div style="text-align: center;">
            <div class="container">
              <div class="row">
                 <div class="col-4">
                    <p style="text-align:right; color: #d71921;">Sale Units</p>
                 </div>
                 <div class="col-4">
                    <div id="toggle-${checkboxindex}" class="toggle-switch" onclick="PosttoggleSwitchAction(this,${checkboxindex})">
                      <div class="toggle-slider"></div>
                     </div>  
                 </div>
                 <div class="col-4">
                    <p style="text-align:left; color: #d71921;">Sale Values</p>
                 </div>
              </div>
            </div>
     
        </div>
        <div style="padding-top:15px; padding-left:15px;">
            <div class="container">
                <div class="row">
                    <div class="col-3">
                        <label style="font-weight:bold">Post Activity Sales</label>
                    </div>
                    <div class="col-3">
                        <label>From:</label>
                        <input disabled type="month" id="startdatepost-${checkboxindex}" value="" style="margin-left: 2%;" />
                    </div>
                    <div class="col-3">
                        <label>To:</label>
                        <input disabled type="month" id="enddatepost-${checkboxindex}" value="" style="margin-left: 2%;" />
                    </div>
                    <div class="col-3">
                        <input style="" onclick="PostActivitySales(${checkboxindex})" type="button" class="searchbutton" value="Search" />
                                             
                    </div>
                </div>
            </div>
        </div>
        <div id="post-${checkboxindex}"></div>
    </div>`;


            $('#tableAcc').append(html);
        }




}


function editteamsChange(val) {

    var checkboxes = document.querySelectorAll(".dropdown-content input[type='checkbox']");
    checkedItems = [];
    var checkboxindex = 0;
    var html = '';

    var checkbox = null;
    var attrid;
    var chkid;
    checkboxes.forEach(function (checkboxS) {
        chkid = checkboxS.id.toString().replace("{", "").replace("}", "");
        if (checkboxS.checked && $('#UpdateTableAcc').find('[_id="' + chkid + '"]').length == 0) {

            checkbox = checkboxS.value
            attrid = chkid
            checkboxindex = chkid;
            //  checkedItems.push(checkbox.value);

        }
        if (checkboxS.checked) {

        }
        else {
            $('#UpdateTableAcc').find('[_id="' + chkid + '"]').remove();
            $('#UpdateTableAcc').find('[_idbtn="' + chkid + '"]').remove();
        }

    });


    //  if (existingHtml.indexOf(`id="chemist-${checkboxindex}"`) === -1) {
    if (checkbox != null) {
        html += `
  
 
    <button style="margin-top:2%;" id="chemist-${checkboxindex.toString().replace("{", "").replace("}", "")}" onclick="togglePanel(this)" _idbtn=${attrid.toString().replace("{", "").replace("}", "")} class="accordion">${checkbox.toString().replace("{", "").replace("}", "") }</button>
    <div class="panel" id="ChemistPanelID-${checkboxindex.toString().replace("{", "").replace("}", "")}" _id=${attrid.toString().replace("{", "").replace("}", "") } style="height:auto;">
        <p style="text-align: center; font-weight: bold; font-size: 20px;">Pre-Sales</p>
        <div style="text-align: center;">
            <div class="container">
              <div class="row">
                 <div class="col-4">
                    <p style="text-align:right; color: #d71921;">Sale Units</p>
                 </div>
                 <div class="col-4">
                    <div id="toggle-${checkboxindex.toString().replace("{", "").replace("}", "")}" class="toggle-switch" onclick="toggleSwitchAction(this,${checkboxindex.toString().replace("{", "").replace("}", "") })">
                      <div class="toggle-slider"></div>
                     </div>  
                 </div>
                 <div class="col-4">
                    <p style="text-align:left; color: #d71921;">Sale Values</p>
                 </div>
              </div>
            </div>
     
        </div>

        <div style="padding-top:15px; padding-left:15px;">
            <div class="container">
                <div class="row">
                    <div class="col-3">
                        <label style="font-weight:bold">Pre Activity Sales</label>
                    </div>
                    <div class="col-3">
                        <label>From:</label>
                        <input type="month" id="startdatepre-${checkboxindex.toString().replace("{", "").replace("}", "")}" value="" style="margin-left: 2%;" />
                    </div>
                    <div class="col-3">
                        <label>To:</label>
                        <input type="month" id="enddatepre-${checkboxindex.toString().replace("{", "").replace("}", "") }" value="" style="margin-left: 2%;" />
                    </div>
                    <div class="col-3">
                     <label>Contribution % :</label>
                      <input id="Contribution-${checkboxindex.toString().replace("{", "").replace("}", "") }"  type="number"  Style=" text-align:right;text-align: right; width: 30%; margin-left: 5%;"></br>
                      <div>
                         <input style="margin-top:2px;" id="search-${checkboxindex.toString().replace("{", "").replace("}", "")}" onclick="EDITPreActivitySales(${checkboxindex.toString().replace("{", "").replace("}", "") })" type="button" class="searchbutton" value="Search"  />  </div>
    
                    </div>
                </div>
            </div>
        </div>
        <div id="pre-${checkboxindex.toString().replace("{", "").replace("}", "") }"> 
           

        
        </div>

        <p style="text-align: center; font-weight: bold; font-size: 20px; margin-top:5%;">Post-Sales</p>
                <div style="text-align: center;">
            <div class="container">
              <div class="row">
                 <div class="col-4">
                    <p style="text-align:right; color: #d71921;">Sale Units</p>
                 </div>
                 <div class="col-4">
                    <div id="toggle-${checkboxindex.toString().replace("{", "").replace("}", "")}" class="toggle-switch" onclick="PosttoggleSwitchAction(this,${checkboxindex.toString().replace("{", "").replace("}", "") })">
                      <div class="toggle-slider"></div>
                     </div>  
                 </div>
                 <div class="col-4">
                    <p style="text-align:left; color: #d71921;">Sale Values</p>
                 </div>
              </div>
            </div>
     
        </div>
        <div style="padding-top:15px; padding-left:15px;">
            <div class="container">
                <div class="row">
                    <div class="col-3">
                        <label style="font-weight:bold">Post Activity Sales</label>
                    </div>
                    <div class="col-3">
                        <label>From:</label>
                        <input disabled type="month" id="startdatepost-${checkboxindex.toString().replace("{", "").replace("}", "") }" value="" style="margin-left: 2%;" />
                    </div>
                    <div class="col-3">
                        <label>To:</label>
                        <input disabled type="month" id="enddatepost-${checkboxindex.toString().replace("{", "").replace("}", "") }" value="" style="margin-left: 2%;" />
                    </div>
                    <div class="col-3">
                        <input style="" onclick="EditPostActivitySales(${checkboxindex.toString().replace("{", "").replace("}", "") })" type="button" class="searchbutton" value="Search" />
                                             
                    </div>
                </div>
            </div>
        </div>
        <div id="post-${checkboxindex.toString().replace("{", "").replace("}", "") }"></div>
    </div>`;


        $('#UpdateTableAcc').append(html);
    }




}

function togglePanel(button) {
    button.classList.toggle("active");
    var panel = button.nextElementSibling;
    if (panel.style.maxHeight) {
        panel.style.maxHeight = null;
    } else {
        panel.style.maxHeight = panel.scrollHeight + "500px";
    }
}



function PreActivitySales(checkboxindex) {
    ;
    var precontribution = document.getElementById("Contribution-" + checkboxindex).value;
    var startdateenable = document.getElementById("startdatepost-" + checkboxindex);
    var enddateenable = document.getElementById("enddatepost-" + checkboxindex);
    if (startdateenable) {
        startdateenable.disabled = false;
        enddateenable.disabled = false;
    } else {
        console.error("Element not found: startdatepre-" + checkboxindex);
    }
    /*    var inputcontribution = document.getElementById('contribution-' + checkboxindex).value;*/
    var fromDate = new Date($("#startdatepre-" + checkboxindex).val());
    var formattedFromDate = fromDate.getFullYear() + '-' +
        (fromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        fromDate.getDate().toString().padStart(2, '0');
    var toDate = new Date($("#enddatepre-" + checkboxindex).val());
    var formattedtoDate = toDate.getFullYear() + '-' +
        (toDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        toDate.getDate().toString().padStart(2, '0');


    var selectedbrick = document.getElementById('selectedbrick').value;
    var parts = selectedbrick.split('-');
    var BrickValue = parts[0].trim();

    var TeamName = document.getElementById('teamNames').innerText;
    var chemistName = document.getElementById("chemist-" + checkboxindex).innerText;
    var Chemistparts = chemistName.split('-');
    var ChemistCode = Chemistparts[0].trim();

    ;
    var preselectedMonthsAndYear = [];
    while (fromDate <= toDate) {
        preselectedMonthsAndYear.push({
            month: fromDate.toLocaleString('default', { month: 'long' }),
            year: fromDate.getFullYear()
        });
        fromDate.setMonth(fromDate.getMonth() + 1);
    }

    $.ajax({
        url: "/ListView/PreAcc", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { inputParameter: preselectedMonthsAndYear, prefromDate: formattedFromDate, pretoDate: formattedtoDate, BrickValue: BrickValue, TeamName: TeamName, ChemistCode: ChemistCode, checkboxindex: checkboxindex, precontribution: precontribution }, // Send data to the controller
        success: function (response) {
            // Handle the response from the controller here

            $("#pre-" + checkboxindex).html(response);

        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });


}


function EDITPreActivitySales(checkboxindex) {
    ;
    var precontribution = document.getElementById("Contribution-" + checkboxindex).value;
    var startdateenable = document.getElementById("startdatepost-" + checkboxindex);
    var enddateenable = document.getElementById("enddatepost-" + checkboxindex);
    if (startdateenable) {
        startdateenable.disabled = false;
        enddateenable.disabled = false;
    } else {
        console.error("Element not found: startdatepre-" + checkboxindex);
    }
    /*    var inputcontribution = document.getElementById('contribution-' + checkboxindex).value;*/
    var fromDate = new Date($("#startdatepre-" + checkboxindex).val());
    var formattedFromDate = fromDate.getFullYear() + '-' +
        (fromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        fromDate.getDate().toString().padStart(2, '0');
    var toDate = new Date($("#enddatepre-" + checkboxindex).val());
    var formattedtoDate = toDate.getFullYear() + '-' +
        (toDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        toDate.getDate().toString().padStart(2, '0');


    var selectedbrick = document.getElementById('selectedbrick').value;
    var parts = selectedbrick.split('-');
    var BrickValue = parts[0].trim();

    var TeamName = document.getElementById('teamNames').innerText;
    var chemistName = document.getElementById("chemist-" + checkboxindex).innerText;
    var Chemistparts = chemistName.split('-');
    var ChemistCode = Chemistparts[0].trim();

    ;
    var preselectedMonthsAndYear = [];
    while (fromDate <= toDate) {
        preselectedMonthsAndYear.push({
            month: fromDate.toLocaleString('default', { month: 'long' }),
            year: fromDate.getFullYear()
        });
        fromDate.setMonth(fromDate.getMonth() + 1);
    }

    $.ajax({
        url: "/ListView/EditPreAcc", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { inputParameter: preselectedMonthsAndYear, prefromDate: formattedFromDate, pretoDate: formattedtoDate, BrickValue: BrickValue, TeamName: TeamName, ChemistCode: ChemistCode, checkboxindex: checkboxindex, precontribution: precontribution }, // Send data to the controller
        success: function (response) {
            // Handle the response from the controller here

            $("#pre-" + checkboxindex).html(response);

        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });


}

function PostActivitySales(checkboxindex) {


    var prefromDate = new Date($("#startdatepre-" + checkboxindex).val());

    var preformattedFromDate = prefromDate.getFullYear() + '-' +
        (prefromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        prefromDate.getDate().toString().padStart(2, '0');
    var pretoDate = new Date($("#enddatepre-" + checkboxindex).val());
    var preformattedtoDate = pretoDate.getFullYear() + '-' +
        (pretoDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        pretoDate.getDate().toString().padStart(2, '0');

    var fromDate = new Date($("#startdatepost-" + checkboxindex).val());
    /*    var formattedFromDate = document.getElementById("startdatepost-" + checkboxindex).value;*/
    var formattedFromDate = fromDate.getFullYear() + '-' +
        (fromDate.getMonth()).toString().padStart(2, '0') + '-' +
        fromDate.getDate().toString().padStart(2, '0');
    var toDate = new Date($("#enddatepost-" + checkboxindex).val());
    /*    var formattedtoDate = document.getElementById("enddatepost-" + checkboxindex).value;*/
    var formattedtoDate = toDate.getFullYear() + '-' +
        (toDate.getMonth()).toString().padStart(2, '0') + '-' +
        toDate.getDate().toString().padStart(2, '0');



    var selectedbrick = document.getElementById('selectedbrick').value;
    var parts = selectedbrick.split('-');
    var BrickValue = parts[0].trim();

    var TeamName = document.getElementById('teamNames').innerText;

    var chemistName = document.getElementById("chemist-" + checkboxindex).innerText;
    var Chemistparts = chemistName.split('-');
    var ChemistCode = Chemistparts[0].trim();



    ;
    var postselectedMonthsAndYear = [];
    while (fromDate <= toDate) {
        postselectedMonthsAndYear.push({
            month: fromDate.toLocaleString('default', { month: 'long' }),
            year: fromDate.getFullYear()
        });
        fromDate.setMonth(fromDate.getMonth() + 1);
    }

    $.ajax({
        url: '/ListView/PostAcc', // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { postinputParameter: postselectedMonthsAndYear, postfromDate: formattedFromDate, posttoDate: formattedtoDate, BrickValue: BrickValue, TeamName: TeamName, ChemistCode: ChemistCode, postcheckboxindex: checkboxindex, prefromDate: preformattedFromDate, pretoDate: preformattedtoDate }, // Send data to the controller
        success: function (response) {
            // Handle the response from the controller here
            $("#post-" + checkboxindex).html(response);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}
function EditPostActivitySales(checkboxindex) {


    var prefromDate = new Date($("#startdatepre-" + checkboxindex).val());

    var preformattedFromDate = prefromDate.getFullYear() + '-' +
        (prefromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        prefromDate.getDate().toString().padStart(2, '0');
    var pretoDate = new Date($("#enddatepre-" + checkboxindex).val());
    var preformattedtoDate = pretoDate.getFullYear() + '-' +
        (pretoDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        pretoDate.getDate().toString().padStart(2, '0');

    var fromDate = new Date($("#startdatepost-" + checkboxindex).val());
    /*    var formattedFromDate = document.getElementById("startdatepost-" + checkboxindex).value;*/
    var formattedFromDate = fromDate.getFullYear() + '-' +
        (fromDate.getMonth()).toString().padStart(2, '0') + '-' +
        fromDate.getDate().toString().padStart(2, '0');
    var toDate = new Date($("#enddatepost-" + checkboxindex).val());
    /*    var formattedtoDate = document.getElementById("enddatepost-" + checkboxindex).value;*/
    var formattedtoDate = toDate.getFullYear() + '-' +
        (toDate.getMonth()).toString().padStart(2, '0') + '-' +
        toDate.getDate().toString().padStart(2, '0');



    var selectedbrick = document.getElementById('selectedbrick').value;
    var parts = selectedbrick.split('-');
    var BrickValue = parts[0].trim();

    var TeamName = document.getElementById('teamNames').innerText;

    var chemistName = document.getElementById("chemist-" + checkboxindex).innerText;
    var Chemistparts = chemistName.split('-');
    var ChemistCode = Chemistparts[0].trim();



    ;
    var postselectedMonthsAndYear = [];
    while (fromDate <= toDate) {
        postselectedMonthsAndYear.push({
            month: fromDate.toLocaleString('default', { month: 'long' }),
            year: fromDate.getFullYear()
        });
        fromDate.setMonth(fromDate.getMonth() + 1);
    }

    $.ajax({
        url: '/ListView/EditPostAcc', // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { postinputParameter: postselectedMonthsAndYear, postfromDate: formattedFromDate, posttoDate: formattedtoDate, BrickValue: BrickValue, TeamName: TeamName, ChemistCode: ChemistCode, postcheckboxindex: checkboxindex, prefromDate: preformattedFromDate, pretoDate: preformattedtoDate }, // Send data to the controller
        success: function (response) {
            // Handle the response from the controller here
            $("#post-" + checkboxindex).html(response);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}
function toggleSwitchAction(element, checkboxindex) {
    ;
    // Toggle the "on" class to change the background color
    element.classList.toggle("on");

    // Get the slider element within the clicked toggle-switch div
    const toggleSlider = element.querySelector(".toggle-slider");

    // Move the slider by changing its transform property
    if (element.classList.contains("on")) {
        toggleSlider.style.transform = "translateX(26px)";
        document.getElementById("values-" + checkboxindex).style.display = "block";
        document.getElementById("sku-" + checkboxindex).style.display = "none";

    } else {
        toggleSlider.style.transform = "translateX(0)";
        document.getElementById("sku-" + checkboxindex).style.display = "block";
        document.getElementById("values-" + checkboxindex).style.display = "none";

    }
}


function PosttoggleSwitchAction(element, checkboxindex) {

    // Toggle the "on" class to change the background color
    element.classList.toggle("on");

    // Get the slider element within the clicked toggle-switch div
    const toggleSlider = element.querySelector(".toggle-slider");

    // Move the slider by changing its transform property
    if (element.classList.contains("on")) {
        toggleSlider.style.transform = "translateX(26px)";
        document.getElementById("postvalue-" + checkboxindex).style.display = "block";
        document.getElementById("postsku-" + checkboxindex).style.display = "none";

    } else {
        toggleSlider.style.transform = "translateX(0)";
        document.getElementById("postsku-" + checkboxindex).style.display = "block";
        document.getElementById("postvalue-" + checkboxindex).style.display = "none";

    }
}

/*1000 seperated function*/
function formatNumberWithCommas(number) {
    return number.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function CalculateBPSPercentage(bpspercentagebtnindex) {


  /*  var chemistCount = $("#tableAcc").find("button").length;*/

    var totalPreSum = 0;
    var totalPostSum = 0;
    var discountpercentageSum = 0;
    var discountpostcentageSum = 0;
    var trackingId = document.getElementById("hcpid").value;
        $("#tableAcc").find("button").each(function (index) {
            var buttonId = $(this).attr("id");
            var parts = buttonId.split('-');

            var button1rightPart = parts[1];

            var TotalPre = parseFloat(document.getElementById(`total-` + button1rightPart).value.replace(/,/g, '')) || 0;
            var TotalPost = parseFloat(document.getElementById(`total-post-` + button1rightPart).value.replace(/,/g, '')) || 0;
            var DiscountPercentagePost = parseFloat(document.getElementById(`discountpercentage-post-` + button1rightPart).value.replace(/,/g, '')) || 0;
            var DiscountPercentagePre = parseFloat(document.getElementById(`discountpercentage-pre-` + button1rightPart).value.replace(/,/g, '')) || 0;

            totalPreSum += TotalPre;
            totalPostSum += TotalPost;

            discountpercentageSum += DiscountPercentagePost;
            discountpostcentageSum += DiscountPercentagePre;


        });

    var GrandTotal = totalPreSum + totalPostSum;
    var discountpercentageSum = discountpercentageSum + discountpostcentageSum;
    var roundedTotalSum = Math.round(GrandTotal);
    var formattedTotalSum = formatNumberWithCommas(roundedTotalSum);

    var TotalValues = document.getElementById("grandtotal");
    TotalValues.value = formattedTotalSum;
    $.ajax({
        url: "/ListView/CalBPS", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { TrackingId: trackingId, Total: roundedTotalSum }, // Send data to the controller
        success: function (data) {

            var a = document.getElementById("bpspercentage");
            var bpspercentage = data.bpspercentage;
            a.value = bpspercentage.toFixed(2);
            var c = document.getElementById("totalroipercentage");
            var aValue = parseFloat(a.value) || 0;
            var roi = discountpercentageSum + aValue;

            c.value = roi;
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });

}

function EditCalculateBPSPercentage(editbpspercentagebtnindex) {



    var totalPreSum = 0;
    var totalPostSum = 0;
    var discountpercentageSum = 0;
    var discountpostcentageSum = 0;
    var trackingId = document.getElementById("hcpid").value;
    $("#UpdateTableAcc").find("button").each(function (index) {
        var buttonId = $(this).attr("id");
        var parts = buttonId.split('-');

        var button1rightPart = parts[1];

        var TotalPre = parseFloat(document.getElementById(`total-` + button1rightPart).value.replace(/,/g, '')) || 0;
        var TotalPost = parseFloat(document.getElementById(`total-post-` + button1rightPart).value.replace(/,/g, '')) || 0;
        var DiscountPercentagePost = parseFloat(document.getElementById(`discountpercentage-post-` + button1rightPart).value.replace(/,/g, '')) || 0;
        var DiscountPercentagePre = parseFloat(document.getElementById(`discountpercentage-pre-` + button1rightPart).value.replace(/,/g, '')) || 0;

        totalPreSum += TotalPre;
        totalPostSum += TotalPost;

        discountpercentageSum += DiscountPercentagePost;
        discountpostcentageSum += DiscountPercentagePre;


    });

    var GrandTotal = totalPreSum + totalPostSum;
    var discountpercentageSum = discountpercentageSum + discountpostcentageSum;
    var roundedTotalSum = Math.round(GrandTotal);
    var formattedTotalSum = formatNumberWithCommas(roundedTotalSum);

    var TotalValues = document.getElementById("grandtotal");
    TotalValues.value = formattedTotalSum;

    $.ajax({
        url: "/ListView/EditCalBPS", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { TrackingId: trackingId, Total: roundedTotalSum }, // Send data to the controller
        success: function (data) {
            var a = document.getElementById("bpspercentage");
            var bpspercentage = data.bpspercentage;
            a.value = bpspercentage.toFixed(2);
            var c = document.getElementById("totalroipercentage");
            var aValue = parseFloat(a.value) || 0;
            var roi = discountpercentageSum + aValue;

            c.value = roi;
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });


}

function calculateValue(index, row, column) {


    var inputValue = document.getElementById("sku-post-input-column-" + index + "-" + row + "-" + column).value;
    var unitprice = document.getElementById("post-UnitPrice-" + index + "-" + row).value;
    var totalInput1 = document.getElementById(`total-post-` + index).value.replace(',', '') || 0;
    var total = 0;


    if (!isNaN(inputValue)) {

        var valueInputClass = 'value-post-input-column-' + index + "-" + row + "-" + column; // Corresponding "Values" input class
        var valueInput = document.getElementById(valueInputClass); // Find the corresponding "Values" input field
        if (!isNaN(valueInput.value)) {
            
            totalInput1 = (parseFloat(totalInput1) - parseFloat(valueInput.value || 0).toFixed(2)).toFixed(2);
        }
        if (valueInput) {
            var calculatedValue = parseFloat(parseFloat(inputValue || 0).toFixed(2) * parseFloat(unitprice)).toFixed(2); // Divide by 2
            valueInput.value = calculatedValue;
        }
    }
    var value = document.getElementById("value-post-input-column-" + index + "-" + row + "-" + column).value;
    var totalInput = document.getElementById(`total-post-` + index);
    total = parseFloat(parseFloat(totalInput1) + parseFloat(value)).toFixed(2);
    var formattedTotalValue = formatNumberWithCommas(total);
    totalInput.value = formattedTotalValue;

}

function EditcalculateValue(index, row, coloum) {

    

    var inputValueId = 'sku-edit-post-row-' + index + '-' + row + '-' + coloum; // ID of the input field you want to read
    var valueInputId = 'value-edit-post-row-' + index + '-' + row + '-' + coloum; // ID of the input field where you want to display the result
    var valueUnitPrice = 'sku-edit-UnitPrice-post-row-' + index + '-' + row;
    // var unitprice = document.getElementById("post-UnitPrice-" + index + "-" + row).value; 
    var inputValue = document.getElementById(inputValueId);
    var valueInput = document.getElementById(valueInputId);
    var unitprice = document.getElementById(valueUnitPrice);


    var inputValueValue = parseFloat(inputValue.value);

    if (!isNaN(inputValueValue)) {
        // Perform the calculation, for example, divide by 2
        var calculatedValue = parseFloat(inputValueValue * parseFloat(unitprice.value)).toFixed(2);  //inputValueValue / 2;

        var a = calculatedValue;

        valueInput.value = a;
    }

    var inputElements = document.querySelectorAll('input[type="number"]');

    var editsum = 0;

    // Iterate through input elements and add their values to the sum
    for (var i = 0; i < inputElements.length; i++) {
        var inputValue = parseFloat(inputElements[i].value) || 0; // Parse the input value as a float
        editsum += inputValue;
    }
    var EditformattedTotalValue = formatNumberWithCommas(editsum);

    // Display the sum

    var totalInput = document.getElementById(`total-post-` + index);

    totalInput.value = EditformattedTotalValue;

}

function edittoggleSwitchAction(element, toggle) {

    element.classList.toggle("on");

    // Get the slider element within the clicked toggle-switch div
    const toggleSlider = element.querySelector(".toggle-slider");

    // Move the slider by changing its transform property
    if (element.classList.contains("on")) {
        toggleSlider.style.transform = "translateX(26px)";
        document.getElementById("value-" + toggle).style.display = "block";
        document.getElementById("sku-" + toggle).style.display = "none";

    } else {
        toggleSlider.style.transform = "translateX(0)";
        document.getElementById("sku-" + toggle).style.display = "block";
        document.getElementById("value-" + toggle).style.display = "none";

    }
}

function editposttoggleSwitchAction(element, toggle) {

    // Toggle the "on" class to change the background color
    element.classList.toggle("on");

    // Get the slider element within the clicked toggle-switch div
    const toggleSlider = element.querySelector(".toggle-slider");

    // Move the slider by changing its transform property
    if (element.classList.contains("on")) {
        toggleSlider.style.transform = "translateX(26px)";
        document.getElementById("postvalue-" + toggle).style.display = "block";
        document.getElementById("postsku-" + toggle).style.display = "none";

    } else {
        toggleSlider.style.transform = "translateX(0)";
        document.getElementById("postsku-" + toggle).style.display = "block";
        document.getElementById("postvalue-" + toggle).style.display = "none";

    }


}

function TrackingIDDetails() {

    var TrackingId = document.getElementById("traciddetail").value;
    $.ajax({
        url: "/ListView/TrackingIDStatusDetails", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { TrackingId: TrackingId }, // Send data to the controller
        success: function (response) {

            $("#trackingidstatusdetail").html(response);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });


}

function getComments(worklistId) {

    $.ajax({
        url: '/ListView/TrackingIDStatusComments', // Adjust the URL according to your route
        type: 'POST',
        data: { id: worklistId }, // Pass the worklistId as a parameter
        success: function (data) {
            var modelVisible = data.comments.length > 0 || data.filepath.length > 0;

            $('#myModalComments').toggleClass('visible', modelVisible);

            if (modelVisible) {
                var commentsHtml = '';
                if (data.comments) {
                    data.comments.forEach(function (comment) {
                        commentsHtml += '<p>' + comment + '</p>';
                    });
                }
                var filePathsTable = $('#filePathsTable tbody');
                filePathsTable.empty(); // Clear existing rows

                if (data.filePath) {

                    data.filePath.forEach(function (filePath) {
                        // Assuming you have a function named 'downloadFile' to handle the button click
                        var rowHtml = '<tr><td>' + filePath + '</td><td><button style="background:white;" onclick="downloadFile(\'' + filePath + '\')">Download</button></td></tr>';
                        filePathsTable.append(rowHtml);
                    });
                }



                $('#myModalComments .modalcomments-body').html(commentsHtml);
                $('#myModalComments .modalfilepath-body').show();
            }
        },
        error: function (error) {
            // Handle the error response here
            console.error(error);
        }
    });
}

function downloadFile(filePath) {
    ;
    var link = document.createElement('a');
    link.href = filePath;
    link.download = filePath.split('\\').pop();
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

//=========== (Start) Hassam ===========
function downloadTemplate(pathId) {
    
    $.ajax({
        url: '/ListView/GetTemplateByPathId',
        type: 'GET',
        data: { pathId },
        success: function (responseData) {
            $("#showChooseFile").show();
            let downloadURL = responseData.path + responseData.name
            let fileDownload = document.createElement("a");
            fileDownload.download = responseData.name;
            fileDownload.href = downloadURL;
            fileDownload.click();
        }
    });
}


//=========== (End) Hassam ===========

function BpsApproval() {

    var fileInput = document.getElementById('files');
    var wlstid = document.getElementById('wlstid').value;
    var comments = document.getElementById("comments").value;
    var trackingid = document.getElementById("tracid").value;

    if (comments == "") {
        document.getElementById('comments').classList.add("error-field");
        document.getElementById('CommentsErrorLabel').classList.remove("hide");
    }
    else {


        var formData = new FormData();

        if (fileInput != null) {
            // Append each selected file to the FormData object
            for (var i = 0; i < fileInput.files.length; i++) {
                formData.append('Files', fileInput.files[i]);
            }
        }

        formData.append('WlstId', wlstid);
        formData.append('Comments', comments);
        formData.append('TrackingId', trackingid);
        $.ajax({
            url: "/ListView/BPSApproval", // Replace with your controller and action names
            type: 'POST', // Use GET or POST based on your server's requirements
            data: formData, // Send the entire FormData object
            contentType: false, // Required for sending FormData
            processData: false, // Required for sending FormData
            success: function (response) {

                if (data = true) {

                    //var modelvisisble = document.getElementById('myModalDetails').style.visibility = 'visible';
                    Swal.fire({
                        icon: "success",
                        title: 'Successfully Approved!',
                        showConfirmButton: false,
                        timer: 3600,
                        width: 680,
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });

                } else {
  /*                  var modelvisisble = document.getElementById('myModalDetails').style.visibility = 'hidden';*/
                }
                setTimeout(function () {
                    window.location.href = "/ListView/PendingView/2"; // you can pass true to reload function to ignore the client cache and reload from the server
                }, 3500);
            },
            error: function (xhr, status, error) {
                // Handle errors here
                console.error("Error:", status, error);
            }
        });
    }
}


function BpsReject() {

    var fileInput = document.getElementById('files');
    var wlstid = document.getElementById('wlstid').value;
    var comments = document.getElementById("comments").value;
    var trackingid = document.getElementById("tracid").value;

    var formData = new FormData();

    // Append each selected file to the FormData object
    for (var i = 0; i < fileInput.files.length; i++) {
        formData.append('Files', fileInput.files[i]);
    }

    formData.append('WlstId', wlstid);
    formData.append('Comments', comments);
    formData.append('TrackingId', trackingid);

    $.ajax({
        url: "/ListView/BPSReject", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: formData, // Send the entire FormData object
        contentType: false, // Required for sending FormData
        processData: false, // Required for sending FormData
        success: function (response) {

            if (data = true) {

                //var modelvisisble = document.getElementById('myModal').style.visibility = 'visible';
                Swal.fire({
                    icon: "success",
                    title: 'Objection Raised Successfully!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

            } else {
              /*  var modelvisisble = document.getElementById('myModal').style.visibility = 'hidden';*/
            }
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}



function BpsObjection() {

    var fileInput = document.getElementById('files');
    var wlstid = document.getElementById('wlstid').value;
    var comments = document.getElementById("comments").value;
    var trackingid = document.getElementById("tracid").value;
    if (comments == "") {
        document.getElementById('comments').classList.add("error-field");
        document.getElementById('CommentsErrorLabel').classList.remove("hide");
    }
    else {
        var formData = new FormData();

        if (fileInput != null) {
            // Append each selected file to the FormData object
            for (var i = 0; i < fileInput.files.length; i++) {
                formData.append('Files', fileInput.files[i]);
            }
        }

        formData.append('WlstId', wlstid);
        formData.append('Comments', comments);
        formData.append('TrackingId', trackingid);

        $.ajax({
            url: "/ListView/BPSObjection", // Replace with your controller and action names
            type: 'POST', // Use GET or POST based on your server's requirements
            data: formData, // Send the entire FormData object
            contentType: false, // Required for sending FormData
            processData: false, // Required for sending FormData
            success: function (data) {

                if (data = true) {

                /*    var modelvisisble = document.getElementById('myModalDetails').style.visibility = 'visible';*/
                    Swal.fire({
                        icon: "error",
                        title: 'Objection Raised Successfully!',
                        showConfirmButton: false,
                        timer: 3600,
                        width: 680,
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });

                } else {
    /*                var modelvisisble = document.getElementById('myModalDetails').style.visibility = 'hidden';*/
                }

                setTimeout(function () {
                    window.location.href = "/ListView/PendingView/2"; // you can pass true to reload function to ignore the client cache and reload from the server
                }, 3500);
            },
            error: function (xhr, status, error) {
                // Handle errors here
                console.error("Error:", status, error);
            }
        });
    }
}




var inputTimer;


function delayedAction(input, checkboxIndex, packcodeIndex, skucoloumIndex) {
    //
    // Clear the previous timer
    clearTimeout(inputTimer);

    // Set a new timer to wait for 500 milliseconds (adjust as needed)
    inputTimer = setTimeout(function () {
        // Perform the action after the delay
        updateActualDiscount(input, checkboxIndex, packcodeIndex, skucoloumIndex);
    }, 500);
}
function updateActualDiscount(input, checkboxIndex, packcodeIndex, skucoloumIndex) {

    //;

    var inputValue = input.value;

    var packCode = document.getElementById("sku-pre-pakcode-" + checkboxIndex + "-" + packcodeIndex).innerHTML;
    var chemist = document.getElementById("chemist-" + checkboxIndex).innerHTML;
    var multiplier = inputValue;
    var chemnameparts = chemist.split('-');
    var ClientCode = chemnameparts[0].trim();
    var startdatepres = new Date($('#startdatepre-' + checkboxIndex).val());
    var enddatepres = new Date($('#enddatepre-' + checkboxIndex).val());
    var sdate = startdatepres.toLocaleString();
    var edate = enddatepres.toLocaleString();


    $.ajax({
        url: "/ListView/UpdateBPSNew", // Replace with your controller and action names
        method: "Post", // Use GET or POST based on your server's requirements
        data: { packCode, ClientCode, sdate, edate, multiplier }, // Send the unique identifier as data
        success: function (data) {

            var noval = 0;
            var inc = 0
            var currgetrow = $('#' + "sku-pre-actdis-" + checkboxIndex + "-" + packcodeIndex).parent().parent();
            var cellCount = currgetrow.children().length;
            var rowData = data[0];
            for (var k = 4; k < cellCount; k++) {
                var columnHeader = document.getElementById("sku-pre-coloum-" + k).innerHTML;
                if (columnHeader != "PackCode" || columnHeader != "Pack Code") {
                    if (columnHeader in rowData) {
                        var value = data[0][columnHeader];
                        var currsku = document.getElementById("sku-pre-column-" + checkboxIndex + "-" + packcodeIndex + "-" + inc);
                        currsku.innerHTML = value;
                    }
                    else {
                        var currsku = document.getElementById("sku-pre-column-" + checkboxIndex + "-" + packcodeIndex + "-" + inc);
                        currsku.innerHTML = noval;
                    }
                }
                inc++;
                console.log(rowData);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error:", status, error);
        }
    });

    $.ajax({
        url: "/ListView/UpdateBPSValues", // Replace with your controller and action names
        method: "Post", // Use GET or POST based on your server's requirements
        data: { packCode, ClientCode, sdate, edate, multiplier }, // Send the unique identifier as data
        success: function (data) {

            var noval = 0;
            var inc = 0
            var currgetrow = $('#' + "sku-pre-actdis-" + checkboxIndex + "-" + packcodeIndex).parent().parent();
            var cellCount = currgetrow.children().length;
            var rowData = data[0];
            for (var k = 4; k < cellCount; k++) {
                var columnHeader = document.getElementById("value-pre-coloum-" + k).innerHTML;
                if (columnHeader != "PackCode" || columnHeader != "Pack Code") {
                    if (columnHeader in rowData) {
                        var value = data[0][columnHeader];
                        var currsku = document.getElementById("value-pre-column-" + checkboxIndex + "-" + packcodeIndex + "-" + inc);
                        currsku.innerHTML = value;
                    }
                    else {
                        var currsku = document.getElementById("value-pre-column-" + checkboxIndex + "-" + packcodeIndex + "-" + inc);
                        currsku.innerHTML = noval;
                    }
                }
                inc++;
                console.log(rowData);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error:", status, error);
        }
    });
}
function findRowByPackCode(table, packCode) {
    for (var i = 1; i < table.rows.length; i++) { // Start from 1 to skip the header row
        var row = table.rows[i];
        var packCodeCell = row.cells[0]; // Assuming PackCode is in the first column

        if (packCodeCell.innerText.trim() === packCode) {
            return row;
        }
    }

    return null; // Return null if the row is not found
}

function BpsASMActivity() {
    ;
    var fileInput = document.getElementById('asmfile');
    var trackingid = document.getElementById("tracid").value;



    var formData = new FormData();

    if (fileInput != null) {
        // Append each selected file to the FormData object
        for (var i = 0; i < fileInput.files.length; i++) {
            formData.append('Files', fileInput.files[i]);
        }
    }
    formData.append('TrackingId', trackingid);
    $.ajax({
        url: "/ListView/ASMActivity", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: formData, // Send the entire FormData object
        contentType: false, // Required for sending FormData
        processData: false, // Required for sending FormData
        success: function (response) {
            if (data = true) {

          /*      var modelvisisble = document.getElementById('myModalDetails').style.visibility = 'visible';*/
                Swal.fire({
                    icon: "success",
                    title: 'Activity Performed Successfully!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

            } else {
                var modelvisisble = document.getElementById('myModalDetails').style.visibility = 'hidden';
            }
            setTimeout(function () {
                window.location.href = "/ListView/ApprovedView/1"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 3500);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });

}

//let asmFilesToUpload = [];
//$("#asmfile").on('change', function (event) {
//    document.getElementById("filesList").innerHTML = "";
//    for (let i = 0; i < event.target.files.length; i++) {

//        let file = event.target.files[i];
//        let fileId = "FID" + (1000 + Math.random() * 9000).toFixed(0);

//        asmFilesToUpload.push({
//            file: file,
//            FID: fileId
//        });

//    }
//    displayASMFiles();
//})


//function displayASMFiles() {
//    for (let i = 0; i < asmFilesToUpload.length; i++) {
//        document.getElementById("filesList").innerHTML += `
//        <li>
//            <div class="row my-2">
//                <div class="col-lg-9">${asmFilesToUpload[i].file.name}</div>
//            </div>
//        </li>
//        `;
//    }
//}

function BpsPoNumber() {
    ;
    var ponum = document.getElementById("ponum").value;
    var trackingid = document.getElementById("tracid").value;
    $.ajax({
        url: "/ListView/PONumberActivity", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { ponum: ponum, trackingid: trackingid }, // Send the entire FormData object
        success: function (response) {
            if (data = true) {

/*                var modelvisisble = document.getElementById('myModalDetails').style.visibility = 'visible';*/
                Swal.fire({
                    icon: "success",
                    title: 'Activity Performed Successfully!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

            } else {
           /*     var modelvisisble = document.getElementById('myModalDetails').style.visibility = 'hidden';*/
            }
            setTimeout(function () {
                window.location.href = "/ListView/PendingView/2"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 3500);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}



$(document).ready(function () {
    $(document).ajaxStart(function () {
        $('.spinner').show();


    });
    $(document).ajaxComplete(function () {
        $('.spinner').hide();
    });

});

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

$(".Comma").each(function () {
    var amount = parseFloat($(this).val());
    if (!isNaN(amount)) {
        var newAmount = amount.toLocaleString('en-US');
        $(this).val(newAmount);
    }
});
