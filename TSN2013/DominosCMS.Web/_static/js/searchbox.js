/// <reference path="jquery-1.4.1-vsdoc.js" />

jQuery(function () {

    jQuery("#menubar #search-box input#kw").focus(function () {
        jQuery(this).css("color", "black");
        jQuery(this).css("font-style", "normal");
        if (jQuery(this).val() == "Search TSN...")
            jQuery(this).val("");
    });
    jQuery("#menubar #search-box input#kw").blur(function () {
        if (trim(jQuery(this).val()) == "") {
            jQuery(this).val("Search TSN...");
            jQuery(this).css("color", "");
            jQuery(this).css("font-style", "");
        }
    });

    jQuery("#menubar #search-box form").submit(function () {
        var kw = trim(jQuery("#kw", this).val());

        if (kw == "" || kw == "Search TSN...")
            return false;
    });

    //fix the problem when the user click the back button from the search results page
    var kwInput = jQuery("#menubar #search-box input#kw");
    var kw = trim(jQuery(kwInput).val());
    if (kw != "Search TSN...") {
        jQuery(kwInput).css("color", "black");
        jQuery(kwInput).css("font-style", "normal");

    }

    /////////////////////////////////////////////////////////////////////////////////

})

function trim (str) {
    return str.replace(/^\s*/, "").replace(/\s*jQuery/, "");
}