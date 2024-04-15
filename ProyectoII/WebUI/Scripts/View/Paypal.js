document.querySelector("#notif-button").classList.add("hidden");

const notififyTransaction = async (userEmail, userId, userContact) => {
    let emailInformation = {
        Name: "",
        LastName: "",
        EmailAdress: userEmail,
        Message: "",
    };
    switch (userContact) {
        case 'Email': await fetch('https://localhost:44395/api/Email/SendTransactionEmail', {
            method: 'POST',
            body: JSON.stringify(emailInformation),
            headers: {
                'Content-Type': 'application/json',
            },

        }); console.log("Email"); break;
        case 'SMS': await fetch('https://localhost:44395/api/Email/SendTransactionSMS?userId=' + userId); console.log("SMS"); break;
        case 'Both': await fetch('https://localhost:44395/api/Email/SendTransactionEmail', {
            method: 'POST',
            body: JSON.stringify(emailInformation),
            headers: {
                'Content-Type': 'application/json',
            },

        }); console.log("2");
            await fetch('https://localhost:44395/api/Email/SendTransactionSMS?userId=' + userId);
            break;
        default: break;
    }
}

 function loadPaypalButton(paypalOrderID, orderAmount) {

    const userData = JSON.parse(localStorage.getItem("UserInformation"));
    const userId = userData['Id'];
    const userEmail = userData['Email'];
    const userContact = userData['PreferredMethod']


     fetch('https://localhost:44395/api/User/Get/' + userId, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    }).then(res => res.json())
        .catch(error => console.error('Error:', error))
        .then(userResponse => {
            console.log('Success:', userResponse);
            let paypalOrder = {
                OrderId: paypalOrderID,
                UserId: userId,
            };
            console.log("acá usuario"); 
            console.log(userResponse); 
            fetch(new ControlActions().URL_API +'Paypal/Create', {
                method: 'POST',
                body: JSON.stringify(paypalOrder),
                headers: {
                    'Content-Type': 'application/json'
                },
            }).then(res => res.json())
                .catch(error => console.error('Error:', error))
                .then(paypalResponse => {
                    document.querySelector("#paypalCodeInstructions").classList.remove("notVisible");
                    document.querySelector("#paypal").classList.remove("notVisible");
                    console.log('Success:', paypalResponse);

                    let paypalMessage = {
                        Name: userResponse.Data.Name,
                        LastName: userResponse.Data.LastName,
                        EmailAdress: userResponse.Data.Email,
                        Message: paypalOrderID,
                    }

                    fetch(new ControlActions().URL_API +'Email/Send', {
                        method: 'POST',
                        body: JSON.stringify(paypalMessage),
                        headers: {
                            'Content-Type': 'application/json'
                        },
                    }).then(res => res.json())
                        .catch(error => console.error('Error:', error))
                        .then(emailResponse => {
                            document.querySelector("#confirmationPaypal").classList.remove("notVisible");

                            let walletBalance = {
                                UserId: 123456789,
                                Balance: orderAmount
                            };

                            fetch(new ControlActions().URL_API +'Wallet/EditByUser', {
                                method: 'PUT',
                                body: JSON.stringify(walletBalance),
                                headers: {
                                    'Content-Type': 'application/json',
                                },

                            }).then(res => res.json())
                                .catch(error => console.error('Error:', error))
                                .then(walletResponse => {
                                    console.log("Wallet money deposit"); 
                                    var today = new Date();

                                    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

                                    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

                                    var dateTime = date + ' ' + time;
                                    let transactionWalletAdminIncrease = {
                                        WalletSender: 'Paypal',
                                        WalletReceiver: 'bcc5a83717d83a33fe776db644ad940e',
                                        Amount: orderAmount,
                                        Description: 'Fee - Paypal transaction',
                                        TransactionDate: dateTime
                                    };

                                    notififyTransaction(userEmail, userId, userContact)

                                    fetch(new ControlActions().URL_API +'transactionwallet/post', {
                                        method: 'POST',
                                        body: JSON.stringify(transactionWalletAdminIncrease),
                                        headers: {
                                            'Accept': 'application/json',
                                            'Content-Type': 'application/json'
                                        },
                                    }).then(response => response.json())
                                        .then(transactionwallet => {
                                            console.log("Transaction wallet"); 
                                        });
                                });

                        });
                });
        });
}


function PaypalConfirmation() {

    let code = document.querySelector('#paypalCode').value;
    const userData = JSON.parse(localStorage.getItem("UserInformation"));
    const userId = userData['Id'];
    const status = "complete";

    fetch(new ControlActions().URL_API +'Paypal/Get/'+ userId, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },

    }).then(res => res.json())
        .catch(error => console.error('Error:', error))
        .then(paypalConfirmResponse => {

            console.log(code);
            if (code === paypalConfirmResponse.Data.OrderId) {

                let paypalUpdate = {
                    OrderId: paypalConfirmResponse.Data.OrderId,
                    UserId: userId,
                    OrderStatus: status
                };

                fetch(new ControlActions().URL_API +'Paypal/Update', {
                    method: 'PUT',
                    body: JSON.stringify(paypalUpdate),
                    headers: {
                        'Content-Type': 'application/json',
                    },

                }).then(res => res.json())
                    .catch(error => console.error('Error:', error))
                    .then(paypalUpdateResponse => {
                        userData.OTP = "c";
                        userData.Status = "Active"
                        fetch(new ControlActions().URL_API +'User/EditStatus',
                            {
                                method: 'PUT',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify(userData)
                            }
                        ).then(response => response.json()).then(json => {
                            Swal.fire({
                                icon: 'success',
                                title: 'Success',
                                footer: '<p>Registration completed. Now you can login with your email and password.</p>',
                                buttonsStyling: false,
                                customClass: {
                                    confirmButton: 'btn btn-Success'
                                }
                            });
                            document.querySelector('.btn-Success').addEventListener('click', () => {
                                window.location.reload();
                                localStorage.removeItem("UserInformation");
                                window.location.replace(new ControlActions().FE +"Dashboard/Login");

                            });
                        });
                    });

            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    footer: '<p>The code is not valid. Check again.</p>',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-Success'
                    }
                });
                
            }
        });
};


$(document).ready(function () {
    //current fee
    fetch(new ControlActions().URL_API +'Fee/GetFee', {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    })
        .then(response => response.json())
        .then(data => {
            if (data.Status === 200) {
                $("#paypal-fee").text(data.Data[0].Amount);

                //console.log(data.Data[0].Amount);
            }; 

        });

    document.getElementById("btnConfirmPaypal").addEventListener("click", () => PaypalConfirmation());

});
