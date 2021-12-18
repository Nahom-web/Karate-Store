$$ = sel => document.querySelector(sel);

const USERS_API = "http://localhost:63164/api/ASPUsers?Email=";
const PRODUCTS_API = "http://localhost:63164/api/Products";
const PRODUCT_CATEGORIES_API = "http://localhost:63164/api/ProductCategories";

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
                    <a title="Update stock" href="./UpdateStock.html?id=${this.id}"><img src="./images/pencil.svg" alt="update product icon" class="pencilIcon"/></a><br>
                    Sell Price: $${this.sellPrice.toFixed(2)} <br>
                    Buy Price: $${this.buyPrice.toFixed(2)}
                    <a title="Update prices" href="./UpdatePrices.html?id=${this.id}"><img src="./images/pencil.svg" alt="update product icon" class="pencilIcon"/></a>
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

if(Managaer !== undefined) {
    $$(".nav").innerHTML += `<span class="navbar-text" id="name"><img src="../images/person-circle.svg" alt="Person Icon" id="nameIcon" />Welcome ${Managaer.username}</span>
    <a class="btn btn-danger NavBtns" href="../Login.html" id="logout">Logout</a>`;
}