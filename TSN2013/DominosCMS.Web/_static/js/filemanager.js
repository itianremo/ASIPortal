/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.min.js" />
/// <reference path="jquery.validate-vsdoc.js" />
var filemanagerpath = "";

$(function () {


    filemanagerpath = $("#filemangerpath").val() + "/List";


    bindFolders();
    bindFiles();

    $("#folders").on("click", "li span.folder-icon", function () {
        var li = $(this).parent();

        $(li).find(">ul").slideToggle("fast");
        //var url = $($(li).find("a")).attr("href");
        //dir(url);

        $(this).toggleClass("open-folder");
    });

    //handle refresh button
    $("#toolbar a.refresh").click(function (e) {
        e.preventDefault();
        reload();
    });

    $("#toolbar a.upfolder").click(function (e) {
        e.preventDefault();
        if ($("#currentFolder").val() === $("#rootFolder").val())
            return;

        var cf = $("#currentFolder").val();
        var index = cf.lastIndexOf("/");


        dir(cf.substr(0, index));
    });

    //handle upload button
    $("#toolbar a.uploadfile").click(function (e) {
        e.preventDefault();

        var dialog = $("<div title='Upload Files...' class='dialog'></div>");
        var url = $(this).attr("href");
        var a2 = this;
        $(this).addClass("busy");

        $.get(url, { dir: $("#currentFolder").val() }, function (data) {
            $(dialog).html(data);
            $(a2).removeClass("busy");
            $(dialog).dialog({
                autoOpen: true,
                width: 600,
                height: 450,
                modal: true,
                buttons: {
                    Close: function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    $(dialog).remove();
                    reload();
                },
                open: function () {
                    //alert("Open");
                }

            });

        });
        //showUploadDialog(dialog, url, 600, 450);

    });

    //handle create folder button
    $("#toolbar a.createfolder").click(function (e) {
        e.preventDefault();
        var a1 = this;
        $(this).addClass("busy");
        var dialog = $("<div title='Create folder' class='dialog' id='createfolder'></div>");
        var url = $(this).attr("href");

        $.get(url, { dir: $("#currentFolder").val() }, function (data) {
            $(dialog).html(data);
            $("#createfolder form", dialog).validate({ messages: { foldername: " *" } });
            $(a1).removeClass("busy");


            $(dialog).dialog({
                autoOpen: true,
                width: 400,
                height: 300,
                modal: true,
                buttons: {
                    Submit: function () {
                        if ($("form", dialog).valid()) {
                            $.post($("form", dialog).attr("action"), { foldername: $("#foldername").val() }, function (data) {
                                reload();
                                reloadFolderList();
                                $(dialog).dialog("close");
                            }, "text");

                        }
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    $(dialog).remove();
                }

            });

        });

        //showCreateFolderDialog(dialog, url, 400, 300);

    });


});




function reload() {
    var currentFolder = $("#currentFolder").val();
    $("#fileslist ul").remove();
    $("#fileslist .loader").show();
    $.get(filemanagerpath, { path: currentFolder }, function (data) {
        $("#fileslist").html(data);
        $("#currentFolderLabel").text(currentFolder);
        bindRightFolders();
        bindFiles();
        $("#fileslist .loader").hide();
    }, "html");

}

function dir(url) {
    $("#fileslist ul").remove();
    $("#fileslist .loader").show();
    $.get(filemanagerpath, { path: url }, function (data) {
        $("#fileslist").html(data);
        $("#currentFolderLabel").text(url);
        bindRightFolders();
        bindFiles();
        $("#fileslist .loader").hide();
    }, "html");
    document.cookie = "lastFolderVisited=" + url


}

function reloadFolderList() {
    var url = $("#filemangerpath").val() + "/Folders";
    $.get(url, null, function (data) {
        $("#folders ul").replaceWith(data);
        $("#folders li a").click(function (e) {
            e.preventDefault();
            var url = $(this).attr("href");
            dir(url);
        });
    }, "html");
}

function bindFolders() {
    //handle folder selection
    $("#fileslist .folder a.thumb, #folders li a").click(function (e) {
        e.preventDefault();
        var url = $(this).attr("href");

        dir(url);
    });
}

function bindFiles() {
    //handle file selection
    $("#fileslist .file a.thumb").click(function (e) {
        e.preventDefault();
        var URL = $(this).attr("href");
        //alert(url);
        var win = tinyMCEPopup.getWindowArg("window");

        // insert information now
        win.document.getElementById(tinyMCEPopup.getWindowArg("input")).value = URL;

        // are we an image browser
        if (typeof (win.ImageDialog) != "undefined") {
            // we are, so update image dimensions...
            if (win.ImageDialog.getImageData)
                win.ImageDialog.getImageData();

            // ... and preview if necessary
            if (win.ImageDialog.showPreviewImage)
                win.ImageDialog.showPreviewImage(URL);
        }

        // close popup window
        tinyMCEPopup.close();


    });

    $("#fileslist li").hover(function () {
        $("span.actions", this).fadeIn("fast");
    }, function () {
        $("span.actions", this).fadeOut("fast");
    });

    $("#fileslist li.file span.actions a.del").click(function (e) {
        e.preventDefault();
        if (confirm("Are you sure you want to delete this file?")) {
            var url = $(this).attr("href");
            var li = $(this).parent().parent();

            $.get(url, null, function () {
                $(li).remove();
            });
        }
    });

    $("#fileslist li.folder span.actions a.del").click(function (e) {
        e.preventDefault();
        if (confirm("Are you sure you want to delete this folder and all of its contents?")) {
            var url = $(this).attr("href");
            var li = $(this).parent().parent();
            $.get(url, null, function () {
                $(li).remove();
                reloadFolderList();
            });
        }
    });


    $("#fileslist li.file span.actions a.open").click(function (e) {
        e.preventDefault();
        var url = $(this).attr("href");

        var dialog = $("<div title='Image' class='dialog'></div>");
        $(dialog).dialog({
            autoOpen: true,
            width: 800,
            height: 600,
            modal: false,
            open: function () {
                var img = $("<img src='" + url + "' alt='' />");
                $(dialog).html(img);
            },
            close: function () {
                $(dialog).remove();
            }

        });
    });
}


function bindRightFolders() {
    $("#fileslist .folder a.thumb").click(function (e) {
        e.preventDefault();
        var url = $(this).attr("href");

        dir(url);
    });
}