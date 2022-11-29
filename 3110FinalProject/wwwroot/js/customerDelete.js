"use strict";

import { deleteCustomer } from "./customerRepository.js";       //Import the create function

(function _customerDelete() {
    var formDeleteCustomer =
        document.querySelector("#deleteCustomer");              //Get the customer form element
    formDeleteCustomer.addEventListener('submit', async e => {
        e.preventDefault();
        var formData = new FormData(formDeleteCustomer);
        var id = formData.get("Id");                        //Get the customer Id from the form data
        try {
            var result = await deleteCustomer(id);          //Send ajax call
        }
        finally {
            window.location.replace(`/customer`);       //Redirect
        }
    });
})();