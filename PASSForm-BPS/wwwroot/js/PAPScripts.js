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

function createaccordion() {



    var checkboxes = document.querySelectorAll(".dropdown-content input[type='checkbox']");
    checkedItems = [];
    var checkboxindex = 0;
    var html = '';

    var checkbox = null;
    var attrid;
    checkboxes.forEach(function (checkboxS) {
        if (checkboxS.checked && $('#tableAcc').find('[_id="' + checkboxS.id + '"]').length == 0) {

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
               <div style="padding-top:15px; padding-left:15px;">
            <div class="container">
                <div class="row">
                    <div class="col-3">
                        <label>From:</label>
                        <input type="month" id="startdatepre-${checkboxindex}" value="" style="margin-left: 2%;" />
                    </div>
                    <div class="col-3">
                        <label>To:</label>
                        <input type="month" id="enddatepre-${checkboxindex}" value="" style="margin-left: 2%;" />
                    </div>
                    <div class="col-3">
                         <input style="margin-top:2px;" id="search-${checkboxindex}" onclick="ActivitySales(${checkboxindex})" type="button" class="searchbutton" value="Search"  />  </div>
                </div>
            </div>
        </div>
     
        </div>

                <div id="pre-${checkboxindex}"> </div>
               
            </div>`;

        loadPartialView(attrid);

        $('#tableAcc').append(html);
    }




}

function loadPartialView(attrid) {
    // Replace 'your_partial_view_url' with the URL of your server-side endpoint that serves the partial view content
    var url = '/PAPView/Accordion_PartialView';

    $.get(url, function (data) {
        $('#pre-' + attrid).html(data); // Insert partial view content into the div
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




$(document).ready(function () {

    // Initialize select2
    $("#distributer").select2();
    $("#selectedbrick").select2();

});

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