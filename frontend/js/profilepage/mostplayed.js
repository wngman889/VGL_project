const gamesData = [
  {
    imgSrc: "../public/most-played/game1.jpg",
    title: "Hogwarts Legacy",
    developer: "Avalanche Software"
  },
  {
    imgSrc: "../public/most-played/game2.jpg",
    title: "NieR: Automata",
    developer: "Square Enix"
  },
  {
    imgSrc: "../public/most-played/game3.jpg",
    title: "Danganronpa V3",
    developer: "Spike Chunsoft"
  },
  {
    imgSrc: "../public/most-played/game4.jpg",
    title: "The Legend of Zelda: Breath of the Wild",
    developer: "Nintendo"
  },
  {
    imgSrc: "../public/most-played/game5.jpg",
    title: "Fire Emblem: Three Houses",
    developer: "Nintendo"
  }
];

const gameSlider = document.getElementById('gameSlider');

  gamesData.forEach((game, index) => {
    const slide = document.createElement('div');
    slide.className = `slide ${index === 0 ? 'first' : ''}`;
    slide.innerHTML = `
      <img class="slide-img" src="${game.imgSrc}" alt="${game.alt}">
      <div class="slide-overlay">
        <div class="slide-info">
          <div class="slide-title">${game.title}</div>
          <div class="slide-dev">${game.developer}</div>
        </div>
      </div>
    `;
    gameSlider.appendChild(slide);
  });

// Modal Creation
const section = document.getElementById("section-most-played");
const cards = section.getElementsByClassName("slide");
const modalcontainer = document.getElementById("modal-container");
const modal = document.getElementById("game-modal");
// Exit Icon Event
modal.getElementsByClassName("modal-exit")[0].addEventListener("click", () =>{
  document.getElementsByTagName("body")[0].classList.remove("overflow-hidden");
  modalcontainer.style.display = "none";
})

// Card Cycling
for (let index = 0; index < cards.length; index++) {
  cards[index].addEventListener("click",() =>{
    showModal(cards[index], index)
    // modal.style.top = window.pageYOffset + 30 + 'px';
    modalcontainer.style.top = window.pageYOffset + 'px';
  })
}

// Most Played Games Interval
let counter = 1;
setInterval(function(){
  document.getElementById('radio' + counter).checked = true;
  counter++;
  if(counter > 5){
    counter = 1;
  }
}, 5000);

let gameinfo;
// JSON info fetching
fetch('../json/gameinfo.json')
  .then(response => response.json())
  .then(data => {
    gameinfo = data;
  })

// Modal Changing
function showModal(card, index) {
  let gamedata = gameinfo[index]

  // Img Change
  modal.getElementsByClassName("modal-img")[0].src = gamedata.gameImg;

  // Entry Info Container
  let maincontainer = modal.getElementsByClassName("modal-main-container")[0]

  // Name Change
  maincontainer.getElementsByClassName("modal-name")[0].innerHTML = gamedata.gameName;

  // Release Change
  maincontainer.getElementsByClassName("modal-release")[0].innerHTML = gamedata.releaseDate;

  // Rating Change
  maincontainer.getElementsByClassName("modal-rating")[0].innerHTML = gamedata.gameRating;

  // Genre Changing
  maincontainer.getElementsByClassName("game-genre")[0].innerHTML = gamedata.gameGenre;

  // Desc Info Container
  modal.getElementsByClassName("modal-desc")[0].innerHTML = gamedata.gameDesc;

  // Reviews Container
  maincontainer = modal.getElementsByClassName("modal-reviews-container")[0].getElementsByClassName("modal-reviews")[0].getElementsByClassName("modal-review");

  // Review Changing
  for (let index = 0; index < maincontainer.length; index++) {
    console.log(gamedata.reviews[index])
    maincontainer[index].getElementsByClassName("modal-reviewer-comment")[0].innerHTML = gamedata.reviews[index];
  }


  // Final Code
  document.getElementsByTagName("body")[0].classList.add("overflow-hidden");
  modalcontainer.style.display = "flex";
}