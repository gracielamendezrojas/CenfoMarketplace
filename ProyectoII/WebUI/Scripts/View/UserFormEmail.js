document.querySelector("#notif-button").classList.add("hidden");

//Controlador JS de la vista

function UserFormEmail() {

    document.getElementById("btnNewOTPEmail").classList.add("notVisible");
    this.ctrlActions = new ControlActions();

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
            title: 'Error',
            footer: '<p>The OTP is no longer valid. Press resend to have a new one.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success'
            }
        });
    
        document.getElementById("Count").textContent = 0;
        document.getElementById("btnNewOTPEmail").classList.remove("notVisible");
    }

    this.CreateOTP = function () {
        var digits = '0123456789';
        let otp = '';
        for (let i = 0; i < 6; i++) {

            otp += digits[Math.floor(Math.random() * 10)];
        }
        return otp;
    }


    this.VerifyOTPEmail = async function () {
        var otpEmail = this.ctrlActions.GetDataForm("frmOTPEmail");
        var realOtp = window.localStorage.getItem('OTPUser');
        if (otpEmail["OTPEmail"] == realOtp) {
            var customer = JSON.parse(window.localStorage.getItem('UserInformation', JSON.stringify(customer)));
            let otp = this.CreateOTP();
            customer.OTP = otp;
            window.localStorage.setItem('OTPUser', otp);
            this.ctrlActions.PostToAPI('OTP' + '/CreateOTPSMS', customer, function (user) { });
            const delay = ms => new Promise(res => setTimeout(res, ms));
            await delay(1000);
            window.location.replace(new ControlActions().FE +"Form/OTPSMS");
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

    this.SendEmailOTP = function () {
        var customer = JSON.parse(window.localStorage.getItem('UserInformation', JSON.stringify(customer)));
        let otp = this.CreateOTP();
        customer.OTP = otp;
        window.localStorage.setItem('OTPUser', otp);
        this.ctrlActions.PostToAPI('OTP' + "/CreateOTPEmail", customer, function (user) {
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
    var v = new UserFormEmail();
    v.TimerChange();
});
