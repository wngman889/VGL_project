// Combination Carousel
const sections = document.querySelectorAll("section");
const maincontent = document.getElementById("main-content");
sections.forEach((section,index) =>{

  if(index === 1){
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

