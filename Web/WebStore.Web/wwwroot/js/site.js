// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function addToCart(productId, quantity) {
    if (document.contains(document.getElementById("addedItemToCart"))) {
        document.getElementById("addedItemToCart").remove();
    }
    var antiForgeryToken = $('#antiForgeryForm input[name=__RequestVerificationToken]').val();
    var data = { productId: productId, quantity: quantity };
    $.ajax({
        type: "POST",
        url: '/shoppingcart/addcartitem',
        data: JSON.stringify(data),
        dataType: "html",
        contentType: "application/json; charset=utf-8",
        headers: {
            'RequestVerificationToken': antiForgeryToken
        },
        success: function (modal) {
            var myModal = new bootstrap.Modal($(modal)) // relatedTarget
            myModal.show();
            renewCartItemCount();
        }
    });
}

function getCartItems() {
    if (document.contains(document.getElementById("cartItems"))) {
        document.getElementById("cartItems").remove();
    }
    $.ajax({
        url: '/shoppingcart/getcartitems',
        dataType: "html",
        success: function (modal) {
            var myModal = new bootstrap.Modal($(modal)) 
            myModal.show();
        }
    });
}

function removeCartItem(id) {
    $.ajax({
        url: '/shoppingCart/RemoveCartItem/' + id,
        dataType: "text",
        success: function (data) {
            var el = document.getElementById(id);
            el.remove();
            var obj = JSON.parse(data);
            document.getElementById("cartTotalPrice").innerText = "$" + obj.totalPrice;
            renewCartItemCount();
        }
    });
}

function renewCartItemCount() {
    $.ajax({
        url: '/shoppingCart/GetCartItemsCount/',
        dataType: "text",
        success: function (data) {
            var obj = JSON.parse(data);
            console.log(data);
            console.log(obj.cartItemsCount);
            console.log(document.getElementById("lblCartCount"));
            document.getElementById("lblCartCount").innerText = obj.cartItemsCount;
        }
    });
}