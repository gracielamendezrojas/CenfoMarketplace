
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


var today = new Date();

var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

var dateTime = date + ' ' + time;


const userData = JSON.parse(localStorage.getItem("UserProfile"));

const userId = userData['Id'];

//table NFT
function ListNFTs() {

    this.tblNFT = 'tblNFT';
    this.service = 'NFT'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "Name,Price,Status";

    this.RetrieveNFTs = function () {
        this.ctrlActions.FillTable(this.service + "/RetrieveByUser?Id=" + userId, this.tblNFT, false);
    }
}

//table Collections
function ListCollections() {

    this.tblCollection = 'tblCollection';
    this.service = 'Collection'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "User,Id,Name,Description,Status,Collection";

    this.RetrieveCollections = function () {

        this.ctrlActions.FillTable(this.service + "/RetrieveAllByUser/" + userId, this.tblCollection, false);
    }
}

//table My Auctions
function ListAuctionNFT() {

    this.tblAuctionNFT = 'tblAuctionNFT';
    this.service = 'Auction'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "NFTName,FormattedCreationDate,FormattedClosingDate,AuctionPrice";

    this.RetrieveAuctions = function () {
        this.ctrlActions.FillTable(this.service + "/RetrieveMyAuctionsNFT/" + userId, this.tblAuctionNFT, false);
    }
}

function ListAuctionCollection() {

    this.tblAuctionsCollection = 'tblAuctionsCollection';
    this.service = 'Auction'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "CollectionName,FormattedCreationDate,FormattedClosingDate,AuctionPrice";

    this.RetrieveAuctionsCollection = function () {

        this.ctrlActions.FillTable(this.service + "/RetrieveMyAuctionsCollection/" + userId, this.tblAuctionsCollection, false);
    }
}

function ListTransactionsReceiver() {

    this.tbl = 'tblTransactionsReceiver';
    this.service = 'TransactionWallet'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "Id,WalletSender,Amount,Description,FormattedDate";

    this.RetrieveTransactionsReceiver = function () {
        this.ctrlActions.FillTable(this.service + "/GetReceiver/" + userId, this.tbl, false);
    }
}
function ListTransactionsSender() {

    this.tbl = 'tblTransactionsSender';
    this.service = 'TransactionWallet'; //Controlador del API de donde toma la informacion
    this.ctrlActions = new ControlActions();
    this.columns = "Id,WalletReceiver,Amount,Description,FormattedDate";

    this.RetrieveTransactionsSender = function () {
        this.ctrlActions.FillTable(this.service + "/GetSender/" + userId, this.tbl, false);
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

//change base price for selected NFT
$('#auction-NFT-basePrice-button').click(async function () {
    localStorage.removeItem("SelectedAuction");

    let NFTAuction = $('#tblAuctionNFT').DataTable().row('.selected').data();

    fetch(new ControlActions().URL_API + 'bid/GetHighestBid/' + NFTAuction.AcquisitionId, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).then(response => response.json())
        .then(data => {
            if (data.Status === 200) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    footer: '<p>You cannot change the base price because this auction already has bids. A viable alternative is to cancel the auction.</p>',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-Success' //insert class here
                    }
                });
            } else if (data.Status === 400) {
                localStorage.setItem("SelectedAuction", JSON.stringify(NFTAuction));
                window.location.replace(new ControlActions().FE + "Dashboard/AuctionContent");
            }
        });

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

//change base price for selected collection
$('#auction-Collection-basePrice-button').click(async function () {
    localStorage.removeItem("SelectedAuction");

    let CollectionAuction = $('#tblAuctionsCollection').DataTable().row('.selected').data();


    fetch(new ControlActions().URL_API + 'bid/GetHighestBid/' + CollectionAuction.AcquisitionId, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).then(response => response.json())
        .then(data => {
            if (data.Status === 200) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    footer: '<p>You cannot change the base price because this auction already has bids. A viable alternative is to cancel the auction.</p>',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-Success' //insert class here
                    }
                });
            } else if (data.Status === 400) {

                localStorage.setItem("SelectedAuction", JSON.stringify(CollectionAuction));
                window.location.replace(new ControlActions().FE + "Dashboard/AuctionCollectionContent");
            }
        });



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

$('#nft-status-status').click(async function () {


    let name = $('#tblNFT').DataTable().row('.selected').data().Name;


    let nft
    let responseStatus
    let response = await fetch(new ControlActions().URL_API + 'nft/retrievebyname?name=' + name)
        .then(response => response.json()).then(json => { nft = json.Data; responseStatus = json.Status });


    if (responseStatus == 200) {
        if (nft.Status == 'On Sale') {
            nft.Status = 'Not On Sale';


            await fetch(new ControlActions().URL_API + 'nft/update',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(nft)
                }
            )

            const userActionData = {
                User: userId,
                Date: new Date().getDate,
                Type: 'NFT status changed'
            }

            await fetch(new ControlActions().URL_API + 'useraction/post',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(userActionData)
                }
            )
            Swal.fire({
                icon: 'success',
                title: 'There NFT status has been changed.',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }
            });
            document.querySelector('.btn-Success').addEventListener('click', () => {
                window.location.reload();
            });
        }
        else if (nft.Status == 'Not On Sale') {
            nft.Status = 'On Sale'


            await fetch(new ControlActions().URL_API + 'nft/update',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(nft)
                }
            )

            const userActionData = {
                User: userId,
                Date: new Date().getDate,
                Type: 'NFT status changed'
            }

            await fetch(new ControlActions().URL_API + 'useraction/post',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(userActionData)
                }
            )
            Swal.fire({
                icon: 'success',
                title: 'There NFT status has been changed.',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }
            });
            document.querySelector('.btn-Success').addEventListener('click', () => {
                window.location.reload();
            });
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'There has been an error.',
                footer: '<p>To change status of this NFT, the auction must be closed or cancelled.</p>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }
            });

        }

    }
    else {
        console.log("500")
    }
});


