/// <reference path="jquery-1.5.1-vsdoc.js" />
/// <reference path="jquery-ui-1.8.11.min.js" />

var testmonials_count = 0;
jQuery(function () {

    /////////////////////////////////////////////////////////////////////////////////
    //menu scripts


    $("a.delete-link").click(function () {
        return confirm("Are you sure you want to delete this item?")
    });

    $("#socialLinks img").hover(function () {
        var src = $(this).attr("src");

        $(this).attr("src", src.replace("-n", "-o"));
    }, function () {
        var src = $(this).attr("src");

        $(this).attr("src", src.replace("-o", "-n"));
    });



    $("#adminPanel").hover(function () {
        $("#adminPanel").stop(true, true).delay(200).animate({ 'top': 0 });
    }, function () {
        $("#adminPanel").stop(true, true).delay(800).animate({ 'top': '-40px' });
    });

    /// Script for testmonials
    var autoPlayTestmonialsTimer = null;

    testmonials_count = $("#testmonials .testmonial").length;
    //alert(testmonials_count);
    if (testmonials_count > 1) { //if more than 1 slides
        autoPlayTestmonialsTimer = setInterval(autoPlayTestmonials, 4000);
    }


    $("#testmonials .testmonial").first().show();

    $("#testmonials .testmonial").hover(function () {
        clearInterval(autoPlayTestmonialsTimer);
    }, function () {
        autoPlayTestmonialsTimer = setInterval(autoPlayTestmonials, 4000);
    });

     



});

function autoPlayTestmonials() {
    moveNextTestmonials();
}

var indx = 0;

function moveNextTestmonials() {
    var i1 = indx;
    var i2 = indx + 1;

    if (i2 == testmonials_count)
    {
        i2 = 0;
    }
    indx = i2;
    var current = $("#testmonials .testmonial").get(i1);
    var next = $("#testmonials .testmonial").get(i2);

    $(current).fadeOut(1000, function () {
        $(next).fadeIn(300);            
    });
    



}

function trim(str) {
    //return str.replace(/^\s*/, "").replace(/\s*jQuery/, "");
}

function browse(path) {
    window.open(path, 'browser', 'toolbar=yes,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=900,height=700').focus();
}
