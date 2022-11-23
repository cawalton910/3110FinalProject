"using strict"
(function _homeIndexMain() {
    console.log("Home Index");
    const createQuoteModalDOM = document.querySelector("#createPurchaseModal");
    const createQuoteModal = new bootstrap.Modal(createQuoteModalDOM);
    const createQuoteButton = document.querySelectorAll("#btnCreatePurchase");
    console.log(createQuoteButton);
    createQuoteButton.forEach(item => {
        item.addEventListener("click", event => {
            var productId = $(event.target).data("id"); //https://stackoverflow.com/questions/5309926/how-can-i-get-the-data-id-attribute
            var productName = 'Enter Quantity for ' + $(event.target).data("title");
            console.log(productId);
            
            $('#productIdInput').val(productId);
            $('#AddItemTitle').text(productName); //https://stackoverflow.com/questions/6411696/how-to-change-a-text-with-jquery
            console.log("Y");
            createQuoteModal.show();
        })
    })
}());
