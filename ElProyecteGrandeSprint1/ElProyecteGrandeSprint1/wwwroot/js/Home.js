function fetchResource() {
    window.fetch(`Home/GetApi?pageSize=20`).then((response) => {
        console.log(response)
        response.json().then((data) => {
            console.log(data);
            for (var element in data) {
                console.log(element);
                let jumbotronBody = jumbotronBodyBuilder(data[element].Image, data[element].Title, data[element].StoreName, data[element].DealRating, data[element].SteamRatingPercent, data[element].SalePrice, data[element].NormalPrice );
                document.querySelector("#containerbody").insertAdjacentHTML('beforeend', jumbotronBody);
            }
        });
    });
}

function jumbotronBodyBuilder(Image, Title, StoreName, DealRating, SteamRatingPercent, SalePrice, NormalPrice) {
    let jumbotronBody = `
                <div class="col-lg-3 col-lg-3" style="display: inline-block; max-width: 350px; height: 350px">
            <div class="card">
                <img class="product-image" src="${Image}">
                <div class="card-body inner-card">
                    <h5 class="card-title text-center">${Title}</h5>
                    <p class="card-text">Store name: ${StoreName}</p>
                    <p class="card-text">Deal rating: ${DealRating}</p>
                    <p class="card-text">Steam rating: ${SteamRatingPercent} %</p>
                    <p class="card-text text-center"><strong>Sale price: $ ${String(SalePrice)}</strong></p>
                    <p class="card-text text-center"><strong>Normal price: $ ${String(NormalPrice)}</strong></p>
                </div>
            </div>
            <br/>
        </div>
    `;
    return jumbotronBody;
}

function populateMainMenu() {
    fetchResource();
} populateMainMenu();