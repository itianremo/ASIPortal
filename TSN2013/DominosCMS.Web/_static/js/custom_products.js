$(function () {
    $("#customProducts").on("click", "table.list th a", function (e) {
        var url = $(this).attr("data-source");
        var order = $(this).attr("data-orderby");
        e.preventDefault();

        $.get(url, { orderby: order }, function (data) {
            //console.log(data);
            $("#customProducts").html(data);
        });
    });

});