//delete NFT
$('#delete-NFT').click(async function () {
    let id = $('#tblNFT').DataTable().row('.selected').data().Id;
    let status = $('#tblNFT').DataTable().row('.selected').data().Status;
    const userActionData = {
        User: userId,
        Type: 'NFT deleted'
    }

    if (status != 'Auction') {
        fetch(new ControlActions().URL_API + 'nft/DeleteById/' + id, {
            method: 'Delete',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(userActionData)
        }).then(response => response.json())
            .then(NFTDelete => {
                if (NFTDelete.Status === 200) {


                    fetch(new ControlActions().URL_API + 'useraction/post', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(userActionData)
                    });

                    Swal.fire({
                        icon: 'success',
                        title: 'Your NFT has been deleted.',
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
            title: 'An NFT in an auction cannot be deleted.',
            footer: '<p>You have to change its status to "On Sale" or "Not On Sale" before you delete an NFT.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    }
});

//validation for NFT auctions
async function validateFieldsForAuctionNFT() {
    var valid = true;

    if ($("#NFTDateStartAuction").val() === "" || $("#NFTDateStartAuction").val() === null) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must select a start date for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#timepickerStartNFT").val() === "" || $("#timepickerStartNFT").val() === null) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must select a start time for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#NFTdateEndAuction").val() === "" || $("#NFTdateEndAuction").val() === null) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must select a end date for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#timepickerEndNFT").val() === "" || $("#timepickerEndNFT").val() === null) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must select a end time for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#txtNFTBasePrice").val() === "" || $("#txtNFTBasePrice").val() === null || isNaN($("#txtNFTBasePrice").val()) || $("#txtNFTBasePrice").val() <= 0) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must stablish a base price for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#Auction-NFT-dropdown").val() === "" || $("#Auction-NFT-dropdown").val() === null) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must select an NFT for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#NFTDateStartAuction").val() != "" && $("#NFTDateStartAuction").val() != null &&
        $("#timepickerStartNFT").val() != "" && $("#timepickerStartNFT").val() != null &&
        $("#NFTdateEndAuction").val() != "" && $("#NFTdateEndAuction").val() != null &&
        $("#timepickerEndNFT").val() != "" && $("#timepickerEndNFT").val() != null) {
        if (($("#NFTDateStartAuction").val() + " " + $("#timepickerStartNFT").val()) >= ($("#NFTdateEndAuction").val() + " " + $("#timepickerEndNFT").val())) {
            valid = false;
            Swal.fire({
                icon: 'error',
                title: 'Invalid form.',
                footer: '<p>The end date must be after the start date (selected time is also considered).</p>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }
            });
        };
    };



    let startdate = {
        DateAuction: $("#NFTDateStartAuction").val() + " " + $("#timepickerStartNFT").val(),
    };

    let dateComparison =
        await fetch(new ControlActions().URL_API + 'auction/datecomparison', {
            method: 'post',
            body: JSON.stringify(startdate),
            headers: {
                'accept': 'application/json',
                'content-type': 'application/json'
            },
        });


    let data = await dateComparison.json();

    if (data.Status === 400) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'invalid form.',
            footer: '<p>the date cannot be a date in the past (selected time is also considered).</p>',
            buttonsstyling: false,
            customclass: {
                confirmbutton: 'btn btn-success' //insert class here
            }
        });
    }

    return valid;
}

//create an Auction for NFT
document.getElementById("auction-NFT").addEventListener('click', async () => {
    let selectedNFT = $("#Auction-NFT-dropdown").val();

    let NFTData
    await fetch(new ControlActions().URL_API + 'NFT/RetrieveByUser/' + userId)
        .then(response => response.json()).then(json => NFTData = json.Data);

    let selectedNFTId;
    let NFTUpdated;
    NFTData.forEach(NFT => {
        if (NFT["Name"] == selectedNFT) {
            selectedNFTId = NFT["Id"];
            NFTUpdated = NFT;

        }
    })

    let Acquisition = {
        CreationDate: $("#NFTDateStartAuction").val() + " " + $("#timepickerStartNFT").val(),
        ClosingDate: $("#NFTdateEndAuction").val() + " " + $("#timepickerEndNFT").val(),
        Creator: userId,
        Price: $("#txtNFTBasePrice").val(),
        Buyer: userId
    };


    if (await validateFieldsForAuctionNFT()) {
        //save in database
        await fetch(new ControlActions().URL_API + 'Acquisition/Post', {
            method: 'POST',
            body: JSON.stringify(Acquisition),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        })
            .then(response => response.json())
            .then(dataAcquisition => {
                let NFTXAcq = {
                    NFT: selectedNFTId,
                    Acquisition_Id: dataAcquisition.Data.Id,
                };
                NFTUpdated.Status = "Auction";
                //update status
                fetch(new ControlActions().URL_API + 'Nft/Update', {
                    method: 'POST',
                    body: JSON.stringify(NFTUpdated),
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                })
                    .then(response => response.json())
                    .then(NFTUpdate => {
                    });
                //save in database
                fetch(new ControlActions().URL_API + 'NFTXAcquisition/Create', {
                    method: 'POST',
                    body: JSON.stringify(NFTXAcq),
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                })
                    .then(response => response.json())
                    .then(dataNFTXAcquisition => {

                        Swal.fire({
                            icon: 'success',
                            title: 'You created a new auction.',
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-Success' //insert class here
                            }
                        });
                        document.querySelector('.btn-Success').addEventListener('click', () => {
                            window.location.reload();
                        });

                    });;

            });;

    }

});


