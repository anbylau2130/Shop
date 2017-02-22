(function () {
    usp.namespace("usp.shop.shopcommoditytype.add");

    usp.shop.shopcommoditytype.add = {
        init: function (btnReturnId, basePage) {
            usp.shop.shopcommoditytype.add.eventInit.btnReturnInit(btnReturnId, basePage);
        },
        eventInit: {
            btnReturnInit: function (id, basePage) {
                $(id).on("click", function () {
                    location.href = basePage;
                    return false;
                });
            }
        }
    }
})(this);