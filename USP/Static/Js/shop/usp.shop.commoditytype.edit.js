(function () {
    usp.namespace("usp.shop.commoditytype.edit");
    usp.shop.commoditytype.edit = {
        init: function (btnReturnId, basePage) {
            $(".datepicker").datetimepicker({
                format: 'YYYY-MM-DD'
            });

            usp.shop.commoditytype.edit.eventInit.btnReturnInit(btnReturnId, basePage);
        },
        eventInit: {
            btnReturnInit: function (id, basePage) {
                $(id).on("click", function () {
                    location.href = basePage;
                    return false;
                });
            }
        },
    }
})(this);