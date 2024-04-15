
const NFT = JSON.parse(localStorage.getItem("SelectedAuction"));
const NFTName = NFT.NFTName;
const NFTStartDate = NFT.FormattedCreationDate;
const NFTEndDate = NFT.FormattedClosingDate;
const NFTPrice = NFT.AuctionPrice;
const NFTAcquisitionId = NFT.AcquisitionId;

function validateForm() {
    var valid = true;

    if ($("#txtBasePrice").val() == null || $("#txtBasePrice").val() == "" || $("#txtBasePrice").val() <= 0 ) {
        valid = false;
        Swal.fire({
            icon: 'error',
            title: 'Invalid form',
            footer: '<p>You must type a base price to change the current auction. Must be higher than zero. </p>',
            buttonsStyling: false,
            customClass: {
                confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
            }
        });

        return valid; 
    }

    return valid;
}

$(document).ready(async function () {
    console.log(NFT);

    $("#name").text(NFTName);
    $("#startDate").text(NFTStartDate);
    $("#endDate").text(NFTEndDate);
    $("#basePrice").text(NFTPrice);

    //change base Price 
    const updateBasePrice = function (event) {

        let updatePrice = {
            Id: NFTAcquisitionId,
            Price: ($("#txtBasePrice").val())
        };
        if (validateForm()) {
            fetch(new ControlActions().URL_API +'Acquisition/UpdatePrice', {
                method: 'PUT',
                body: JSON.stringify(updatePrice),
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },

            })
                .then(response => response.json())
                .then(dataUpdate => {
                    console.log(dataUpdate);
                    console.log(dataUpdate.Status);
                    if (dataUpdate.Status == 200) {
                        const userActionData = {
                            User: NFT.Creator,
                            Type: 'Auction base price changed'
                        }

                        fetch(new ControlActions().URL_API +'useraction/post',
                            {
                                method: 'POST',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify(userActionData)
                            }
                        )
                            .then(response => response.json())
                            .then(dataUserAction => {
                                console.log(dataUserAction);
                            });

                        Swal.fire({
                            icon: 'success',
                            title: 'Success',
                            footer: '<p>The base price has been updated</p>',
                            buttonsStyling: false,
                            customClass: {
                                confirmButton: 'btn btn-Success btn-primary btn-sm' //insert class here
                            }
                        });
                        document.querySelector('.btn-Success').addEventListener('click', () => {
                            $("#basePrice").text(updatePrice.Price);
                            $("#txtBasePrice").val('');
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

      
    $("#auction-change-price").click(updateBasePrice);

}); 