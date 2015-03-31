/// <reference path="jquery.validate-vsdoc.js" />
/// <reference path="jquery-1.5.1-vsdoc.js" />

$(function () {

    var isPublic = $("#chkPublic input[type=checkbox]").is(":checked");

    if (isPublic)
    {
        $("#securityOptions select").attr("disabled", "disabled");
        $("#securityOptions select").val("");
    }

    $("#chkPublic input[type=checkbox]").change(function () {
        if (this.checked) {
            $("#securityOptions select").attr("disabled", "disabled");
            $("#securityOptions select").val("");
        } else {
            $("#securityOptions select").removeAttr("disabled")
        }
    })
});