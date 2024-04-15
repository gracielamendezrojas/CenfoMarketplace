document.querySelector("#notif-button").classList.add("hidden");

//Controlador JS de la vista
var idDocument;
var sampleDocument;
var usuarios;
var responseFetch;
var idSusc;

function UserFormDocument() {

    this.ctrlActions = new ControlActions();
    var customer = JSON.parse(window.localStorage.getItem('UserInformation', JSON.stringify(customer)));

    this.createUser = async function () {
        customer.OTP = "c";
        console.log(customer);

        let response = await fetch(new ControlActions().URL_API +'User/CreateUserPhone',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(customer)
            }
        ).then(response => response.json()).then(json => { responsefetch = json.Status });

        return responsefetch;
    };

    this.createSuscription = async function () {
        susc = {
            User: customer.Id
        }
        let response = await fetch(new ControlActions().URL_API +'Suscription/Post',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(susc)
            }
        ).then(response => response.json()).then(json => { responsefetch = json.Status });

        return responsefetch;
    };

    this.getUsuarios = async function () {
        await fetch(new ControlActions().URL_API +'Suscription/GetAll')
            .then(data => {
                return data.json();
            })
            .then(post => {
                usuarios = post.Data;
            })
        return usuarios;
    };
    this.createDocumentId = async function () {
        documentData = {
            Filepath: idDocument,
            Description: "This is the id photo of the user with the id" + customer.id,
            Subscription: idSusc
        }
        console.log(documentData);
        let response = await fetch(new ControlActions().URL_API +'Document/Post',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(documentData)
            }
        ).then(response => response.json()).then(json => { responsefetch = json.Status });

        return responsefetch;
    };
    this.createDocumentSample = async function () {
        documentt = {
            Filepath: sampleDocument,
            Description: "This is the work sample of the user with the id" + customer.id,
            Subscription: idSusc
        }
        let response = await fetch(new ControlActions().URL_API +'Document/Post',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(documentt)
            }
        ).then(response => response.json()).then(json => { responsefetch = json.Status });

        return responsefetch;
    };


    this.sendInformation = async function () {
        if (idDocument != null && sampleDocument != null) {

            //Create User Phone, Password, Wallet and Role
            let primero = await this.createUser();

            if (primero == 200) {

                let segundo = await this.createSuscription();
                console.log(segundo);
                let tercero = await this.getUsuarios();

                for (const ele of tercero) {
                    if (ele.User == customer["Id"]) {
                        idSusc = ele.Id;
                    }
                }

                //Create Documents
                let cuarto = await this.createDocumentId();
                let quinto = await this.createDocumentSample();

                window.location.replace(new ControlActions().FE +"Dashboard/Paypal");
            }
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                footer: '<p>Please upload both files.</p>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success'
                }
            });
        
        }
    }


    //Cloudinary
    var myWidget = cloudinary.createUploadWidget({
        cloudName: 'cenfotecmarket',
        uploadPreset: 'gcut4nfj'
    }, (error, result) => {
        if (!error && result && result.event === "success") {
            sampleDocument = result.info.secure_url;
        }
    }
    )

    var myWidgett = cloudinary.createUploadWidget({
        cloudName: 'cenfotecmarket',
        uploadPreset: 'gcut4nfj'
    }, (error, result) => {
        if (!error && result && result.event === "success") {
            idDocument = result.info.secure_url;
        }
    }
    )

    document.getElementById("upload_widget").addEventListener("click", function () {
        myWidget.open();
    }, false);

    document.getElementById("upload_widgetUno").addEventListener("click", function () {
        myWidgett.open();
    }, false);

}


$(document).ready(function () {
    var v = new UserFormDocument();
});

