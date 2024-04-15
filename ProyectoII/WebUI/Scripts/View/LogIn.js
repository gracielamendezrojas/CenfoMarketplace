
if (localStorage.getItem("UserProfile") === null) {
    document.querySelector(".navbar-nav").classList.add("hidden");
    document.querySelector("#logout-button").classList.add("hidden");
    document.querySelector("#notif-button").classList.add("hidden");
}
else {
    document.querySelector(".navbar-nav").classList.remove("hidden");
    document.querySelector("#logout-button").classList.remove("hidden");
    document.querySelector("#notif-button").classList.remove("hidden");
}

document.querySelector('#logout-button').addEventListener('click', () => {
    localStorage.removeItem("UserProfile");
    localStorage.removeItem("UserRoles");
});


//console.log(JSON.parse(localStorage.getItem("UserRoles"))[0]["Id"]);

async function myFunction() {
    let Email = document.querySelector('#input-email').value;
    let Password = document.querySelector('#input-password').value;
    
    let data = {
        "Email": Email,
        "Password": Password
    }

    let localstorage;
    let responsefetch;
    let responseMessage;

    let response = await fetch(new ControlActions().URL_API +'user/Login',
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        }
    ).then(response => response.json()).then(json => { localstorage = json.Data; responsefetch = json.Status; responseMessage = json.Message});

    console.log(localstorage);
    
    if (responsefetch == 505) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: responseMessage,
            footer: '<a href="">Try Again!</a>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }                        
        })        
    }

    if (responsefetch == 200) {
        localStorage.setItem("UserProfile", JSON.stringify(localstorage.User));
        localStorage.setItem("UserRoles", JSON.stringify(localstorage.Roles));
        if (JSON.parse(localStorage.getItem("UserRoles"))[0]["Id"] == 1) {
            $(".dashboard-link").attr("href", new ControlActions().FE +"dashboard/admindashboard");
            window.location.replace(new ControlActions().FE +"Dashboard/admindashboard");
        }
        else if (JSON.parse(localStorage.getItem("UserRoles"))[0]["Id"] == 2) {
            $(".dashboard-link").attr("href", new ControlActions().FE +"dashboard/buyerdashboard");
            window.location.replace(new ControlActions().FE +"Dashboard/buyerdashboard");
        }
        else if (JSON.parse(localStorage.getItem("UserRoles"))[0]["Id"] == 3) {
            $(".dashboard-link").attr("href", new ControlActions().FE +"dashboard/creatordashboard");
            window.location.replace(new ControlActions().FE +"Dashboard/creatordashboard");
        }
        else {
            return null;
        }
        
    } else {
        document.querySelector('#input-password').classList.add("is-invalid");
        document.querySelector('#input-email').classList.add("is-invalid");
    }  

}

document.getElementById("btn-sign-in").addEventListener("click", () => myFunction());




