$$ = sel => document.querySelector(sel);

const USERS_API = "http://localhost:63164/api/ASPUsers?Email=";
const PRODUCTS_API = "http://localhost:63164/api/Products";
const PRODUCT_CATEGORIES_API = "http://localhost:63164/api/ProductCategories";
const ORDERS_API = "http://localhost:63164/api/Orders";

let loginForm = $$("#account");
let email = $$("#email");
let Managaer;
let productManager;
const searchedProductBtn = $$("#searchProductBtn");
let productPageErrorMessage = $$("#productPageErrorMessage");
let messages = $$(".messages");
let containerProducts = $$("#container-products");
let containerProductsByCategory = $$("#container-products-by-categories");
let productEntered = $$("#inpProduct");
let categoryTypes = $$("#categoryTypes");
let allProductsByCategoryBtn = $$("#allProductsByCategoryBtn");

let containerOrders = $$("#container-orders");

class ProductManager{
    constructor() {
        this.products = [];
        this.categories = {};
        this.searchValue = "";
        this.foundProducts = [];
    }

    async searchValueResults() {
        await fetch(`${PRODUCTS_API}?ProductName=${this.searchValue}`)
            .then(resp => {
                return resp.json();
            })
            .then(response => {
                this.foundProducts = [];
                searchResult.innerHTML = "Search Results for: " + this.searchValue;
                for(let i = 0; i < response.length; i++){
                    this.foundProducts.push(new Product(response[i].productId, response[i].prodCatId, response[i].description, response[i].manufacturer, response[i].stock, response[i].buyPrice, response[i].sellPrice))
                }
            })
            .catch(err => {
                messages.style.display = "inline";
                productPageErrorMessage.innerHTML = err;
            })
    }


    async productsByCategory() {
        await fetch(`${PRODUCTS_API}/ProductCategories`)
            .then(resp => {
                return resp.json();
            })
            .then(response => {
                this.foundProducts = [];
                for(let i = 0; i < response.length; i++){
                    this.foundProducts.push(new Product(response[i].productId, response[i].prodCatId, response[i].description, response[i].manufacturer, response[i].stock, response[i].buyPrice, response[i].sellPrice))
                }
            })
            .catch(err => {
                messages.style.display = "inline";
                productPageErrorMessage.innerHTML = err;
            })
    }

}

class Product {
    constructor(_id, _catId, _description, _manufacturer, _stock, _buyPrice, _sellPrice) {
        this.id = _id;
        this.catId = _catId;
        this.description = _description;
        this.manufacturer = _manufacturer;
        this.stock = _stock;
        this.buyPrice = _buyPrice;
        this.sellPrice = _sellPrice;
    }

    displayProduct() {
        return `<h5 class="card-title">${this.description}</h5>
                <p class="card-text">
                    Stock: ${this.stock}
                    <a title="Update stock" href="./UpdateStock?id=${this.id}"><img src="./images/pencil.svg" alt="update product icon" class="pencilIcon"/></a><br>
                    Sell Price: $${this.sellPrice.toFixed(2)} <br>
                    Buy Price: $${this.buyPrice.toFixed(2)}
                    <a title="Update prices" href="./UpdatePrices?id=${this.id}"><img src="./images/pencil.svg" alt="update product icon" class="pencilIcon"/></a>
                </p>
        `;
    }

}

class ProductCategory {
    constructor(_id, _name) {
        this.id = _id;
        this.name = _name;
        this.products = [];
    }

}

class OrderReports {
    constructor() {

    }

}

class Order{
    constructor(_id, _customerId, _dateCreated, _dateFulfilled, _total, _taxes) {
        this.id = _id;
        this.customerId = _customerId;
        this.dateCreated = _dateCreated;
        this.dateFulfilled = _dateFulfilled;
        this.total = _total;
        this.taxes = _taxes;
    }

}

class Manager {
    constructor(_id, _username) {
        this.id = _id;
        this.username = _username;
    }

}

if(Managaer === undefined && (window.location === "./Index.html" || window.location === "./Orders.html" || window.location === "./Index.html")){
    window.location = "./login.html";
    $$("#error-message").innerHTML = "Can't Access Page";
}

if(Managaer !== undefined) {
    $$(".nav").innerHTML += `<span class="navbar-text" id="name"><img src="../images/person-circle.svg" alt="Person Icon" id="nameIcon" />Welcome ${Managaer.username}</span>
    <a class="btn btn-danger NavBtns" href="../Login.html" id="logout">Logout</a>`;
}


let checkForm = async (e) =>{
    e.preventDefault();
    await fetch(`${USERS_API}${email.value}`)
        .then(resp => {
            return resp.json();
        })
        .then(response => {
            Managaer = new Manager(response.id, response.userName)
            window.location = "./Index.html";
        })
        .catch(err => {
            messages.style.display = "inline";
            productPageErrorMessage.innerHTML = err;
        })
}

let getProducts = async () => {
    productManager = new ProductManager();
    await fetch(`${PRODUCTS_API}`)
        .then(resp => {
            return resp.json();
        })
        .then(response => {
            //_id, _catId, _description, _manufacturer, _stock, _buyPrice, _sellPrice
            console.table(productManager.products);
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
    let currentCategory = 0;
    for (let p = 0; p < productManager.foundProducts.length; p++) {
        if (productManager.foundProducts[p].catId !== currentCategory) {
            currentCategory = productManager.foundProducts[p].catId;
            containerProductsByCategory.innerHTML += `<h4 class="categorySubTitle">${productManager.categories[productManager.foundProducts[p].catId].name}</h4>`;
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
    for(let q = 0; q < productManager.categories[categoryNumber].products.length; q++){
        containerProducts.innerHTML += `<div class="card"><div class="card-body">${productManager.categories[categoryNumber].products[q].displayProduct()}</div></div>`;
    }
}

if (loginForm !== null){
    loginForm.addEventListener('submit', checkForm);
}

if (searchedProductBtn !== null){
    searchedProductBtn.addEventListener('click', displayProducts);
}

if (productEntered !== null){
    productEntered.addEventListener("keyup", async function(event) {
        if (event.key === "Enter") {
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

window.addEventListener('load', getProducts)

console.log("Welcome to console.");