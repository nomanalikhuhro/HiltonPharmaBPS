// Validation.js

function validateForm() {
    var isValid = true; // Assume form is valid initially

    // Clear previous error messages
    $(".error-message").text("");

    // Validate Start Date
    var startDate = $("#startdate").val().trim();
    if (startDate === "") {
        $("#startDateError").text("Discount StartDate is required");
        isValid = false;
    }

    // Validate End Date
    var endDate = $("#enddate").val().trim();
    if (endDate === "") {
        $("#endDateError").text("Discount EndDate is required");
        isValid = false;
    }

    // Validate Distributor
    var distributor = $("#distributer").val().trim();
    if (distributor === "Select") {
        $("#distributorError").text("Please select a Distributor");
        isValid = false;
    }

    // Validate MacroBrick
    var macroBrick = $("#selectedbrick").val().trim();
    if (macroBrick === "Select") {
        $("#macroBrickError").text("Please select a MacroBrick");
        isValid = false;
    }



    // Validate Remarks
    var remarks = $("#createcomments").val().trim();
    if (remarks === "") {
        $("#remarksError").text("Remarks are required");
        isValid = false;
    }

    // Validate Discount Type
    var discountType = $("#selecteddis").val().trim();
    if (discountType === "Select") {
        $("#discountTypeError").text("Please select a Discount Type");
        isValid = false;
    }

    return isValid;
}


function validateFormIvInjection() {
    var isValid = true; // Assume form is valid initially

    // Clear previous error messages
    $(".error-message").text("");

    // Validate Start Date
    var startDate = $("#startdate").val().trim();
    if (startDate === "") {
        $("#startDateError").text("Discount StartDate is required");
        isValid = false;
    }

    // Validate End Date
    var endDate = $("#enddate").val().trim();
    if (endDate === "") {
        $("#endDateError").text("Discount EndDate is required");
        isValid = false;
    }


    return isValid;
}

function validateFormApprovalObjection() {
    var isValid = true; // Assume form is valid initially

    // Clear previous error messages
    $(".error-message").text("");

    // Validate Start Date
    var startDate = $("#comments").val().trim();
    if (startDate === "") {
        $("#commentsError").text("Comments Required!");
        isValid = false;
    }



    return isValid;
}
