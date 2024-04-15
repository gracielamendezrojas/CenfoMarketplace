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
const userContact = uData['PreferredMethod'];
const userEmail = uData['Email'];


const notififyTransaction = async(userEmail, userId, userContact) => {
    let emailInformation = {
        Name: "",
        LastName: "",
        EmailAdress: userEmail,
        Message: "",
    };
    switch (userContact) {
        case 'Email': await fetch(new ControlActions().URL_API +'Email/SendTransactionEmail', {
            method: 'POST',
            body: JSON.stringify(emailInformation),
            headers: {
                'Content-Type': 'application/json',
            },

        }); console.log("Email"); break;
        case 'SMS': await fetch(new ControlActions().URL_API +'Email/SendTransactionSMS?userId=' + userId); console.log("SMS"); break;
        case 'Both': await fetch(new ControlActions().URL_API +'Email/SendTransactionEmail', {
            method: 'POST',
            body: JSON.stringify(emailInformation),
            headers: {
                'Content-Type': 'application/json',
            },

        }); console.log("2");
            await fetch(new ControlActions().URL_API +'Email/SendTransactionSMS?userId=' + userId);
            break;
        default: break;
    }
}

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

function ListTransactionsReceiver() {

    this.tbl = 'tblTransactionsReceiver';
    this.service = 'TransactionWallet';
    this.ctrlActions = new ControlActions();
    this.columns = "Id,WalletSender,Amount,Description,FormattedDate";

    this.RetrieveTransactionsReceiver = function () {
        this.ctrlActions.FillTable(this.service + "/GetReceiver/" + uData["Id"], this.tbl, false);
    }
}
function ListTransactionsSender() {

    this.tbl = 'tblTransactionsSender';
    this.service = 'TransactionWallet'; 
    this.ctrlActions = new ControlActions();
    this.columns = "Id,WalletReceiver,Amount,Description,FormattedDate";

    this.RetrieveTransactionsSender = function () {
        this.ctrlActions.FillTable(this.service + "/GetSender/" + uData["Id"], this.tbl, false);
    }
}

