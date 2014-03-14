$(function () {
    $(window).scroll(function () {
        if ($(".navbar").offset().top > 30) {
            $(".navbar-fixed-top").addClass("sticky");
        }
        else {
            $(".navbar-fixed-top").removeClass("sticky");
        }
    });

    staticHeader.initialize();

 
});



var staticHeader = {
    initialize: function () {
        if ($(".navbar-static-top").length) {
            $("body").css("padding-top", 0);
        }
    }
}


