// Nahom Haile
// Web Programming VI
// ClassesAndVariables.js
// File contents: includes all the constants, variables, and classes used

$$ = sel => document.querySelector(sel);

const USERS_API = "http://localhost:63164/api/ASPUsers?Email=";
const PRODUCTS_API = "http://localhost:63164/api/Products";
const PRODUCT_CATEGORIES_API = "http://localhost:63164/api/ProductCategories";
const ORDERS_API = "http://localhost:63164/api/Orders";

let loginForm = $$("#account");
let loginErrorMessage = $$("#EmailErrorMessage");
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
let searchResult = $$("#searchResult");
let stockField = $$("#stock");
let updateStock = $$("#updateStock");
let stockErrorMessage = $$("#stockErrorMessage");
let productToUpdate = $$("#productToUpdate");
let sellPriceField = $$("#sellPrice");
let buyPriceField = $$("#buyPrice");
let updatePrices = $$("#updatePrices");
let containerOrders = $$(".container-orders");
let dates = $$("#dates");
let customer = $$("#customer");


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
                searchResult.innerHTML = "Search Results for: <span class='font-weight-bold'>" + this.searchValue + "</span>";
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

    async getProduct(id){
        await fetch(`${PRODUCTS_API}/${id}`)
            .then(resp => {
                return resp.json();
            })
            .then(response => {
                return response;
            })
            .catch(err => {
                messages.style.display = "inline";
                productPageErrorMessage.innerHTML = err;
            })
    }

}

class Product {
    constructor(_id, _catId, _description, _manufacturer, _stock, _buyPrice, _sellPrice) {
        this.productId = _id;
        this.prodCatId = _catId;
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
                    <a title="Update stock" href="./UpdateStock.html?id=${this.productId}"><img src="./images/pencil.svg" alt="update product icon" class="pencilIcon"/></a><br>
                    Sell Price: $${this.sellPrice.toFixed(2)} <br>
                    Buy Price: $${this.buyPrice.toFixed(2)}
                    <a title="Update prices" href="./UpdatePrices.html?id=${this.productId}"><img src="./images/pencil.svg" alt="update product icon" class="pencilIcon editPricesImg"/></a>
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

class Manager {
    constructor(_id, _username) {
        this.id = _id;
        this.username = _username;
    }

}

class OrderReports {
    constructor() {
        this.orders = []
        this.customers = []
        this.dates = []
    }

    getCustomers(){
        let arr = [];
        for(let x = 0; x < this.orders.length; x++){
            arr.push(this.orders[x].customer);
        }
        this.customers = [...new Set(arr)];
    }

    getDates(){
        let arr = [];
        for(let x = 0; x < this.orders.length; x++){
            arr.push(this.orders[x].dateCreated);
        }
        this.dates = [...new Set(arr)];
    }

    async getOrdersByDate(date) {
        await fetch(`${ORDERS_API}/Date/${date}`)
            .then(resp => {
                return resp.json();
            })
            .then(response => {
                console.log(response);
            })
            .catch(err => {
                messages.style.display = "inline";
                productPageErrorMessage.innerHTML = err;
            })
    }


    async getOrdersByCustomer() {
        await fetch(`${ORDERS_API}/Customers/${date}`)
            .then(resp => {
                return resp.json();
            })
            .then(response => {
                console.log(response);
            })
            .catch(err => {
                messages.style.display = "inline";
                productPageErrorMessage.innerHTML = err;
            })
    }

}

class Customer{
    constructor(_id, _name) {
        this.id = _id;
        this.name = _name;
    }

}

class OrderItem {
    constructor(_product) {
        this.product = _product;
    }

}

class Order{
    constructor(_dateCreated, _dateFulfilled, _total, _customer) {
        this.dateCreated = _dateCreated;
        this.dateFulfilled = _dateFulfilled;
        this.total = _total;
        this.customer = _customer;
        this.orderItems = [];
    }

}