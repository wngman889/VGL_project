import axios from "axios";

let gamesData = [];
async function fetchData() {
  const randomGemsEndpoint = "http://localhost:5077/api/Games/get-random-gems-owned-games";

  try {
    const response = await axios.post(randomGemsEndpoint, {
      "steamId": localStorage.getItem("SteamId"),
      "count": 8
    });

    gamesData = response.data;
    generateGameCards(gamesData);
  } catch (error) {
    console.error(error.message);
  }
}

function generateGameCards(data) {
  const carousel = document.getElementById("spring-game-carousel");
  
  data.forEach(game => {
    const card = document.createElement("li");
    card.className = "card";

    const gameImg = document.createElement("img");
    gameImg.className = "card-game-img";
    gameImg.src = `https://cdn.cloudflare.steamstatic.com/steam/apps/${game.appId}/hero_capsule.jpg`;
    gameImg.alt = game.name;

    gameImg.onerror = () => {
      gameImg.src = "../misc/game-not-available-gem.png";
    };

    const gameInfo = document.createElement("div");
    gameInfo.className = "card-game-info";

    const combinedInfo = document.createElement("div");
    combinedInfo.className = "card-game-combinedinfo";

    const gameTitle = document.createElement("div");
    gameTitle.className = "card-game-title";
    gameTitle.textContent = game.name;

    const gameStudio = document.createElement("div");
    gameStudio.className = "card-game-studio";
    gameStudio.textContent = game.developer;

    combinedInfo.appendChild(gameTitle);
    combinedInfo.appendChild(gameStudio);

    gameInfo.appendChild(combinedInfo);

    const gameRelease = document.createElement("div");
    gameRelease.className = "card-game-release";
    gameRelease.textContent = game.releaseDate;

    gameInfo.appendChild(gameRelease);

    card.appendChild(gameImg);
    card.appendChild(gameInfo);

    carousel.appendChild(card);
  });
}
fetchData();
