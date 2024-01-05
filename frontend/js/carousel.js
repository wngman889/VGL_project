// Combination Carousel
const sections = document.querySelectorAll("section");
let modalcontainer = document.getElementsByClassName("modal-container");
modalcontainer = modalcontainer[0]
const modal = modalcontainer.querySelector("#game-modal");
const maincontent = document.getElementById("main-content");
sections.forEach((section,index) =>{

  // Skipping the first Carousel for Most Played
  if(index === 0){
    const cards = section.getElementsByClassName("slide");
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

    return;
  }
  const carousel = section.querySelector("ul");
  const cards = carousel.querySelectorAll(".card");
  const arrowright = section.querySelector(".arrow-button--right")
  const arrowleft = section.querySelector(".arrow-button--left")
  let carouselScrollPos = carousel.scrollLeft;

  // Define the URL of the icon image
  const iconAddImageUrl = 'imgs/misc/icon-game-add.png';
  const iconRemoveImageUrl = 'imgs/misc/icon-game-remove.png';

  // Arrow Scrolling 
  arrowright.addEventListener("click", () => {
    carouselScrollPos = move(carousel,carousel.clientWidth);
    checkScrollLimits(carousel, arrowleft, arrowright,carouselScrollPos);
  });
  arrowleft.addEventListener("click", () => {
    carouselScrollPos = move(carousel,-carousel.clientWidth);
    checkScrollLimits(carousel, arrowleft, arrowright,carouselScrollPos);
  });

  // Hover Animation for each card
  cards.forEach(card =>{
    card.addEventListener("mouseover", () => {
      card.style.transform = "scale(1.1)";
      card.style.transition = "0.3s ease";
      addMask(card);
      iconImage.style.opacity = '1';
    });
  
    card.addEventListener("mouseleave", () => {
      card.style.transform = "scale(1)";
      removeMask(card);
      iconImage.style.opacity = '0';
    });

      // Create a mask element for each card
      const maskElement = document.createElement('div');
      maskElement.className = 'card-mask';

      // Append the mask element to the card
      card.appendChild(maskElement);

      // Create the icon image element
      const iconImage = document.createElement('img');
      iconImage.src = iconAddImageUrl;
      iconImage.classList.add('card-icon');
  
      // Append the icon image element to the card
      card.appendChild(iconImage);

      // Add click event handler for the icon image
      iconImage.addEventListener('click', () => {
      const popup = document.createElement('div');

      // Extract the relative image path
      const splitParts = iconImage.src.split('/'); // Split the URL by '/'
      const relativeImagePath = splitParts.slice(-3).join('/'); // Extract the last 3 parts and join them back with '/'

       // If the item hasn't been added yet
      if(relativeImagePath === iconAddImageUrl){
        popup.textContent = 'Game added';
        popup.className = 'popup-added';
        iconImage.src = iconRemoveImageUrl;
      } // If the item has been added
      else{
        popup.textContent = 'Game removed';
        popup.className = 'popup-removed';
        iconImage.src = iconAddImageUrl;
      }

      // Append the popup element to the card
      card.appendChild(popup);

      // Remove the popup after a second
      setTimeout(() => {
        card.removeChild(popup);
      }, 1000);
  });
  })

})

let gameinfo;
// JSON info fetching
fetch('json/gameinfo.json')
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

// Function to add the mask effect
function addMask(card) {
  const maskElement = card.querySelector('.card-mask');
  maskElement.style.opacity = '1';
}

// Function to remove the mask effect
function removeMask(card) {
  const maskElement = card.querySelector('.card-mask');
  maskElement.style.opacity = '0';
}

// Carousel Scrolling
function move(carousel, amount) {
  carousel.scrollLeft += amount;
  return carousel.scrollLeft += amount;
}

// Scroll Checking
function checkScrollLimits(carousel, leftArrow, rightArrow, scrollLeftPos) {
  const maxScrollLeft = carousel.scrollWidth - carousel.clientWidth;

  if (scrollLeftPos <= 0) {
    leftArrow.style.display = "none";
  } else{
    leftArrow.style.display = "flex";
  }
  if (scrollLeftPos < maxScrollLeft) {
    rightArrow.style.display = "flex";
  } else {
    rightArrow.style.display = "none";
  }
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


