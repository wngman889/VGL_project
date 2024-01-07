import axios from "axios";

const gameSlider = document.getElementById('gameSlider');
const modalcontainer = document.getElementById("modal-container");
const modal = document.getElementById("game-modal");

let gamesData = [];
async function fetchData() {
  const mostplayedEndpoint = "http://localhost:5077/api/Games/get-most-played-owned-games";

  try {
    const response = await axios.post(mostplayedEndpoint, {
      "steamId": localStorage.getItem("SteamId"),
      "count": 5
    });

    gamesData = response.data;
    renderGameSlider();
  } catch (error) {
    console.error(error.message);
  }
}

// Render game slider based on the fetched data
function renderGameSlider() {
  gamesData.forEach((game, index) => {
    const slide = document.createElement('div');
    slide.className = `slide ${index === 0 ? 'first' : ''}`;
    slide.innerHTML = `
      <img class="slide-img" src="https://cdn.cloudflare.steamstatic.com/steam/apps/${game.appId}/capsule_616x353.jpg" alt="${game.name}">
      <div class="slide-overlay">
        <div class="slide-info">
          <div class="slide-title">${game.name}</div>
          <div class="slide-dev">${game.developer}</div>
        </div>
      </div>
    `;
    slide.addEventListener("click", () => showModal(game, index));
    gameSlider.appendChild(slide);
  });
}

// Show modal with game details
function showModal(game) {
  modal.getElementsByClassName("modal-img")[0].src = `https://cdn.cloudflare.steamstatic.com/steam/apps/${game.appId}/capsule_616x353.jpg`;

  let maincontainer = modal.getElementsByClassName("modal-main-container")[0]
  maincontainer.getElementsByClassName("modal-name")[0].innerHTML = game.name;
  maincontainer.getElementsByClassName("modal-release")[0].innerHTML = game.releaseDate;
  maincontainer.getElementsByClassName("modal-rating")[0].innerHTML = game.developer;
  maincontainer.getElementsByClassName("game-genre")[0].innerHTML = game.genre;
  modal.getElementsByClassName("modal-desc")[0].innerHTML = game.description;

  document.getElementsByTagName("body")[0].classList.add("overflow-hidden");
  modalcontainer.style.display = "flex";
}

// Most Played Games Interval
let counter = 2;
setInterval(function(){
  document.getElementById('radio' + counter).checked = true;
  counter++;
  if(counter > 5){
    counter = 1;
  }
}, 5000);


// Event listener for modal exit
modal.getElementsByClassName("modal-exit")[0].addEventListener("click", () => {
  document.getElementsByTagName("body")[0].classList.remove("overflow-hidden");
  modalcontainer.style.display = "none";
});

// Fetch data on page load
fetchData();