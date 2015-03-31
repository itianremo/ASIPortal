/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.js" />


$(function () {
    $(".btns button").click(function (e) {
        e.preventDefault();
        if ($(this).hasClass("validate")) {
            if ($("#proName").val() == "") {
                alert("Please click Edit to fill out the Project Details before saving");
                return;
            }
        }

        location.href = $(this).attr("data-href");
    });


    $("a.define-section").click(function (e) {
        e.preventDefault();

        var url = $(this).attr("href");
        var dialog = "<div title='Edit Submittal Item...' id='definSectionDialog'>";

        $(dialog).append($("<iframe  />").attr({
            "src": url,
            "width": '100%',
            "height": '100%',
            "style": "border: none"
        })).dialog({
            width: 1150,
            height: 550,
            resizable: false,
            buttons: {
                Close: function () { $(this).dialog("close"); }
            }
        });

    });



    $("table.list tbody").sortable({

        stop: function (event, ui) {
            submit_form();
        },

        helper: function (e, ui) {
            ui.children().each(function () {
                $(this).width($(this).width());
            });

            return ui;
        }
    });

    $("table.list").tablesorter().bind("sortEnd", function () {
        submit_form();
    });

    $("a.move-up, a.move-down").click(function (e) {
        e.preventDefault();
        var row = $(this).parents("tr:first");
        if ($(this).is("a.move-up")) {
            row.insertBefore(row.prev());
        } else {
            row.insertAfter(row.next());
        }
        submit_form();
    });


});

function submit_form() {
    var frm = $("form#frmSubmittalItems");

    var url = $(frm).attr("action");
    var to = setTimeout(function () {
        $(".overlay").show();
    }, 500);
    
    $.post(url, $(frm).serialize(), function (data) {
        $("div.overlay").hide();
        clearTimeout(to);
    });
}