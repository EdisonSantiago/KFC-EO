$(document).ready(function() {
    $(".toggler").on("click", function(event) {
        event.preventDefault();
        $(this).siblings(".details").show();
    });


});
