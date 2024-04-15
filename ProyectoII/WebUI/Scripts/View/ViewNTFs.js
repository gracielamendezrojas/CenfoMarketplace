$(document).ready(async function () {
    const userData = JSON.parse(localStorage.getItem("UserProfile"));

    const userId = userData['Id'];

    let url;
    if (JSON.parse(localStorage.getItem("UserRoles"))[0]["Id"] == 1) {
        url = new ControlActions().URL_API +'NFT/RetrieveAll'
    }
    else if (JSON.parse(localStorage.getItem("UserRoles"))[0]["Id"] == 2 || JSON.parse(localStorage.getItem("UserRoles"))[0]["Id"] == 3) {
        url = new ControlActions().URL_API +"NFT/RetrieveByUser?Id="+userId
    }


    let catData
    await fetch(url)
        .then(response => response.json()).then(json => catData = json.Data);
    catData.forEach(nft => {
        document.getElementById("contenedor").innerHTML += `
<div  >

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
        <p>Collection ${nft.Collection}</p>
    </div>
</div>
</div>`

    })  
});

