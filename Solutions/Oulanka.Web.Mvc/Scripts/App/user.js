$(document).ready(function () {
    $("#changepwd_form #btnSave").on("click", function () {
        savePassword();
    });
});


var ShowChangePwd = function (userId) {

    $("#UserId").val(userId);
    $("#Password").val('');

    $("#changepwd_form").modal("show");
}

var savePassword = function () {
    var userId = $("#UserId").val();
    var password = $("#Password").val();

    var url = assignmentUrl;

    $.ajax({
        url: url,
        type: "POST",
        data: { password: password, userId: userId }
    }).done(function () {
        alert("Saved ok!");
        $("#changepwd_form").modal("hide");
    }).fail(function () {
        alert("error al guardar");
        $("#changepwd_form").modal("hide");
    });
}