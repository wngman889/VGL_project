import axios from "axios";

let gamesData = [];

async function fetchData() {
  const recommendedGamesEndpoint = "http://localhost:5077/api/Games/get-most-recommended-owned-games";

  try {
    const response = await axios.post(recommendedGamesEndpoint, {
      "steamId": localStorage.getItem("SteamId"),
      "count": 10
    });

    gamesData = response.data;

    populateCarousel();

  } catch (error) {
    console.error(error.message);
  }
}

function populateCarousel() {
  const carouselList = document.getElementById("top-games-carousel");
  const gameNumbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

  gamesData.forEach((game, index) => {
    const listItem = document.createElement("li");
    listItem.classList.add("card-info");

    const gameNumber = gameNumbers[index]

    listItem.innerHTML = `
      <div class="top-number">${gameNumber}.</div>
      <div class="card">
        <img class="card-game-img" src=https://cdn.cloudflare.steamstatic.com/steam/apps/${game.appId}/hero_capsule.jpg alt="Game Picture" />
        <div class="card-game-info">
          <div class="card-game-combinedinfo">
            <div class="card-game-title">${game.name}</div>
            <div class="card-game-studio">${game.developer}</div>
          </div>
          <div class="card-game-release">${game.releaseDate}</div>
        </div>
      </div>
    `;
    carouselList.appendChild(listItem);
    
    const cardImg = listItem.querySelector(".card-game-img");
    cardImg.onerror = () => {
      cardImg.src = `https://cdn.cloudflare.steamstatic.com/steam/apps/${game.appId}/header.jpg`;
      // Alternatively, you can set a local fallback image
      // gameImg.src = "../misc/game-not-available-gem.png";
    };
  });
}

fetchData();
