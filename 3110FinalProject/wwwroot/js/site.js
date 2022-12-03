"using strict"
(function _addItem() {
    const addItemModalDOM = document.querySelector("#createPurchaseModal");
    const createAddItemModal = new bootstrap.Modal(addItemModalDOM);
    const addItemButton = document.querySelectorAll("#btnCreatePurchase");
    addItemButton.forEach(item => {
        item.addEventListener("click", event => {
            var productId = $(event.target).data("id"); //https://stackoverflow.com/questions/5309926/how-can-i-get-the-data-id-attribute
            var productName = 'Enter Quantity for ' + $(event.target).data("title");      
            $('#productIdInput').val(productId);
            $('#AddItemTitle').text(productName); //https://stackoverflow.com/questions/6411696/how-to-change-a-text-with-jquery
            createAddItemModal.show();
        })
    })
}());
