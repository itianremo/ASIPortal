/// <reference path="jquery-1.5.1-vsdoc.js" />
; (function ($) {
    var defaults = {
        width: "941px",
        height: "280px"
    };

    $.fn.rotator = function (options) {
        return new Rotator(this, options);
    };



    function Rotator(element, options) {
        this.element = element;
        this.config = $.extend({}, defaults, options);
        this.wrapper = null;
        this.selectors = null;
        this.list = null;
        this.init();
    }

    Rotator.prototype.init = function () {
        this.element.hide();

        var autoPlayTimer = null;

        this.wrapper = $("<div />", {
            width: this.config.width,
            height: this.config.height,
            id: "bannerWrapper"
        });

        this.list = $("<ul />", {id: "list"}).appendTo(this.wrapper);

        var banners_count = $("li", this.element).length;
        this.selectors = $("<div class='selectors'></div>");

        var context = this;
        $("li", this.element).each(function (i) {
            var image = $("img", this).attr("src");
            $("<li />", {
                width: context.config.width,
                height: context.config.height
            }).css({ "background-image": 'url(' + image + ')', "position": "absolute" }).hide().hover(function () {
                clearInterval(autoPlayTimer);
            }, function () {
                autoPlayTimer = setInterval(context.autoPlay, 5000);

            }).appendTo(context.list);

            $(context.selectors).append($("<a />"));

        });

        $("a", this.selectors).each(function (i) {
            $(this).click(function (e) {
                e.preventDefault();
                var item = $("li", context.list).get(i);
                //var visibleItem = $("li", list).is(":not(:hidden)");
                $("li", context.list).hide();
                $(item).slideDown();
                $("a", this.selectors).removeClass("selected");
                $(this).addClass("selected");
            });
        })

        if (banners_count > 1) {
            autoPlayTimer = setInterval(this.autoPlay, 5000);
            $(this.wrapper).append(this.selectors);
        }

        $("a", this.selectors).first().addClass("selected");
        $("li", this.list).first().show();
        $(this.wrapper).insertAfter(this.element);

        return this.wrapper;
    }
    Rotator.prototype.autoPlay = function () {
        var selected = $("a[class='selected']", this.selectors);

        var index = $("a", this.selectors).index(selected) + 1;
        if (index == $("a", this.selectors).length) {

            $("a", this.selectors).removeClass("selected");
            $("a", this.selectors).first().addClass("selected");
            $("li", this.list).fadeOut();
            $("li", this.list).first().fadeIn();
        }
        else {
            var item = $("li", this.list).get(index);
            //var visibleItem = $("li", list).is(":not(:hidden)");
            $("li", this.list).fadeOut();
            $(item).fadeIn();
            $("a", this.selectors).removeClass("selected");
            var selector = $("a", this.selectors).get(index);
            $("a", this.selectors).removeClass("selected");
            $(selector).addClass("selected");
        }
    }
}(jQuery));

