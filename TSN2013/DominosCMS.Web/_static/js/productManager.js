/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.min.js" />

$(function () {
    var pro_id = $("#pro_id").val();

    if (pro_id == 0) 
        $("#tabs").tabs({ disabled: [1, 2, 3, 4] });
    else 
        $("#tabs").tabs();
    

    $("#specsList, #TechProp ul").sortable();
    $(".various").button();
    $("#productPhoto").on("click", "a.delete-photo", function (e) {
        e.preventDefault();
        var photo = $(this).attr("data-photo");
        var url = $(this).attr("href");
        $.ajaxSetup({ cashe: false });

        $.getJSON(url, { photo: photo }, function (data) {
            //console.log(data);

            if (data.success) {
                if (photo === "photo1") {
                    $("#productImage").attr("src", "/_static/images/gallery.png");
                    $("#Photo").val("");
                }
                else {
                    $("#productImage2").attr("src", "/_static/images/gallery.png");
                    $("#Photo2").val("");
                }
            }
        });

    });
    //Add specs button
    $("#btnAddSpecs").click(function (e) {
        e.preventDefault();
        
        var url = $(this).attr("href");
        
        var dialog = "<div title='Add Specification'>";

        $(dialog).append($("<iframe id='SpecsAdd' />").attr({
            "src": url,
            "width": '100%',
            "height": '100%',
            "style": "border: none"

        })).dialog({
            width: 1000,
            height: 800,
            buttons: {
                Submit: function () {
                    var frm = $("#SpecsAdd").contents().find("form");
                    
                    var url = $(frm).attr("action");
                    
                    var d = this;
                    $.post(url, $(frm).serialize(), function (data, status) {
                        if (status == "success" && data.Message == "Done") {
                            $("#specsList").load($("#FindSpecsUrl").val(), null, null);
                            $(d).dialog("close");
                            
                        }
                    }, "json");

                },
                Close: function () { $(this).dialog("close"); }
            }
        });


    });
    /// Add file button
    $("#btnAddFile").click(function (e) {
        e.preventDefault();
        var url = $(this).attr("href");

        var dialog = "<div title='Add File'>";

        $(dialog).append($("<iframe id='FileAdd' />").attr({
            "src": url,
            "width": '100%',
            "height": '100%',
            "style": "border: none"

        })).dialog({
            width: 600,
            height: 500,
            buttons: {
                Submit: function () {
                    var frm = $("#FileAdd").contents().find("form");
                    var url = $(frm).attr("action");
                    var d = this;
                    $.post(url, $(frm).serialize(), function (data, status) {
                        if (status == "success" && data.Message == "Done") {
                            $("#filesList").load($("#FindFilesUrl").val(), null, null);
                            $(d).dialog("close");
                            
                        }
                    }, "json");

                },
                Close: function () {
                    $(this).dialog("close");
                }
            }
        });


    });

    $("a.saveOrder").click(function (e) {
        e.preventDefault();
        var divList = $("#Specifications form");
        $(divList).submit();

    });

    $("#ImportSections").click(function () {

        if ($(this).text() == "Import Sections")
            $(this).text("Hide");
        else
            $(this).text("Import Sections");

        $("#bulk_sections").slideToggle();
        $("#import_notes_frm").hide();
    });

    $("#ImportNotes").click(function () {
        $("#bulk_sections").hide();
        $("#import_notes_frm").slideToggle();
    });
    $("#Sections").on("click", "a.edit-link", function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        var dialog = "<div title='Edit Section'>";

        $(dialog).append($("<iframe id='editSection' />").attr({
            "src": url,
            "width": '95%',
            "height": '95%',
            "style": "border: none"

        })).dialog({
            width: 950,
            height: 800,
            buttons: {
                Submit: function () {
                    var frm = $("#editSection").contents().find("form");
                    var url = $(frm).attr("action");
                    var d = this;
                    $.ajaxSetup({ cashe: false });
                    $.post(url, $(frm).serialize(), function (data, status) {
                        if (status == "success" && data == "done") {
                            $("#sectionsListTable").load($("#SectionsListUrl").val(), null, null);
                            $(d).dialog("close");

                        }
                    });
                },
                Close: function () { $(this).dialog("close"); }
            }
        });
    });


    $("#Sections").on("click", "a.delete-link", function (e) {
        e.preventDefault();
        var a = this;
        $.getJSON($(this).attr("href"), null, function (data) {

            $(a).parent().parent().remove();
        })
    });

    $("#notes form").submit(function (e) {
        e.preventDefault();
        var url = $(this).attr("action");
        var frm = this;
        $.post(url, $(this).serialize(), function (data) {
            $("#notesPlaceHolder").html(data);

            $("#noteText", frm).val("");
            $("#noteNumber", frm).val("");
            $("#note_id", frm).val("0");
        });
    });


    $("#notes #notesList").on("click", "a.delete-note", function (e) {
        e.preventDefault();
        if (confirm("Are you sure you want to delete this note?")) {
            var url = $(this).attr("href");

            $.get(url, null, function (data) {
                $("#notesPlaceHolder").html(data);
            });
        }
    });
    $("#notes #notesList").on("click", "a.edit-link", function (e) {
        e.preventDefault();

        var number = $(this).attr("data-number");
        var txt = $(this).attr("data-text");
        var id = $(this).attr("data-id");

        $("#noteText").val(txt);
        $("#noteNumber").val(number);
        $("#note_id").val(id);
    });




    bindActionLinks();
    bindFilesLinks();


    $('#file_upload').uploadify({
        swf: '/_static/js/lib/uploadfy/uploadify.swf',
        uploader: '/ProductsViewer/UploadPhoto/',
        fileTypeDesc: "Images",
        fileTypeExts: "*.jpg;*.jpeg;*.bmp;*.png;*.gif",
        'fileSizeLimit': '200KB',
        height: '20',
        width: '60',
        // Your options here
        buttonText: 'Edit...',
        removeTimeout: 0,
        multi: false,
        onUploadSuccess: function (file, data, response) {
            //alert('The file ' + file.path + ' was successfully uploaded with a response of ' + response + ':' + data);
            $("#productImage").attr("src", data);
            $("#productPhoto input:hidden#Photo").val(data);
        }
    });

    $('#file_upload2').uploadify({
        swf: '/_static/js/lib/uploadfy/uploadify.swf',
        uploader: '/ProductsViewer/UploadPhoto2/',
        fileTypeDesc: "Images",
        fileTypeExts: "*.jpg;*.jpeg;*.bmp;*.png;*.gif",
        'fileSizeLimit': '200KB',
        height: '20',
        width: '60',
        // Your options here
        buttonText: 'Edit...',
        removeTimeout: 0,
        multi: false,
        onUploadSuccess: function (file, data, response) {
            //alert('The file ' + file.path + ' was successfully uploaded with a response of ' + response + ':' + data);
            $("#productImage2").attr("src", data);
            $("#productPhoto input:hidden#Photo2").val(data);
        }
    });
});




