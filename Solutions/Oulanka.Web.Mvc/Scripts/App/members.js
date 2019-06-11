$(document).ready(function() {
    $("#assign_form #btnSave").on("click",function() { saveMemberAssign(); })
});


var showAssignModal = function (projectId) {

    $("#ProjectId").val(projectId);
    $("#UserId").val('');
    $("#GroupId").val('');

    $("#assign_form").modal("show");
}

var saveMemberAssign = function() {
    var url = "/projects/assignmember";

    $.ajax({
        url: url,
        type: "POST",
        data: {
            projectId: $("#ProjectId").val(),
            userId: $("#UserId").val(),
            groupId: $("#GroupId").val()
        }
    }).done(function() {
        var notificationsUrl = "/notifications/AddPageMessageType";
        notificationsUrl += "?message=usuario asigando correctamente&type=Success&isDismissable=true";
        $.ajax({
            url: notificationsUrl,
            type: "GET"
        }).done(function() {
            location.href = "/projects/members/"+$("#ProjectSlug").val();
        });
    }).fail(function() {
        alert("El usuario ya esta asignado!.");
    });

}