class Btn1 {

    
    requestOTP() {
        var v = new UserFormPwsReset();
        $('.row:eq(1)').toggle();
        $('.row-btn:eq(0)').toggle();
        $('.row-btn:eq(1)').toggle();
        $('span').toggle();
        v.ValidateUser();
        v.SentNewOtpByEmail();
        v.TimerChange();
    }
    
    confirmOTP() {
        var v = new UserFormPwsReset();
        $('.row:eq(0)').hide();
        $('.row:eq(1)').hide();        
        $('.row:eq(2)').toggle();
        $('.row:eq(3)').toggle();    
       
        $('.row-btn:eq(2)').toggle();        
        v.VerifyOTPEmail();

    }    
    resetPassword() {
        var v = new UserFormPwsReset();
        $('.row:eq(2)').toggle();
        $('.row:eq(3)').toggle();
        $('.row-btn:eq(2)').toggle();
        v.ChangePassword();

    }
}

function UserFormPwsReset() {
    $('.row:eq(1)').hide();
    $('.row:eq(2)').hide();
    $('.row:eq(3)').hide();
    $('.row-btn:eq(1)').hide();
    $('.row-btn:eq(2)').hide();
    $('span').hide();
    var coutner;

    this.ValidateUser = async function () {
        let id = $('#input-id').val();
        let responseMessage;
        let data;
        let response = await fetch(new ControlActions().URL_API +'user/GetUserbyID/' + id,
            {
                method: 'Get',
                headers: { 'Content-Type': 'application/json' }
            }
        ).then(response => response.json()).then(json => { data = json.Data ;responseMessage = json.Message });

        if (responseMessage != "Valid User") {
            Swal.fire({
                icon: 'error',
                title: 'ID not found',
                text: responseMessage,
                footer: '<a>Try Again!</a>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }

            }).then((result) => { window.location.assign(new ControlActions().FE +"Dashboard/ResetPassword"); })

        } else {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'OTP sent to ' + data.Email,
                showConfirmButton: false,
                timer: 1800
            })

        }

        return data.Email;
    }

    this.SentNewOtpByEmail = async function () {
        let id = $('#input-id').val();
        let responsestatus;
        let responseMessage;
        let response = await fetch(new ControlActions().URL_API +'otp/SentOtpByEmail/'+ id,
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' }               
            }
        ).then(response => response.json()).then(json => { responsefetch = json.Status; responseMessage = json.Message });



        return response;
    }


    this.VerifyOTPEmail = async function () {
        let id = $('#input-id').val();
        let otp = $('#input-otp').val();
        counter = -2;        

        let data = {
            "OTPNumber": otp,
            "UserId": id,
            "DateTime": new Date()
        }

        let status;
        let message;

        let response = await fetch(new ControlActions().URL_API +'otp/Validate/',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            }
        ).then(response => response.json()).then(json => { status = json.Status; message = json.Message; responseMessage = json.Message });

        if (status != "200") {
            Swal.fire({
                icon: 'error',
                title: 'Invalid OTP',
                text: responseMessage,
                footer: '<a href="">Try Again!</a>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }

            }).then((result) => { window.location.assign(new ControlActions().FE +"Dashboard/ResetPassword"); })

        } else {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: message,
                showConfirmButton: false,
                timer: 1800
            })
        }
    }

    this.TimerChange = async function () {
        counter = 90;
        while (counter != -1) {
            counter -= 1;
            const delay = ms => new Promise(res => setTimeout(res, ms));
            await delay(1000);
            document.getElementById("Count").textContent = counter;

        }
        window.localStorage.setItem('OTPUser', 0);
        Swal.fire({
            icon: 'error',
            title: 'The OTP is not longer valid',
            footer: '<p>Press resend to have a new one!</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
        document.getElementById("Count").textContent = 0;
        document.getElementById("btnNewOTPEmail").classList.remove("notVisible");
    }

    

    this.ChangePassword = async function () {

        let user = $('#input-id').val();
        let pass = $('#input-password').val();
        let passconfirm = $('#input-password-confirm').val();

        let apiData;
        let apiStatus;
        let apiMessage;
        let apiTransactionDate;


        let data = {
            "Passwordd": pass,
            "CreationDate": new Date(),
            "User": user
        }

        if (pass === passconfirm && /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$/.test($("#input-password").val())) {
            $('#input-password').removeClass("is-invalid");
            $('#input-password').removeClass("is-invalid");
            let response = await fetch(new ControlActions().URL_API +'Password/Post',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(data)
                }
            ).then(response => response.json()).then(json => { apiData = json.Data; apiStatus = json.Status; apiMessage = json.Message });

            if (apiStatus != "200") {
                Swal.fire({
                    icon: 'error',
                    title: 'Unable to update password',
                    text: 'Unable to update password',
                    footer: '<a href="">Try Again!</a>',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-Success' //insert class here
                    }
                }).then((result) => { window.location.assign(new ControlActions().FE +"Dashboard/ResetPassword"); })

            } else {
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: 'Password Change',
                    showConfirmButton: false,
                    timer: 1800
                }).then((result) => { window.location.assign(new ControlActions().FE +"Dashboard/Login"); })
            }

        } else if (!/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$/.test($("#input-password").val())) {

            $('#input-password').addClass("is-invalid");
            $('#input-password-confirm').addClass("is-invalid");

        }





        else {

            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Password missmatch',
                showConfirmButton: false,
                timer: 1800
            })
        }
        
    }



}

//Show Password
$('#checkPass').change(function () {
    var type = ($(this).is(':checked') ? 'text' : 'password'),
        input = $('#input-password'),
        input2 = $('#input-password-confirm'),
        replace = input.clone().attr('type', type)
        replace2 = input2.clone().attr('type', type)
        input2.replaceWith(replace2);
        input.replaceWith(replace);
});


$(document).ready(function () {
    var v = new UserFormPwsReset();    

});

