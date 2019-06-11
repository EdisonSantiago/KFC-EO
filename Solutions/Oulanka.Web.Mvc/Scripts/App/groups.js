$(document).ready(function () {
    $("#assign_form #btnSave").on("click", function () {
        saveAssign();
    });
});


var showAssignModal = function (groupId) {

    $("#UserId").val('');
    $("#GroupId").val(groupId);

    $("#assign_form").modal("show");
}

var saveAssign = function () {

    var groupId = $("#GroupId").val();
    var userId = $("#UserId").val();

    var url = assignmentUrl + "" + '?groupId=' + groupId + "&userId=" + userId;

    $.ajax({
        url: url,
        type: "GET"
    })
        .done(function () {
            location.reload();
            $("#assign_form").modal("hide");
        })
        .fail(function () {
            alert("occurrió un error al guardar");
            $("#assign_form").modal("hide");
        });

}