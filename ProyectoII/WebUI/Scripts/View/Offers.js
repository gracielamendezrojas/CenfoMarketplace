
let balanceData;
let colData;
let collectionData;
var today = new Date();
var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
var dateTime = date + ' ' + time;




const auctionInfo = JSON.parse(localStorage.getItem("SelectedAuction"));
const acquisitionId = auctionInfo.AcquisitionId;


async function setautobid() {

    let autolocal = {
        "AutoBidId": 0,
        "AquisitionId": JSON.parse(localStorage.getItem("SelectedAuction")).AcquisitionId,
        "Amount": 0,
        "MaxAmount": $('#txtPrice').val(),
        "Increment": $('#txtincrement').val(),
        "UserId": JSON.parse(localStorage.getItem("UserProfile")).Id
    }

    $('#txtincrement').prop("disabled", true);
    $('#txtPrice').prop("disabled", true);

    let myautodata;


    let autorespose = await fetch(new ControlActions().URL_API +'Autobid/PostAutobid',
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(autolocal)
        }
    ).then(autorespose => autorespose.json()).then(json => { myautodata = json.Data; });
    console.log(myautodata);
}


async function delautobid() {

    let autolocal = {
        "AutoBidId": 0,
        "AquisitionId": JSON.parse(localStorage.getItem("SelectedAuction")).AcquisitionId,
        "Amount": 0,
        "MaxAmount": $('#txtPrice').val(),
        "Increment": $('#txtincrement').val(),
        "UserId": JSON.parse(localStorage.getItem("UserProfile")).Id
    }

    let myautodata;


    let autorespose = await fetch(new ControlActions().URL_API +'Autobid/DelMyAutobid',
        {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(autolocal)
        }
    ).then(autorespose => autorespose.json()).then(json => { myautodata = json.Data; });
    console.log(myautodata);
}


function ListOffers() {

    this.tbl = 'tblOffers';
    this.service = 'Bid';
    this.ctrlActions = new ControlActions();
    this.columns = "User,Amount,FormattedDate";

    this.RetrieveOffers = function () {
        this.ctrlActions.FillTable(this.service + "/RetrieveAllByAquistion/" + acquisitionId, this.tbl, false);
    }
}

class Btn1 {

    SetBid() {
        $("#place-auto-offer").toggle();
        $("#cancel-auto-offer").toggle();



        setautobid();
    }

        CancelBid() {
            $("#place-auto-offer").toggle();
            $("#cancel-auto-offer").toggle();

            delautobid();
            window.location.reload();


        }
   
