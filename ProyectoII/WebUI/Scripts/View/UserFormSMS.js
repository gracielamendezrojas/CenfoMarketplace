document.querySelector("#notif-button").classList.add("hidden");
//Controlador JS de la vista

function UserFormSMS() {
    document.getElementById("btnNewOTPSMS").classList.add("notVisible");
    var customer = JSON.parse(window.localStorage.getItem('UserInformation', JSON.stringify(customer)));

    this.service = 'User';
    this.ctrlActions = new ControlActions();

    this.CreateOTP = function () {
        var digits = '0123456789';
        let otp = '';
        for (let i = 0; i < 6; i++) {

            otp += digits[Math.floor(Math.random() * 10)];
        }
        return otp;
    }

    this.createUser = async function () {
        customer.OTP = "c";
        let response = await fetch(new ControlActions().URL_API +'User/CreateUserPhone',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(customer)
            }
        ).then(response => response.json()).then(json => { responsefetch = json.Status });

        return responsefetch;
    };

    this.createRoleXUser = async function () {
        var roleN;
        if (customer.Role == "Content Creator") {
            roleN = 3;
        } else if (customer.Role == "Buyer") {
            roleN = 2;
        }

        role = {
            RoleId: roleN,
            UserId: customer.Id
        }
        let response = await fetch(new ControlActions().URL_API +'RoleXUser/Create',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(role)
            }
        ).then(response => response.json()).then(json => { responsefetch = json.Status });

        return responsefetch;
    };

    this.VerifyOTP = async function () {
        var otpSMS = this.ctrlActions.GetDataForm("frmOTPSMS");
        var realOtp = window.localStorage.getItem('OTPUser');
        console.log(otpSMS, realOtp);
        if (otpSMS["OTPSMS"] == realOtp) {
            if (customer.Role == "Buyer") {
                let primero = await this.createUser();
                if (primero == 200) {
                    let sexto = await this.createRoleXUser();
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        footer: '<p>Registration Completed! Now you can log in with your email and password.</p>',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn btn-Success'
                        }
                    });
                   
                    localStorage.removeItem("UserInformation");
                    window.location.replace(new ControlActions().FE +"Dashboard/Login");
                }
            } else {
                window.location.replace(new ControlActions().FE +"Form/Document");
            }

        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                footer: '<p>Invalid OTP.</p>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success'
                }
            });
            
        }
    }


    this.CreateAccount = function () {
        var customer = JSON.parse(window.localStorage.getItem('UserInformation', JSON.stringify(customer)));
        this.ctrlActions.PostToAPI(this.service + "/CreateUserPhone", customer, function (user) {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                footer: '<p>' + user.Name + ' registrado correctamente.</p>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success'
                }
            });
        })
    }

    this.TimerChange = async function () {
        var counter = 90;
        while (counter != -1) {
            counter -= 1;
            const delay = ms => new Promise(res => setTimeout(res, ms));
            await delay(1000);
            document.getElementById("Count").textContent = counter;

        }
        window.localStorage.setItem('OTPUser', 0);
        Swal.fire({
            icon: 'error',
            title: 'OTP',
            footer: '<p>The OTP is no longer valid. Press resend to have a new one.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success'
            }
        });
  
        document.getElementById("Count").textContent = 0;
        document.getElementById("btnNewOTPSMS").classList.remove("notVisible");

    }

    this.SendSMSOTP = function () {
        var customer = JSON.parse(window.localStorage.getItem('UserInformation', JSON.stringify(customer)));
        let otp = this.CreateOTP();
        customer.OTP = otp;
        window.localStorage.setItem('OTPUser', otp);
        this.ctrlActions.PostToAPI('OTP' + "/CreateOTPSMS", customer, function (user) {
            Swal.fire({
                icon: 'success',
                title: 'Success',
                footer: '<p>New OTP sent.</p>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success'
                }
            });
           
        });
        this.TimerChange();
    }

}

$(document).ready(function () {
    var v = new UserFormSMS();
    v.TimerChange();
});


