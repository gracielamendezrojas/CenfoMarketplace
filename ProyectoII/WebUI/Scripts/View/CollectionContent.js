
let balanceData;
let colData;

let collectionData;

var today = new Date();

var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

var dateTime = date + ' ' + time;
const uData = JSON.parse(localStorage.getItem("UserProfile"));


const notififyTransaction = async (userEmail, userId, userContact) => {
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

        }); console.log(userContact); break;
        case 'SMS': await fetch(new ControlActions().URL_API +'Email/SendTransactionSMS?userId=' + userId); console.log(userContact); break;
        case 'Both': await fetch(new ControlActions().URL_API +'Email/SendTransactionEmail', {
            method: 'POST',
            body: JSON.stringify(emailInformation),
            headers: {
                'Content-Type': 'application/json',
            },

        }); console.log(userContact);
            await fetch(new ControlActions().URL_API +'Email/SendTransactionSMS?userId=' + userId);
            break;
        default: break;
    }
}

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
    await fetch(new ControlActions().URL_API +'wallet/getbyuser?id=' + collectionData["User"])
        .then(response => response.json()).then(json => { creatorWallet = json.Data; creatorWalletStatus = json.Status });
    
    if (responseStatus == 200) {
        console.log(creatorWallet)
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

                let status;
                //Colocar envio de invoice
                const invoice = {
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
                        body: JSON.stringify(invoice)
                    }
                ).then(response => response.json()).then(json => status = json.Status);

                //Notification Campana
                let statuss;
                const noti = {
                    User: creatorUser.Id,
                    Message: "An NFT has been purchased.",
                    Subject: "" + nft.Name + " has been purchased.",
                };
                await fetch(new ControlActions().URL_API +'Notifications/Post',
                    {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(noti)
                    }
                ).then(response => response.json()).then(json => statuss = json.Status);


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
                        

                        nft.Collection = colData[0].Id
                        nft.Status = "Not On Sale";

                        let nftChangeStatus
                        await fetch(new ControlActions().URL_API +'nft/update',
                            {
                                method: 'POST',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify(nft)
                            }
                        ).then(response => response.json()).then(json => nftChangeStatus = json.Status);

                        if (nftChangeStatus == 200) {
                            const userActionData = {
                                User: balanceData["UserId"],
                                Date: new Date().getDate,
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
                                Creator: collectionData["User"]
                            }

                            console.log(acquisition)

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
                            }});
                            document.querySelector('.btn-Success').addEventListener('click', async () => {
                                const userDataE = JSON.parse(localStorage.getItem("UserProfile"));
                                const userContact = userDataE['PreferredMethod']

                                await notififyTransaction(userDataE["Email"], balanceData["UserId"], userContact)

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

$(document).ready(async function () {
    collectionData = JSON.parse(localStorage.getItem("SelectedCollection"));
    document.querySelector("#title").innerHTML = collectionData["Name"]

    const userData = JSON.parse(localStorage.getItem("UserProfile"));

    const userId = userData['Id'];

    await fetch(new ControlActions().URL_API +'wallet/getbyuser?id=' + userId)
        .then(response => response.json()).then(json => balanceData = json.Data);

    await fetch(new ControlActions().URL_API +'collection/RetrieveAllByUser?Id=' + userId)
        .then(response => response.json()).then(json => colData = json.Data);

    let total = 0;
    
    let catData
    await fetch(new ControlActions().URL_API +'nft/retrievebycollection?collection=' + collectionData["Id"])
        .then(response => response.json()).then(json => catData = json.Data);
    catData.forEach(nft => {
        total += nft.Price;
        document.getElementById("contenedor").innerHTML += `
    
<div >

<div class="card mb-3 rounded-3 shadow-lg p-3 mb-5 bg-white" style="border: solid 1px grey">
    <h3 class="card-header">${nft.Name}</h3>
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

    document.querySelector("#total").innerHTML += total;

    document.querySelector("#purchase-collection").addEventListener('click', async() => {

        if (balanceData["Balance"] >= total) {
            let adminWallet;
            let adminWalletStatus;
            await fetch(new ControlActions().URL_API +'wallet/get?id=bcc5a83717d83a33fe776db644ad940e')
                .then(response => response.json()).then(json => { adminWallet = json.Data; adminWalletStatus = json.Status });

            let creatorWalletStatus
            await fetch(new ControlActions().URL_API +'wallet/getbyuser?id=' + collectionData["User"])
                .then(response => response.json()).then(json => { creatorWallet = json.Data; creatorWalletStatus = json.Status });

            let creatorUser
            await fetch(new ControlActions().URL_API +'user/get?id=' + collectionData["User"])
                .then(response => response.json()).then(json => { creatorUser = json.Data; console.log(creatorUser) });

            if (creatorWalletStatus == 200 && adminWalletStatus == 200) {
                creatorWallet["Balance"] += (total - total * 0.1);
                adminWallet["Balance"] += total * 0.1;


                const transactionWalletCreatorIncrease = {
                    WalletSender: balanceData["Id"],
                    WalletReceiver: creatorWallet["Id"],
                    Amount: total - total * 0.1,
                    Description: 'Collection Sale',
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
                    Amount: total * 0.1,
                    Description: 'Collection Sale Commission',
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

                    balanceData["Balance"] -= total

                    let buyerWalletDecreaseStatus;
                    await fetch(new ControlActions().URL_API +'wallet/edit',
                        {
                            method: 'PUT',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(balanceData)
                        }
                    ).then(response => response.json()).then(json => buyerWalletDecreaseStatus = json.Status);

                    if (buyerWalletDecreaseStatus == 200) {

                        collectionData["User"] = userId
                        collectionData["SaleStatus"] = 'Not On Sale'

                        let collectionChangeStatus
                        await fetch(new ControlActions().URL_API +'collection/update',
                            {
                                method: 'POST',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify(collectionData)
                            }
                        ).then(response => response.json()).then(json => collectionChangeStatus = json.Status);

                        if (collectionChangeStatus == 200) {             
                            const userActionData = {
                                User: balanceData["UserId"],
                                Date: new Date().getDate,
                                Type: 'Collection purchased'
                            }

                            await fetch(new ControlActions().URL_API +'useraction/post',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(userActionData)
                                }
                            )

                            let status;
                            //Colocar envio de invoice
                            const invoicee = {
                                Email: uData.Email,
                                Name: uData.Name,
                                LastName: uData.LastName,
                                NFT: null,
                                Collection: collectionData.Name,
                                Price: total,
                            };
                            await fetch(new ControlActions().URL_API +'Invoice/SentEmailInvoice',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(invoicee)
                                }
                            ).then(response => response.json()).then(json => status = json.Status);

                            //Notification Campana
                            let statuss;
                            const notif = {
                                User: creatorUser.Id,
                                Message: "A Collection has been purchased.",
                                Subject: ""+collectionData.Name+" Collection has been purchased.",
                            };
                            await fetch(new ControlActions().URL_API +'Notifications/Post',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(notif)
                                }
                            ).then(response => response.json()).then(json => statuss = json.Status);

                            const acquisition = {
                                CreationDate: dateTime,
                                ClosingDate: dateTime,
                                Price: total,
                                Buyer: balanceData["UserId"],
                                Creator: collectionData["User"]
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
                            }});
                            document.querySelector('.btn-Success').addEventListener('click', async() => {
                                const userDataE = JSON.parse(localStorage.getItem("UserProfile"));
                                const userContact = userDataE['PreferredMethod']
                                await notififyTransaction(userDataE["Email"], balanceData["UserId"], userContact)

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
    });

});
