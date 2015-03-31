/// <reference path="jquery-1.5.1-vsdoc.js" />
var flag = false;
jQuery(function () {
    var opentime, closetime;
    $('#menubar ul li').hover(function () {
        $(this).find('>ul').stop(true, true).delay(200).fadeIn();
        $(this).addClass("selected-menu");
    }, function () {
        $(this).find('>ul').stop(true, true).delay(300).fadeOut();
        $(this).removeClass("selected-menu");
        
    });

    //$('#menubar ul li').hover(function () {
    //    $(this).find('>ul')
    //    .stop(true, true).toggle();
    //});


    //jQuery("#menubar ul li").hover(function () {
    //    var obj = this;
    //    window.clearTimeout(closetime);
    //    opentime = window.setTimeout(function () {
    //        flag = true;
    //        jQuery(">ul", obj).fadeIn("fast");
    //        jQuery(obj).addClass("selected-menu");
    //    }, 100);
    //}, function () {
    //    flag = false;
    //    jQuery(">ul", this).delay(320);
    //    if (flag) return;
    //    window.clearTimeout(opentime);
    //    jQuery("ul", this).fadeOut("fast");
    //    jQuery(this).removeClass("selected-menu");
    //    jQuery("li", this).removeClass("selected-menu");


    //});

});

function hideMenu(obj) {


}

function browse(path) {
    window.open(path, 'browser', 'toolbar=yes,location=no,status=yes,menubar=no,scrollbars=yes,resizable=yes,width=900,height=700').focus();
}