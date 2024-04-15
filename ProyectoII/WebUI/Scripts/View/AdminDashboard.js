

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


const uData = JSON.parse(localStorage.getItem("UserProfile"));

const userId = uData['Id'];
const walletUserId = 123456789;

function ListUsers() {

    this.tblUsers = 'tblUsers';
    this.service = 'User'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "Id,Name,Email,Nickname,Status";

    this.RetrieveUsers = function () {
        this.ctrlActions.FillTable(this.service + "/GetAll", this.tblUsers, false);
    }
}

function ListCategories() {

    this.tblCategories = 'tblCategories';
    this.service = 'Category'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "Name";

    this.RetrieveCategories = function () {
        this.ctrlActions.FillTable(this.service + "/RetrieveAll", this.tblCategories, false);
    }
}

function ListAudit() {

    this.tblAudit = 'tblAudit';
    this.service = 'UserAction'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "User,Type,FormattedDate";

    this.RetrieveActions = function () {
        this.ctrlActions.FillTable(this.service + "/GetAll", this.tblAudit, false);
    }
}
//table Auctions
function ListAuctionNFT() {

    this.tblAuctionNFT = 'tblAuctionNFT';
    this.service = 'Auction'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "NFTName,FormattedCreationDate,FormattedClosingDate,AuctionPrice";

    this.RetrieveAuctions = function () {

        this.ctrlActions.FillTable(this.service + "/RetrieveAllNFTAuctions", this.tblAuctionNFT, false);
    }
}

function ListAuctionCollection() {

    this.tblAuctionsCollection = 'tblAuctionsCollection';
    this.service = 'Auction'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "CollectionName,FormattedCreationDate,FormattedClosingDate,AuctionPrice";

    this.RetrieveAuctionsCollection = function () {

        this.ctrlActions.FillTable(this.service + "/RetrieveAllCollectionAuctions", this.tblAuctionsCollection, false);
    }
}
//selection of an NFT in auction
$('#tblAuctionNFT tbody').on('click', 'tr', function () {

    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        $('#tblAuctionNFT').DataTable().$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});



//selection of a Collection in auction
$('#tblAuctionsCollection tbody').on('click', 'tr', function () {

    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        $('#tblAuctionsCollection').DataTable().$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});

function ListTransactionsReceiver() {

    this.tbl = 'tblTransactionsReceiver';
    this.service = 'TransactionWallet'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "Id,WalletSender,Amount,Description,FormattedDate";

    this.RetrieveTransactionsReceiver = function () {
        this.ctrlActions.FillTable(this.service + "/GetReceiver/" + walletUserId, this.tbl, false);
    }
}

function ListCollections() {

    this.tblCollection = 'tblCollection';
    this.service = 'Collection'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "User,Name,Description,Status";

    this.RetrieveCollections = function () {

        this.ctrlActions.FillTable(this.service + "/RetrieveAll", this.tblCollection, false);
    }
}
let image = "";
let usuarios;