        async DirectOffer() {
            let x = $("#txtdirectbit").val();
            let tipeauction = JSON.parse(localStorage.getItem("SelectedAuction")).type;

            console.log("!!")


            if (x < JSON.parse(localStorage.getItem("SelectedAuction")).AuctionPrice) {
                $("#txtdirectbit").addClass('is-invalid');
            } else if (x != '') {
                $("#txtdirectbit").removeClass('is-invalid');
                $("#sapn-current-price").text(x);

                let auctionOBJ = JSON.parse(localStorage.getItem("SelectedAuction"));

                auctionOBJ.AuctionPrice = x

                localStorage.setItem("SelectedAuction", JSON.stringify(auctionOBJ));

                let typeint;
                if (tipeauction == 'NFT') {
                    typeint = 1
                } else {
                    typeint = 2
                }

                let data = {
                    "Amount": x,
                    "Date": $.now(),
                    "Acquisition": JSON.parse(localStorage.getItem("SelectedAuction")).AcquisitionId,
                    "User": JSON.parse(localStorage.getItem("UserProfile")).Id,
                    "Type": typeint
                }

                let message;
                let responseMessage;
                let status;
                let response = await fetch(new ControlActions().URL_API + 'Bid/Post',
                    {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(data)
                    }
                ).then(response => response.json()).then(json => { status = json.Status; message = json.Message; responseMessage = json.Message });

                if (status == 500) {
                    Swal.fire({
                        icon: 'error',
                        title: 'There has been an error',
                        footer: '<p>Your offer has been rejected.</p>',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                        }
                    });
                } else if (status == 200) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Your bid has been saved',
                        buttonsStyling: false,
                        customClass: {
                            confirmButton: 'btn-primary btn-flat m-b-30 m-t-30 button-style btn btn-Success' //insert class here
                        }
                    });
                    document.querySelector('.btn-Success').addEventListener('click', () => {
                        window.location.reload();
                    });
                }

            } else {
                $("#txtdirectbit").addClass('is-invalid');
            }

        }

    

    
}







    $(document).ready(async function() {

        var offers = new ListOffers;
        offers.RetrieveOffers();

        selectedAuction = JSON.parse(localStorage.getItem("SelectedAuction"));
        let divTitle = document.getElementById("contenedortitle");
        let divgalery = document.getElementById("contenedor");
        $("#cancel-auto-offer").hide();

        let catData1
        await fetch(new ControlActions().URL_API +'Acquisition/Get?id=' + selectedAuction["AcquisitionId"])
            .then(response => response.json()).then(json => catData1 = json.Data);


        let catData
        await fetch(new ControlActions().URL_API + 'nft/RetrieveByCollectionAuction?collection=' + selectedAuction["Collection"])
            .then(response => response.json()).then(json => catData = json.Data);


        let mylastbid
        await fetch(new ControlActions().URL_API+'bid/RetrieveMyLastBid?arquisition=' + selectedAuction["AcquisitionId"] + '&user=' + JSON.parse(localStorage.getItem("UserProfile"))["Id"])
            .then(response => response.json()).then(json => mylastbid = json.Data);

        console.log(catData1)
        if (selectedAuction.type == 'NFT') {
            divTitle.innerHTML += `
        <div> <h3>My Last Bid : ${mylastbid.Amount}</h3> <div>
        <div>
        <h3>NFT Name: ${selectedAuction.NFTName}</h3>
        <h3>Closing Date: ${selectedAuction.FormattedClosingDate}</h3>
        <h3>Highest Price ($): ${catData1['Price']}</h3>
        </div>
        `;


            catData.forEach(nft => {
                if (nft.Id == auctionInfo.NFT) {
                    divgalery.innerHTML += `
                <div>
                    <div class="card mb-3 rounded-3 shadow-lg p-3 mb-5 bg-white" style="border: solid 1px grey">
                        <h3 class="card-header">${nft.Name}</h3>
                        <div class="card-body">
                        </div>
                        <div class="d-block user-select-none" width="100%" height="200" aria-label="Placeholder: Image cap" focusable="false" role="img" preserveAspectRatio="xMidYMid slice" viewBox="0 0 318 180" style="font-size:1.125rem;text-anchor:middle;  ">
                            <image src=${nft.Route} style="width:100%" />
                        </div>
                        <div class="card-body">
                            <p class="card-text">$${nft.Price}</p>
                        </div>
                    </div>
                </div>`
                }
            })
        }

        if (selectedAuction.type == 'Collection') {
            divTitle.innerHTML += `
        <div> <h3>My Last Bid : ${mylastbid.Amount}</h3> <div>
        <h3>Collection Name: ${selectedAuction.CollectionName}</h3>
        <h3>Closing Date: ${selectedAuction.FormattedClosingDate}</h3>
        <h3>Highest Price ($): ${catData1['Price']}</h3>
        `;


            catData.forEach(nft => {
                divgalery.innerHTML += `
            <div>
                <div class="card mb-3 rounded-3 shadow-lg p-3 mb-5 bg-white" style="border: solid 1px grey">
                    <h3 class="card-header">${nft.Name}</h3>
                    <div class="card-body">
                    </div>
                    <div class="d-block user-select-none" width="100%" height="200" aria-label="Placeholder: Image cap" focusable="false" role="img" preserveAspectRatio="xMidYMid slice" viewBox="0 0 318 180" style="font-size:1.125rem;text-anchor:middle;  ">
                        <image src=${nft.Route} style="width:100%" />
                    </div>
                    <div class="card-body">
                        <p class="card-text">$${nft.Price}</p>
                    </div>
                </div>
            </div>`
            })


        }

        let autolocal = {
            "AutoBidId": 0,
            "AquisitionId": JSON.parse(localStorage.getItem("SelectedAuction")).AcquisitionId,
            "Amount": 0,
            "MaxAmount": 0,
            "Increment": 0,
            "UserId": JSON.parse(localStorage.getItem("UserProfile")).Id
        }

        let myautodata;

        let autorespose = await fetch(new ControlActions().URL_API+'Autobid/GetMyAutobid',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(autolocal)
            }
        ).then(autorespose => autorespose.json()).then(json => { myautodata = json.Data; });

        if (myautodata.Increment > 0) {
            $('#txtincrement').val(myautodata.Increment).prop("disabled", true );
            $('#txtPrice').val(myautodata.MaxAmount).prop("disabled", true);
            $("#place-auto-offer").toggle();
            $("#cancel-auto-offer").toggle();

        }
    }

)
