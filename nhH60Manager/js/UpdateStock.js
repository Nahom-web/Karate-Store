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

let getProduct = async (id)=> {
    await fetch(`${PRODUCTS_API}/${id}`)
        .then(resp => {
            return resp.json();
        })
        .then(response => {
            stock.value = response.stock;
        })
        .catch(err => {
            messages.style.display = "inline";
            productPageErrorMessage.innerHTML = err;
        })
}

getProduct(productId);

console.log(stock);