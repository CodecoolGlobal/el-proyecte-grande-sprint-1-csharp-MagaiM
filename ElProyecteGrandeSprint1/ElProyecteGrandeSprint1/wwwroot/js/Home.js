document.querySelector("body > header > nav > div > a").innerHTML = "nope";
function fetchResource() {
    window.fetch(`Home/GetApi?page=1`).then((response) => {
        response.json().then((data) => {
            console.log(data);
            for (var element in data) {
                console.log(element);
                let jumbotronBody = jumbotronBodyBuilder(data[element].title, data[element].user, data[element].time_ago, data[element].url);
                document.querySelector("#containerbody").insertAdjacentHTML('beforeend', jumbotronBody);
            }
        });
    });
}

function jumbotronBodyBuilder(Title, Author, TimeAgo, url) {
    if (Author == null)
        Author = "Unknown";
    let jumbotronBody = `
        <div class="jumbotron">
            <div>
                Title : <a href="${url}">${Title}</a>
                </div>
            <div>
                Author : ${Author}
                </div>
            <div>
                TimeAgo : ${TimeAgo}
                </div>
        </div>
    `;
    return jumbotronBody;
}

function populateMainMenu() {
    fetchResource();
} populateMainMenu();