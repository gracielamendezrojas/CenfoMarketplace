


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

//Controlador JS de la vista

let image = "";
let usuarios;

function UserForm() {

    this.service = 'User';
    this.ctrlActions = new ControlActions();

    fetch(new ControlActions().URL_API +'User/GetAll')
        .then(data => {
            return data.json();
        })
        .then(post => {
            usuarios = post.Data;
        })


    this.CreateOTP = function () {
        var digits = '0123456789';
        let otp = '';
        for (let i = 0; i < 5; i++) {

            otp += digits[Math.floor(Math.random() * 10)];
        }
        return otp;
    }

    this.SaveInformation = async function () {
        var customer = this.ctrlActions.GetDataForm("frmCustomer");
        var paso = 0;
        for (let clave in customer) {
            if (clave != "undefined") {
                if (customer[clave].length == 0) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        footer: '<p>' + clave + ' field is empty!</p>',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn btn-Success'
                        }
                    });
                } else {
                    paso += 1;
                }
            }
        }
        if (paso == 10) {
            var existe = false;
            if (/^\d+$/.test(customer["Id"]) && (customer["Id"].length == 9 || customer["Id"].length == 11)) {
                if ((/^[A-Za-z]+$/).test(customer["Name"])) {
                    if ((/^[A-Za-z]+$/).test(customer["LastName"])) {
                        if ((/^[A-Za-z]+$/).test(customer["NickName"])) {
                            var mail_format = /^w+([.-]?w+)*@w+([.-]?w+)*(.w{2,3})+$/;
                            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(customer["Email"])) {
                                if (/^\d+$/.test(customer["Phone"]) && customer["Phone"].length == 8) {
                                    var today = new Date();
                                    var birthDate = new Date(customer["DOB"]);
                                    var age = today.getFullYear() - birthDate.getFullYear();
                                    var m = today.getMonth() - birthDate.getMonth();
                                    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
                                        age--;
                                    }
                                    if (age >= 18) {
                                        if (image != "") {
                                            if (/(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{12,}/.test(customer["Password"])) {
                                                for (const ele of usuarios) {
                                                    if (ele.Id == customer["Id"] || ele.Email == customer["Email"]) {
                                                        existe = true;
                                                    }
                                                }
                                                if (!existe) {
                                                    customer.Avatar = image;
                                                    window.localStorage.setItem("UserInformation", JSON.stringify(customer));
                                                    let otp = this.CreateOTP();
                                                    customer.OTP = otp;
                                                    window.localStorage.setItem('OTPUser', otp);
                                                    this.ctrlActions.PostToAPI('OTP' + "/CreateOTPEmail", customer, function (user) { });
                                                    const delay = ms => new Promise(res => setTimeout(res, ms));
                                                    await delay(1000);
                                                    window.location.replace(new ControlActions().FE +"Form/OTPEmail");
                                                } else {
                                                    Swal.fire({
                                                        icon: 'error',
                                                        title: 'Error',
                                                        footer: '<p>This id or email is already registered.</p>',
                                                        buttonsStyling: false,
                                                        customClass: {
                                                            confirmButton: 'btn btn-Success'
                                                        }
                                                    });
                                                    
                                                }

                                            } else {
                                                Swal.fire({
                                                    icon: 'error',
                                                    title: 'Error',
                                                    footer: '<p>Must contain at least one number and one uppercase and lowercase letter, and at least 12 or more characters.</p>',
                                                    buttonsStyling: false,
                                                    customClass: {
                                                        confirmButton: 'btn btn-Success'
                                                    }
                                                });
                                                
                                            }

                                        } else {
                                            Swal.fire({
                                                icon: 'error',
                                                title: 'Error',
                                                footer: '<p>Upload an image.</p>',
                                                buttonsStyling: false,
                                                customClass: {
                                                    confirmButton: 'btn btn-Success'
                                                }
                                            });
                                        
                                        }
                                    } else {
                                        Swal.fire({
                                            icon: 'error',
                                            title: 'Error',
                                            footer: '<p>You are under 18 years old, you can´t make an account.</p>',
                                            buttonsStyling: false,
                                            customClass: {
                                                confirmButton: 'btn btn-Success'
                                            }
                                        });
                                        
                                    }

                                } else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Error',
                                        footer: '<p>Invalid phone format.</p>',
                                        buttonsStyling: false,
                                        customClass: {
                                            confirmButton: 'btn btn-Success'
                                        }
                                    });
                                    
                                }
                            } else {
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Error',
                                    footer: '<p>Invalid email format.</p>',
                                    buttonsStyling: false,
                                    customClass: {
                                        confirmButton: 'btn btn-Success'
                                    }
                                });
                             
                            }
                        } else {
                            Swal.fire({
                                icon: 'error',
                                title: 'Error',
                                footer: '<p>Invalid nickname, only characters allowed.</p>',
                                buttonsStyling: false,
                                customClass: {
                                    confirmButton: 'btn btn-Success'
                                }
                            });
                         
                        }
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            footer: '<p>Invalid last name, only characters allowed.</p>',
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-Success'
                            }
                        });
                       
                    }
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        footer: '<p>Invalid name, only characters allowed.</p>',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn btn-Success'
                        }
                    });
                   
                }
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    footer: '<p>Invalid Id.</p>',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-Success'
                    }
                });
                
            }
        }
    }

    //Cloudinary
    var myWidget = cloudinary.createUploadWidget({
        cloudName: 'cenfotecmarket',
        uploadPreset: 'gcut4nfj'
    }, (error, result) => {
        if (!error && result && result.event === "success") {
            image = result.info.secure_url;
        }
    }
    )

    document.getElementById("upload_widget").addEventListener("click", function () {
        myWidget.open();
    }, false);

    //Show Password

    $('#checkPass').change(function () {
        var type = ($(this).is(':checked') ? 'text' : 'password'),
            input = $('#txtPassword'),
            replace = input.clone().attr('type', type)
        input.replaceWith(replace);
    });

}

$(document).ready(function () {
    var v = new UserForm();
});
