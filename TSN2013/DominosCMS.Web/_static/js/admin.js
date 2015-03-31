/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.min.js" />

$(function () {
    bindTinyMCEEditor($("body"));

    bindEditLinks();
    //auto generate url from title

    var titleControl = $("#createPageForm #Title");
    var urlControl = $("#createPageForm #Url");

    $(titleControl).keyup(function () {
        $(urlControl).val(convertToUrl($(this).val()));
    });

    $(titleControl).blur(function () {
        $(urlControl).val(convertToUrl($(this).val()));
        validateUrl($(urlControl).val());
    });

    $(urlControl).change(function () {
        var url = $(this).val();
        if (url != '')
            validateUrl(url);
    });

    $("#editPageForm #Url").change(function () {
        var url = $(this).val();
        if (url != '')
            validateUrlUpdate(url);
    });

    $("a.delete-link").click(function () {
        return confirm("Are you sure you want to delete this item?")
    });

});



function validateUrl(url) {
    var msgWrapper = $("#urlMsgWrapper");
    $(msgWrapper).attr("class", "spinner");

    checkUrl(url, msgWrapper);
}

function validateUrlUpdate(url) {
    var msgWrapper = $("#urlMsgWrapper");
    var oldUrl = $("#editPageForm .oldUrl").val();
    $(msgWrapper).attr("class", "spinner");
    checkUrlUpdate(url, oldUrl, msgWrapper);
}


function checkUrlUpdate(url, oldUrl, msgWrapper) {
    if (url == '') {
        $(msgWrapper).text("Please enter url").attr("class", "invalid-url");
        return;
    }

    $.get("/Pages/ValidateUrlUpdate/", { url: url, oldUrl: oldUrl }, function (data) {
        if (data.res)
            $(msgWrapper).text("Valid").attr("class", "valid-url");
        else
            $(msgWrapper).text("Url is taken, try another one").attr("class", "invalid-url");

        return true;
    }, "json");
}

function checkUrl(url, msgWrapper) {
    if (url == '') {
        $(msgWrapper).text("Please enter url").attr("class", "invalid-url");
        return;
    }

    $.get("/Pages/ValidateUrl/?url=" + url, null, function (data) {
        if (data.res)
            $(msgWrapper).text("Valid").attr("class", "valid-url");
        else
            $(msgWrapper).text("Url is taken, try another one").attr("class", "invalid-url");

        return true;
    }, "json");
}

function bindEditLinks() {
    $("a.edit-specs-item").click(function () {
        var dialog = $("<div title='Edit' class='dialog'></div>");
        var url = $(this).attr("href");
        var reloadUrl = $(this).attr("data-link");
        var indx = parseInt($(this).attr("data-index"));

        showDialog(dialog, url, reloadUrl, indx);

        return false;
    });
}

//bind tabs
$(function () {
    bindTabs(0);
});

function bindTabs(t) {
    $("#tabs").tabs({
        ajaxOptions: {
            error: function (xhr, status, index, anchor) {
                $(anchor.hash).html(
						    "Couldn't load this tab. We'll try to fix this as soon as possible. " +
						    "If this wouldn't be a demo.");
            }
        },
        cashe: false
    });
}

//Bind dialog
$(function () {
    $("a.add-tab").click(function () {

        var dialog = $("<div title='Add tab...' class='dialog'></div>");
        var url = $(this).attr("href");
        var reloadUrl = $(this).attr("data-link");

        showDialog(dialog, url, reloadUrl, 0);

        return false;
    });
});

function showDialog(obj, url, reloadUrl, selectedTab) {
    $(obj).dialog({
        autoOpen: true,
        width: 800,
        height: 600,
        modal: true,
        open: function (event, ui) {
            var d = this;
            $.get(url, null, function (data) {
                $(d).html(data);
                bindTinyMCEEditor(d);
            });
        },
        buttons: {
            Submit: function () { handleSubmit(this) },
            Cancel: function () { $(this).dialog("close") }
        },
        close: function () {
            $.get(reloadUrl, null, function (data) {
                $("#productSpecs").html(data);
                bindTabs(selectedTab);
                bindEditLinks();
                $("#tabs").tabs("select", selectedTab);
                $(obj).remove();
            });
        }

    });
}




function handleSubmit(obj) {
    $("form", obj).submit();
}


function closeDialog() {
    $("div.dialog").dialog("close");
}



function bindTinyMCEEditor(context) {
    $(".rte-fullfeature textarea", context).tinymce({
        // Location of TinyMCE script
        script_url: '/_static/js/tiny_mce/tiny_mce.js',

        // General options
        theme: "advanced",
        plugins: "autolink,lists,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,advlist,MyPlugin",
        file_browser_callback: 'myFileBrowser',

        // Theme options
        theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
        theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
        theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,pagebreak",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: true,
        relative_urls: false,

        // Example content CSS (should be your site CSS)
        content_css: "/_static/css/editor.css"

        // Drop lists for link/image/media/template dialogs
        // template_external_list_url: "lists/template_list.js",
        // external_link_list_url: "lists/link_list.js",
        // external_image_list_url: "lists/image_list.js",
        // media_external_list_url: "lists/media_list.js",

    });
}


function convertToUrl(Text) {
    return Text
        .toLowerCase()
        .replace(/[^\w ]+/g, '')
        .replace(/ +/g, '-')
        ;
}

function myFileBrowser(field_name, url, type, win) {

    // alert("Field_Name: " + field_name + "nURL: " + url + "nType: " + type + "nWin: " + win); // debug/testing

    /* If you work with sessions in PHP and your client doesn't accept cookies you might need to carry
    the session name and session ID in the request string (can look like this: "?PHPSESSID=88p0n70s9dsknra96qhuk6etm5").
    These lines of code extract the necessary parameters and add them back to the filebrowser URL again. */

    var cmsURL = "/FileManager/";     // your URL could look like "/scripts/my_file_browser.php"
    if (cmsURL.indexOf("?") < 0) {
        //add the type as the only query parameter
        cmsURL = cmsURL + "?type=" + type;
    }
    else {
        //add the type as an additional query parameter
        // (PHP session ID is now included if there is one at all)
        cmsURL = cmsURL + "&type=" + type;
    }

    tinyMCE.activeEditor.windowManager.open({
        file: cmsURL,
        title: 'File Browser',
        width: 750,  // Your dimensions may differ - toy around with them!
        height: 600,
        resizable: "yes",
        inline: "yes",  // This parameter only has an effect if you use the inlinepopups plugin!
        close_previous: "no"
    }, {
        window: win,
        input: field_name
    });
    return false;
}
