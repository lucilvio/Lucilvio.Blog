(function($) {

    $(document).on("ready", function () {

        $("form").attr("novalidate", "novalidate");

        $("form").validate({
            errorPlacement: function (error, element) {
                return true;
            }
        });

        $("form").on("submit", function () {
            if (!$(this).valid()) {
                return false;
            }
        });

    });

})(jQuery);