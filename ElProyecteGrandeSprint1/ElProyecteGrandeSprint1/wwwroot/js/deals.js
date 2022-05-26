function fetchResource() {
    window.fetch(`Deals/GetApi?pageSize=60`).then((response) => {
        console.log(response)
        response.json().then((data) => {
            console.log(data);
            for (var element in data) {
                console.log(element);
                let jumbotronBody = jumbotronBodyBuilder(data[element].Image, data[element].Title, data[element].StoreName, data[element].DealRating, data[element].SteamRatingPercent, data[element].SalePrice, data[element].NormalPrice);
                document.querySelector("#containerbody").insertAdjacentHTML('beforeend', jumbotronBody);
                collapseCards();
            }
        });
    });
}

function jumbotronBodyBuilder(Image, Title, StoreName, DealRating, SteamRatingPercent, SalePrice, NormalPrice) {
    if (StoreName === "N2Game") {
        StoreName = "2Game";
    }
    let jumbotronBody = `
                <div class="col-md-auto col-md-auto bg-dark" style="display: inline-block; width: 300px; height: auto">
                <div class="card" style="box-shadow: 1px 2px 3px 4px rgba(112,128,144,0.4); margin:10px 10px 10px 20px">
                <img class="product-image" src="${Image}" style="max-width: auto; max-height: auto;">
                <div class="card-body inner-card" style="background-color: #3A373F">
                <h5 class="card-title text-center">${Title}</h5>
                    <p class="card-text">Store name: ${StoreName}</p>
                    <p class="card-text">Deal rating: ${DealRating}</p>
                    <p class="card-text text-center"><strong>Sale price: $ ${String(SalePrice)}</strong></p>
                    <p class="card-text text-center"><strong>Normal price: $ ${String(NormalPrice)}</strong></p>
                </div>
            </div>
            <br/>
        </div>
    `;
    return jumbotronBody;
}

function collapseCards() {
    let cards = document.getElementsByClassName("card");
    let cardTitleList = [];
    for (let i = 1; i < cards.length; i++) {
        let cardTitle = document.querySelector(`#containerbody > div:nth-child(${i}) > div > div > h5`).innerHTML;
        let card = document.querySelector(`#containerbody > div:nth-child(${i}) > div`);
        if (cardTitleList.includes(cardTitle)){
            card.parentElement.remove();
            i--;
        }else {
            cardTitleList.push(cardTitle);
        }

    } 
}


function populateMainMenu() {
    fetchResource();

} populateMainMenu();