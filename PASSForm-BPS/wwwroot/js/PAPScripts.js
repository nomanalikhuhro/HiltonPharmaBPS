//$(document).ready(function () {

//    // Initialize select2
//    $("#distributer").select2();
//    $("#selectedbrick").select2();

//});



function nextButtonClick() {
   

    var reqid = document.getElementById('RequestId').value;
    var selectedOption = document.querySelector('input[name="options"]:checked');
    var selectedOptionValue = selectedOption ? selectedOption.value : null;

    if (selectedOptionValue !== null) {
        $.ajax({
            url: "/PAPView/Create", // Replace with your controller and action names
            method: "GET", // Use GET or POST based on your server's requirements
            data: { RequestId: reqid, PAPType: selectedOptionValue }, // Send the unique identifier as data
            success: function (data) {
                $('body').html(data);
            },
            error: function (xhr, status, error) {
                console.error("Error:", status, error);
            }
        });
    } else {

        Swal.fire({
            icon: "info",
            title: 'Select Options Available!!',
            showConfirmButton: false,
            timer: 1500,
            width: 380,
            allowOutsideClick: false,
            allowEscapeKey: false,
            customClass: {
                title: 'small-font',
                icon: 'small-icon'
            }
        });
    }
}



function getmacrobrickbydiscode() {
    var selecteddis = document.getElementById('distributer').value;
    var parts = selecteddis.split('-');

    // Extract the left part (the part before the '-')
    var leftValue = parts[0].trim();

    $.ajax({
        url: "/PAPView/GetMacrobrick", // Replace with your controller and action names
        method: "GET", // Use GET or POST based on your server's requirements
        data: { disValue: leftValue }, // Send the unique identifier as data
        success: function (data) {

            $('#selectedbrick').empty();

            // Add default option
            $('#selectedbrick').append($('<option>', {
                value: 'Select',
                text: 'Select'
            }));

            // Populate options with brick code and name
            $.each(data, function (index, item) {
                $('#selectedbrick').append($('<option>', {
                    value: item.macrobrickCode + ' - ' + item.macroBrickName,
                    text: item.macrobrickCode + ' - ' + item.macroBrickName
                }));
            });

        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}

var acc = document.getElementsByClassName("accordion");
var i;

for (i = 0; i < acc.length; i++) {
    acc[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var panel = this.nextElementSibling;
        if (panel.style.maxHeight) {
            panel.style.maxHeight = null;
        } else {
            panel.style.maxHeight = panel.scrollHeight + "px";

           
            
        }
    });
}






function getchembymacrobrickcode() {


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
        url: "/PAPView/GetChemist", // Replace with your controller and action names
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

function createaccordion() {
    var checkboxes = document.querySelectorAll(".dropdown-content input[type='checkbox']");

    checkboxes.forEach(function (checkboxS) {
        var checkbox = checkboxS.value;
        var attrid = checkboxS.id;
        var checkboxindex = checkboxS.id;

        // If the checkbox is checked and the corresponding accordion section doesn't exist, create it
        if (checkboxS.checked && $('#tableAcc').find('[_id="' + checkboxS.id + '"]').length == 0) {
            var html = `
                <button style="margin-top:2%;" id="chemist-${checkboxindex}" onclick="togglePanel(this)" _idbtn=${attrid} class="accordion">${checkbox}</button>
                <div class="panel" id="ChemistPanelID-${checkboxindex}" _id=${attrid} style="height:auto;">
                    <div style="padding-top: 5%; padding-bottom: 5%; padding-left: 3%;" id="pre-${checkboxindex}"></div>
                </div>`;
            $('#tableAcc').append(html);
            loadPartialView(checkboxindex);
        }
        // If the checkbox is unchecked, remove the corresponding accordion section
        else if (!checkboxS.checked) {
            $('#tableAcc').find('[_id="' + checkboxS.id + '"]').remove();
            $('#tableAcc').find('[_idbtn="' + checkboxS.id + '"]').remove();
        }
    });
}

function loadPartialView(checkboxindex) {
    var TeamName = document.getElementById('TeamName').innerHTML;
    var chemistName = document.getElementById("chemist-" + checkboxindex).innerText;
    var Chemistparts = chemistName.split('-');
    var ChemistCode = Chemistparts[0].trim();

    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            document.getElementById(`pre-${checkboxindex}`).innerHTML = xhr.responseText;
        }
    };

    var url = "/PAPView/PartialAcc"; // URL to the controller action

    xhr.open("POST", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");

    // Data to be sent to the server
    var data = JSON.stringify({
        TeamName: TeamName,
        ChemistCode: ChemistCode
    });

    xhr.send(data);
}

function togglePanel(button) {
    var panel = button.nextElementSibling;
    button.classList.toggle("active");
    if (panel.style.maxHeight) {
        panel.style.maxHeight = null;
    } else {
        panel.style.maxHeight = panel.scrollHeight + "px";
    }
}


function PostValCalculation(chemcode, proindex) {

    var inputValueId = 'PostUnits-' + chemcode + '-' + proindex; // ID of the input field you want to read
    var valueInputId = 'PostValue-' + chemcode + '-' + proindex; // ID of the input field where you want to display the result
    var valueUnitPrice = 'UnitPrice-' + chemcode + '-' + proindex;

    var inputValue = document.getElementById(inputValueId);
    var valueInput = document.getElementById(valueInputId);
    var unitprice = document.getElementById(valueUnitPrice);


    var inputValueValue = parseFloat(inputValue.value);

    if (isNaN(inputValueValue)) {
        var calculatedValue = parseFloat(parseFloat(inputValueValue || 0).toFixed(2) * parseFloat(unitprice.innerHTML)).toFixed(2);

        var a = calculatedValue;

        valueInput.value = a;
    } else {
        var calculatedValue = parseFloat(parseFloat(inputValueValue || 0).toFixed(2) * parseFloat(unitprice.innerHTML)).toFixed(2);

        var a = calculatedValue;

        valueInput.value = a;
    }

}

function CreatePAPBPS() {
    debugger;

    var isValid = validateForm(); // Validate the form

    if (!isValid) {
        return; // Stop further processing if form is invalid
    }


    var PAPType = document.getElementById("PAPType").value.trim();
    var Dis = document.getElementById("distributer");
    var selectedDistributorValue = Dis.value || "";
    var discode = selectedDistributorValue.split('-');
    var DistributorCode = discode.shift().trim();
    var Brick = document.getElementById("selectedbrick");
    var selectedBrickValue = Brick.options[Brick.selectedIndex].value;
    var brickcode = selectedBrickValue.split('-');
    var MacroBrickCode = brickcode.shift().trim();

    var ReqId = document.getElementById('papareqid').value.trim();
    var Comment = document.getElementById('createcomments').value;
    var DiscountType = document.getElementById("selecteddis");
    var selectedDiscountType = DiscountType.value || "";

    var preactualfromDate = new Date($('#startdate').val());
    var prefromDate = preactualfromDate.getFullYear() + '-' +
        (preactualfromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        preactualfromDate.getDate().toString().padStart(2, '0');
    var preactualtoDate = new Date($('#enddate').val());
    var pretoDate = preactualtoDate.getFullYear() + '-' +
        (preactualtoDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        preactualtoDate.getDate().toString().padStart(2, '0');
    var currentDate = new Date();
    var PAPHeaderData = {

        DistributerCode: DistributorCode,
        BrickCode: MacroBrickCode,
        DiscountDateFrom: prefromDate,
        DiscountDateTo: pretoDate,
        TrackingId: ReqId,
        DiscountType: selectedDiscountType,
        Remarks: Comment,
        CurrentDate: currentDate,

    }

    var PAPSalesarr = [];
    var salesArrString;

    var chemistCount = $("#tableAcc").find("button").length;
    $("#tableAcc").find("button").each(function (index) {

        var buttonId = $(this).attr("id");
        var parts = buttonId.split('-');
        var button1rightPart = parts[1];
        
        var chemist = document.getElementById("chemist-" + button1rightPart).innerText;
        var chemcodeparts = chemist.split('-');
        var ChemistCode = chemcodeparts[0].trim();
        var productPreSkuCount = $("#tbl-product-" + button1rightPart).find("tr").length;

        var prdArr = [];
        for (var j = 0; j < productPreSkuCount; j++) {
            var productName = document.getElementById("ProductName-" + button1rightPart + "-" + j).innerHTML;
            var productCode = document.getElementById("PackCode-" + button1rightPart + "-" + j).innerHTML
            var PreUnit = document.getElementById("PreUnits-" + button1rightPart + "-" + j).innerHTML;
            var PreValue = document.getElementById("PreValue-" + button1rightPart + "-" + j).innerHTML;
            var UnitPrice = document.getElementById("UnitPrice-" + button1rightPart + "-" + j).innerHTML;
            var EstimatedUnit = document.getElementById("PostUnits-" + button1rightPart + "-" + j).value;
            var EstimatedValue = document.getElementById("PostValue-" + button1rightPart + "-" + j).value;
            var Discount = document.getElementById("Discount-" + button1rightPart + "-" + j).value;
            var capp = document.getElementById("Capping-" + button1rightPart).value;

            prdArr.push({
                 PackCode: productCode,
                LastYearSKU: PreUnit, LastYearValue: PreValue,
                Discount: Discount, 
                ExpectedBusinessUnit: EstimatedUnit, ExpectedBusinessValue: EstimatedValue,
                UnitPrice: UnitPrice, Capping : capp
            })
        }

        PAPSalesarr.push({ChemistCode: ChemistCode, ProductArr: prdArr});
    });

    var PAPsalesArrString = JSON.stringify(PAPSalesarr);

    $.ajax({

        url: "/PAPView/CreatePAPBpsRecord", // Replace with the URL of your controller action
        method: "POST", // Use POST since you are sending data
        data: { PAPSalesarr: PAPsalesArrString, PAPHeaderData: PAPHeaderData },

        success: function (data) {
            

            if (data = true) {

                Swal.fire({
                    icon: "success",
                    title: 'Record Created Successfully!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        title: 'small-font',
                        icon: 'small-icon'
                    }
                });

            } else {

            }
            setTimeout(function () {
                window.location.href = "/PAPView/PharmaciesSubmitted"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 3500);

        },
        error: function (xhr, status, error) {
            console.error("Error:", status, error);
        }
    });
    
}

function CreatePAPIVInjectionBPS() {
    debugger;
    var isValid = validateFormIvInjection(); // Validate the form

    if (!isValid) {
        return; // Stop further processing if form is invalid
    }
 

    var PAPType = document.getElementById("PAPType").value;
  

    var ReqId = document.getElementById('papivreqid').value;
    var Team = document.getElementById('TeamName').innerHTML;


    var preactualfromDate = new Date($('#startdate').val());
    var prefromDate = preactualfromDate.getFullYear() + '-' +
        (preactualfromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        preactualfromDate.getDate().toString().padStart(2, '0');
    var preactualtoDate = new Date($('#enddate').val());
    var pretoDate = preactualtoDate.getFullYear() + '-' +
        (preactualtoDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        preactualtoDate.getDate().toString().padStart(2, '0');
    var currentDate = new Date();
    var PAPHeaderData = {


        DiscountFromDate: prefromDate,
        DiscountToDate: pretoDate,
        TrackingId: ReqId,
        CurrentDate: currentDate,

    }

    var PAPSalesarr = [];
    var salesArrString;

    var chemistCount = $("#tableAcc").find("button").length;


        var productPreSkuCount = $("#tableproducts").find("tr").length;

        var prdArr = [];
        for (var j = 0; j < productPreSkuCount; j++) {
            var productName = document.getElementById("ProductName-" + j).innerHTML;
            var productCode = document.getElementById("PackCode-" + j).innerHTML;

            var Discount = document.getElementById("Discount-" + j).value;
            var Capp = document.getElementById("EditCapping").value;


            prdArr.push({
                PackCode: productCode,
        
                Discount: Discount, Capping: Capp

            })
        }

    PAPSalesarr.push({ Team: Team, ProductArr: prdArr });


    var PAPsalesArrString = JSON.stringify(PAPSalesarr);

    $.ajax({

        url: "/PAPView/CreatePAPIvInjectionBpsRecord", 
        method: "POST", 
        data: { PAPIvInjectionSalesarr: PAPsalesArrString, PAPIvInjectionHeaderData: PAPHeaderData },

        success: function (data) {


            if (data = true) {

                Swal.fire({
                    icon: "success",
                    title: 'Record Created Successfully!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        title: 'small-font',
                        icon: 'small-icon'
                    }
                });

            } else {

            }
            setTimeout(function () {
                window.location.href = "/PAPView/IVInjectionSubmitted"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 3500);

        },
        error: function (xhr, status, error) {
            console.error("Error:", status, error);
        }
    });

}

function editaccordion(val) {

    var checkboxes = document.querySelectorAll(".dropdown-content input[type='checkbox']");
    checkedItems = [];
    var checkboxindex = 0;
    var html = '';

    var checkbox = null;
    var attrid;
    var chkid;
    checkboxes.forEach(function (checkboxS) {
        chkid = checkboxS.id.toString().replace("{", "").replace("}", "");
        if (checkboxS.checked && $('#UpdatePharmaciesTableAcc').find('[_id="' + chkid + '"]').length == 0) {

            checkbox = checkboxS.value
            attrid = chkid
            checkboxindex = chkid;
            //  checkedItems.push(checkbox.value);

        }
        if (checkboxS.checked) {

        }
        else {
            $('#UpdatePharmaciesTableAcc').find('[_id="' + chkid + '"]').remove();
            $('#UpdatePharmaciesTableAcc').find('[_idbtn="' + chkid + '"]').remove();
        }

    });


    //  if (existingHtml.indexOf(`id="chemist-${checkboxindex}"`) === -1) {
    if (checkbox != null) {
        html += `
  
 
  <button style="margin-top:2%;" id="chemist-${checkboxindex}" onclick="togglePanel(this)" _idbtn=${attrid} class="accordion">${checkbox}</button>
                <div class="panel" id="ChemistPanelID-${checkboxindex}" _id=${attrid} style="height:auto;">
                    <div style="padding-top: 5%; padding-bottom: 5%; padding-left: 3%;" id="pre-${checkboxindex}"></div>
                </div>`;


        $('#UpdatePharmaciesTableAcc').append(html);
        EditloadPartialView(checkboxindex);
    }






}
function EditloadPartialView(checkboxindex) {
    var TeamName = document.getElementById('TeamName').innerHTML;
    var chemistName = document.getElementById("chemist-" + checkboxindex).innerText;
    var Chemistparts = chemistName.split('-');
    var ChemistCode = Chemistparts[0].trim();

    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            document.getElementById(`pre-${checkboxindex}`).innerHTML = xhr.responseText;
        }
    };

    var url = "/PAPView/EditPartialAcc"; // URL to the controller action

    xhr.open("POST", url, true);
    xhr.setRequestHeader("Content-Type", "application/json");

    // Data to be sent to the server
    var data = JSON.stringify({
        TeamName: TeamName,
        ChemistCode: ChemistCode
    });

    xhr.send(data);
}

function EditPAPcalculateValue(chemcode, proindex) {

    var inputValueId = 'PostEditUnits-' + chemcode + '-' + proindex; // ID of the input field you want to read
    var valueInputId = 'PostEditValue-' + chemcode + '-' + proindex; // ID of the input field where you want to display the result
    var valueUnitPrice = 'EditUnitPrice-' + chemcode + '-' + proindex;

    var inputValue = document.getElementById(inputValueId);
    var valueInput = document.getElementById(valueInputId);
    var unitprice = document.getElementById(valueUnitPrice);


    var inputValueValue = parseFloat(inputValue.value);

    if (isNaN(inputValueValue)) {
        var calculatedValue = parseFloat(parseFloat(inputValueValue || 0).toFixed(2) * parseFloat(unitprice.innerHTML)).toFixed(2);

        var a = calculatedValue;

        valueInput.value = a;
    } else {
        var calculatedValue = parseFloat(parseFloat(inputValueValue || 0).toFixed(2) * parseFloat(unitprice.innerHTML)).toFixed(2);

        var a = calculatedValue;

        valueInput.value = a;
    }

   
}

function EditPAPPharmaciesBPS() {
    debugger;
    var PAPType = document.getElementById("PAPType").value.trim();
    var Dis = document.getElementById("distributer");
    var selectedDistributorValue = Dis.value || "";
    var discode = selectedDistributorValue.split('-');
    var DistributorCode = discode.shift().trim();
    var Brick = document.getElementById("selectedbrick");
    var selectedBrickValue = Brick.value || "";
    var brickcode = selectedBrickValue.split('-');
    var MacroBrickCode = brickcode.shift().trim();

    var ReqId = document.getElementById('papareqid').value.trim();
    var Comment = document.getElementById('createcomments').value;
    var DiscountType = document.getElementById("selecteddis");
    var selectedDiscountType = DiscountType.value || "";

    var preactualfromDate = new Date($('#startdate').val());
    var prefromDate = preactualfromDate.getFullYear() + '-' +
        (preactualfromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        preactualfromDate.getDate().toString().padStart(2, '0');
    var preactualtoDate = new Date($('#enddate').val());
    var pretoDate = preactualtoDate.getFullYear() + '-' +
        (preactualtoDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        preactualtoDate.getDate().toString().padStart(2, '0');
    var currentDate = new Date();
    var PAPHeaderData = {

        DistributerCode: DistributorCode,
        BrickCode: MacroBrickCode,
        DiscountDateFrom: prefromDate,
        DiscountDateTo: pretoDate,
        TrackingId: ReqId,
        DiscountType: selectedDiscountType,
        Remarks: Comment,
        CurrentDate: currentDate,

    }

    var PAPSalesarr = [];
    var salesArrString;

    var chemistCount = $("#UpdatePharmaciesTableAcc").find("button").length;
    $("#UpdatePharmaciesTableAcc").find("button").each(function (index) {

        var buttonId = $(this).attr("id");
        var parts = buttonId.split('-');
        var button1rightPart = parts[1];

        var chemist = document.getElementById("chemist-" + button1rightPart).innerText;
        var chemcodeparts = chemist.split('-');
        var ChemistCode = chemcodeparts[0].trim();
        var productPreSkuCount = $("#tbl-product-" + button1rightPart).find("tr").length;

        var prdArr = [];
        for (var j = 0; j < productPreSkuCount; j++) {
            var productName = document.getElementById("EditProductName-" + button1rightPart + "-" + j).innerHTML;
            var productCode = document.getElementById("EditPackCode-" + button1rightPart + "-" + j).innerHTML
            var PreUnit = document.getElementById("PreEditUnits-" + button1rightPart + "-" + j).innerHTML;
            var PreValue = document.getElementById("PreEditValue-" + button1rightPart + "-" + j).innerHTML;
            var UnitPrice = document.getElementById("EditUnitPrice-" + button1rightPart + "-" + j).innerHTML;
            var EstimatedUnit = document.getElementById("PostEditUnits-" + button1rightPart + "-" + j).value;
            var EstimatedValue = document.getElementById("PostEditValue-" + button1rightPart + "-" + j).value;
            var Discount = document.getElementById("EditDiscount-" + button1rightPart + "-" + j).value;
            var Capp = document.getElementById("EditCapping-" + button1rightPart).value;



            prdArr.push({
                PackCode: productCode,
                LastYearSKU: PreUnit, LastYearValue: PreValue,
                Discount: Discount,
                ExpectedBusinessUnit: EstimatedUnit, ExpectedBusinessValue: EstimatedValue,
                UnitPrice: UnitPrice, Capping: Capp
            })
        }

        PAPSalesarr.push({ ChemistCode: ChemistCode, ProductArr: prdArr });
    });

    var PAPsalesArrString = JSON.stringify(PAPSalesarr);

    $.ajax({

        url: "/PAPView/EditPAPBpsRecord", // Replace with the URL of your controller action
        method: "POST", // Use POST since you are sending data
        data: { EditPAPSalesarr: PAPsalesArrString, EditPAPHeaderData: PAPHeaderData },

        success: function (data) {


            if (data = true) {

                Swal.fire({
                    icon: "success",
                    title: 'Record Created Successfully!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        title: 'small-font',
                        icon: 'small-icon'
                    }
                });

            } else {

            }
            setTimeout(function () {
                window.location.href = "/PAPView/PharmaciesSubmitted"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 3500);

        },
        error: function (xhr, status, error) {
            console.error("Error:", status, error);
        }
    });

}


function EditPAPIvinjectionBPS() {

    var ReqId = document.getElementById('papivreqid').value;
    var Team = document.getElementById('TeamName').innerHTML;
    var preactualfromDate = new Date($('#startdate').val());
    var prefromDate = preactualfromDate.getFullYear() + '-' +
        (preactualfromDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        preactualfromDate.getDate().toString().padStart(2, '0');
    var preactualtoDate = new Date($('#enddate').val());
    var pretoDate = preactualtoDate.getFullYear() + '-' +
        (preactualtoDate.getMonth() + 1).toString().padStart(2, '0') + '-' +
        preactualtoDate.getDate().toString().padStart(2, '0');
    var currentDate = new Date();
    var PAPHeaderData = {


        DiscountFromDate: prefromDate,
        DiscountToDate: pretoDate,
        TrackingId: ReqId,
        CurrentDate: currentDate,

    }

    var PAPSalesarr = [];
    var salesArrString;

    var chemistCount = $("#UpdatetableAcc").find("button").length;


    var productPreSkuCount = $("#updatetableproducts").find("tr").length;

    var prdArr = [];
    for (var j = 0; j < productPreSkuCount; j++) {
        var productName = document.getElementById("EditProductName-" + j).innerHTML;
        var productCode = document.getElementById("EditPackCode-" + j).innerHTML;

        var Discount = document.getElementById("EditDiscount-" + j).value;
        var Capp = document.getElementById("EditCapping").value;


        prdArr.push({
            PackCode: productCode,

            Discount: Discount,Capping:Capp

        })
    }

    PAPSalesarr.push({ Team: Team, ProductArr: prdArr });


    var PAPsalesArrString = JSON.stringify(PAPSalesarr);

    $.ajax({

        url: "/PAPView/EditPAPIvInjectionBpsRecord",
        method: "POST",
        data: { EditPAPIvInjectionSalesarr: PAPsalesArrString, EditPAPIvInjectionHeaderData: PAPHeaderData },

        success: function (data) {


            if (data = true) {

                Swal.fire({
                    icon: "success",
                    title: 'Record Created Successfully!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        title: 'small-font',
                        icon: 'small-icon'
                    }
                });

            } else {

            }
            setTimeout(function () {
                window.location.href = "/PAPView/IVInjectionSubmitted"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 3500);

        },
        error: function (xhr, status, error) {
            console.error("Error:", status, error);
        }
    });
}


function BPSPAPPharmaciesAproval() {

    var isValid = validateFormApprovalObjection(); // Validate the form

    if (!isValid) {
        return; // Stop further processing if form is invalid
    }
    var wlstid = document.getElementById('wlstid').value;
    var comments = document.getElementById("comments").value;
    var trackingid = document.getElementById("papareqid").value;
    var PAPType = document.getElementById("PAPType").value;
    
        $.ajax({
            url: "/PAPView/BPSPAPPharmaciesApproval", // Replace with your controller and action names
            type: 'POST', // Use GET or POST based on your server's requirements
            data: { WlstId: wlstid, comments: comments, TrackingId: trackingid, PAPType: PAPType }, // Send the entire FormData object
            success: function (response) {

                if (data = true) {

                    Swal.fire({
                        icon: "success",
                        title: 'Successfully Approved!',
                        showConfirmButton: false,
                        timer: 3600,
                        width: 680,
                        allowOutsideClick: false,
                        allowEscapeKey: false,
                        customClass: {
                            title: 'small-font',
                            icon: 'small-icon'
                        }
                    });

                } else {
                    
                }
                setTimeout(function () {
                    window.location.href = "/PAPView/PendingApprovalsPharmacies"; // you can pass true to reload function to ignore the client cache and reload from the server
                }, 3500);
            },
            error: function (xhr, status, error) {
                // Handle errors here
                console.error("Error:", status, error);
            }
        });
}

function BPSPAPPharmacesObjection() {


    var isValid = validateFormApprovalObjection(); // Validate the form

    if (!isValid) {
        return; // Stop further processing if form is invalid
    }

    var wlstid = document.getElementById('wlstid').value;
    var comments = document.getElementById("comments").value;
    var trackingid = document.getElementById("papareqid").value;
    var PAPType = document.getElementById("PAPType").value;

    $.ajax({
        url: "/PAPView/BPSPAPPharmaciesObjection", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { WlstId: wlstid, comments: comments, TrackingId: trackingid, PAPType: PAPType }, // Send the entire FormData object
        success: function (response) {

            if (data = true) {

                Swal.fire({
                    icon: "success",
                    title: 'Successfully Approved!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        title: 'small-font',
                        icon: 'small-icon'
                    }
                });

            } else {

            }
            setTimeout(function () {
                window.location.href = "/PAPView/PendingApprovalsPharmacies"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 3500);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}


function BSPPAPIVInjectionApproval() {

    var isValid = validateFormApprovalObjection(); // Validate the form

    if (!isValid) {
        return; // Stop further processing if form is invalid
    }
    var wlstid = document.getElementById('wlstid').value;
    var comments = document.getElementById("comments").value;
    var trackingid = document.getElementById("papivreqid").value;
    var PAPType = document.getElementById("PAPType").value;

    $.ajax({
        url: "/PAPView/BPSPAPPharmaciesApproval", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { WlstId: wlstid, comments: comments, TrackingId: trackingid, PAPType: PAPType }, // Send the entire FormData object
        success: function (response) {

            if (data = true) {

                Swal.fire({
                    icon: "success",
                    title: 'Successfully Approved!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        title: 'small-font',
                        icon: 'small-icon'
                    }
                });

            } else {

            }
            setTimeout(function () {
                window.location.href = "/PAPView/PendingApprovalsIvInjection"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 3500);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}


function BSPPAPIVInjectionRejection() {

    var isValid = validateFormApprovalObjection(); // Validate the form

    if (!isValid) {
        return; // Stop further processing if form is invalid
    }
    var wlstid = document.getElementById('wlstid').value;
    var comments = document.getElementById("comments").value;
    var trackingid = document.getElementById("papivreqid").value;
    var PAPType = document.getElementById("PAPType").value;

    $.ajax({
        url: "/PAPView/BPSPAPPharmaciesObjection", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { WlstId: wlstid, comments: comments, TrackingId: trackingid, PAPType: PAPType }, // Send the entire FormData object
        success: function (response) {

            if (data = true) {

                Swal.fire({
                    icon: "success",
                    title: 'Successfully Approved!',
                    showConfirmButton: false,
                    timer: 3600,
                    width: 680,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        title: 'small-font',
                        icon: 'small-icon'
                    }
                });

            } else {

            }
            setTimeout(function () {
                window.location.href = "/PAPView/PendingApprovalsIvInjection"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 3500);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}


function ReqidIdStatus() {
    debugger;
    var reqid = document.getElementById('papRequestIdstatus').value;
    const selectedRadio = document.querySelector('input[name="options"]:checked');

    // Get the corresponding label text
    var selectedLabel = selectedRadio ? document.querySelector(`label[for="${selectedRadio.id}"]`).textContent : '';

    if (selectedLabel === "") {
        Swal.fire({
            icon: "info",
            title: 'Select Options Available!!',
            showConfirmButton: false,
            timer: 1500,
            width: 380,
            allowOutsideClick: false,
            allowEscapeKey: false,
            customClass: {
                title: 'small-font',
                icon: 'small-icon'
            }
        });
    }

    if (reqid === "") {
        Swal.fire({
            icon: "info",
            title: 'Enter Tracking Id!!',
            showConfirmButton: false,
            timer: 1500,
            width: 380,
            allowOutsideClick: false,
            allowEscapeKey: false,
            customClass: {
                title: 'small-font',
                icon: 'small-icon'
            }
        });
    }
    

    $.ajax({
        url: "/PAPView/PAPTrackingIDStatusDetails", // Replace with your controller and action names
        type: 'POST', // Use GET or POST based on your server's requirements
        data: { reqid: reqid, selectedLabel: selectedLabel }, // Send the entire FormData object
        success: function (response) {

        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}