function UserForm() {
    this.service = 'User';
    this.ctrlActions = new ControlActions();

    fetch(new ControlActions().URL_API+'User/GetAll')
        .then(data => {
            return data.json();
        })
        .then(post => {
            usuarios = post.Data;
        })


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
        if (paso == 8) {
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
                                                    customer.Role = "Admin"
                                                    this.ctrlActions.PostToAPI("User" + "/CreateUserPhone", customer, function (user) {
                                                        Swal.fire({
                                                            icon: 'success',
                                                            title: 'Success',
                                                            footer: '<p>'+customer.Name + " has been registered succesfully."+'</p>',
                                                            buttonsStyling: false,
                                                            customClass: {
                                                                confirmButton: 'btn btn-Success'
                                                            }
                                                        });
                                                        
                                                    });
                                                    document.getElementById("frmCustomer").reset();
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
                    footer: '<p>Invalid Id!</p>',
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

//$('#status-button').click(async function () {

//    let id = document.querySelector('.user-status-input').value;

//    const userData = JSON.parse(localStorage.getItem("UserProfile"));

//    const userId = userData['Id'];



//    if (userId == id) {
//        alert("Cannot inactivate current user")
//    }
//    if (isNaN(id)) {
//        console.log(isNaN(id))
//        document.querySelector('.user-status-input').classList.add('is-invalid');
//        document.querySelector('.user-status-input').value = "";
//        console.log(document.querySelector('.user-status-input').classList)
//        document.querySelector('.user-status-input').placeholder = 'Not an ID'
//    }
//    else {
//        document.querySelector('.user-status-input').classList.remove('is-invalid');
//        document.querySelector('.user-status-input').value = "";
//        document.querySelector('.user-status-input').placeholder = 'User ID'
//        let user
//        let responseStatus
//        let response = await fetch('https://localhost:44395/api/user/get?id=' + id)
//            .then(response => response.json()).then(json => { user = json.Data; responseStatus = json.Status });

//        console.log(user);
//        if (responseStatus == 200) {
//            if (user.Status == 'Active') {
//                user.Status = 'Inactive';
//            }
//            else {
//                user.Status = 'Active'
//            }

//            await fetch('https://localhost:44395/api/user/edit',
//                {
//                    method: 'PUT',
//                    headers: { 'Content-Type': 'application/json' },
//                    body: JSON.stringify(user)
//                }
//            )

//            window.location.reload();

//        }
//        else {
//            document.querySelector('.user-status-input').classList.add('is-invalid')
//            document.querySelector('.user-status-input').placeholder = 'Not an user'
//        }

//    }
//});


document.getElementById("create-category").addEventListener('click', async () => {
    let name;

    const userData = JSON.parse(localStorage.getItem("UserProfile"));

    const userId = userData['Id'];

    if (document.querySelector("#category-name").value != "") {
        name = document.querySelector("#category-name").value
    }
    else {
        document.querySelector("#category-name").classList.add('is-invalid')
    }

    if (name) {
        let data = {
            "User": userId,
            "Name": name
        };
        let response = await fetch(new ControlActions().URL_API +'category/create',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            }
        );
        window.location.reload();
    }
});


$('#tblUsers tbody').on('click', 'tr', function () {

    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        $('#tblUsers').DataTable().$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});

$('#status-button').click(async function () {


    let id = $('#tblUsers').DataTable().row('.selected').data().Id;

    const userData = JSON.parse(localStorage.getItem("UserProfile"));


    let user
    let responseStatus
    let response = await fetch(new ControlActions().URL_API +'user/get?id=' + id)
        .then(response => response.json()).then(json => { user = json.Data; responseStatus = json.Status });

    if (responseStatus == 200) {
        if (user.Status == 'Active') {
            user.Status = 'Inactive';
        }
        else {
            user.Status = 'Active'
        }

        await fetch(new ControlActions().URL_API +'user/edit',
            {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(user)
            }
        )

        window.location.reload();

    }
    else {
        console.log("500")
    }
});

$(document).ready(async function () {

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
                $("#currentFee").text(data.Data[0].Amount);
            } else {
                alert("Fee not found")
            }

        });


    //update fee
    const changeFeeFunction = function (event) {
        event.preventDefault();

        let fee = {
            Admin: userId,
            Amount: $("#changeFeetxt").val(),
        };

        if (fee.Amount > 0 && !(isNaN(fee.Amount))) {
            fetch(new ControlActions().URL_API +'Fee/Edit', {
                method: 'PUT',
                body: JSON.stringify(fee),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },

            })
                .then(response => response.json())
                .then(dataUpdate => {
                    if (dataUpdate.Status == 200) {
                        $("#currentFee").text(fee.Amount);
                        Swal.fire({
                            icon: 'success',
                            title: 'The fee has been changed',
                            footer: '<p>CenfoMarket-Place has a new fee for registration</p>',
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-Success' //insert class here
                            }
                        });
                        document.querySelector('.btn-Success').addEventListener('click', () => {
                            window.location.reload();
                        });
                    }
                });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'There has been an error',
                footer: '<p>Try again!</p>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }
            });
        }


    }
    $("#changeFee").click(changeFeeFunction);


    //load auctions
    var a = new ListAuctionNFT();
    var ac = new ListAuctionCollection();
    a.RetrieveAuctions();
    ac.RetrieveAuctionsCollection();

    //cancel NFT auction
    const cancelAuction = function (event) {
        event.preventDefault();
        let IdNFT = $('#tblAuctionNFT').DataTable().row('.selected').data().NFT;
        fetch(new ControlActions().URL_API +'nft/updateStatus/' + IdNFT, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },

        })
            .then(response => response.json())
            .then(data => {
                if (data.Status == 200) {

                    const userActionData = {
                        User: userId,
                        Date: new Date().getDate,
                        Type: 'NFT auction has been cancelled'
                    }

                    fetch(new ControlActions().URL_API +'useraction/post',
                        {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(userActionData)
                        }
                    )

                    window.location.reload();
                    Swal.fire({
                        icon: 'success',
                        title: 'The selected auction has been cancelled',
                        footer: '<p>Your NFT is now for direct sale.</p>',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                        }
                    });
                    document.querySelector('.btn-Success').addEventListener('click', () => {
                        window.location.reload();
                    });
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'There has been an error.',
                        footer: '<p>Select the auction, you want to cancel. Try again!</p>',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn btn-Success' //insert class here
                        }
                    });

                }



            });
    }
    $("#auction-NFT-cancel-button").click(cancelAuction);


    //cancel collection Auctions
    const cancelAuctionCollections = function (event) {
        event.preventDefault();

        let IdCollection = $('#tblAuctionsCollection').DataTable().row('.selected').data().Collection;
        fetch(new ControlActions().URL_API +'collection/UpdateStatus/' + IdCollection, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },

        })
            .then(response => response.json())
            .then(dataCancelCollection => {
                if (dataCancelCollection.Status == 200) {

                    fetch(new ControlActions().URL_API +'Nft/RetrieveAll', {
                        method: 'GET',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                    }).then(response => response.json())
                        .then(NFTData => {

                            let allNFTs = NFTData.Data;

                            allNFTs.forEach(NFT => {

                                if (NFT["Collection"] === IdCollection) {

                                    let NFTUpdated = NFT;
                                    NFTUpdated.Status = "On sale";

                                    fetch(new ControlActions().URL_API +'Nft/Update', {
                                        method: 'POST',
                                        body: JSON.stringify(NFTUpdated),
                                        headers: {
                                            'Accept': 'application/json',
                                            'Content-Type': 'application/json'
                                        },
                                    }).then(response => response.json())
                                        .then(NFTUpdate => {
                                        });
                                }
                            });

                        });

                    const userActionData = {
                        User: userId,
                        Date: new Date().getDate,
                        Type: 'Collection auction has been cancelled'
                    }

                    fetch(new ControlActions().URL_API +'useraction/post',
                        {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(userActionData)
                        }
                    )

                    Swal.fire({
                        icon: 'success',
                        title: 'The selected auction has been cancelled',
                        footer: '<p>Your Collection is now for direct sale.</p>',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                        }
                    });
                    document.querySelector('.btn-Success').addEventListener('click', () => {
                        window.location.reload();
                    });

                } else {
                    Swal.fire({
                        icon: 'error',
                        title: 'There has been an error.',
                        footer: '<p>Select the auction, you want to cancel. Try again!</p>',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn btn-Success' //insert class here
                        }
                    });
                    document.querySelector('.btn-Success').addEventListener('click', () => {
                        window.location.reload();
                    });


                }
            });
    }
    $("#auction-collection-cancel-button").click(cancelAuctionCollections);


    let earningsData
    await fetch(new ControlActions().URL_API +'wallet/get?id=bcc5a83717d83a33fe776db644ad940e')
        .then(response => response.json()).then(json => earningsData = json.Data.Balance);

    let userData
    await fetch(new ControlActions().URL_API +'user/GetAll')
        .then(response => response.json()).then(json => userData = json.Data);

    var v = new ListUsers;
    var c = new ListCategories;
    var a = new ListAudit;
    var co = new ListCollections;
    var tr = new ListTransactionsReceiver;

    v.RetrieveUsers();
    c.RetrieveCategories();
    a.RetrieveActions();
    co.RetrieveCollections();
    tr.RetrieveTransactionsReceiver();
    
    document.querySelector('#earnings').innerHTML += earningsData;
    document.querySelector('#users').innerHTML = userData.length
});


function displayContent(evt, nameOfContent) {
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(nameOfContent).style.display = "block";
    evt.currentTarget.className += " active";
}

$(document).ready(function () {
    var v = new UserForm();
});




