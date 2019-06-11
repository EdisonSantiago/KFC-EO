
var initialAmmount = $("#InitialAmmount").val();

$(document).ready(function () {


    $(".do-calculation").on("blur", calculateTotals);
    $("#AgreementDate").on("blur", function () {
        $("#PaymentDate").val($(this).val());
    });



});

var calculateTotals = function () {

    var total = 0;
    var initialAmmount = $("#InitialAmmount").val().replace(",", ".");
    var downPaymentAmmount = $("#DownPaymentAmmount").val().replace(",", ".");
    var subtotal = initialAmmount - downPaymentAmmount;
    total = subtotal;

    $("#SubtotalAmmount").val(subtotal.toString().replace(".", ","));
    $("#SubtotalAmmountVal").text(decimalToFixed(subtotal));

    var hasInterests = $("#HasInterest").val();
    if (hasInterests) {
        var interest = $("#InterestRate").val().replace(",", ".");
        var months = $("#MonthsTerm").val();
        var monthlyPayment = calculateMonthlyPayment(subtotal, interest, months);

        var totalAmmount = monthlyPayment * months;
        total = totalAmmount;
        var interestAmmount = totalAmmount - subtotal;
        $("#AgreementInterestAmmount").val(interestAmmount.toString().replace(".", ","));
        $("#AgreementInterestAmmountVal").text(decimalToFixed(interestAmmount));
        $("#MonthlyAmmount").val(monthlyPayment.toString().replace(".", ","));
        $("#MonthlyAmmountVal").text(decimalToFixed(monthlyPayment));
    }

    var totalAmmountString = total.toString().replace(".", ",");

    $("#TotalAmmount").val(totalAmmountString);
    $("#TotalAmmountVal").text(decimalToFixed(total));
}

var calculateMonthlyPayment = function (subtotal, interest, months) {
    var result = subtotal * ((interest / 100) / 12) / (1 - Math.pow((1 + (interest / 100) / 12), (-months)));
    result = Math.round(result * 100) / 100;
    return result;
}

