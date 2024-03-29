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
