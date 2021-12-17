$$ = sel => document.querySelector(sel);

const ORDERS_API = "http://localhost:63164/api/Orders";

let containerOrders = $$(".container-orders");
let dates = $$("#dates");
let customer = $$("#customer");

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

let getOrders = async () => {
    orderReports = new OrderReports();
    await fetch(`${ORDERS_API}`)
        .then(resp => {
            return resp.json();
        })
        .then(response => {
            console.log(response);
            for(let i = 0; i < response.length; i++){
                orderReports.orders.push(new Order(
                    response[i].dateCreated,
                    response[i].dateFulfilled,
                    response[i].grandTotal,
                    new Customer(
                        response[i].customer.customerId,
                        response[i].customer.name
                    )
                ));
                for(let o = 0; o < response[i].orderItems.length; o++){
                    orderReports.orders[i].orderItems.push(
                        new OrderItem(
                            response[i].orderItems[o].product.description
                        )
                    );
                }
            }
            console.log(orderReports.orders[0]);
        })
        .catch(err => {
            messages.style.display = "inline";
            productPageErrorMessage.innerHTML = err;
        })
    displayOrders(orderReports.orders);
    orderReports.getCustomers()
    orderReports.getDates();
    console.log(orderReports.customers);
    console.log(orderReports.dates);
    PopulateDates()
    PopulateCustomers()
}

let displayOrders = (arr) =>{
    containerOrders.innerHTML += "<table class=\"table table-striped bg-white\" id='orders'>" +
        "<thead class=\"thead-dark\">" +
        "<tr>" +
        "<th>Customers Name</th>" +
        "<th>Date Created</th>" +
        "<th>Date Fulfilled</th>" +
        "<th>Total</th>"+
        "</tr>" +
        "</thead>" +
        "<tbody>";
    for(let a = 0; a < arr.length; a++){
        let newRow = $$("#orders").insertRow(-1);

        var cell1 = newRow.insertCell(0);
        let customerName  = document.createTextNode(`${arr[a].customer.name}`);
        cell1.appendChild(customerName);

        var cell2 = newRow.insertCell(1);
        let dateCreated  = document.createTextNode(`${arr[a].dateCreated}`);
        cell2.appendChild(dateCreated);

        var cell3 = newRow.insertCell(2);
        let dateFulfilled  = document.createTextNode(`${arr[a].dateFulfilled}`);
        cell3.appendChild(dateFulfilled);

        var cell4 = newRow.insertCell(3);
        let total = document.createTextNode(`$${arr[a].total.toFixed(2)}`);
        cell4.appendChild(total);
    }
    containerOrders.innerHTML += "</tbody></table>";
}

PopulateDates = () =>{
    for(let d in orderReports.dates){
        dates.innerHTML += `<option value="${orderReports.dates[d]}">${orderReports.dates[d]}</option>`
    }
}

PopulateCustomers = () =>{
    let currentCustomer = 0;
    for(let c in orderReports.customers){
        if(orderReports.customers[c].id !== currentCustomer){
            currentCustomer = orderReports.customers[c].id;
            customer.innerHTML += `<option value="${orderReports.customers[c].id}">${orderReports.customers[c].name}</option>`;
        }
    }
}

let displayOrdersForDate = (index) =>{
    if (index !== 0){
        orderReports.getOrdersByDate(index);
    }
}

let displayOrdersForCustomer = (index) => {

}

if(dates !== null){
    dates.addEventListener('change', async function() {
        await displayOrdersForDate(dates.value);
    })
}

if(customer !== null){
    customer.addEventListener('change', async function() {
        await displayOrdersForCustomer(customer.value);
    })
}


window.addEventListener('load', getOrders)