//validation for Collection auctions
async function validateFieldsForAuctionCollection() {
    var valid = true;
    if ($("#CollectionDateStartAuction").val() === "" || $("#CollectionDateStartAuction").val() === null) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must select a start date for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#timepickerStartCollection").val() === "" || $("#timepickerStartCollection").val() === null) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must select a start time for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#CollectionDateEndAuction").val() === "" || $("#CollectionDateEndAuction").val() === null) {
        valid = false;
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must select a end date for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#timepickerEndCollection").val() === "" || $("#timepickerEndCollection").val() === null) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must select a end time for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#txtCollectionBasePrice").val() === "" || $("#txtCollectionBasePrice").val() === null || isNaN($("#txtCollectionBasePrice").val()) || $("#txtCollectionBasePrice").val() <= 0) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must stablish a base price for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#Auction-Collection-dropdown").val() === "" || $("#Auction-Collection-dropdown").val() === null) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Incomplete form.',
            footer: '<p>Must select a collection for the auction.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    };

    if ($("#CollectionDateStartAuction").val() != "" && $("#CollectionDateStartAuction").val() != null &&
        $("#timepickerStartCollection").val() != "" && $("#timepickerStartCollection").val() != null &&
        $("#CollectionDateEndAuction").val() != "" && $("#CollectionDateEndAuction").val() != null &&
        $("#timepickerEndCollection").val() != "" && $("#timepickerEndCollection").val() != null) {
        if (($("#CollectionDateStartAuction").val() + " " + $("#timepickerStartCollection").val()) >= ($("#CollectionDateEndAuction").val() + " " + $("#timepickerEndCollection").val())) {
            valid = false;
            Swal.fire({
                icon: 'error',
                title: 'Invalid form.',
                footer: '<p>The end date must be after the start date (selected time is also considered).</p>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }
            });
        };
    };

    let startdate = {
        DateAuction: $("#CollectionDateStartAuction").val() + " " + $("#timepickerStartCollection").val(),
    };

    let dateComparison =
        await fetch(new ControlActions().URL_API + 'auction/datecomparison', {
            method: 'post',
            body: JSON.stringify(startdate),
            headers: {
                'accept': 'application/json',
                'content-type': 'application/json'
            },
        });

    let data = await dateComparison.json();

    if (data.Status === 400) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'invalid form.',
            footer: '<p>the date cannot be a date in the past (selected time is also considered).</p>',
            buttonsstyling: false,
            customclass: {
                confirmbutton: 'btn btn-success' //insert class here
            }
        });
    }


    return valid;
}

//create an Auction for Collections
document.getElementById("auction-collection").addEventListener('click', async () => {

    let selectedCollection = $("#Auction-Collection-dropdown").val();
    let CollectionData
    await fetch(new ControlActions().URL_API + 'Collection/RetrieveAllByUser/' + userId)
        .then(response => response.json()).then(json => CollectionData = json.Data);

    let selectedCollectionId;
    let updatedCollection;
    CollectionData.forEach(Collection => {
        if (Collection["Name"] == selectedCollection) {
            selectedCollectionId = Collection["Id"];
            updatedCollection = Collection;
        }
    })

    let Acquisition = {
        CreationDate: $("#CollectionDateStartAuction").val() + " " + $("#timepickerStartCollection").val(),
        ClosingDate: $("#CollectionDateEndAuction").val() + " " + $("#timepickerEndCollection").val(),
        Creator: userId,
        Price: $("#txtCollectionBasePrice").val(),
        Buyer: userId
    };


    if (await validateFieldsForAuctionCollection()) {
        //save Acquisition
        fetch(new ControlActions().URL_API + 'Acquisition/Post', {
            method: 'POST',
            body: JSON.stringify(Acquisition),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
        })
            .then(response => response.json())
            .then(dataAcquisition => {

                //Retrieve All NFTs
                fetch(new ControlActions().URL_API + 'Nft/RetrieveAll', {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    },
                })
                    .then(response => response.json())
                    .then(NFTData => {
                        let allNFTs = NFTData.Data;
                        //NFTs from the selected collection
                        allNFTs.forEach(collectionNFT => {
                            if (collectionNFT["Collection"] === selectedCollectionId) {

                                if (collectionNFT["Status"] === "Auction") {
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'We have a problem, an NFT from this collection has an auction.',
                                        footer: '<p>You have to delete the auction from the NFT to have an auction from the whole collection.</p>',
                                        buttonsStyling: false,
                                        customClass: {
                                            confirmButton: 'btn btn-Success' //insert class here
                                        }
                                    });

                                } else {
                                    let NFTId = collectionNFT["Id"];

                                    let NFTUpdated = collectionNFT;
                                    NFTUpdated.Status = "Auction";
                                    //update NFT status
                                    fetch(new ControlActions().URL_API + 'Nft/Update', {
                                        method: 'POST',
                                        body: JSON.stringify(NFTUpdated),
                                        headers: {
                                            'Accept': 'application/json',
                                            'Content-Type': 'application/json'
                                        },
                                    })
                                        .then(response => response.json())
                                        .then(NFTUpdate => {
                                            let collectionNFTAcquisition = {
                                                NFT: NFTId,
                                                Acquisition_Id: dataAcquisition.Data.Id,
                                            };
                                            fetch(new ControlActions().URL_API + 'NFTXAcquisition/Create', {
                                                method: 'POST',
                                                body: JSON.stringify(collectionNFTAcquisition),
                                                headers: {
                                                    'Accept': 'application/json',
                                                    'Content-Type': 'application/json'
                                                },
                                            })
                                                .then(response => response.json())
                                                .then(dataNFTXAcquisition => {
                                                });
                                        });
                                    //update collection
                                    updatedCollection.SaleStatus = "Auction";
                                    fetch(new ControlActions().URL_API + 'Collection/Update', {
                                        method: 'POST',
                                        body: JSON.stringify(updatedCollection),
                                        headers: {
                                            'Accept': 'application/json',
                                            'Content-Type': 'application/json'
                                        },
                                    })
                                        .then(response => response.json())
                                        .then(CollectionUpdate => {
                                            //save NFTXAcquisition
                                            Swal.fire({
                                                icon: 'success',
                                                title: 'You created a new auction.',
                                                buttonsStyling: false,
                                                customClass: {
                                                    confirmButton: 'btn btn-Success' //insert class here
                                                }
                                            });
                                            document.querySelector('.btn-Success').addEventListener('click', () => {
                                                window.location.reload();
                                            });

                                        });;

                                }



                            };
                        });
                    });;
            });;
    }
});


