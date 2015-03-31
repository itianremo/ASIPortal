/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.js" />


$(function () {
    $("#tabs").tabs();


    //bind add clip button

    $("#customList a, #clipsList .action a, #reportsList  a").click(function (e) {
        e.preventDefault();

        var a = this;

        if ($(a).is(".delete-section")) {
            var url = $(a).attr("data-Remove-Action");
            
            $(a).addClass("loading");

            $.post(url, { submittal_id: $("#submittal_id").val() }, function (data) {
                $(a).attr("title", "Click to add to submittal");
                $(a).text("Add");
                $(a).removeClass("loading");
                $(a).removeClass("delete-section");
            });

        } else {
            //console.log("Add this to submittal");
            var url = $(a).attr("href");
            $(a).addClass("loading");
            //console.log($("#submittal_id").val());

            $.post(url, { submittal_id: $("#submittal_id").val(), po: $("#Punchouts").val(), cw: $("#CoatingWeights").val(), bootkit: $("#BootKit").val() }, function (data) {

                $(a).removeClass("loading");
                $(a).addClass("delete-section");
                $(a).attr("title", "Click to remove from submittal");
                $(a).text("Remove");
            });
            
        }

    });

    //bind product select
    $("#product").change(function () {
        var id = $(this).val();
        $($("<span/>", { id: "fieldsLoader", "class": "loading" })).insertAfter(this).show();
        loadAttributes(id);

        updatePhoto(id);
    });

    $("#btnProgress").click(function (e) {
        e.preventDefault();
        var url = $(this).attr("data-href");
        var dialog = "<div title='Submittal Progress'>";

        $(dialog).append($("<iframe id='progress' />").attr({
            "src": url,
            "width": '100%',
            "height": '100%',
            "style": "border: none"
                    
    })).dialog({
        width: 600,
        height: 400,
        buttons: {
            Close: function () { $(this).dialog("close"); }
        }
    });
    });
    $("#btnComplete, #btnGeneratePDF, #btnCancel").click(function (e) {
        var url = $(this).attr("data-href");

        location.href = url;
    });

});


function loadAttributes(id)
{
    var fields = $("#fields");
    var Selects = new Array();
    var url = "/Submittals/LoadFields2?id=" + id + "&SelectNo=0&Selects=" + Selects;

    $(fields).load(url, null, function () {
        updateSections();

    });
}

function AdjustCurrentCoatingnPunched() {
    if ($("#CoatingWeights").length > 0 || $("#Punchouts").length > 0) {

        $("#trPunched").attr("style", "visibility:visible;");

        if ($("#CoatingWeights").length > 0) {
            $("#G60").text($("#CoatingWeights").val() + ", ");
        }
        else {
            $("#G60").text("");
        }
        if ($("#Punchouts").length > 0) {
            $("#Punched").text($("#Punchouts").val());
        }
        else {
            $("#Punched").text("");
        }
    }
    else {
        //$("#trPunched").attr("style", "visibility:hidden;");
    }
}

function updateSections(obj)
{
    var id = $(obj).attr("id")
    if (id === "CoatingWeights" || id === "Punchouts") {
        AdjustCurrentCoatingnPunched();
        return;
    }

    //if drop down list has one item, select it 
    $("#fields select").each(function () {
        var opts = $("option", this).length;

        var v1 = $("option", this).first().val();
        
        if (opts == 2 && v1 == '') {
            this.selectedIndex = 1; //select the index 1
            //$(this).trigger("change");
        }
    });
    /////////////////////////////////////////////////////////////////////

    var id = $("#product").val();
    var wd = $("#fields #WebDepth").val();
    var fw = $("#fields #FlangeWidth").val();
    var mt = $("#fields #MilThickness").val();
    var yt = $("#fields #YeildStrength").val();
    var lt = $("#fields #LegLength").val();
    $.ajaxSetup({ cache: false });

    var params = { "pro_id": id, "wd": wd, "fw": fw, "mt": mt, "yt": yt, lt: lt, submittal_id: $("#submittal_id").val() };

    //////////////////////////////////////
    var sec_label = $("#sectionLabel");

    if (sec_label) {
        //get the label
        $.get("/Submittals/GetLabel/", params, function (data) {
            $(sec_label).text(data);
        });
    }

    $.get("/Submittals/GetSections/", params, function (data) {
        $("#sections").html(data);
        $("span#fieldsLoader").remove();
        AdjustCurrentCoatingnPunched();


        $("#sections table a.action").click(function (e) {
            e.preventDefault();
            var a = this;

            if ($(a).is(".delete-section")) {
                var url = $(a).attr("data-Remove-Action");
                //console.log(url);
                $(a).addClass("loading");

                $.post(url, { submittal_id: $("#submittal_id").val() }, function (data) {
                   
                    $(a).attr("title", "Click to add to submittal");
                    $(a).text("Add");
                    // mglil
                    var AddButton = $("button.add-section");
                    AddButton.text("Add to submittal");
                    //end mglil
                    $(a).removeClass("loading");
                    $(a).removeClass("delete-section");
                });

            } else {
                //console.log("Add this to submittal");
                var url = $(a).attr("href");
                $(a).addClass("loading");
                
                $.post(url, { submittal_id: $("#submittal_id").val(), po: $("#Punchouts").val(), cw: $("#CoatingWeights").val(), bootkit: $("#BootKit").val() }, function (data) {
                    $(a).attr("title", "Click to remove to submittal");
                    // mglil
                    var AddButton = $("button.add-section");
                    AddButton.text("Remove");
                    //end mglil
                    $(a).removeClass("loading");
                    $(a).addClass("delete-section");
                    $(a).text("Remove");
                });
            }
        })

        //remove the button if any 

        $("#frm button.add-section").remove();
        //get number of sections 
        var count = $("#sections table a.action").length;

        if (count == 1) {
            //add button 

            $("<button />").attr({
                "class": "add-section"
                
            }).text("Add to submittal").click(function () {
                $("#sections table a.action").trigger("click");
                var caption = $(this).text();
                if (caption.match("^Add"))
                    $(this).text("Remove from Submittal");
                else
                    $(this).text("Add to submittal");

            }).appendTo($("#frm"));
        }

    });

}

function removeSubmittalItem() {
    
}

function updatePhoto(id) {
    var url = "/ProductsViewer/GetProductImagePath/" + id;
    

    $.get(url, null, function (data) {
        if (data) {
            //remove them first 

            $("#photo1, #photo2, #pro_photo span").remove();

            //hidden field to store photo1 path 
            $("<hidden />").attr({
                "value": data.Photo1,
                id: "photo1"
            }).appendTo($("#pro_photo"));
            //hidden field to store photo2 path 
            $("<hidden />").attr({
                "value": data.Photo2,
                id: "photo2"
            }).appendTo($("#pro_photo"));

            $("<span />").attr({
                "class": "section-label",
                id: "sectionLabel"
            }).appendTo($("#pro_photo"));

            if (data.Photo1) {
                $("#productImage").attr("src", data.Photo1);
                $("#productImage").show();
            } else {
                $("#productImage").hide();
                $("#pro_photo span").remove();

            }
            
        } 
    }, "json");
}
