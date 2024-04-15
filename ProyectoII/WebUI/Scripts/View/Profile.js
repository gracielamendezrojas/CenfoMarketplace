//validation for sign in
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
    localStorage.removeItem("Roles");

});


//validation for form Update
function validateFormFields() {

    $("#nickNameUpdateFeedback").addClass("hidden");
    $("#lastNameUpdateFeedback").addClass("hidden");
    $("#nameUpdateFeedback").addClass("hidden");


    var valid = true; 
    if ($("#nameUpdate").val() == null || $("#nameUpdate").val() == "") {
        $("#nameUpdateFeedback").removeClass("hidden");
        valid = false; 
    }

    if ($("#lastNameUpdate").val() == null || $("#lastNameUpdate").val() == "") {
        $("#lastNameUpdateFeedback").removeClass("hidden");
        valid = false;
    }

    if ($("#nickNameUpdate").val() == null || $("#nickNameUpdate").val() == "") {
        $("#nickNameUpdateFeedback").removeClass("hidden");
        valid = false;
    }

    //if ($("#emailUpdate").val() == null || $("#emailUpdate").val() == "") {
    //    $("#emailUpdateFeedback").removeClass("hidden");
    //    valid = false;
    //}

    //if ($("#DBOUpdate").val() == null || $("#DBOUpdate").val() == "") {
    //    $("#DBOUpdateFeedback").removeClass("hidden");
    //    valid = false;
    //}


    return valid; 
}


//validation for password
function validateFormFieldsPassword() {
    var valid = false;


    $("#passwordUpdateFeedbackRegex").addClass("hidden");
    $("#passwordUpdateFeedback").addClass("hidden");
    $("#currentPasswordFeedback").addClass("hidden");
    $("#passwordConfirmationUpdateFeedback").addClass("hidden");

    if ($("#passwordUpdate").val() == null || $("#passwordUpdate").val() == "") {
        $("#passwordUpdateFeedback").removeClass("hidden");
        valid = false;
    }


    if ($("#passwordUpdateConfirmation").val() != $("#passwordUpdate").val()) {
        $("#passwordConfirmationUpdateFeedback").removeClass("hidden");
        valid = false;
    }
    if (/(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{12,}/.test($("#passwordUpdate").val())) {
        valid = true;
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            footer: '<p>Your new password must contain at least one number, one uppercase letter, one lowercase letter and at least 12 or more characters</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
        }
                    });
    
       
    }

    if ($("#currentPassword").val() == null || $("#currentPassword").val() == "") {
        $("#currentPasswordFeedback").removeClass("hidden");

        valid = false;
    }
    //regex from registration

    return valid;
}




