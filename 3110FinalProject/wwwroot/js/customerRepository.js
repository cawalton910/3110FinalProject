"using strict";
//Repository the handles creating, updating, and deleting customers AJAX requests
const baseAddress = "/api/customerapi";         //Path to the CustomerAPIController


//Sends AJAX request to api/customerapi/edit with the formdata to update a customer
export async function update(formData) {
    const address = `${baseAddress}/edit`;
    const response = await fetch(address, {
        method: "put",
        body: formData
    });
    if (!response.ok) {
        throw new Error("There was an error updating the customer.");
    }
    return await response.json();
}


//Sends AJAX request to api/customerapi/create with the formdata to create a customer
export async function create(formData) {
    const address = `${baseAddress}/create`;
    const response = await fetch(address, {
        method: "post",
        body: formData
    });
    console.log(response);
    if (!response.ok) {
        throw new Error("There was an error creating the customer.");
    }
    return await response.json();
}


//Sends AJAX request to api/customerapi/delete/{id} with the customer id to delete
export async function deleteCustomer(id) {
    const address = `${baseAddress}/delete/${id}`;
    console.log(address);
    const response = await fetch(address, {
        method: "delete"
    });
    if (!response.ok) {
        throw new Error("There was an error deleting the customer.");
    }
    return await response.text();
}