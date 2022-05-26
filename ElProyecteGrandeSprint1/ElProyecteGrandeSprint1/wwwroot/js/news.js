async function fetchResource(game) {
    return await window.fetch(`News/GetGNews?game=${game}`)
        .then(async (response) => {
            return response.json();
        });
}

function addEventListeners() {
    const otherNews = document.querySelector(".other-news");
    for (let child of otherNews.children) {
        child.addEventListener("click",
            e => {
                showGameNews(child, e);
            });
    }
}

function addEventListenerToButton() {
    const latestNewsButton = document.querySelector(".top-news-button");

    latestNewsButton.addEventListener("click", e => {showLatestNews()});
}

function showLatestNews() {
    const gameNews = document.querySelectorAll(".game-news");
    if (gameNews.length > 0) {
        for (let item of gameNews) {
            item.parentElement.removeChild(item);
        }
    }

    const latestNews = document.querySelector(".recent-news");
    latestNews.removeAttribute("hidden");
}

function gameNewsHtmlBuilder(gameNews) {
    let result = `<div class="game-news">
                    <button class="top-news-button" type="button" style="float: right; margin-bottom: 5px;">
                        <span class="">Latest News</span>
                    </button>`;
    for (let item of gameNews) {
        result += `<div class="news" style="width: 100%">
                        <div class="col-lg-3 col-lg-3" style="display: inline-block; width: 100%">
                            <div class="card" style="flex-direction: row; background-color: #3A373F">
                                <img class="product-image" src="${item.Image}" style="max-width: 50%">
                                <div class="card-body inner-card" style="width: 50%">
                                    <a href="${item.Link}" style="text-decoration: none"><h5 class="card-title">${item.Title}</h5></a>
                                    <p class="card-text">${item.Description}</p>
                                    <p class="card-text">${item.Date}</p>
                                    <a href="${item.Source.Url}" style="text-decoration: none"><p class="card-text">${item.Source.Name}</p></a>
                                </div>
                            </div>
                        <br/>
                    </div>
                </div>
`;
    }
    result += "</div>";
    return result;
}

async function showGameNews(game, event) {

    const latestNews = document.querySelector(".recent-news");
    const gameNews = document.querySelectorAll(".game-news");
    const allNews = document.querySelector(".news-container");

    latestNews.setAttribute("hidden", true);

    if (gameNews.length !== 0) {
        for (let item of gameNews) {
            item.parentElement.removeChild(item);
        }
    }

    const data = await fetchResource(game.textContent);
    const gameNewsBody = gameNewsHtmlBuilder(data);
    allNews.insertAdjacentHTML("afterbegin", gameNewsBody);

    addEventListenerToButton();
}

addEventListeners();