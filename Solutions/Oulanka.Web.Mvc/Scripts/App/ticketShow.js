$(document).ready(function () {

    

    $("#assign_form #btnSave").on("click", function () {
        saveAssign();
    });
    $("#resource_form #btnSave").on("click", function () {
        saveResource();
    });
    $("#type_form #btnSave").on("click", function () {
        saveType();
    });

    $("#close_form #btnSave").on("click", function () {
        saveCloseTicket();
    });

    $("#priority_form #btnSave").on("click", function () {
        saveTicketPriority();
    });

    $("#status_form #btnSave").on("click", function () {
        saveTicketStatus();
    });

    $("#dates_form #btnSave").on("click", function () {
        saveTicketDates();
    });
    $("#process_form #btnSave").on("click", function () {
        saveTicketProcess();
    });
    $("#req_form #btnSave").on("click", function () {
        saveTicketReq();
    });
    $("#note_form #btnSave").on("click", function () {
        saveTicketNote();
    });

    $("#AssignmentType").on("change", function() {
        if ($(this).val() === "3") {
            $("#prov_panel").show();
        } else {
            $("#prov_panel").hide();
        }
    });

    $('#dates_form input[name="DateToChange"]').datetimepicker({
        locale: 'es'
    });

    $('#dates_form input[name="DateToChange"]').on("dp.change", function(e) {
        $("#Date").val(e.date.format('YYYY-MM-DD HH:mm'));
    });

    /*$('#dates_form input[name="DateToChange"]').daterangepicker({
        singleDatePicker: true,
        timePicker: true,
        timePickerIncrement: 30,
        locale: {
            format: 'MM/DD/YYYY h:mm A',
            applyLabel: "Aplicar"
        }
    },
    function (start, end, label) {
        $("#Date").val(start.format('YYYY-MM-DD'));
    });*/

    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });

    if ($("#RateValue").length) {
        $("#RateValue").rating();
    }

    CKEDITOR.replace('Content', {
        // Define the toolbar groups as it is a more accessible solution.
        toolbarGroups: [
            { "name": "basicstyles", "groups": ["basicstyles"] },
            { "name": "links", "groups": ["links"] },
            { "name": "paragraph", "groups": ["list", "blocks"] },
            { "name": "document", "groups": ["mode"] },
            { "name": "insert", "groups": ["insert"] },
            { "name": "styles", "groups": ["styles"] },
            { "name": "about", "groups": ["about"] }
        ],
        // Remove the redundant buttons from toolbar groups defined above.
        removeButtons: 'Underline,Strike,Subscript,Superscript,Anchor,Styles,Specialchar'
    });

    $("#Attachments").fileinput({
        showUpload: false,
        'language': 'es',
        mainClass: "input-group-sm"
    });

    tabsBehavior();
});

var tabsBehavior = function() {
    $('#ticket-tabs a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    });

    // store the currently selected tab in the hash value
    $("ul.nav-tabs > li > a").on("shown.bs.tab", function (e) {
        var id = $(e.target).attr("href").substr(1);
        window.location.hash = id;
    });

    // on load of the page: switch to the currently selected tab
    var hash = window.location.hash;
    $('#ticket-tabs a[href="' + hash + '"]').tab('show');
}

var viewNote = function(noteId, ticketId) {
    var url = noteViewUrl+"?noteId="+noteId+"&ticketId="+ticketId;
    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            noteId: noteId
        }
    }).done(function (data) {
        $("#note_container").html(data);
    }).fail(function () {
        "ocurrio un error";
    });
}

var saveTicketNote = function() {
    var url = noteSaveUrl;
    var ticketId = $("#note_form #TicketId").val();
    var note = {
        Id: $("#note_form #NoteId").val(),
        Content: CKEDITOR.instances.Content.getData(),
        IsVisible: $("#note_form #IsVisible").is(":checked"),
        NoteType: $("#note_form #NoteType").val()

};

    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId:ticketId,
            note: note
        }
    }).done(function() {
        location.reload();
    }).fail(function() {
        "ocurrio un error";
    });

}