let image;
var widget = cloudinary.createUploadWidget(
    {
        cloudName: 'cenfotecmarket',
        uploadPreset: 'gcut4nfj'
    },
    (error, result) => {
        if (!error && result && result.event === "success") {
            console.log('Done uploading..: ', result.info.secure_url);
            image = result.info.secure_url;
        }

    });

document.getElementById("upload_widget").addEventListener("click", function () {
    widget.open();
}, false);



//create an NFT
document.getElementById("create-nft").addEventListener('click', async () => {
    let name, price;

    if (document.querySelector("#nft-name").value != "") {
        name = document.querySelector("#nft-name").value
    }
    else {
        document.querySelector("#nft-name").classList.add('is-invalid')
    }

    if (!isNaN(document.querySelector("#nft-price").value) && document.querySelector("#nft-price").value > 0) {
        price = document.querySelector("#nft-price").value
    }
    else {
        document.querySelector("#nft-price").classList.add('is-invalid')
    }

    let colData
    await fetch(new ControlActions().URL_API + 'collection/RetrieveAll')
        .then(response => response.json()).then(json => colData = json.Data);

    let selectedCollection;
    colData.forEach(collection => {
        if (collection["Name"] == document.querySelector("#collection-dropdown").value) {
            selectedCollection = collection["Id"]
        }
    })


    let catData
    await fetch(new ControlActions().URL_API + 'category/RetrieveAll')
        .then(response => response.json()).then(json => catData = json.Data);

    let selectedCategory;
    catData.forEach(category => {
        if (category["Name"] == document.querySelector("#category-dropdown").value) {
            selectedCategory = category["Id"]
        }
    })

    if (name && price && image) {
        let data = {
            "Name": name,
            "Price": price,
            "Route": image,
            "Category": selectedCategory,
            "Collection": selectedCollection,
            "Status": "On Sale"
        };
        let response = await fetch(new ControlActions().URL_API + 'nft/create',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            }
        );
        window.location.reload();
    }
});

//create a collection
document.querySelector('#create-collection').addEventListener('click', async () => {
    let name, description;
    if (document.querySelector("#col-name").value != "") {
        name = document.querySelector("#col-name").value
    }
    else {
        document.querySelector("#col-name").classList.add("is-invalid");
    }
    if (document.querySelector("#col-description").value != "") {
        description = document.querySelector("#col-description").value
    }
    else {
        document.querySelector("#col-description").classList.add("is-invalid");
    }

    if (name && description) {
        let data = {
            "Name": name,
            "Description": description,
            "Status": "Active",
            "SaleStatus": "On Sale",
            "User": userId
        }

        let response = await fetch(new ControlActions().URL_API + 'collection/create',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(data)
            }
        );
        window.location.reload();
    }
})




$('#tblCollection tbody').on('click', 'tr', function () {

    if ($(this).hasClass('selected')) {
        $(this).removeClass('selected');
    }
    else {
        $('#tblCollection').DataTable().$('tr.selected').removeClass('selected');
        $(this).addClass('selected');
    }
});

$('#collection-status-button').click(async function () {

    let name = $('#tblCollection').DataTable().row('.selected').data().Name;

    let collection
    let responseStatus
    let response = await fetch(new ControlActions().URL_API + 'collection/retrievebyname?name=' + name)
        .then(response => response.json()).then(json => { collection = json.Data; responseStatus = json.Status });
    if (responseStatus == 200) {
        if (collection.SaleStatus == 'On Sale') {
            collection.SaleStatus = 'Not On Sale';

            let resCollection = await fetch(new ControlActions().URL_API + 'collection/update',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(collection)
                }
            )

            const userActionData = {
                User: userId,
                Date: new Date().getDate,
                Type: 'Collection status changed'
            }

            await fetch(new ControlActions().URL_API + 'useraction/post',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(userActionData)
                }
            )
            Swal.fire({
                icon: 'success',
                title: 'There Collection status has been changed.',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }
            });
            document.querySelector('.btn-Success').addEventListener('click', () => {
                window.location.reload();
            });

        }
        else if (collection.SaleStatus == 'Not On Sale') {
            collection.SaleStatus = 'On Sale';

            let resCollection = await fetch(new ControlActions().URL_API + 'collection/update',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(collection)
                }
            )

            const userActionData = {
                User: userId,
                Date: new Date().getDate,
                Type: 'Collection status changed'
            }

            await fetch(new ControlActions().URL_API + 'useraction/post',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(userActionData)
                }
            )
            Swal.fire({
                icon: 'success',
                title: 'There Collection status has been changed',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }
            });
            document.querySelector('.btn-Success').addEventListener('click', () => {
                window.location.reload();
            });
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'There has been an error.',
                footer: '<p>To change status of this collection, the auction must be closed or cancelled.</p>',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn btn-Success' //insert class here
                }
            });
            return
        }

    }
    else {
        console.log("500")
    }
});



