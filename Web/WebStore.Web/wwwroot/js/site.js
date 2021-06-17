// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function addToCart(productId, quantity) {
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
        success: function (modal)
        {
            var myModal = new bootstrap.Modal($(modal)) // relatedTarget
            myModal.show();

        }
    });
}