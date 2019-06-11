//How frequently to check for session expiration in milliseconds
var sess_pollInterval = 60000;
//How many minutes the session is valid for
var sess_expirationMinutes = 20;
//How many minutes before the warning prompt
var sess_warningMinutes = 17;
var sess_intervalID;
var sess_lastActivity;


$(document).ready(function () {
    dismissAlerts();
    createUiControls();
    bindSearchButton();
    initSlimScroll();
    initSession();
});

var dismissAlerts = function () {
    var $alert = $(".dismissable");
    window.setTimeout(function () { $alert.alert('close'); }, 5000);
};

var initSlimScroll = function() {
    $(".box-views .slimScrollDiv").slimScroll({
        height: '200px'
    });
}

var createUiControls = function () {
     //$(".datefield").datepicker();
}

var JSONDateToString = function (jsonDate) {
    var dateString = jsonDate.substr(6);
    var theTime = new Date(parseInt(dateString));
    var month = theTime.getMonth() + 1;
    var day = theTime.getDate();
    var year = theTime.getFullYear();
    var theDate = day + "/" + month + "/" + year;

    return theDate;
}

var decimalToFixed = function (value) {
    var decimal = parseFloat(Math.round(value * 100) / 100).toFixed(2);

    return decimal.toString().replace(".", ",");
}


var populateDropDownList = function (elementToPopulate, url) {
    $.ajax({
        type: "GET",
        url: url,
        dataType: "json",
        success: function (data) {
            var $list = $(elementToPopulate);
            $list.empty();
            $list.append($("<option></option>")
                    .attr("value", "").text("Seleccione uno"));

            $.each(data.records, function (i, item) {
                $list.append($("<option></option>")
                    .attr("value", item.Value)
                    .text(item.Text));
            });
        }
    });
}

var bindSearchButton = function () {

    var searchValue = getQueryValue("query");

    $("#searchTerm").val(searchValue);


    $("#btnSimpleSearch").on("click", function () {
        var searchTerm = $("#searchTerm").val();
        var url = "/search/?query=" + searchTerm;

        location.href = url;
    });
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

var getQueryValue = function(key) {
    var vars = getUrlVars();

    return vars[key];
}




var initSession = function() {
    sess_lastActivity = new Date();
    sessSetInterval();
    $(document).bind('keypress.session', function (ed, e) {
        sessKeyPressed(ed, e);

    });
}

function sessSetInterval() {
    sess_intervalID = setInterval('sessInterval()', sess_pollInterval);
}

function sessClearInterval() {
    clearInterval(sess_intervalID);

}
function sessKeyPressed(ed, e) {
    sess_lastActivity = new Date();
}

function sessInterval() {
    var now = new Date();
    //get milliseconds of differences
    var diff = now - sess_lastActivity;
    //get minutes between differences

    var diffMins = (diff / 1000 / 60);
    if (diffMins >= sess_warningMinutes) {
        //warn before expiring

        //stop the timer
        sessClearInterval();
        //prompt for attention
        var active = confirm('Su sesión expirará en ' + (sess_expirationMinutes - sess_warningMinutes) +
            ' minutos (as of ' + now.toTimeString() + '), presione OK para seguir logoneado ' +
            'o presione CANCEL para salir. \nUna vez que salga del sistema todos los cambios se perderán.');
        if (active == true) {
            now = new Date();

            diff = now - sess_lastActivity;
            diffMins = (diff / 1000 / 60);
            if (diffMins > sess_expirationMinutes) {
                sessLogOut();
            }
            else {
                initSession();
                sessSetInterval();
                sess_lastActivity = new Date();
            }
        }
        else {
            sessLogOut();
        }
    }
}

function sessLogOut() {
    window.location.href = "/account/logoff";
}