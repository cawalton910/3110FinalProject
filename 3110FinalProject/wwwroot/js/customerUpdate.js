"use strict";

import { update } from "./customerRepository.js";       //Import the create function

(function _customerUpdate() {
    var formUpdateCustomer =
        document.querySelector("#updateCustomer");              //Get the customerVM form element
    formUpdateCustomer.addEventListener('submit', async e => {
        e.preventDefault();
        var formData = new FormData(formUpdateCustomer);
        try {
            var result = await update(formData);            //Send ajax call
        }
        finally {
            window.location.replace(`/customer`);
        }
    });
})();