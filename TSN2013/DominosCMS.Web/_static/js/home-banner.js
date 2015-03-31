/// <reference path="jquery-1.5.1-vsdoc.js" />

$(function () {
    var banner_count = $("#bannerWrapper ul li").length;
    var autoPlayTimer = null;
    //add the nav buttons

    if (banner_count > 1) { //if more than 1 slides
        autoPlayTimer = setInterval(autoPlay, 5000);
        var selectors = $("<div class='selectors'></div>");

        for (i = 1; i <= banner_count; i++) {
            $(selectors).append($("<a href='#' rel='" + i + "'></a>"))
        }

        $("#bannerWrapper").append(selectors);
    }
    var rel = 1;
    //alert(banner_count);
    $("#bannerWrapper ul li[rel='" + rel + "']").show();
    $("#bannerWrapper .selectors a[rel='" + rel + "']").addClass("selected");

    $("#bannerWrapper ul li").hover(function () {
        clearInterval(autoPlayTimer);
    }, function () {
        autoPlayTimer = setInterval(autoPlay, 5000);
    });

    $("#bannerWrapper .selectors a").hover(function () {
        clearInterval(autoPlayTimer);
    }, function () {
        autoPlayTimer = setInterval(autoPlay, 5000);
    });

    $("#bannerWrapper .selectors a").click(function (e) {

        e.preventDefault();
        var selected = $("#bannerWrapper .selectors a[class='selected']");
        var current = parseInt($(selected).attr("rel"));

        var next = $(this).attr("rel");
        if (current == next) return;
        if (rel > banner_count)
            rel = 1;

        //alert("#bannerWrapper ul li[rel='" + current + "']");
        var currentBanner = $("#bannerWrapper ul li[rel='" + current + "']");
        var nextBanner = $("#bannerWrapper ul li[rel='" + next + "']");
        $(nextBanner).fadeIn(1000);
        $(currentBanner).fadeOut(1000);

        //change the selected thumb
        $(selected).removeClass("selected");
        $(this).addClass("selected");


    });
});

function autoPlay() {
    moveNext();
}

function moveNext() {
    var selected = $("#bannerWrapper .selectors a[class='selected']");
    var current = parseInt($(selected).attr("rel"));
    var next = current + 1;
    var banner_count = $("#bannerWrapper ul li").length
    if (next > banner_count)
        next = 1;

    var currentBanner = $("#bannerWrapper ul li[rel='" + current + "']");
    var nextBanner = $("#bannerWrapper ul li[rel='" + next + "']");
    $(nextBanner).fadeIn(1000);
    $(currentBanner).fadeOut(1000);

    //change the selected thumb
    $(selected).removeClass("selected");
    $("#bannerWrapper .selectors a[rel='" + next + "']").addClass("selected");

}