$(document).ready(async function () {

    //Get userInformation 
    let userUpdate;

    const userData = JSON.parse(localStorage.getItem("UserProfile"));
    const userId = userData['Id'];

    let response = await fetch(new ControlActions().URL_API +'User/Get/' + userId, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    })
        .then(response => response.json())
        .then(data => {
            if (data.Status === 200) {
                $("#nameProfile").text(data.Data.Name + " " + data.Data.LastName);
                $("#nicknameProfile").text(data.Data.NickName);
                $("#emailProfile").text(data.Data.Email);
                $("#avatar").attr("src", data.Data.Avatar);
                $("#nameUpdate").val(data.Data.Name);
                $("#lastNameUpdate").val(data.Data.LastName);
                $("#nickNameUpdate").val(data.Data.NickName);
                //$("#emailUpdate").val(data.Data.Email);
                let date = data.Data.DOB.substr(0, 10);
                $("#DBOUpdate").val(date);
                userUpdate = data.Data;

            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    footer: '<p>User not found</p>',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
                    }
                });

               
            }

        });


    await fetch(new ControlActions().URL_API +'Phone/GetByUser/' + userId, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    })
        .then(response => response.json())
        .then(dataPhone => {
            if (dataPhone.Status === 200) {
                $("#phoneProfile").text(dataPhone.Data.Number);
                //$("#phoneUpdate").val(dataPhone.Data.Number);


            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    footer: '<p>Number not found</p>',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
                    }
                });


            }

        });

    // function for update profile
    const updateProfile = function (event) {
        event.preventDefault();
        userUpdate.Name = $("#nameUpdate").val();
        userUpdate.LastName = $("#lastNameUpdate").val();
        userUpdate.NickName = $("#nickNameUpdate").val();
        //userUpdate.Email = $("#emailUpdate").val();
        userUpdate.DBO = $("#DBOUpdate").val();

        let userUpdateValidation = validateFormFields();
        if (userUpdateValidation) {
            fetch(new ControlActions().URL_API +'User/Edit', {
                method: 'PUT',
                body: JSON.stringify(userUpdate),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },

            })
                .then(response => response.json())
                .then(dataUpdate => {
                    if (dataUpdate.Status == 200) {
                        $("#nameProfile").text(userUpdate.Name + " " + userUpdate.LastName);
                        $("#nicknameProfile").text(userUpdate.NickName);
                        Swal.fire({
                            icon: 'success',
                            title: 'Success',
                            footer: '<p>Your profile has been updated</p>',
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
                        }
                    });
            document.querySelector('.btn-Success').addEventListener('click', () => {
                window.location.reload();
                    });
                      
                    } else if (dataUpdate.Status == 400) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            footer: '<p>There has been a problem with the update. Try again</p>',
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
                            }
                        });
                       

                    }
                });
        }
    }
    $("#updateBtn").click(updateProfile);

    //cloudinary
    var widget = cloudinary.createUploadWidget(
        {
            cloudName: 'cenfotecmarket',
            uploadPreset: 'gcut4nfj'
        },
        (error, result) => {
            if (!error && result && result.event === "success") {
                let image;
                console.log('Done uploading..: ', result.info.secure_url);
                image = result.info.secure_url;
                updateImageProfile(image);
            }

        });

    document.getElementById("upload_widget_Update").addEventListener("click", function () {
        widget.open();
    }, false);


    // function for update picture
    const updateImageProfile = function (image) {
        event.preventDefault();
        userUpdate.Avatar = image; 

        let userUpdateValidation = validateFormFields();
        if (userUpdateValidation) {
            fetch(new ControlActions().URL_API +'User/Edit', {
                method: 'PUT',
                body: JSON.stringify(userUpdate),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },

            })
                .then(response => response.json())
                .then(dataUpdate => {
                    if (dataUpdate.Status == 200) {
                        $("#avatar").attr("src", image);

                    } else if (dataUpdate.Status == 400) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            footer: '<p>There has been a problem with the update. Try again</p>',
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
                            }
                        });
                        
                    }
                });
        }
    }



    //function for update pw
    const updatePassword = function (event) {
        event.preventDefault();

        let passwordUpdateValidation = validateFormFieldsPassword();

        let checkPassword = {
            User: userId,
            Passwordd: $("#currentPassword").val(),
        };

        let updatePassword = {
            User: userId,
            Passwordd: $("#passwordUpdate").val(),
        };

        //validation and then checking current password
        if (passwordUpdateValidation) {
            fetch(new ControlActions().URL_API +'Password/CheckPassword', {
                method: 'POST',
                body: JSON.stringify(checkPassword),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },

            })
                .then(response => response.json())
                .then(dataCheckPassword => {
                    if (dataCheckPassword.Status == 400) {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            footer: '<p>Wrong password. Try again</p>',
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
                            }
                        });
                       
                    } else if (dataCheckPassword.Status == 200) {
                        //update password
                        fetch(new ControlActions().URL_API +'Password/Edit', {
                            method: 'PUT',
                            body: JSON.stringify(updatePassword),
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json'
                            },
                        })
                            .then(response => response.json())
                            .then(dataChangePw => {
                                if (dataChangePw.Status === 200) {
                                    Swal.fire({
                                        icon: 'success',
                                        title: 'Success',
                                        footer: '<p>Your password has been updated</p>',
                                        buttonsStyling: false,
                                        customClass: {
                                            confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
                                    }});
                       

                                   
                                    ($("#passwordUpdate").val(''));
                                    ($("#passwordUpdateConfirmation").val(''));
                                    ($("#currentPassword").val('')); 
                                } else {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'Error',
                                        footer: '<p>Your password was not updated. Try again. Make sure not to repit your one of your last 5 passwords</p>',
                                        buttonsStyling: false,
                                        customClass: {
                                            confirmButton: 'btn btn-Success btn-primary btn-sm'  //insert class here
                                        }
                                    });
                                    
                                }
                            });

                    }
                });
        } 
    }
    $("#updatePasswordBtn").click(updatePassword);


    //Show password text
    $('#checkPassUpdate').change(function () {
        var type = ($(this).is(':checked') ? 'text' : 'password'),
            input = $('#passwordUpdate'),
            input2 = $('#passwordUpdateConfirmation'),
            input3 = $('#currentPassword'),

            replace = input.clone().attr('type', type),
            replace2 = input2.clone().attr('type', type),
            replace3 = input3.clone().attr('type', type);

        input.replaceWith(replace);
        input2.replaceWith(replace2);
        input3.replaceWith(replace3);
    });

});



