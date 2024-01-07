import axios from "axios";

let gamesData = [];

async function fetchData() {
  const randomnewsEndpoint = "http://localhost:5077/api/Games/get-news-for-random-games";

  try {
    const response = await axios.post(randomnewsEndpoint, {
      "steamId": localStorage.getItem("SteamId"),
      "maxCount": 6
    });

    gamesData = response.data;

    const latestSection = document.getElementById("latest-section");

    gamesData.forEach(game => {
      const card = generateArticleHTML(game)
      latestSection.appendChild(card);
    });
  } catch (error) {
    console.error(error.message);
  }
}

function generateArticleHTML(game) {
  const articleCard = document.createElement("div");
  articleCard.className = "article-card";

  articleCard.innerHTML = `
    <img class="article-card-img" src="https://cdn.cloudflare.steamstatic.com/steam/apps/${game.appId}/header.jpg" alt="Article Image" />
    <div class="article-info">
      <div class="article-row1">
        <div class="article-title">${game.title}</div>
      </div>
      <div class="article-desc">${game.contents}</div>
      <div class="article-details">
        <div class="article-author">${game.gameName}</div>
        <div class="article-time">${game.author}</div>
      </div>
    </div>
  `;
  return articleCard;
}

fetchData();