//table Auctions
function ListAuctionNFT() {

    this.tblAuctionNFT = 'tblAuctionNFT';
    this.service = 'Auction'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "NFTName,FormattedCreationDate,FormattedClosingDate,AuctionPrice";

    this.RetrieveAuctions = function () {

        this.ctrlActions.FillTable(this.service + "/RetrieveAllNFTAuctions" , this.tblAuctionNFT, false);
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


function ListAuctionBuyer() {

    this.tblAuctionsBuyer = 'tblAuctionsBuyer';
    this.service = 'Auction'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "FormattedCreationDate,FormattedClosingDate,AuctionPrice";

    this.RetrieveAuctionsBuyer = function () {

        this.ctrlActions.FillTable(this.service + "/RetrieveMyAuctionsBuyer?Id=" + userId, this.tblAuctionsBuyer, false);
    }
}


let balanceData;
let colData;
async function changeOwner(nftId) {

    let nft
    let responseStatus
    let response = await fetch(new ControlActions().URL_API +'nft/retrieve?nft=' + nftId)
        .then(response => response.json()).then(json => { nft = json.Data; responseStatus = json.Status });

    let creatorUser
    await fetch(new ControlActions().URL_API +'user/getbynft?id=' + nftId)
        .then(response => response.json()).then(json => { creatorUser = json.Data; console.log(creatorUser) });


    let adminWallet;
    let adminWalletStatus;
    await fetch(new ControlActions().URL_API +'wallet/get?id=bcc5a83717d83a33fe776db644ad940e')
        .then(response => response.json()).then(json => { adminWallet = json.Data; adminWalletStatus = json.Status });
    let creatorWallet;
    let creatorWalletStatus
    await fetch(new ControlActions().URL_API +'wallet/getbyuser?id=' + creatorUser.Id)
        .then(response => response.json()).then(json => { creatorWallet = json.Data; creatorWalletStatus = json.Status });

    if (responseStatus == 200) {
        if (balanceData["Balance"] >= nft.Price) {
            if (creatorWalletStatus == 200 && adminWalletStatus == 200) {
                creatorWallet["Balance"] += (nft.Price - nft.Price * 0.1);
                adminWallet["Balance"] += nft.Price * 0.1;


                const transactionWalletCreatorIncrease = {
                    WalletSender: balanceData["Id"],
                    WalletReceiver: creatorWallet["Id"],
                    Amount: nft.Price - nft.Price * 0.1,
                    Description: 'NFT Sale',
                    TransactionDate: dateTime
                }
                await fetch(new ControlActions().URL_API +'transactionwallet/post',
                    {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(transactionWalletCreatorIncrease)
                    }
                );

                const transactionWalletAdminIncrease = {
                    WalletSender: balanceData["Id"],
                    WalletReceiver: adminWallet["Id"],
                    Amount: nft.Price * 0.1,
                    Description: 'NFT Sale Commission',
                    TransactionDate: dateTime
                }
                await fetch(new ControlActions().URL_API +'transactionwallet/post',
                    {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(transactionWalletAdminIncrease)
                    }
                );


                let creatorWalletIncreaseStatus;

                await fetch(new ControlActions().URL_API +'wallet/edit',
                    {
                        method: 'PUT',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(creatorWallet)
                    }
                ).then(response => response.json()).then(json => creatorWalletIncreaseStatus = json.Status);

                let adminWalletIncreaseStatus;
                await fetch(new ControlActions().URL_API +'wallet/edit',
                    {
                        method: 'PUT',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(adminWallet)
                    }
                ).then(response => response.json()).then(json => adminWalletIncreaseStatus = json.Status);

                if (creatorWalletIncreaseStatus == 200 && adminWalletIncreaseStatus == 200) {

                    await notififyTransaction(creatorUser.Email, creatorUser.Id, creatorUser.PreferredMethod)

                    balanceData["Balance"] -= nft.Price


                    let buyerWalletDecreaseStatus;

                    await fetch(new ControlActions().URL_API +'wallet/edit',
                        {
                            method: 'PUT',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(balanceData)
                        }
                    ).then(response => response.json()).then(json => buyerWalletDecreaseStatus = json.Status);

                    if (buyerWalletDecreaseStatus == 200) {

                        await notififyTransaction(userEmail, userId, userContact)

                        nft.Collection = colData[0].Id;
                        nft.Status = "Not On Sale";

                        let status;
                         //Colocar envio de invoice
                        const invoicee = {
                            Email: uData.Email,
                            Name: uData.Name,
                            LastName: uData.LastName,
                            NFT: nft.Name,
                            Collection: null,
                            Price: nft.Price,
                        };
                        await fetch(new ControlActions().URL_API +'Invoice/SentEmailInvoice',
                            {
                                method: 'POST',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify(invoicee)
                            }
                        ).then(response => response.json()).then(json => status = json.Status);

                        //Notification Camapana
                        let statuss;
                        const noti = {
                            User: creatorUser.Id,
                            Message: "An NFT has been purchased.",
                            Subject: ""+nft.Name+" has been purchased.",
                        };
                        await fetch(new ControlActions().URL_API +'Notifications/Post',
                            {
                                method: 'POST',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify(noti)
                            }
                        ).then(response => response.json()).then(json => statuss = json.Status);

                        let nftChangeStatus
                        await fetch(new ControlActions().URL_API +'nft/update',
                            {
                                method: 'POST',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify(nft)
                            }
                        ).then(response => response.json()).then(json => nftChangeStatus = json.Status);

                        if (nftChangeStatus == 200) {

                            var today = new Date();

                            var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

                            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

                            var dateTime = date + ' ' + time;

                            const userActionData = {
                                User: balanceData["UserId"],
                                Date: dateTime,
                                Type: 'NFT purchased'
                            }

                            await fetch(new ControlActions().URL_API +'useraction/post',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(userActionData)
                                }
                            )


                            const acquisition = {
                                CreationDate: dateTime,
                                ClosingDate: dateTime,
                                Price: nft.Price,
                                Buyer: balanceData["UserId"],
                                Creator: creatorUser.Id
                            }


                            await fetch(new ControlActions().URL_API +'acquisition/post',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(acquisition)
                                }
                            )
                            Swal.fire({
                                icon: 'success',
                                title: 'Purchase complete',
                                buttonsStyling: false,
                                customClass: {
                                    confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                                }
                            });
                            document.querySelector('.btn-Success').addEventListener('click', () => {
                                window.location.replace(new ControlActions().FE + "Dashboard/buyerdashboard");
                            });
                            
                        }
                    }
                }

            }

        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Not enough balance',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                }
            });
           
        }
    }
    else {
        console.log("500")
    }
}


function ListCollections() {

    this.tblCollection = 'tblCollection';
    this.service = 'Collection'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "User,Name,Description,Status";

    this.RetrieveCollections = function () {

        this.ctrlActions.FillTable(this.service + "/RetrieveAllOnSale", this.tblCollection, false);
    }
}

$('#tblCollection tbody').on('click', 'tr', function () {

    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        $('#tblCollection').DataTable().$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});

