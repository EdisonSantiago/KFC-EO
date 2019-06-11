$(document).ready(function () {
    if ($(".select2").length)
    { $(".select2").select2(); }
    initDates();
    initSubmitButtons();

    $("#addView_form #btnSave").on("click", function () {

        $("#ViewName").val($("#viewnametxt").val());

        $("#advSearchForm").submit();
    });

    bindSearchButton();

});


var bindSearchButton = function () {

    var searchValue = getQueryValue("query");

    $("#userSearchTerm").val(searchValue);


    $("#btnUserSimpleSearch").on("click", function () {
        var searchTerm = $("#userSearchTerm").val();
        var url = "/my/search/?query=" + searchTerm;

        location.href = url;
    });
}

var setSelectValues = function (control, values) {
    var valuesArray = values.split(",");
    var $element = $(control).select2();
    $element.val(valuesArray).trigger("change");

}

var initSubmitButtons = function () {
    $("#saveViewBtn").on("click", function () {
        $("#ActionType").val($(this).val());

        $("#addView_form").modal("show");
    });

    $("#searchBtn").on("click", function () {
        $("#ActionType").val($(this).val());

        $("#advSearchForm").submit();
    });
}



var initDates = function () {

    if ($('input[name="DateCreateStart"]').length) {
        $('input[name="DateCreateStart"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: { format: 'YYYY/MM/DD' }
        },
         function (start, end, label) {
             $("#CreatedOnStart").val(start.format('YYYY-MM-DD'));
         });
    }

    if ($('input[name="DateCreateEnd"]').length) {
        $('input[name="DateCreateEnd"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: { format: 'YYYY/MM/DD' }
        },
       function (start, end, label) {
           $("#CreatedOnEnd").val(start.format('YYYY-MM-DD'));

       });
    }

    if ($('input[name="DateUpdateStart"]').length) {
        $('input[name="DateUpdateStart"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: { format: 'YYYY/MM/DD' }
        },
       function (start, end, label) {
           $("#UpdatedOnStart").val(start.format('YYYY-MM-DD'));

       });
    }

    if ($('input[name="DateUpdateEnd"]').length) {
        $('input[name="DateUpdateEnd"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: { format: 'YYYY/MM/DD' }
        },
       function (start, end, label) {
           $("#UpdatedOnEnd").val(start.format('YYYY-MM-DD'));

       });
    }

    if ($('input[name="DateResolvedStart"]').length) {
        $('input[name="DateResolvedStart"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: { format: 'YYYY/MM/DD' }
        },
       function (start, end, label) {
           $("#ResolvedOnStart").val(start.format('YYYY-MM-DD'));

       });
    }

    if ($('input[name="DateResolvedEnd"]').length) {
        $('input[name="DateResolvedEnd"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: { format: 'YYYY/MM/DD' }
        },
       function (start, end, label) {
           $("#ResolvedOnEnd").val(start.format('YYYY-MM-DD'));

       });
    }

    if ($('input[name="DateClosedStart"]').length) {
        $('input[name="DateClosedStart"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: { format: 'YYYY/MM/DD' }
        },
       function (start, end, label) {
           $("#ClosedOnStart").val(start.format('YYYY-MM-DD'));

       });
    }

    if ($('input[name="DateClosedEnd"]').length) {
        $('input[name="DateClosedEnd"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            locale: { format: 'YYYY/MM/DD' }
        },
       function (start, end, label) {
           $("#ClosedOnEnd").val(start.format('YYYY-MM-DD'));

       });
    }
}