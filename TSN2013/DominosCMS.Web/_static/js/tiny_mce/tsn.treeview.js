/// <reference path="../jquery-1.5.1-vsdoc.js" />
/// <reference path="../jquery-ui-1.8.11.min.js" />


$(function () {
    $("#tree").treeview({
        collapsed: true,
        animated: "fast",
        control: "#sidetreecontrol",
        prerendered: true,
        persist: "location"
    });

    bindLinks();

    //bind add menu link

    var a = $("#browser #details #toolbar a.newOpener");
    var dialog = $("<div id='addForm' />");


    //open it
    $(a).click(function () {
        //define dialog
        $(dialog).dialog({
            autoOpen: true,
            width: 600,
            show: "fade",
            hide: "resize",
            height: 500,
            modal: true,

            open: function (event, ui) {
                var d = this;
                var url = $(a).attr("href");
                $.get(url, null, function (data) {
                    $(d).html(data);

                    $(d).find("#pID").val($("#toolbar #ParentID").val());
                });

            },
            close: function () {
                $(this).dialog('destroy').remove();
            }

        });
        return false;
    });

    $("a.saveOrder").click(function () {

        var divList = $("#browser #details #subMenuList form");

        $(divList).submit();
        return false;
    });



})

function bindLinks() {
    var links = $("#tree a, #subMenuList a.menuItem");
    var divList = $("#browser #details #subMenuList");
    var rndm = Math.floor(Math.random() * 100000);

    $("ul", divList).sortable();
    $(links).unbind("click");

    $(links).click(function () {
        var url = $(this).attr("href") + "?" + rndm;
        var parentID = $(this).attr("data-id");

        $("#ParentID").val(parentID);
        $.get(url, null, function (data) {
            $(divList).html(data);
            bindLinks();
        });


        return false;
    });

    //bind edit link

    $("#browser #details #subMenuList a.edit").click(function () {
        var rndm = Math.floor(Math.random() * 100000);
        var url = $(this).attr("href") + "?" + rndm;

        $("<div title='Edit'></div>").dialog({
            autoOpen: true,
            width: 600,
            show: "fade",
            hide: "resize",
            height: 500,
            modal: true,

            open: function (event, ui) {
                var d = this;
                $.get(url, null, function (data) {
                    $(d).html(data);
                });

            },
            close: function () {
                $(this).dialog('destroy').remove();
            }

        });

        return false;
    });

    $("#browser #details #subMenuList a.delete").click(function () {
        if (confirm("Are you sure?")) {
            var url = $(this).attr("href");
            var spinner = $("<span class='spinner'> &nbsp;</span>");
            $(spinner).insertBefore($(this));
            $.get(url, null, function (data) {
                alert(data);
                $(spinner).remove();
            });
        }
        return false;
    });


}

function closePopup()
{
    location.reload();
}