$('#collection-purchase').click(async function () {

    let name = $('#tblCollection').DataTable().row('.selected').data().Name;

    let collection
    let responseStatus
    let response = await fetch(new ControlActions().URL_API +'collection/retrievebyname?name=' + name)
        .then(response => response.json()).then(json => { collection = json.Data; responseStatus = json.Status });

    if (responseStatus == 200) {

        localStorage.setItem("SelectedCollection", JSON.stringify(collection));
        window.location.replace(new ControlActions().FE +"Dashboard/CollectionContent");
    }
    else {
        console.log("500")
    }
});


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

//change base price for selected NFT
$('#bid-nft-button').click(async function () {
    localStorage.removeItem("SelectedAuction");

    let NFTAuction = $('#tblAuctionNFT').DataTable().row('.selected').data();
    let NFTAuction1 = {
        ...NFTAuction,
        type:'NFT'
    }

    localStorage.setItem("SelectedAuction", JSON.stringify(NFTAuction1));
    window.location.replace(new ControlActions().FE +"Dashboard/Offers");

});


//change base price for selected NFT
$('#bid-collection-button').click(async function () {
    localStorage.removeItem("SelectedAuction");

    let NFTCollection = $('#tblAuctionsCollection').DataTable().row('.selected').data();
    let NFTAuction1 = {
        ...NFTCollection,
        type: 'Collection'
    }

    localStorage.setItem("SelectedAuction", JSON.stringify(NFTAuction1));
    window.location.replace(new ControlActions().FE +"Dashboard/Offers");

});

