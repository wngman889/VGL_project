// Add an event listener to the navbar
const navbar = document.querySelector('.navbar');

const activePage = window.location.pathname
const navli = [...document.querySelectorAll("nav li")];

// Adding Games List Icon
navli.push(document.querySelector(".profile-header").querySelector(".game-list"));


// Check for an active page in the sidenav and game list icon
navli.forEach(li => {
  li_a = li.querySelector("a")
  if(li_a.pathname === activePage){
    li.classList.add("selected-icon");
    handleNavbar(navbar);
    return;
  }
})

function handleNavbar(Navbar) {
  // Find the li element with the "selected-icon" class
  const selectedItem = Navbar.getElementsByClassName("selected-icon")[0];

  if (selectedItem) {
    // Get the previous and next sibling elements
    const previousItem = selectedItem.previousElementSibling;
    const nextItem = selectedItem.nextElementSibling;

    // Apply CSS classes to the previous and next siblings
    if (previousItem) {
      previousItem.classList.add('previous-icon');
    }

    if (nextItem) {
      nextItem.classList.add('next-icon');
    }
  }
}