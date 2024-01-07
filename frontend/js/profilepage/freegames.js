import axios from "axios";

let gamesData = [];

async function fetchData() {
  const freeGamesEndpoint = "http://localhost:5077/api/Games/get-random-free-owned-games";

  try {
    const response = await axios.post(freeGamesEndpoint, {
      "steamId": localStorage.getItem("SteamId"),
      "count": 8
    });

    gamesData = response.data;

  } catch (error) {
    console.error(error.message);
  }
}

document.addEventListener('DOMContentLoaded', async function () {
  await fetchData();

  // Function to create a game card
  function createGameCard(game) {
    const li = document.createElement('li');
    li.classList.add('card');

    // Game Img
    const img = document.createElement('img');
    img.classList.add('card-game-img');
    img.src = `https://cdn.cloudflare.steamstatic.com/steam/apps/${game.appId}/header.jpg`
    img.alt = game.name;
    li.appendChild(img);

    // Game Info
    const infoDiv = document.createElement('div');
    infoDiv.classList.add('card-game-info');
    li.appendChild(infoDiv);

    // Game Title
    const titleDiv = document.createElement('div');
    titleDiv.classList.add('card-game-title');
    titleDiv.textContent = game.name;
    infoDiv.appendChild(titleDiv);

    // Game Review Info
    const combinedInfoDiv = document.createElement('div');
    combinedInfoDiv.classList.add('card-game-combinedinfo');
    infoDiv.appendChild(combinedInfoDiv);

    // Game Reviewer
    const reviewerDiv = document.createElement('div');
    reviewerDiv.classList.add('card-game-reviewer');
    reviewerDiv.textContent = game.developer;
    combinedInfoDiv.appendChild(reviewerDiv);

    // Game Review
    const reviewDiv = document.createElement('div');
    reviewDiv.classList.add('card-game-review');
    reviewDiv.textContent = game.genre;
    combinedInfoDiv.appendChild(reviewDiv);

    return li;
  }

  // Function to populate the reviews carousel
  function populateReviewsCarousel() {
    const reviewsCarousel = document.querySelector('.reviews-carousel');

    gamesData.forEach(game => {
      const gameCard = createGameCard(game);
      reviewsCarousel.appendChild(gameCard);
    });
  }

  // Populate the reviews carousel on page load
  populateReviewsCarousel();
});