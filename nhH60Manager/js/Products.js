// Nahom Haile
// Web Programming VI
// Product.js
// File contents: has the functions for the product page

let getProducts = async () => {
    productManager = new ProductManager();
    await fetch(`${PRODUCTS_API}`)
        .then(resp => {
            return resp.json();
        })
        .then(response => {
            for(let i = 0; i < response.length; i++){
                productManager.products[i] = new Product(response[i].productId, response[i].prodCatId, response[i].description, response[i].manufacturer, response[i].stock, response[i].buyPrice, response[i].sellPrice);
            }
            displayProducts()
        })
        .catch(err => {
            messages.style.display = "inline";
            productPageErrorMessage.innerHTML = err;
        })
    await getProductCategories()
}

let getProductCategories = async () => {
    await fetch(`${PRODUCT_CATEGORIES_API}`)
        .then(resp => {
            return resp.json();
        })
        .then(response => {
            for(let i = 0; i < response.length; i++){
                productManager.categories[response[i].categoryId] = new ProductCategory(response[i].categoryId, response[i].prodCat);
                for(let x = 0; x < response[i].products.length; x++){
                    productManager.categories[response[i].categoryId].products.push(new Product(response[i].products[x].productId, response[i].products[x].prodCatId, response[i].products[x].description, response[i].products[x].manufacturer, response[i].products[x].stock, response[i].products[x].buyPrice, response[i].products[x].sellPrice))
                }
            }
        })
        .catch(err => {
            messages.style.display = "inline";
            productPageErrorMessage.innerHTML = err;
        })
    displayProductCategories()
}

displayProductCategories = () =>{
    for(let c in productManager.categories){
        categoryTypes.innerHTML += `<option value="${productManager.categories[c].categoryId}">${productManager.categories[c].name}</option>`
    }
}

let getProductsSortedByCategory = async () => {
    await productManager.productsByCategory();
    console.table(productManager.products);
    containerProducts.innerHTML = "";
    containerProductsByCategory.innerHTML = "";
    searchResult.innerHTML = "";
    let currentCategory = 0;
    for (let p = 0; p < productManager.foundProducts.length; p++) {
        if (productManager.foundProducts[p].catId !== currentCategory) {
            currentCategory = productManager.foundProducts[p].catId;
            containerProductsByCategory.innerHTML += `<h4 class="categorySubTitle">${productManager.categories[productManager.foundProducts[p].prodCatId].name}</h4>`;
            containerProductsByCategory.innerHTML += `<div class="card"><div class="card-body">${productManager.foundProducts[p].displayProduct()}</div></div>`
        } else {
            containerProductsByCategory.innerHTML += `<div class="card"><div class="card-body">${productManager.foundProducts[p].displayProduct()}</div></div>`
        }
    }
}

let displayProducts = async (e) => {
    containerProductsByCategory.innerHTML = "";
    productManager.searchValue = productEntered.value;
    if (productManager.searchValue === "") {
        containerProducts.innerHTML = "";
        productManager.products.forEach((element, index) =>
            containerProducts.innerHTML += `<div class="card"><div class="card-body">${element.displayProduct()}</div></div>`
        );
    } else {
        await productManager.searchValueResults();
        productEntered.innerHTML = "";
        containerProducts.innerHTML = "";
        productManager.foundProducts.forEach((element, index) =>
            containerProducts.innerHTML += `<div class="card"><div class="card-body">${element.displayProduct()}</div></div>`
        );
    }
}

let displayProductsForCategory = async (categoryNumber) => {
    containerProducts.innerHTML = "";
    containerProductsByCategory.innerHTML = "";
    searchResult.innerHTML = "";
    for(let q = 0; q < productManager.categories[categoryNumber].products.length; q++){
        containerProducts.innerHTML += `<div class="card"><div class="card-body">${productManager.categories[categoryNumber].products[q].displayProduct()}</div></div>`;
    }
}

if (searchedProductBtn !== null){
    searchedProductBtn.addEventListener('click', displayProducts);
}

if (productEntered !== null){
    productEntered.addEventListener("keyup", async function(event) {
        if (event.key === "Enter") {
            searchResult.innerHTML = "";
            await displayProducts();
        }
    });
}

if (allProductsByCategoryBtn !== null){
    allProductsByCategoryBtn.addEventListener('click', getProductsSortedByCategory)
}

if(categoryTypes !== null){
    categoryTypes.addEventListener('change', async function() {
        await displayProductsForCategory(categoryTypes.selectedIndex);
    })
}

console.log(window.location)

if(window.location.pathname === "/nhH60Manager/Products.html"){
    window.addEventListener('load', getProducts)
}

console.log("Welcome to console.");