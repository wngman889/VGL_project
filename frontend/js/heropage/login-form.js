import axios from "axios";
// The Whole Login Container
const logincontainer = document.querySelector(".login-form-wrapper");
// Exit Icon
const exiticon = logincontainer.querySelector(".login-close");
// Input Form
const inputform = logincontainer.querySelector(".input-form");
// Unselected Form
const unselectedform = logincontainer.querySelector(".unselected-form");
// Unselected button
const changeformbutton = unselectedform.querySelector(".unselected-button");

let loginSelected = true;

const loginbuttons = [...document.getElementsByClassName("login")];
const singupbuttons = [...document.getElementsByClassName("sign-up")];

loginbuttons.forEach(loginbutton =>{
  loginbutton.addEventListener("click", () =>{
    document.getElementsByTagName("body")[0].classList.add("overflow-hidden");
    logincontainer.style.display = "flex";
    loginSelected = true;
    changeForm(loginSelected,inputform,unselectedform,logincontainer);
  }) 
})

singupbuttons.forEach(signupbutton =>{
    signupbutton.addEventListener("click", () =>{
      document.getElementsByTagName("body")[0].classList.add("overflow-hidden");
      logincontainer.style.display = "flex";
      loginSelected = false;
      changeForm(loginSelected,inputform,unselectedform,logincontainer);
    })
})

// Exiting
exiticon.addEventListener("click", () => {
  logincontainer.style.display = "none";
  document.getElementsByTagName("body")[0].classList.remove("overflow-hidden");
})

changeformbutton.addEventListener("click", () =>{
  loginSelected = !loginSelected;
  changeForm(loginSelected,inputform,unselectedform,logincontainer);
})

// Form Changing
function changeForm(loginselected,inputForm,unselectedForm, loginContainer){
  // Input Form Variables
  const hiddenInputs = inputForm.querySelectorAll('.is-hidden');
  const inputformtext = inputForm.querySelector(".input-text");
  const inputtype = inputForm.querySelector(".input-type");
  const inputbutton = inputForm.querySelector(".login-button");
  const forgottext = inputForm.querySelector(".login-forgot");

  // Unselected Form Variables
  const unselectedformtext = unselectedForm.querySelector(".unselected-text");
  const unselectedformdetails = unselectedform.querySelector(".unselected-details");
  const unselectedbutton = unselectedform.querySelector(".unselected-button");

  function clearForm(form) {
    const inputs = form.querySelectorAll('input');
    inputs.forEach((input) => {
      input.value = '';
      input.required = !loginselected; // Set to not required if loginselected is true
    });
  }

  // Clear input values and update required attributes for both forms
  clearForm(inputForm);
  clearForm(unselectedForm);

  // If Login is Selected
  if(loginselected){
    hiddenInputs.forEach((input) => {
      input.style.display = "none";
    });
    inputformtext.innerHTML = "Or use your account";
    inputtype.innerHTML = "Login"
    inputbutton.textContent = "Login";
    forgottext.style.display = "block";
    inputForm.style.borderRadius = "1rem 0px 0px 1rem";

    unselectedForm.style.background = "linear-gradient(122.34deg, #FF5C00 -16.04%, #FF9838 100%)";
    unselectedformtext.innerHTML = "Hello!";
    unselectedformdetails.innerHTML = "Are you here for the first time? Start your journey with us.";
    unselectedbutton.textContent = "Sign up";
    unselectedForm.style.borderRadius = "0px 1rem 1rem 0px";

    loginContainer.querySelector(".login-container").style.flexDirection = "row";
  } // If Sign up is Selected
  else{
    hiddenInputs.forEach((input) => {
      input.style.display = "block";
    });
    inputformtext.innerHTML = "Or use your email for registration";
    inputtype.innerHTML = "Sign up"
    inputbutton.textContent = "Sign up";
    forgottext.style.display = "none";
    inputForm.style.borderRadius = "0px 1rem 1rem 0px";
    
    unselectedForm.style.background = "linear-gradient(122.47deg, #FF5C00 43.18%, #FF9838 159.23%)";
    unselectedformtext.innerHTML = "Welcome back!";
    unselectedformdetails.innerHTML = "To keep connected with us please login with your pesonal info.";
    unselectedbutton.textContent = "Login";
    unselectedForm.style.borderRadius = "1rem 0px 0px 1rem";

    loginContainer.querySelector(".login-container").style.flexDirection = "row-reverse";
  }
}

// Screen Adjustment
window.addEventListener('scroll', function() {
  logincontainer.style.top = window.pageYOffset + 'px';
});

// FORM REQUEST ENDPOINT STUFF

const loginEndpoint = "http://localhost:5077/api/User/login";
const registerEndpoint = "http://localhost:5077/api/User/register";

const form = document.querySelector(".input-form");

form.addEventListener("submit", async function (event) {
  event.preventDefault();

  const email = form.querySelector('input[type="email"]').value;
  const password = form.querySelector('input[type="password"]').value;

  if (!loginSelected) {
    const steamid = form.querySelector('input[placeholder="SteamId"]').value;
    const name = form.querySelector('input[placeholder="Name"]').value;

    try {
      const response = await axios.post(registerEndpoint, {
        "username": name,
        "password": password,
        "email": email,
        "steamId": steamid
      });

      localStorage.setItem("SteamId",response.data.steamId);
      window.location.href = 'profile.html';

      console.log("Response:", response.data);
    } catch (error) {
      console.error("Registration failed:", error.message);
    }
  } else {
    try {
      const response = await axios.post(loginEndpoint, {
        "email": email,
        "password": password,
      });
      
      localStorage.setItem("SteamId",response.data.steamId);
      window.location.href = 'profile.html';

      console.log("Response:", response.data.steamId);
    } catch (error) {
      console.error("Registration failed:", error.message);
    }
  }
});