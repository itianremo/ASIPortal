/// <reference path="jquery-1.4.1-vsdoc.js" />


$(function () {
    $("#Button1").click(function () {
        var fs = $("#FloorSystems").val();
        var fm = $("#FireStoppingMethods").val();
        var jw = $("#JointWidths").val();
        var mda = $("#MaximumDeflectionAmounts").val();
        var msg = "FRList/?fs=" + fs + "&";
        msg += "fm=" + fm + "&";
        msg += "jw=" + jw + "&";
        msg += "mda=" + mda;
        //alert(msg);
        $("#loader").show();
        
        $("#frList").load(msg, null, function () {
            $(".fire-rated").fancybox({
                width: 1000,
                height: 800,
                autoSize: false,
                closeClick: false,
                openEffect: 'elastic',
                closeEffect: 'elastic',
                closeBtn: true

            });
            $("#loader").hide();
        });
        return false;
    });
    
})