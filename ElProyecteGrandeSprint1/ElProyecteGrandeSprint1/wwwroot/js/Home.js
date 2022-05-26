let slideIndex = 0;

async  function fetchAndInsertFastDeals() {
    await  window.fetch(`Home/GetApi?pageSize=10`).then((response) => {
        console.log(response)
        response.json().then((data) => {
            console.log(data);
            for (var element in data) {
                console.log(element);
                let jumbotronBody = slidesBodyBuilder(data[element].Image, data[element].Title, data[element].StoreName, data[element].DealRating, data[element].SteamRatingPercent, data[element].SalePrice, data[element].NormalPrice, element);
                document.querySelector("#slideshow-container").insertAdjacentHTML('beforeend', jumbotronBody);
            }
            showSlides();
        });
    });
}



async function fetchAndInsertFreeGames() {
    await window.fetch(`Home/RecentFreeToPlayGames`).then((response) => {
        console.log(response);
        response.json().then((data) => {
            console.log(data);
            for (var element in data) {
                console.log(element);
                let jumbotronBody = jumbotronBodyBuilder(data[element].Image, data[element].Title, data[element].StoreName, data[element].DealRating, data[element].SteamRatingPercent, data[element].SalePrice, data[element].NormalPrice, element);
                document.querySelector("#containerbody").insertAdjacentHTML('beforeend', jumbotronBody);
            }
        });
    });
}
function slidesBodyBuilder(Image, Title, StoreName, DealRating, SteamRatingPercent, SalePrice, NormalPrice, element) {
    //let jumbotronBody = `
    //            <div class="col-lg-3 col-lg-3" style="display: inline-block; max-width: 350px; height: 350px mySlides fade">
    //        <div class="card">
    //            <div class="numbertext">1 / 3</div>
    //            <img src="${Image}" style="width:100%">
    //            <div class="Title">Caption Text</div>
    //            <div class="card-body inner-card">
    //                <h5 class="card-title text-center">${Title}</h5>
    //                <p class="card-text">Store name: ${StoreName}</p>
    //                <p class="card-text">Deal rating: ${DealRating}</p>
    //                <p class="card-text">Steam rating: ${SteamRatingPercent} %</p>
    //                <p class="card-text text-center"><strong>Sale price: $ ${String(SalePrice)}</strong></p>
    //                <p class="card-text text-center"><strong>Normal price: $ ${String(NormalPrice)}</strong></p>
    //            </div>
    //        </div>
    //        <br/>
    //    </div>
    //`;

    let body = `
    <div class="mySlides fade" style="display: none;">
        <img clas="class="product-image" src="${Image}" style="width:100% max-height:1000px">
        <p class="card-text"><strong>Store name: ${StoreName} <br> Sale price: $${String(SalePrice)} <br> Normal price: $ ${String(NormalPrice)}</strong></p>
        </div>`;
    return body;
}

function jumbotronBodyBuilder(Image, Title, StoreName, DealRating, SteamRatingPercent, SalePrice, NormalPrice, element) {
    //let jumbotronBody = `
    //            <div class="col-lg-3 col-lg-3" style="display: inline-block; max-width: 350px; height: 350px mySlides fade">
    //        <div class="card">
    //            <div class="numbertext">1 / 3</div>
    //            <img src="${Image}" style="width:100%">
    //            <div class="Title">Caption Text</div>
    //            <div class="card-body inner-card">
    //                <h5 class="card-title text-center">${Title}</h5>
    //                <p class="card-text">Store name: ${StoreName}</p>
    //                <p class="card-text">Deal rating: ${DealRating}</p>
    //                <p class="card-text">Steam rating: ${SteamRatingPercent} %</p>
    //                <p class="card-text text-center"><strong>Sale price: $ ${String(SalePrice)}</strong></p>
    //                <p class="card-text text-center"><strong>Normal price: $ ${String(NormalPrice)}</strong></p>
    //            </div>
    //        </div>
    //        <br/>
    //    </div>
    //`;

    let body = `
    <div class="fade" style="display: none;">
        <img clas="class="product-image" src="${Image}" style="width:100% max-height:1000px">
        <p class="card-text"><strong>Store name: ${StoreName} <br> Sale price: $${String(SalePrice)} <br> Normal price: $ ${String(NormalPrice)}</strong></p>
        </div>`;
    return body;
}

function populateMainMenu() {
    fetchAndInsertFastDeals();
    fetchAndInsertFreeGames();
} populateMainMenu();

function showSlides() {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("dot");
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1 }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
    setTimeout(showSlides, 5000); // Change image every 2 seconds
}