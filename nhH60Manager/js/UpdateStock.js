// Nahom Haile
// Web Programming VI
// UpdateStock.js
// File contents: has the functions to update the stock for a product

let search = window.location.search;
console.log(search);
let qString = search.substring(1);
while(qString.indexOf("+") !== -1){
    qString  = qString.replace("+", "");
}
let qArray = qString.split("&");
let values = [];
for(let i = 0; i < qArray.length; i++){
    let pos = qArray[i].search("=");
    let keyVal = qArray[i].substring(0, pos);
    let dataVal = qArray[i].substring(pos + 1);
    dataVal = decodeURIComponent(dataVal);
    values[keyVal] = dataVal;
}

let productId = parseInt(values['id'])

let product;

let getProduct = async (id)=> {
    await fetch(`${PRODUCTS_API}/${id}`)
        .then(resp => {
            return resp.json();
        })
        .then(response => {
            console.log(response)
            product = new Product(response.productId, response.prodCatId, response.description, response.manufacturer, response.stock, response.buyPrice, response.sellPrice);
            productToUpdate.innerHTML = product.description;
            console.log(product);
            stockField.value = product.stock;
        })
        .catch(err => {
            messages.style.display = "inline";
            productPageErrorMessage.innerHTML = err;
        })
}

getProduct(productId);

let postStockUpdates = async (e) => {
    e.preventDefault();
    product.stock = parseInt(stockField.value);
    console.table(product);
    await fetch(`${PRODUCTS_API}/${product.productId}`, {
        method: 'PUT',
        headers: new Headers({
            'Content-Type': 'application/json; charset=UTF-8'
        }),
        body: JSON.stringify(product)
    })
        .then(resp => {
            if(resp.status === 204){
                window.location = "./Products.html";
            }
            if(resp.status === 400){
                stockErrorMessage.innerHTML = response.errors.id[0];
            }
        })
        .catch(err => {
            messages.style.display = "inline";
            productPageErrorMessage.innerHTML = err;
            console.log(err);
        })
}

updateStock.addEventListener('submit', postStockUpdates);