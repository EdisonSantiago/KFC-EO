$(document).ready(function () {

    $(".adCheckBox").on("click", function () {
        processAddToSystem($(this));
    });

});


var processAddToSystem = function (elem) {

    var formValue = $("#loginsToAdd").val();
    var itemsArray = new Array();
    if (formValue !== "") {
        itemsArray = formValue.split(",");
    }


    var thisCheck = $(elem);
    if (thisCheck.is(":checked")) {
        itemsArray.push($(thisCheck).val());
    } else {
        var index = itemsArray.indexOf($(thisCheck).val());
        if (index > -1) {
            itemsArray.splice(index, 1);
        }
    }

    formValue = itemsArray.toString();
    $("#loginsToAdd").val(formValue);

}

var importAdUsers = function()
{
    var formValue = $("#loginsToAdd").val();
    var valueArray = formValue.split(",");
    var url = "/users/importadusers";

    $.ajax({
        url: url,
        type: "POST",
        data: {
            usernames: valueArray
        }
    }).done(function () {

    var notificationsUrl = "/notifications/AddPageMessageType";
    notificationsUrl += "?message=usuarios importados correctamente&type=Success&isDismissable=true";

        $.ajax({
            url: notificationsUrl,
            type: "GET"
        }).done(function () {
            location.href = "/users";
        });
        
    }).fail(function() {
        alert("ocurrió un error al importar!");
    });
}