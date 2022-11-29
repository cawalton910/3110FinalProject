"use strict";

import { create } from "./customerRepository.js";       //Import the create function

(function _customerCreate() {
    var formCreateCustomer =
        document.querySelector("#createCustomer");              //Get the customerVM form element
    formCreateCustomer.addEventListener('submit', async e => {
        e.preventDefault();
        var formData = new FormData(formCreateCustomer);
        try {
            var result = await create(formData);            //Send ajax call
        }
        finally {
            window.location.replace(`/customer`);       //Redirect
        }

    });
})();