$('#tblNFT tbody').on('click', 'tr', function () {

    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        $('#tblNFT').DataTable().$('tr.selected').removeClass('selected');
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

$(document).ready(async function () {
    function initPayPalButton() {
        $('#description').val('Money for wallet');
        var description = document.querySelector('#smart-button-container #description');
        var amount = document.querySelector('#smart-button-container #amount');
        var descriptionError = document.querySelector('#smart-button-container #descriptionError');
        var priceError = document.querySelector('#smart-button-container #priceLabelError');
        var invoiceid = document.querySelector('#smart-button-container #invoiceid');
        var invoiceidError = document.querySelector('#smart-button-container #invoiceidError');
        var invoiceidDiv = document.querySelector('#smart-button-container #invoiceidDiv');
        var elArr = [description, amount];
        if (invoiceidDiv.firstChild.innerHTML.length > 1) {
            invoiceidDiv.style.display = "block";
        }
        var purchase_units = [];
        purchase_units[0] = {};
        purchase_units[0].amount = {};
        function validate(event) {
            return event.value.length > 0;
        }
        paypal.Buttons({
            style: {
                color: 'gold',
                shape: 'rect',
                label: 'pay',
                layout: 'vertical',
            },
            onInit: function (data, actions) {
                actions.disable();
                if (invoiceidDiv.style.display === "block") {
                    elArr.push(invoiceid);
                }
                elArr.forEach(function (item) {
                    item.addEventListener('keyup', function (event) {
                        var result = elArr.every(validate);
                        if (result) {
                            actions.enable();
                        } else {
                            actions.disable();
                        }
                    });
                });
            },
            onClick: function () {
                if (description.value.length < 1) {
                    descriptionError.style.visibility = "visible";
                } else {
                    descriptionError.style.visibility = "hidden";
                }
                if (amount.value.length < 1) {
                    priceError.style.visibility = "visible";
                } else {
                    priceError.style.visibility = "hidden";
                }
                if (invoiceid.value.length < 1 && invoiceidDiv.style.display === "block") {
                    invoiceidError.style.visibility = "visible";
                } else {
                    invoiceidError.style.visibility = "hidden";
                }
                purchase_units[0].description = description.value;
                purchase_units[0].amount.value = amount.value;
                if (invoiceid.value !== '') {
                    purchase_units[0].invoice_id = invoiceid.value;
                }
            },
            createOrder: function (data, actions) {
                return actions.order.create({
                    purchase_units: purchase_units,
                });
            },
            onApprove: function (data, actions) {
                return actions.order.capture().then(function (orderData) {
                    let walletBalance = {
                        UserId: userId,
                        Balance: $('#amount').val(),
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
                        });
                     notififyTransaction(userEmail, userId, userContact)
                    // Full available details
                    console.log('Capture result', orderData, JSON.stringify(orderData, null, 2));
                    // Show a success message within this page, e.g.
                    const element = document.getElementById('paypal-button-container');
                    element.innerHTML = '';
                    element.innerHTML = '<h3>Thank you for your payment!</h3>';

                    //Wallet Transactions
                    fetch(new ControlActions().URL_API +'Wallet/GetByUser/' + userId, {
                        method: 'GET',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                    }).then(res => res.json())
                        .catch(error => console.error('Error:', error))
                        .then(walletInfo => {

                            var today = new Date();

                            var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

                            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

                            var dateTime = date + ' ' + time;

                            //Transaction 
                            let transactionWalletBuyerIncrease = {
                                WalletSender: 'Paypal',
                                WalletReceiver: walletInfo.Data.Id,
                                Amount: $('#amount').val(),
                                Description: 'Paypal deposit into personal wallet',
                                TransactionDate: dateTime
                            };
                            fetch(new ControlActions().URL_API +'transactionwallet/post', {
                                method: 'POST',
                                body: JSON.stringify(transactionWalletBuyerIncrease),
                                headers: {
                                    'Accept': 'application/json',
                                    'Content-Type': 'application/json'
                                },
                            }).then(response => response.json())
                                .then(transactionwallet => {
                                    Swal.fire({
                                        icon: 'success',
                                        title: 'You have added money to your wallet.',
                                        footer: '<p>Check your Wallet!</p>',
                                        buttonsStyling: false,
                                        customClass: {
                                            confirmButton: 'btn btn-Success' //insert class here
                                        }
                                    });
                                    document.querySelector('.btn-Success').addEventListener('click', () => {
                                         window.location.reload();
                                    });
                                });
                        });

                });
            },
            onError: function (err) {
                console.log(err);
            }
        }).render('#paypal-button-container');
    }
    initPayPalButton();




    const userData = JSON.parse(localStorage.getItem("UserProfile"));

    const userId = userData['Id'];

    let earningsData
    await fetch(new ControlActions().URL_API +'wallet/getbyuser?id=' + userId)
        .then(response => response.json()).then(json => earningsData = json.Data.Balance);

    await fetch(new ControlActions().URL_API +'wallet/getbyuser?id=' + userId)
        .then(response => response.json()).then(json => balanceData = json.Data);

    await fetch(new ControlActions().URL_API +'collection/RetrieveAllByUser?Id=' + userId)
        .then(response => response.json()).then(json => colData = json.Data);

    document.querySelector('#moneyBuyer').innerHTML += earningsData;

    var co = new ListCollections;
    co.RetrieveCollections();
    var ts = new ListTransactionsSender;
    var tr = new ListTransactionsReceiver;
    ts.RetrieveTransactionsSender();
    tr.RetrieveTransactionsReceiver();
    var cb = new ListAuctionBuyer();

    //load auctions
    var a = new ListAuctionNFT();
    var ac = new ListAuctionCollection();
    a.RetrieveAuctions();
    ac.RetrieveAuctionsCollection();
    cb.RetrieveAuctionsBuyer();


    //function for update notification preferred method
    const updateNotification = function (event) {
        event.preventDefault();
        let userNotification = {
            Id: userId,
            PreferredMethod: $("#txtPreferredMethodd").val(),
        };


        fetch(new ControlActions().URL_API +'User/EditNotificationMethod', {
            method: 'PUT',
            body: JSON.stringify(userNotification),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        }).then(response => response.json())
            .then(data => {
                if (data.Status == 200) {
                    let userDataN = {
                        ...uData,
                        PreferredMethod: userNotification.PreferredMethod
                    }
                    localStorage.setItem("UserProfile", JSON.stringify(userDataN));

                    Swal.fire({
                        icon: 'success',
                        title: 'Your notification method has been changed',
                        //footer: '<p></p>',
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
                        footer: '<p>Try again!</p>',
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
    $("#notification-change-button").click(updateNotification);

    //load current notification preferred method
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
                $("#current-notificationMethod").text(data.Data.PreferredMethod);

            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'There has been an error',
                    footer: '<p>Current notification method not found</p>',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
                    }
                });
                document.querySelector('.btn-Success').addEventListener('click', () => {
                    window.location.reload();
                });
            }

        });

    let catData
    await fetch(new ControlActions().URL_API +'nft/retrieveactive')
        .then(response => response.json()).then(json => catData = json.Data);
    
    catData.forEach(nft => {
        document.getElementById("contenedor").innerHTML += `
    
<div >

<div class="card mb-3 rounded-3 shadow-lg p-3 mb-5 bg-white" style="border: solid 1px grey">
    <h3 class="card-header" style="color: white">${nft.Name}</h3>
    <div class="card-body">       
    </div>
    <div class="d-block user-select-none" width="100%" height="200" aria-label="Placeholder: Image cap" focusable="false" role="img" preserveAspectRatio="xMidYMid slice" viewBox="0 0 318 180" style="font-size:1.125rem;text-anchor:middle;  ">
        <image src=${nft.Route} style="width:100%"/>
    </div>
    <div class="card-body">
        <p class="card-text">$${nft.Price}</p>
    </div>
    <div class="card-body">
       <button class="btn btn-primary btn-sm"  onclick="changeOwner(${nft.Id});">Buy</button>
    </div>
</div>
</div>

    `
    })
});