function bindFilesLinks() {
    $("#filesList").on("click", "a.delete-link", function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        var delLink = this;
        $.get(url, null, function (data) {
            if (data == 'Deleted') {
                var tr = $(delLink).parent().parent();
                $(tr).remove();
            }
        });
    });

    $("#filesList").on("click", "a.edit-link", function (e) {
        e.preventDefault();
        var url = $(this).attr("href");

        var dialog = $("#editFileDiv");
        if ($(dialog).length == 0)
            dialog = "<div title='Edit File' id='editFileDiv' >";

        $(dialog).html("");

        $(dialog).append($("<iframe id='FileEditor' />").attr({
            "src": url,
            "width": '100%',
            "height": '100%',
            "style": "border: none"

        })).dialog({
            width: 600,
            height: 500,
            buttons: {
                Submit: function () {

                    var frm = $("#FileEditor").contents().find("form");
                    var url = $(frm).attr("action");
                    var d = this;
                    $.post(url, $(frm).serialize(), function (data, status) {
                        if (status == "success" && data.Message == "Done") {
                            $("#filesList").load($("#FindFilesUrl").val(), null, null);
                            $(d).dialog("close");
                                   

                        }
                    }, "json");
                },
                Close: function () {
                    $(this).dialog("close");
                            
                }
            }
        });

    });


}

function bindActionLinks() {
    $("#specsList").on("click", "a.delete-link", function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        var delLink = this;
        $.get(url, null, function (data) {
            if (data == 'Deleted') {
                var li = $(delLink).parent().parent();
                $(li).remove();
            }
        });
    });

    $("#specsList").on("click", "a.edit-link", function (e) {
        e.preventDefault();
        var url = $(this).attr("href");
        var dialog = "<div title='Edit Specification' class='specseditor'>";

        

        $(dialog).append($("<iframe id='SpecsEditor2' />").attr({
            "src": url,
            "width": '100%',
            "height": '100%',
            "style": "border: none"

        })).dialog({
            width: 950,
            height: 800,
            buttons: {
            }
        });

    });

}


function specsUpdated() {
    $("div.specseditor").dialog("close");
    $("#specsList").load($("#FindSpecsUrl").val(), null, null);
}
