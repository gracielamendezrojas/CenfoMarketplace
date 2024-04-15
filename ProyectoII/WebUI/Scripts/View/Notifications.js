//Controlador JS de la vista

function Notifications() {

}

var notificaciones;

$(document).ready(function () {
	$(".notification_icon .fa-bell").click(function () {
		$(".dropdown").toggleClass("active");
	})

    const userData = JSON.parse(localStorage.getItem("UserProfile"));
    var userId = userData['Id'];

    const uRol = JSON.parse(localStorage.getItem("UserRoles"));
    var rol = uRol[0].Name;
    if (rol == 'Admin') {
        userId =123456789;
    } else {
        userId = userData['Id'];
    }

    var response = fetch(new ControlActions().URL_API +'Notifications/GetAllUserId/' + userId, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },

    })
        .then(data => {
            return data.json();
        })
        .then(post => {
            notificaciones = post.Data;
            for (const ele of notificaciones.slice(-8)){

                var html = "" +
                    "<div class='notify_item'>" +
                    "<div class='notify_img'>" +
                    "<img src='https://i.postimg.cc/MpvtbLYr/520648.png' alt='profile_pic' style='width: 35px'></img>" +
                    "</div>" +
                    "<div class='notify_info'>" +
                    "<p style='text-transform:none;'>" + ele["Subject"] + "</p>" +
                    "</div>" +
                    "</div>";
                document.getElementById("dropdown").insertAdjacentHTML('afterbegin',html);
            }
        })
});
