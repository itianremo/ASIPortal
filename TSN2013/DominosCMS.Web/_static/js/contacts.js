/// <reference path="jquery-1.7.1.min.js" />

$(function () {
    var listUrl = $("#listUrl").val();
    var contacts_orderby = "Name";
    updatePager();

    $(".list a.sortLink").click(bindSort);
    function bindSort() {
        //alert($(this));
        var url = $(this).attr("data-source");
        var order = $(this).attr("data-orderby");
        contacts_orderby = order;
        //$("#current-sort").val(order);
        var rndm = Math.floor((Math.random() * 10000) + 1);
        $.get(url, { orderby: order, r: rndm }, function (data) {
            $("#contacts").html(data);
            $("a.sortLink").click(bindSort);

        });
        return false;
    }

    $("#showNumber").change(function () {
        var count = $(this).val();
        $("#dataArea").css("opacity", 0.5);
        $.get(listUrl, { "count": count }, function (data) {
            $("#dataArea").html(data);
            $("#dataArea").css("opacity", 1);
            updatePager();
        });

        
        
    });

    $("a.letter").click(function (e) {
        e.preventDefault();
        var count = $("#showNumber").val();
        var letter = $(this).text();
        $("#letter").val(letter);

        $("#dataArea").css("opacity", 0.5);
        $.get(listUrl, { "count": count, filter: letter }, function (data) {
            $("#dataArea").html(data);
            $("#dataArea").css("opacity", 1);
            updatePager();
        });

    });

    $("a.letter-all").click(function (e) {
        e.preventDefault();
        var count = $("#showNumber").val();
        $("#dataArea").css("opacity", 0.5);
        $.get(listUrl, { "count": count }, function (data) {
            $("#dataArea").html(data);
            $("#dataArea").css("opacity", 1);
            updatePager();
        });
    });

    $("a#next").click(function (e) {
        e.preventDefault();
        var count = $("#showNumber").val();
        var letter = $("#letter").val();
        var currentPage = parseInt($("#currentPage").val());
        if (currentPage == $("#totalPages").val()) {
            //console.log("NO MORE PAGES!");
            return;
        }

        var nextPage = currentPage + 1;
        //get next page
        $("#dataArea").css("opacity", 0.5);
        $.get(listUrl, { orderby: contacts_orderby, "count": count, filter: letter, page: nextPage }, function (data) {
            $("#dataArea").html(data);
            $("#dataArea").css("opacity", 1);
            $(".list a.sortLink").click(bindSort);
        });

        $("#currentPage").val(nextPage);
        //console.log(nextPage);

    });

    $("a#prev").click(function (e) {
        e.preventDefault();
        var count = $("#showNumber").val();
        var letter = $("#letter").val();
        var currentPage = parseInt($("#currentPage").val());
        if (currentPage == 1) {
            //console.log("NO MORE PAGES!");
            return;
        }

        var prevPage = currentPage - 1;
        //get next page
        $("#dataArea").css("opacity", 0.5);
        $.get(listUrl, { orderby: contacts_orderby, "count": count, filter: letter, page: prevPage }, function (data) {
            $("#dataArea").html(data);
            $("#dataArea").css("opacity", 1);
            $(".list a.sortLink").click(bindSort);
        });

        $("#currentPage").val(prevPage);
        //console.log(nextPage);
    });

});

function updatePager() {
    var pages = $("#totalPages").val();
    if (pages <= 1)
        $("#pager").hide();
    else
        $("#pager").show();

}