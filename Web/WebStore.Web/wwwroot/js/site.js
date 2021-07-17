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
        $('#cartItems').modal('hide');
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
            var items = document.getElementsByClassName("lblCartCount");
            for (var i = 0; i < items.length; i++) {
                items[i].innerText = obj.cartItemsCount;
            }
            if (document.contains(document.getElementById("checkoutCartItemsCount"))) {
                if (obj.cartItemsCount == 0) {
                    $('#staticModalForEmptyCart').modal('show');
                }
                document.getElementById("checkoutCartItemsCount").innerText = obj.cartItemsCount;
            }
        }
    });
}

function clearCart() {
    $.ajax({
        url: '/shoppingCart/ClearCart/',
        dataType: "text",
        success: function (data) {
            renewCartItemCount();
            getCartItems();
        }
    });
}


function addToCartWithQuantity(id) {
    var el = document.getElementById("quantityValue");
    var quanity = parseInt(el.value);
    if (quanity >= 1) {
        addToCart(id, quanity);
    }
}

function drawChart(info) {
    var obj = JSON.parse(info);
    console.log(obj);
    var xValues = obj.days;
    var yValues = obj.sales;

    const data = {
        labels: xValues,
        datasets: [{
            fill: false,
            label: 'Sales',
            backgroundColor: 'rgb(255, 99, 132)',
            borderColor: 'rgb(255, 99, 132)',
            data: yValues,
        }]
    };

    const config = {
        type: 'line',
        data: data,
        options: {
            plugins: {
                filler: {
                    propagate: false,
                },
            },
            interaction: {
                intersect: false,
            }
        },
    };

    var myChart = new Chart(
        document.getElementById('myChart'),
        config
    );
}