//delete Collection
$('#delete-collection').click(async function () {

    let collection = $('#tblCollection').DataTable().row('.selected').data();
    const userActionData = {
        User: userId,
        Type: 'Collection deleted'
    }

    if (status != 'Auction') {

        fetch(new ControlActions().URL_API + 'nft/RetrieveByCollectionContent/' + collection.Id, {
            method: 'Get',
            headers: { 'Content-Type': 'application/json' },
        }).then(response => response.json())
            .then(data => {
                if (data.Data.length != 0) {
                    Swal.fire({
                        icon: 'error',
                        title: 'A collection with NFTs cannot be deleted.',
                        footer: '<p>Only empty collections can be deleted.</p>',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn btn-Success' //insert class here
                        }
                    });
                } else {
                    fetch(new ControlActions().URL_API + 'collection/DeleteById/' + collection.Id, {
                        method: 'Delete',
                        headers: { 'Content-Type': 'application/json' },
                    }).then(response => response.json())
                        .then(dataDelete => {
                            if (dataDelete.Status == 200) {
                                fetch(new ControlActions().URL_API + 'useraction/post',
                                    {
                                        method: 'POST',
                                        headers: { 'Content-Type': 'application/json' },
                                        body: JSON.stringify(userActionData)
                                    }
                                )
                                    .then(response => response.json())
                                    .then(dataUserAction => {
                                    });

                                Swal.fire({
                                    icon: 'success',
                                    title: 'Collection deleted succesfully.',
                                    buttonsStyling: false,
                                    customClass: {
                                        confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                                    }
                                });
                                document.querySelector('.btn-Success').addEventListener('click', () => {
                                    window.location.replace(new ControlActions().FE + "Dashboard/creatordashboard");
                                });
                            }
                        });
                }
            });
    } else {
        Swal.fire({
            icon: 'error',
            title: 'A collection in an auction cannot be deleted.',
            footer: '<p>You have to change its status to "On Sale" or "Not On Sale" before you delete your collection.</p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success' //insert class here
            }
        });
    }
});


$('#NFT-from-collection').click(async function () {
    localStorage.removeItem("SelectedCollection");

    let collection = $('#tblCollection').DataTable().row('.selected').data();
    let collectionId = collection.Id;
    if (collectionId > 0) {
        localStorage.setItem("SelectedCollection", JSON.stringify(collection));
        window.location.replace(new ControlActions().FE + "Dashboard/CollectionContentCreator");
    }
});

const notififyTransaction = async (userEmail, userId, userContact) => {
    let emailInformation = {
        Name: "",
        LastName: "",
        EmailAdress: userEmail,
        Message: "",
    };
    switch (userContact) {
        case 'Email': await fetch(new ControlActions().URL_API + 'Email/SendTransactionEmail', {
            method: 'POST',
            body: JSON.stringify(emailInformation),
            headers: {
                'Content-Type': 'application/json',
            },

        }); console.log("Email"); break;
        case 'SMS': await fetch(new ControlActions().URL_API + 'Email/SendTransactionSMS?userId=' + userId); console.log("SMS"); break;
        case 'Both': await fetch(new ControlActions().URL_API + 'Email/SendTransactionEmail', {
            method: 'POST',
            body: JSON.stringify(emailInformation),
            headers: {
                'Content-Type': 'application/json',
            },

        }); console.log("2");
            await fetch(new ControlActions().URL_API + 'Email/SendTransactionSMS?userId=' + userId);
            break;
        default: break;
    }
}


