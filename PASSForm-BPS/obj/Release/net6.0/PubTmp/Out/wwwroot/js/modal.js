$('#openHcpDetailsModal').on('click', function () {
    $('#hcpDetailsModal').show();
});

$('#closeHCPModal').on('click', function () {
    $('#hcpDetailsModal').hide();
});

$('#openPreviousPlanModal').on('click', function () {
    $('#previousPlanModal').show();
});

$('#closePreviousPlanModal').on('click', function () {
    $('#previousPlanModal').hide();
});

$('#closePreviousModal').on('click', function () {
    $('#previousPlanModal').hide();
});

$('#closeHCPDetailsModal').on('click', function () {
    $('#hcpDetailsModal').hide();
});

$('#openPurchaseOrderModal').on('click', function () {
    $('#purchaseOrderModal').show();
});

$('#closePurchaseOrderModal').on('click', function () {
    $('#purchaseOrderModal').hide();
});

$('#closePurchaseModal').on('click', function () {
    $('#purchaseOrderModal').hide();
});

$(document).ready(function () {
    $('#hcpDetailsModal').hide();
    $('#previousPlanModal').hide();
    $('#purchaseOrderModal').hide();

    $(document).ready(function () {

        $('#bpsRequestDataTable thead tr')
            .clone(true)
            .addClass('filters')
            .appendTo('#bpsRequestDataTable thead');

        var table = $('#bpsRequestDataTable').DataTable({
            order: [[5, "desc"]],
            orderCellsTop: true,
            fixedHeader: true,

            initComplete: function () {
                var api = this.api();
                api
                    .columns()
                    .eq(0)
                    .each(function (colIdx) {
                        if (colIdx <= 5) {
                            var cell = $('.filters th').eq(
                                $(api.column(colIdx).header()).index()
                            );
                            var title = $(cell).text();

                            $(cell).html('<input type="text" class="form-control" />');
                            $(
                                'input',
                                $('.filters th').eq($(api.column(colIdx).header()).index())
                            )
                                .off('keyup change')
                                .on('change', function (e) {
                                    $(this).attr('title', $(this).val());
                                    var regexr = '({search})';

                                    var cursorPosition = this.selectionStart;
                                    api
                                        .column(colIdx)
                                        .search(
                                            this.value != ''
                                                ? regexr.replace('{search}', '(((' + this.value + ')))')
                                                : '',
                                            this.value != '',
                                            this.value == ''
                                        )
                                        .draw();
                                })
                                .on('keyup', function (e) {
                                    e.stopPropagation();

                                    $(this).trigger('change');
                                    $(this)
                                        .focus()[0]
                                        .setSelectionRange(cursorPosition, cursorPosition);
                                });

                        }
                    });
            },
        });
    });
});

let filesToUpload = []
$("#files").on('change', function (event) {
    document.getElementById("filesList").innerHTML = "";
    for (let i = 0; i < event.target.files.length; i++) {

        let file = event.target.files[i];
        let fileId = "FID" + (1000 + Math.random() * 9000).toFixed(0);

        filesToUpload.push({
            file: file,
            FID: fileId
        });

    }
    displayFiles();
})


function displayFiles() {
    for (let i = 0; i < filesToUpload.length; i++) {
        document.getElementById("filesList").innerHTML += `
        <li>
            <div class="row my-2">
                <div class="col-lg-9">${filesToUpload[i].file.name}</div>
            </div>
        </li>
        `;
    }
}

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

                var modelvisisble = document.getElementById('myModalDetails').style.visibility = 'visible';

            } else {
                var modelvisisble = document.getElementById('myModalDetails').style.visibility = 'hidden';
            }
            setTimeout(function () {
                window.location.href = "/ListView/PendingView/2"; // you can pass true to reload function to ignore the client cache and reload from the server
            }, 5000);
        },
        error: function (xhr, status, error) {
            // Handle errors here
            console.error("Error:", status, error);
        }
    });
}