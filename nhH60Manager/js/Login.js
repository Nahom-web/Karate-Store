// Nahom Haile
// Web Programming VI
// Login.js
// File contents: checks the login form

let checkForm = async (e) =>{
    e.preventDefault();
    await fetch(`${USERS_API}${email.value}`)
        .then(resp => {
            return resp.json();
        })
        .then(response => {
            if(response.status !== undefined && response.status === 400){
                console.log("Not a manager");
                loginErrorMessage.innerHTML = "Invalid login attempt. Please use your manage account.";
            } else{
                Managaer = new Manager(response.id, response.userName)
                window.location = "./Index.html";
            }
        })
        .catch(err => {
            messages.style.display = "inline";
            productPageErrorMessage.innerHTML = err;
        })
}

loginForm.addEventListener('submit', checkForm);