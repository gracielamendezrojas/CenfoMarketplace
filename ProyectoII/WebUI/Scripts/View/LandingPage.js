function sendEmailCenfoMarket(event) {
    event.preventDefault();
    document.getElementById("email-sent").classList.add("notVisible");

    let emailCustomer = document.querySelector('#emailCustomer').value;

    let emailInformation = {
        Name: "",
        LastName: "",
        EmailAdress: "",
        Message: emailCustomer,
    };
    if (emailCustomer != null && emailCustomer != "") {
        fetch(new ControlActions().URL_API +'Email/SendEmailForCenfomarket', {
            method: 'POST',
            body: JSON.stringify(emailInformation),
            headers: {
                'Content-Type': 'application/json',
            },

        }).then(res => res.json())
            .catch(error => console.error('Error:', error))
            .then(response => {
                console.log("Sent.");
                document.getElementById("email-sent").classList.remove("notVisible");
                $("#emailCustomer").val('');
            });
    } else {

        Swal.fire({
            icon: 'error',
            title: 'Please provide an email',
            footer: '<p>Try again!</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    
};


$(document).ready(function () {
    document.getElementById("btn-sendEmailCenfoMarket").addEventListener("click", (event) => sendEmailCenfoMarket(event));

});