//Close NFT auction
document.querySelector('#auction-NFT-closeAuction-button').addEventListener('click', async () => {
    let NFTId = $('#tblAuctionNFT').DataTable().row('.selected').data().NFT;
    let auction = $('#tblAuctionNFT').DataTable().row('.selected').data();
    let validUser;


    let response = await fetch(new ControlActions().URL_API + 'bid/GetHighestBid/' + auction.AcquisitionId, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).then(response => response.json())
        .then(data => {
            validUser = data.Data.User;
            if (data.Data.User === 0) {
                Swal.fire({
                    icon: 'error',
                    title: 'There has been an error.',
                    footer: '<p>This auction does not have bids yet.</p>',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-Success' //insert class here
                    }
                });
                return;
            }
        });


    if (validUser != 0) {

        let nft
        let response = await fetch(new ControlActions().URL_API + 'nft/retrieve?nft=' + auction.NFT)
            .then(response => response.json()).then(json => { nft = json.Data; responseStatus = json.Status });

        let adminWallet;
        let adminWalletStatus;
        await fetch(new ControlActions().URL_API + 'wallet/get?id=bcc5a83717d83a33fe776db644ad940e')
            .then(response => response.json()).then(json => { adminWallet = json.Data; adminWalletStatus = json.Status });

        let creatorWallet;
        let creatorWalletStatus
        await fetch(new ControlActions().URL_API + 'wallet/getbyuser?id=' + auction.Creator)
            .then(response => response.json()).then(json => { creatorWallet = json.Data; creatorWalletStatus = json.Status });

        let highestBid;
        let highestBidStatus;
        await fetch(new ControlActions().URL_API + 'bid/GetHighestBid/' + auction.AcquisitionId)
            .then(response => response.json()).then(json => { highestBid = json.Data; highestBidStatus = json.Status });


        let buyerWallet;
        let buyerWalletStatus
        await fetch(new ControlActions().URL_API + 'wallet/getbyuser?id=' + highestBid.User)
            .then(response => response.json()).then(json => { buyerWallet = json.Data; buyerWalletStatus = json.Status });

        let buyerCollection;
        await fetch(new ControlActions().URL_API + 'collection/buyerDefaultCollection/' + highestBid.User)
            .then(response => response.json()).then(json => { buyerCollection = json.Data; });


        let creatorUser
        await fetch(new ControlActions().URL_API + 'user/GetUserbyID/' + userId)
            .then(response => response.json()).then(json => { creatorUser = json.Data; });

        let buyerUser
        await fetch(new ControlActions().URL_API + 'user/GetUserbyID/' + highestBid.User)
            .then(response => response.json()).then(json => { buyerUser = json.Data; });

        if (buyerWallet["Balance"] >= auction.AuctionPrice) {

            if (creatorWalletStatus == 200 && adminWalletStatus == 200) {
                creatorWallet["Balance"] += (auction.AuctionPrice - auction.AuctionPrice * 0.1);
                adminWallet["Balance"] += auction.AuctionPrice * 0.1;


                const transactionWalletCreatorIncrease = {
                    WalletSender: buyerWallet["Id"],
                    WalletReceiver: creatorWallet["Id"],
                    Amount: auction.AuctionPrice - auction.AuctionPrice * 0.1,
                    Description: 'NFT Auction Best Offer Closure',
                    TransactionDate: dateTime
                }
                await fetch(new ControlActions().URL_API + 'transactionwallet/post',
                    {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(transactionWalletCreatorIncrease)
                    }
                );

                const transactionWalletAdminIncrease = {
                    WalletSender: buyerWallet["Id"],
                    WalletReceiver: adminWallet["Id"],
                    Amount: auction.AuctionPrice * 0.1,
                    Description: 'NFT Auction Commission',
                    TransactionDate: dateTime
                }
                await fetch(new ControlActions().URL_API + 'transactionwallet/post',
                    {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(transactionWalletAdminIncrease)
                    }
                );



                let creatorWalletIncreaseStatus;

                await fetch(new ControlActions().URL_API + 'wallet/edit',
                    {
                        method: 'PUT',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(creatorWallet)
                    }
                ).then(response => response.json()).then(json => creatorWalletIncreaseStatus = json.Status);

                let adminWalletIncreaseStatus;
                await fetch(new ControlActions().URL_API + 'wallet/edit',
                    {
                        method: 'PUT',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(adminWallet)
                    }
                ).then(response => response.json()).then(json => adminWalletIncreaseStatus = json.Status);

                if (creatorWalletIncreaseStatus == 200 && adminWalletIncreaseStatus == 200) {

                    await notififyTransaction(creatorUser.Email, creatorUser.Id, creatorUser.PreferredMethod)

                    buyerWallet["Balance"] -= auction.AuctionPrice

                    let buyerWalletDecreaseStatus;

                    await fetch(new ControlActions().URL_API + 'wallet/edit',
                        {
                            method: 'PUT',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(buyerWallet)
                        }
                    ).then(response => response.json()).then(json => buyerWalletDecreaseStatus = json.Status);

                    if (buyerWalletDecreaseStatus == 200) {
                        await notififyTransaction(buyerUser.Email, buyerUser.Id, buyerUser.PreferredMethod)

                        nft.Collection = buyerCollection.Id;
                        nft.Status = "Not On Sale";

                        let nftChangeStatus
                        await fetch(new ControlActions().URL_API + 'nft/update',
                            {
                                method: 'POST',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify(nft)
                            }
                        ).then(response => response.json()).then(json => nftChangeStatus = json.Status);

                        if (nftChangeStatus == 200) {

                            const userActionData = {
                                User: buyerWallet["UserId"],
                                Date: dateTime,
                                Type: 'NFT auction won'
                            }

                            await fetch(new ControlActions().URL_API + 'useraction/post',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(userActionData)
                                }
                            )


                            const acquisition = {
                                CreationDate: auction.CreationDate,
                                ClosingDate: dateTime,
                                Price: auction.AuctionPrice,
                                Buyer: buyerWallet["UserId"],
                                Creator: auction.Creator
                            }

                            await fetch(new ControlActions().URL_API + 'acquisition/edit',
                                {
                                    method: 'PUT',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(acquisition)
                                }
                            )


                            //Colocar envio de invoice
                            const invoice = {
                                Email: buyerUser.Email,
                                Name: buyerUser.Name,
                                LastName: buyerUser.LastName,
                                NFT: auction.NFTName,
                                Collection: null,
                                Price: auction.AuctionPrice,
                            };
                            await fetch(new ControlActions().URL_API + 'Invoice/SentEmailInvoice',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(invoice)
                                }
                            ).then(response => response.json());

                            //Notification Campana
                            let statuss;
                            const noti = {
                                User: buyerUser.Id,
                                Message: "An auction has been closed.",
                                Subject: "You won the " + auction.NFTName + " auction.",
                            };
                            await fetch(new ControlActions().URL_API + 'Notifications/Post',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(noti)
                                }
                            ).then(response => response.json());

                            Swal.fire({
                                icon: 'success',
                                title: 'Best offer taken successfully.',
                                buttonsStyling: false,
                                customClass: {
                                    confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                                }
                            });
                            document.querySelector('.btn-Success').addEventListener('click', () => {
                                window.location.replace(new ControlActions().FE + "Dashboard/creatordashboard");
                            });
                        }
                    }
                }
            }
        } else {
            Swal.fire({
                icon: 'error',
                title: 'The buyer does not have enough balance.',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                }
            });
        }
    }
})



