$(document).ready(function () {
    $("#btn_addPay").on("click", showPayForm);
    $("#btn_doPay").on("click", doPayment);

    $("#changestatus_button").on("click", doChangeStatus);
    $("#delete_button").on("click", doDelete);
    $(".grid-table-btn").on("click", doGridAction);
});

var showPayForm = function () {
    $("#pay_name").val("");
    $("#pay_cedula").val("");
    $("#pay_date").val("");
    $("#pay_value").val("");
    $("#pay_comment").val("");
    $("#pay_id").val("0");
    $("#pay_form").modal("show");

}

var doPayment = function () {
    var name = $("#pay_name").val();
    var cedula = $("#pay_cedula").val();
    var date = $("#pay_date").val();
    var value = $("#pay_value").val();
    var comment = $("#pay_comment").val();
    var agreementId = $("#pay_agreementId").val();
    var id = $("#pay_id").val();
    

    var urlToAction = "/agreements/registerpaymentsubscription/";
    var params = "?id="+id+"&agreementId=" + agreementId + "&name=" + name + "&cedula=" + cedula + "&date=" + date + "&value=" + value + "&comment=" + comment;

    window.location = urlToAction + params;

}

var doChangeStatus = function () {
    if (confirm("Esta seguro que desea cambiar El Status de este Convenio?")) {
        var urlToAction = $("#changestatus_button").attr("data-href");
        window.location = urlToAction;
    }
}

var doDelete = function () {
    if (confirm("Esta seguro que desea eliminar este Convenio? TODOS LOS DATOS SE ELIMINARÁN!")) {
        var urlToAction = $("#delete_button").attr("data-href");
        window.location = urlToAction;
    }
}


var doGridAction = function () {
    var action = $(this).attr("data-action");
    var id = $(this).attr("data-id");
    var agreement = $(this).attr("data-agreementId");

    switch (action) {
        case "apply":
            doApplyPayment(id, agreement);
            break;
        case "edit":
            showEditPaymentForm(id, agreement);
            break;
        case "delete":
            doDeletePayment(id, agreement);
            break;
    }

}

var doApplyPayment = function (id, agreement) {

}

var showEditPaymentForm = function (id, agreement) {

    var url = "/agreements/getpaymentsubscription/?id="+id+"&agreementId=" + agreement;

    $.get(url, function (data) {
        $("#pay_name").val(data.subscription.PersonWhoPaysName);
        $("#pay_cedula").val(data.subscription.PersonWhoPaysIdentification);
        $("#pay_date").val(JSONDateToString(data.subscription.PaymentDate));
        $("#pay_value").val(decimalToFixed(data.subscription.PaymentValue));
        $("#pay_comment").val(data.subscription.Comment);
        $("#pay_id").val(data.subscription.Id);

        $("#pay_form").modal("show");
    });
}

var doDeletePayment = function (id, agreement) {
    if (confirm("Seguro desea eliminar este Abono?")) {
        var url = "/agreements/deletepaymentsubscription/?id=" + id + "&agreementId=" + agreement;
        window.location = url;
    }
}