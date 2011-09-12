// Handles Basket Show Hide
(function ($) {
    $(".total a").live('click', function () {
        var target = $(".minibasket .items");

        if(!target.hasClass("toggled") && !target.is(":animated")) {
        $(target).slideToggle('slow', function () {
            target.addClass("toggled");    
        });
        }
    });

    $('.items').live('mouseleave', function () {
        var target = $(".items");
        
        target.slideToggle('slow', function () {
            target.removeClass("toggled");
        });
    });

    $("a[data-zoom=true]").live("click", function (evt) {
        var viewer = new Seadragon.Viewer("zoomContainer");

        var zoomImage = $(this).data('zoom-image');
        // Need to get the model from the attribute

        viewer.openDzi(zoomImage);
    });

} (jQuery));

$(document).ready(function () {
    $(".basketQuantity").change(function () {
        $("input[type=submit]").click();
    });

});