//Close Collection auction
document.querySelector('#auction-Collection-closeAuction-button').addEventListener('click', async () => {
    let auction = $('#tblAuctionsCollection').DataTable().row('.selected').data();

    let CollectionId = auction.Collection;

    let validUser;
    await fetch(new ControlActions().URL_API + 'bid/GetHighestBid/' + auction.AcquisitionId, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
    }).then(response => response.json())
        .then(data => {
            validUser = data.Data.User;
            if (data.Data.User == 0) {

                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    footer: '<p>This auction does not have bids yet.</p>',
                    buttonsStyling: false,
                    customClass: {
                        confirmButton: 'btn btn-Success' //insert class here
                    }
                });
            }
        });


    if (validUser != 0) {
        let collection;
        let response = await fetch(new ControlActions().URL_API + 'collection/retrieveOne/' + CollectionId)
            .then(response => response.json()).then(json => { collection = json.Data; responseStatus = json.Status });

        let adminWallet;
        let adminWalletStatus;
        await fetch(new ControlActions().URL_API + 'wallet/get?id=bcc5a83717d83a33fe776db644ad940e')
            .then(response => response.json()).then(json => { adminWallet = json.Data; adminWalletStatus = json.Status });

        let creatorWallet;
        let creatorWalletStatus
        await fetch(new ControlActions().URL_API + 'wallet/getbyuser/' + userId)
            .then(response => response.json()).then(json => { creatorWallet = json.Data; creatorWalletStatus = json.Status });

        let highestBid;
        let highestBidStatus;
        await fetch(new ControlActions().URL_API + 'bid/GetHighestBid/' + auction.AcquisitionId)
            .then(response => response.json()).then(json => { highestBid = json.Data; highestBidStatus = json.Status });

        let buyerWallet;
        let buyerWalletStatus
        await fetch(new ControlActions().URL_API + 'wallet/getbyuser?id=' + highestBid.User)
            .then(response => response.json()).then(json => { buyerWallet = json.Data; buyerWalletStatus = json.Status });

        let buyerCollection;
        await fetch(new ControlActions().URL_API + 'collection/buyerDefaultCollection/' + highestBid.User)
            .then(response => response.json()).then(json => { buyerCollection = json.Data; });


        let creatorUser
        await fetch(new ControlActions().URL_API + 'user/GetUserbyID/' + userId)
            .then(response => response.json()).then(json => { creatorUser = json.Data; });

        let buyerUser
        await fetch(new ControlActions().URL_API + 'user/GetUserbyID/' + highestBid.User)
            .then(response => response.json()).then(json => { buyerUser = json.Data; });

        if (buyerWallet["Balance"] >= auction.AuctionPrice) {


            if (creatorWalletStatus == 200 && adminWalletStatus == 200) {
                creatorWallet["Balance"] += (auction.AuctionPrice - auction.AuctionPrice * 0.1);
                adminWallet["Balance"] += auction.AuctionPrice * 0.1;


                const transactionWalletCreatorIncrease = {
                    WalletSender: buyerWallet["Id"],
                    WalletReceiver: creatorWallet["Id"],
                    Amount: auction.AuctionPrice - auction.AuctionPrice * 0.1,
                    Description: 'NFT Auction Best Offer Closure',
                    TransactionDate: dateTime
                }
                await fetch(new ControlActions().URL_API + 'transactionwallet/post',
                    {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(transactionWalletCreatorIncrease)
                    }
                );

                const transactionWalletAdminIncrease = {
                    WalletSender: buyerWallet["Id"],
                    WalletReceiver: adminWallet["Id"],
                    Amount: auction.AuctionPrice * 0.1,
                    Description: 'NFT Auction Commission',
                    TransactionDate: dateTime
                }
                await fetch(new ControlActions().URL_API + 'transactionwallet/post',
                    {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(transactionWalletAdminIncrease)
                    }
                );

                let creatorWalletIncreaseStatus;

                await fetch(new ControlActions().URL_API + 'wallet/edit',
                    {
                        method: 'PUT',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(creatorWallet)
                    }
                ).then(response => response.json()).then(json => creatorWalletIncreaseStatus = json.Status);

                let adminWalletIncreaseStatus;
                await fetch(new ControlActions().URL_API + 'wallet/edit',
                    {
                        method: 'PUT',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(adminWallet)
                    }
                ).then(response => response.json()).then(json => adminWalletIncreaseStatus = json.Status);

                if (creatorWalletIncreaseStatus == 200 && adminWalletIncreaseStatus == 200) {

                    await notififyTransaction(creatorUser.Email, creatorUser.Id, creatorUser.PreferredMethod);

                    buyerWallet["Balance"] -= auction.AuctionPrice;

                    let buyerWalletDecreaseStatus;

                    await fetch(new ControlActions().URL_API + 'wallet/edit',
                        {
                            method: 'PUT',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(buyerWallet)
                        }
                    ).then(response => response.json()).then(json => buyerWalletDecreaseStatus = json.Status);

                    if (buyerWalletDecreaseStatus == 200) {

                        await notififyTransaction(buyerUser.Email, buyerUser.Id, buyerUser.PreferredMethod);

                        await fetch(new ControlActions().URL_API + 'Nft/RetrieveAll', {
                            method: 'GET',
                            headers: {
                                'Accept': 'application/json',
                                'Content-Type': 'application/json'
                            },
                        }).then(response => response.json())
                            .then(NFTData => {

                                let allNFTs = NFTData.Data;

                                allNFTs.forEach(NFT => {

                                    if (NFT["Collection"] === CollectionId) {

                                        let NFTUpdated = NFT;
                                        NFTUpdated.Status = "Not On Sale";

                                        fetch(new ControlActions().URL_API + 'Nft/Update', {
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


                        collection.SaleStatus = "Not On Sale";
                        collection.User = buyerWallet["UserId"];

                        let collectionChangeStatus
                        await fetch(new ControlActions().URL_API + 'collection/update',
                            {
                                method: 'POST',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify(collection)
                            }
                        ).then(response => response.json()).then(json => collectionChangeStatus = json.Status);

                        if (collectionChangeStatus == 200) {
                            const userActionData = {
                                User: buyerWallet["UserId"],
                                Date: dateTime,
                                Type: 'Collection auction won'
                            }

                            await fetch(new ControlActions().URL_API + 'useraction/post',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(userActionData)
                                }
                            )


                            const acquisition = {
                                CreationDate: auction.CreationDate,
                                ClosingDate: dateTime,
                                Price: auction.AuctionPrice,
                                Buyer: buyerWallet["UserId"],
                                Creator: auction.Creator
                            }

                            await fetch(new ControlActions().URL_API + 'acquisition/edit',
                                {
                                    method: 'PUT',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(acquisition)
                                }
                            )

                            //Colocar envio de invoice
                            const invoicee = {
                                Email: buyerUser.Email,
                                Name: buyerUser.Name,
                                LastName: buyerUser.LastName,
                                NFT: null,
                                Collection: auction.CollectionName,
                                Price: auction.AuctionPrice,
                            };
                            await fetch(new ControlActions().URL_API + 'Invoice/SentEmailInvoice',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(invoicee)
                                }
                            ).then(response => console.log(response));


                            //Notification Campana
                            let status;
                            const notif = {
                                User: buyerUser.Id,
                                Message: "An auction has been won.",
                                Subject: "You won the " + auction.CollectionName + " auction.",
                            };
                            await fetch(new ControlActions().URL_API + 'Notifications/Post',
                                {
                                    method: 'POST',
                                    headers: { 'Content-Type': 'application/json' },
                                    body: JSON.stringify(notif)
                                }
                            ).then(response => response.json());



                            Swal.fire({
                                icon: 'success',
                                title: 'Best offer taken successfully.',
                                buttonsStyling: false,
                                customClass: {
                                    confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                                }
                            });
                            document.querySelector('.btn-Success').addEventListener('click', () => {
                                window.location.replace(new ControlActions().FE + "Dashboard/creatordashboard");
                            });
                        }
                    }

                }
            }

        } else {
            Swal.fire({
                icon: 'error',
                title: 'The buyer does not have enough balance.',
                buttonsStyling: false,
                customClass: {
                    confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                }
            });

        }
    }
})

//load information
$(document).ready(async function () {

    $('#timepickerStartNFT').timepicker({
        timeFormat: 'h:mm p',
        interval: 15,
        dynamic: false,
        dropdown: true,
        scrollbar: true
    });

    $('#timepickerEndNFT').timepicker({
        timeFormat: 'h:mm p',
        interval: 15,
        dynamic: false,
        dropdown: true,
        scrollbar: true
    });

    $('#timepickerStartCollection').timepicker({
        timeFormat: 'h:mm p',
        interval: 15,
        dynamic: false,
        dropdown: true,
        scrollbar: true
    });

    $('#timepickerEndCollection').timepicker({
        timeFormat: 'h:mm p',
        interval: 15,
        dynamic: false,
        dropdown: true,
        scrollbar: true
    });

    const userData = JSON.parse(localStorage.getItem("UserProfile"));

    const userId = userData['Id'];

    let earningsData
    await fetch(new ControlActions().URL_API + 'wallet/getbyuser?id=' + userId)
        .then(response => response.json()).then(json => earningsData = json.Data.Balance);

    let catData
    await fetch(new ControlActions().URL_API + 'category/RetrieveAll')
        .then(response => response.json()).then(json => catData = json.Data);

    let colData
    await fetch(new ControlActions().URL_API + 'collection/RetrieveAll')
        .then(response => response.json()).then(json => colData = json.Data);

    let NFTData
    await fetch(new ControlActions().URL_API + 'NFT/RetrieveByUser/' + userId)
        .then(response => response.json()).then(json => NFTData = json.Data);


    let CollectionData
    await fetch(new ControlActions().URL_API + 'Collection/RetrieveAllByUser/' + userId)
        .then(response => response.json()).then(json => CollectionData = json.Data);


    catData.forEach(category => {
        let o = document.createElement("option");
        o.innerHTML = category["Name"];
        document.querySelector("#category-dropdown").appendChild(o);
    });

    colData.forEach(collection => {
        let o = document.createElement("option");
        if (collection["User"] == userId) {
            o.innerHTML = collection["Name"];
            document.querySelector("#collection-dropdown").appendChild(o);
        }
    });


    NFTData.forEach(NFT => {
        let o = document.createElement("option");
        o.innerHTML = NFT["Name"];
        if (NFT["Status"] != "Auction") {
            document.querySelector("#Auction-NFT-dropdown").appendChild(o);
        }
    });


    CollectionData.forEach(Collection => {
        let o = document.createElement("option");
        o.innerHTML = Collection["Name"];
        if (Collection["SaleStatus"] != "Auction") {
            document.querySelector("#Auction-Collection-dropdown").appendChild(o);
        }
    });

    document.querySelector('#earnings').innerHTML += earningsData;
    //load tables
    var n = new ListNFTs();
    var c = new ListCollections();
    var a = new ListAuctionNFT();
    var ac = new ListAuctionCollection();

    n.RetrieveNFTs();
    c.RetrieveCollections();
    a.RetrieveAuctions();
    ac.RetrieveAuctionsCollection();

    var ts = new ListTransactionsSender;
    var tr = new ListTransactionsReceiver;
    ts.RetrieveTransactionsSender();
    tr.RetrieveTransactionsReceiver();


    //cancel NFT auction
    const cancelAuction = function (event) {
        event.preventDefault();
        let IdNFT = $('#tblAuctionNFT').DataTable().row('.selected').data().NFT;

        fetch(new ControlActions().URL_API + 'nft/updateStatus/' + IdNFT, {
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

                    fetch(new ControlActions().URL_API + 'useraction/post',
                        {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(userActionData)
                        }
                    )

                    Swal.fire({
                        icon: 'success',
                        title: 'The selected auction has been cancelled.',
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
        fetch(new ControlActions().URL_API + 'collection/UpdateStatus/' + IdCollection, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },

        })
            .then(response => response.json())
            .then(dataCancelCollection => {
                if (dataCancelCollection.Status == 200) {

                    fetch(new ControlActions().URL_API + 'Nft/RetrieveAll', {
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
                                    NFTUpdated.Status = "On Sale";

                                    fetch(new ControlActions().URL_API + 'Nft/Update', {
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

                    fetch(new ControlActions().URL_API + 'useraction/post',
                        {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(userActionData)
                        }
                    )

                    Swal.fire({
                        icon: 'success',
                        title: 'The selected auction has been cancelled.',
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


    //function for update notification preferred method
    const updateNotification = function (event) {
        event.preventDefault();
        let userNotification = {
            Id: userId,
            PreferredMethod: $("#txtPreferredMethodd").val(),
        };


        fetch(new ControlActions().URL_API + 'User/EditNotificationMethod', {
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
                        ...userData,
                        PreferredMethod: userNotification.PreferredMethod
                    }
                    localStorage.setItem("UserProfile", JSON.stringify(userDataN));

                    Swal.fire({
                        icon: 'success',
                        title: 'Your notification method has been changed.',
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
    let response = await fetch(new ControlActions().URL_API + 'User/Get/' + userId, {
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
                    title: 'There has been an error.',
                    footer: '<p>Current notification method not found.</p>',
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
};








