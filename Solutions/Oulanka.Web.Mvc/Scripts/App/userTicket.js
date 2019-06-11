$(document).ready(function () {
    $("#rate_form #btnSave").on("click", function () {
        saveTicketRate();
    });

    $("#close_form #btnSave").on("click", function () {
        saveCloseTicket();
    });
});

var ShowCloseModal = function (ticketId, username) {
    $("#Comments").val('');
    $("#Username").val(username);
    $("#TicketId").val(ticketId);

    $("#close_form").modal("show");
}


var saveCloseTicket = function () {

    var ticketId = $("#TicketId").val();
    var username = $("#Username").val();
    var comments = $("#Comments").val();
    var url = closeUrl;

    if (comments !== "") {
        $.ajax({
            url: url,
            type: "POST",
            data: {
                ticketId: ticketId,
                username: username,
                comments: comments
            }
        }).done(function () {
            $("#close_form").modal("hide");
            location.reload();

        }).fail(function () {
            alert("error al cerrar");
        });

    } else {
        alert("Debe incluir un comentario");
    }
};


var showRateTicket = function (ticketId) {
    $("#TicketId").val(ticketId);
    $("#RateValue").val(0);

    $("#rate_form").modal("show");
}

var saveTicketRate = function () {
    var ticketId = $("#TicketId").val();
    var rateValue = $("#RateValue").val();

    var url = rateUrl + "&rateValue=" + rateValue;

    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            rateValue: rateValue
        }
    }).done(function () {
        location.reload();
    }).fail(function () {
        alert("ocurrio un error al calificar!");
        $("#rate_form").modal("hide");
    });
}