/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.js" />


$(function () {
    $("#tabs").tabs();


    //bind product select
    $("#product").change(function () {
        var id = $(this).val();
        $($("<span/>", { id: "fieldsLoader", "class": "loading" })).insertAfter(this).show();
        loadAttributes(id);

        updatePhoto(id);
    });

    var proID = $("#productID").val();

    $("#product").val(proID);
    loadAttributes(proID);

    $("#sections").on("click", "a", function (e) {
        e.preventDefault();

        var msg = "Please confirm your selection\n\n";
        msg += "\tSection name: " + $(this).text() + "\n\n";
        
        var cw = $("#CoatingWeights").val();
        var po = $("#Punchouts").val();
        var bootkit = $("#BootKit").val();

        //console.log($(this).text() + " -- " + $(this).attr("href"));
        if (cw)
            msg += "  Coating Weight: " + cw;
        else
            msg += "  Coating Weight: G60";

        msg += "\n";

        if (po)
            msg += "  Punchout: " + po;
        else
            msg += "  Punchout: Punched";

        if (bootkit)
            msg += "\n  Boot Kit: " + bootkit;


        if (confirm(msg)) {
            var url = $(this).attr("href");
            if (!po) po = "";
            if (!cw) cw = "";
            if (!bootkit) bootkit = "";
            url = url + "&cw=" + cw + "&po=" + po + "&bootkit=" + bootkit;
            if ($("#dtype").val() == "fav")
                url = url + "&type=fav&favID=" + $("#favID").val();
            else
                url = url + "&type=" + $("#dtype").val();
            //alert(url);
            window.parent.location.href = url + "&cw=" + cw + "&po=" + po + "&bootkit=" + bootkit;

        }
    });

});


function loadAttributes(id)
{
    var fields = $("#fields");
    var Selects = new Array();
    var url = "/Submittals/LoadFields2?id=" + id + "&SelectNo=0&Selects=" + Selects;

    $(fields).load(url, null, function () {
        updateSections();
        updatePhoto(id);
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
        }
    });
    /////////////////////////////////////////////////////////////////////


    var id = $("#product").val();
    var wd = $("#fields #WebDepth").val();
    var fw = $("#fields #FlangeWidth").val();
    var mt = $("#fields #MilThickness").val();
    var yt = $("#fields #YeildStrength").val();
    var lt = $("#fields #LegLength").val();
    var sitem_id = $("#sitem_id").val();

    $.ajaxSetup({ cache: false });

    var params = { "pro_id": id, "wd": wd, "fw": fw, "mt": mt, "yt": yt, lt: lt, submittal_id: $("#submittal_id").val(), defineView: true, sitem: sitem_id };

    //////////////////////////////////////
    var sec_label = $("#sectionLabel");

    if (sec_label)
    {
        //get the label
        $.get("/Submittals/GetLabel/", params, function (data) {
            $(sec_label).text(data);
        });
    }



    $.get("/Submittals/GetSections/", params, function (data) {
        $("#sections").html(data);
        $("span#fieldsLoader").remove();
        AdjustCurrentCoatingnPunched();

    });



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