var addNote = function(ticketId) {
    $("#note_form").modal("show");
    $("#note_form #TicketId").val(ticketId);
    $("#note_form #TicketId").val();
}

var editNote = function (noteId, ticketId) {

    var url = noteEditUrl + "?noteId=" + noteId + "&ticketId=" + ticketId;

    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            noteId: noteId
        }
    }).done(function (note) {
        $("#note_form #NoteType").val(note.NoteType);
        CKEDITOR.instances.Content.setData(note.Content);
        $("#note_form #IsVisible").val(note.IsVisible);
    }).fail(function () {
        "ocurrio un error";
    });
    $("#note_form").modal("show");
    $("#note_form #TicketId").val(ticketId);
    $("#note_form #NoteId").val(noteId);
}

// change recurrency
var recurrent = function (ticketId, value) {
    var isRecurrent = (value === "true");

    var url = recurrentUrl + "&isRecurrent=" + isRecurrent;
    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            isRecurrent: isRecurrent
        }
    })
       .done(function () {
           location.reload();
       })
       .fail(function () {
           alert("occurrió un error al guardar");
       });

}

var deleteTicket= function(ticketId) {
    if (confirm("Está seguro que desea eliminar este Incidente #"+ticketId+"?")) {
        location.href = delUrl;
    }
}

var showTypeModal = function (ticketId) {
    $("#TicketId").val(ticketId);

    $("#type_form").modal("show");
}

var saveType = function () {
    var ticketId = $("#TicketId").val();
    var ticketTypeId = $("#TicketTypeId").val();
    var requestTypeId = $("#RequestTypeId").val();

    var url = typeUrl + "&ticketTypeId=" + ticketTypeId+"&requestTypeId="+requestTypeId;

    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            ticketTypeId: ticketTypeId,
            requestTypeId: requestTypeId
        }
    })
        .done(function () {
            location.reload();
            $("#req_form").modal("hide");
        })
        .fail(function () {
            alert("occurrió un error al guardar");
            $("#req_form").modal("hide");
        });
}

var showResourceModal = function (ticketId) {
    $("#TicketId").val(ticketId);

    $("#resource_form").modal("show");
}

var saveResource = function () {
    var ticketId = $("#TicketId").val();
    var resourceId = $("#ResourceId").val();
    var componentId = $("#ComponentId").val();

    var url = resourceUrl + "&resourceId=" + resourceId + "&componentId=" + componentId;

    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            resourceId: resourceId,
            componentId: componentId
        }
    })
        .done(function () {
            location.reload();
            $("#req_form").modal("hide");
        })
        .fail(function () {
            alert("occurrió un error al guardar");
            $("#req_form").modal("hide");
        });
}

var showReqModal = function (ticketId) {
    $("#Requirement").val('');
    $("#TicketId").val(ticketId);

    $("#req_form").modal("show");
}

var saveTicketReq = function () {
    var ticketId = $("#TicketId").val();
    var requirement = $("#Requirement").val();

    var url = reqUrl + "&requirement="+requirement;

    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            requirement: requirement
        }
    })
        .done(function () {
            location.reload();
            $("#req_form").modal("hide");
        })
        .fail(function () {
            alert("occurrió un error al guardar");
            $("#req_form").modal("hide");
        });
}

// Process  Form
var showProcessModal = function (ticketId) {
    $("#ProcessId").val('');
    $("#TicketId").val(ticketId);

    $("#process_form").modal("show");
}

var saveTicketProcess = function () {
    var ticketId = $("#TicketId").val();
    var processId = $("#ProcessId").val();

    var url = processUrl;

    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            processId: processId
        }
    })
        .done(function () {
            location.reload();
            $("#process_form").modal("hide");
        })
        .fail(function () {
            alert("occurrió un error al guardar");
            $("#process_form").modal("hide");
        });
}

//ChangeStatus Priority
var showDatesModal = function (ticketId, dateType) {
    $("#Status").val('');
    $("#DateType").val(dateType);
    $("#TicketId").val(ticketId);

    $("#dates_form").modal("show");
}

