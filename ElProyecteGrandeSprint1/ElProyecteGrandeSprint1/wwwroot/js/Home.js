let slideIndex = 0;


async  function fetchAndInsertFastDeals() {
    await  window.fetch(`Home/GetApi?pageSize=20`).then((response) => {
        console.log(response)
        response.json().then((data) => {
            console.log(data);
            for (var element in data) {
                console.log(element);
                let jumbotronBody = slidesBodyBuilder(data[element].Image, data[element].StoreName, data[element].SalePrice, data[element].NormalPrice, data[element].Title);
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
            data = data.slice(0, 15);
            for (var element in data) {
                console.log(element);
                let jumbotronBody = jumbotronBodyBuilder(data[element].Thumbnail, data[element].Title, data[element].ShortDescription, data[element].Genre, data[element].Platform, data[element].GameUrl, data[element].ReleaseDate);
                document.querySelector("#containerbody").insertAdjacentHTML('beforeend', jumbotronBody);
                collapseCards();
            }
        });
    });
}


function collapseCards() {
    let cards = document.getElementsByClassName("mySlides");
    let cardTitleList = [];
    for (let i = 1; i < cards.length; i++) {
        let cardTitle = document.querySelector(`#slideshow-container > div:nth-child(${i}) > h5`).innerHTML;
        let card = document.querySelector(`#slideshow-container > div:nth-child(${i})`);
        if (cardTitleList.includes(cardTitle)) {
            card.remove();
            i--;
        } else {
            cardTitleList.push(cardTitle);
        }

    }
}


function slidesBodyBuilder(Image, StoreName, SalePrice, NormalPrice, Title) {
    if (StoreName === "N2Game") {
        StoreName = "2Game";
    }
    let body = `
    <div class="mySlides fade" style="display: none;">
        <img class="slide-show-image" src="${Image}">
        <h5 class="card-title text-center">${Title}</h5>
        <p class="card-text"><strong> Store name: ${StoreName} <br> Sale price: $${String(SalePrice)} <br> Normal price: $ ${String(NormalPrice)}</strong></p>
        </div>`;
    return body;
}


function jumbotronBodyBuilder(Thumbnail, Title, ShortDescription, Genre, Platform, GameUrl, ReleaseDate) {
    let jumbotronBody = `
                <div class="col-md-auto col-md-auto bg-dark" style="display: inline-block; max-width: 300px; height: auto mySlides fade">
            <div class="card bg-dark" style="box-shadow: 1px 2px 3px 4px rgba(112,128,144,0.4); margin:10px 10px 10px 20px">
                <a href="${GameUrl}"><img src="${Thumbnail}" style="width:100%"> </a>
                <div class="card-body inner-card" style="background-color: #3A373F">
                    <h5 class="card-title text-center">${Title}</h5>
                    <p class="card-text">Short Description: ${ShortDescription}</p>
                    <p class="card-text">Genre: ${Genre}</p>
                    <p class="card-text">Platform: ${Platform}</p>
                    <p class="card-text text-center"><strong>ReleaseDate: ${ReleaseDate}</strong></p>
                </div>
            </div>
            <br/>
        </div>`;
    return jumbotronBody;
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
    //for (i = 0; i < dots.length; i++) {
    //    dots[i].className = dots[i].className.replace(" active", "");
    //}
    slides[slideIndex - 1].style.display = "block";
/*    dots[slideIndex - 1].className += " active";*/
    setTimeout(showSlides, 5000); // Change image every 2 seconds
}