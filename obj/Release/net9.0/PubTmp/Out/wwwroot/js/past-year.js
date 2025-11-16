jQuery.validator.addMethod("pastyear", function (value, element, param) {

    if (!value) return false;

    var builtYear = new Date(value);
    if (builtYear == "Invalid Date") return false;

    var today = new Date();
    var maxYears = Number(param);

    var diffYears = today.getFullYear() - builtYear.getFullYear();

    // cannot be future
    if (builtYear > today) return false;

    // cannot be more than maxYears old
    if (diffYears > maxYears) return false;

    return true;
});

jQuery.validator.unobtrusive.adapters.addSingleVal("pastyear", "years");