var saveTicketDates = function () {

    var ticketId = $("#TicketId").val();
    var date = $("#Date").val();
    var dateType = $("#DateType").val();

    var url = dateUrl;

    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            dateType: dateType,
            date: date
        }
    })
        .done(function () {
            location.reload();
            $("#dates_form").modal("hide");
        })
        .fail(function () {
            alert("occurrió un error al guardar");
            $("#dates_form").modal("hide");
        });

}

//ChangeStatus Priority
var showStatusModal = function (ticketId) {
    $("#Status").val('');
    $("#TicketId").val(ticketId);

    $("#status_form").modal("show");
}

var saveTicketStatus = function () {

    var ticketId = $("#TicketId").val();
    var status = $("#Status").val();

    var url = statusUrl;

    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            status: status
        }
    })
        .done(function () {
            location.reload();
            $("#status_form").modal("hide");
        })
        .fail(function () {
            alert("occurrió un error al guardar");
            $("#status_form").modal("hide");
        });

}

//ChangeTicket Priority
var showPriorityModal = function (ticketId) {
    $("#PriorityId").val('');
    $("#TicketId").val(ticketId);

    $("#priority_form").modal("show");
}

var saveTicketPriority = function () {

    var ticketId = $("#TicketId").val();
    var priorityId = $("#PriorityId").val();

    var url = priorityUrl;

    $.ajax({
        url: url,
        type: "POST",
        data: {
            ticketId: ticketId,
            priorityId: priorityId
        }
    })
        .done(function () {
            location.reload();
            $("#priority_form").modal("hide");
        })
        .fail(function () {
            alert("occurrió un error al guardar");
            $("#priority_form").modal("hide");
        });

}


//AssignTicket
var showAssignModal = function (ticketId) {

    $("#Comments").val('');
    $("#UserId").val('');
    $("#TicketId").val(ticketId);
    $("#AssignmentType").val('');

    $("#assign_form").modal("show");
}

var saveAssign = function () {
    var assignment =
    {
        Comments: $("#Comments").val(),
        IsUserVisible: $("#IsVisible").is(":checked"),
        AssignmentType: $("#AssignmentType").val(),
        
};

    var ticketId = $("#TicketId").val();
    var userId = $("#UserId").val();
    var providerId = $("#ProviderId").val();
    if (providerId == "")
        providerId = 0;

    var providerTicket = $("#ReqNumber").val();

    var url = assignmentUrl + "" + "?ticketId=" + ticketId + "&userId=" + userId + "&providerId=" + providerId + "&providerTicket=" + providerTicket;

    $.ajax({
        url: url,
        type: "POST",
        data: {
            assignment: assignment,
            userId: userId,
            ticketId: ticketId,
            providerId: providerId,
            providerTicket: providerTicket
        }
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

//CloseTicketModal
var ShowCloseModal = function (ticketId, username) {
    $("#close_form #Comments").val('');
    $("#close_form #Username").val(username);
    $("#close_form #TicketId").val(ticketId);

    $("#close_form").modal("show");
}

var saveCloseTicket = function () {

    var ticketId = $("#TicketId").val();
    var username = $("#Username").val();
    var comments = $("#close_form #Comments").val();
    var timeSpent = $("#TimeSpent").val();
    var isResolved = $("#IsResolved").is(":checked");

    var isTimeValid = (timeSpent.search(/^\d{2}:\d{2}$/) != -1) &&
            (timeSpent.substr(0, 2) >= 0 && timeSpent.substr(0, 2) <= 72) &&
            (timeSpent.substr(3, 2) >= 0 && timeSpent.substr(3, 2) <= 59)


    if (isTimeValid) {
        var url = closeUrl;

        $.ajax({
                url: url,
                type: "POST",
                data: {
                    ticketId: ticketId,
                    username: username,
                    comments: comments,
                    timeSpent: timeSpent,
                    isResolved: isResolved
                }
            })
            .done(function() {
                location.reload();
                $("#close_form").modal("hide");
            })
            .fail(function() {
                alert("occurrió un error al guardar");
                $("#close_form").modal("hide");
            });
    } else {
        alert("El formato del tiempo es incorrecto debe ser HH:MM");
    }

   

}