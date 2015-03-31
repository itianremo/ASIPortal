/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.min.js" />


$(function () {
    $("a.select-contacts").button().click(function (e) {
        var url = $(this).attr("data-contact-source");
        //show dialog 
        var dialog = "<div title='Select contacts' id='dlgContacts'>";
        $(dialog).load(url).dialog({
            width: 550,
            height: 400,
            modal: true,
            buttons: {
                Ok: function () {
                    var emails = '';
                    var contacts = $("input:checked", this).each(function (obj) {
                        if ($(this).val())
                            emails += $(this).val() + ',';
                    });
                    console.log(emails);
                    $("#to").val(emails);
                    $(this).dialog("close");
                    
                },
                Cancel: function () { $(this).dialog("close"); }

            }

        });;
        


    });

    $("#formDiv form").submit(function (e) {
        e.preventDefault();

        var url = $(this).attr("action");
        var data = $(this).serialize();

        //Show progress 
        $("#progress").dialog({
            height: 140,
            modal: true,
            closeOnEscape: false,
            draggable: false,
            resizable: false,
            open: function (event, ui) {
                $(".ui-dialog-titlebar-close", ui.dialog || ui).hide();
            }
        });

        $.post(url, data, function (data, status) {
            if (data.success) {

                $("#progress").dialog("close");
                location.href = "/Submittals/SubmittalSent";

            } else {
                alert("Sorry we can not send your submittal now, please try again later");
            }
        }, "json");

    });


});