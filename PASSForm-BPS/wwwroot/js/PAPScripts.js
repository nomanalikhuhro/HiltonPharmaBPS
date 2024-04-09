$(document).ready(function () {

    // Initialize select2
    $("#distributer").select2();
    $("#selectedbrick").select2();

});

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
    var TeamName = document.getElementById('TeamName').innerText;
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