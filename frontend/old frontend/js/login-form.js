
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
  const inputname = inputForm.querySelector(".is-hidden");
  const inputformtext = inputForm.querySelector(".input-text");
  const inputtype = inputForm.querySelector(".input-type");
  const inputbutton = inputForm.querySelector(".login-button");
  const forgottext = inputForm.querySelector(".login-forgot");

  // Unselected Form Variables
  const unselectedformtext = unselectedForm.querySelector(".unselected-text");
  const unselectedformdetails = unselectedform.querySelector(".unselected-details");
  const unselectedbutton = unselectedform.querySelector(".unselected-button");

  // If Login is Selected
  if(loginselected){
    inputname.style.display = "none";
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
    inputname.style.display = "block";
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

async function handleLogin() {
  var nameInput = document.querySelector('.login-input.is-hidden[type="text"]');
  var isHidden = nameInput.classList.contains('is-hidden');
        
        if (isHidden) {
          // If the name input is hidden, call handleSignup
          await handleSignup();
        }
  var emailInput = document.querySelector('.login-input[type="email"]');
  var passwordInput = document.querySelector('.login-input[type="password"]');
  var email = emailInput.value;
  var password = passwordInput.value;

  try {
    // var nameInput = document.querySelector('.login-input.is-hidden[type="text"]');// the name field
    // var name = nameInput.value;
    // if (name.trim()) {
    //   // If the name is not null go to sign up
    //   handleSignup();
    //   return;
    // }
    const url = `https://localhost:7262/api/User/login?email=${encodeURIComponent(email)}&password=${encodeURIComponent(password)}`;
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('HTTP error! Status: ' + response.status);
    }

    const data = await response.json();
    console.log('Login success:', data);
    // Redirect to 'profile.html' or perform other actions on successful login
    window.location.href = 'profile.html';
  } catch (error) {
    console.error('Login error:', error.message);
    if (error.message.includes('401')) {
      console.log('Incorrect username or password');
    } else {
      console.log('An error occurred during login:', error.message);
    }
  }
}

async function handleSignup() {
  var nameInput = document.querySelector('.login-input.is-hidden[type="text"]');
  var emailInput = document.querySelector('.login-input[type="email"]');
  var passwordInput = document.querySelector('.login-input[type="password"]');

  var name = nameInput.value;
  var email = emailInput.value;
  var password = passwordInput.value;

  try {
    const url = `https://localhost:7262/api/User/register?username=${encodeURIComponent(name)}&password=${encodeURIComponent(password)}&email=${encodeURIComponent(email)}`;
    const response = await fetch(url, {
      method: 'POST',  
      headers: {
        'Content-Type': 'application/json',
      },
    });

    if (!response.ok) {
      throw new Error('HTTP error! Status: ' + response.status);
    }

    const responseData = await response.text(); // Read response as text

    // Check if the response contains "User Created"
    if (responseData.includes('User Created')) {
      console.log('Signup success:', responseData);

      // Perform additional action after successful signup
      // For example, display a confirmation message
      alert('Signup successful!');

      // Redirect to 'profile.html' or perform other actions on successful signup
      window.location.href = 'profile.html';
    } else {
      console.log('Unexpected response:', responseData);
    }
  } catch (error) {
    console.error('Signup error:', error.message);
    if (error.message.includes('409')) {
      console.log('Email already in use');
    } else {
      console.log('An error occurred during signup:', error.message);
    }
  }
  return;
}


// Attach the event listener to the login button
document.addEventListener('DOMContentLoaded', function () {
  var loginButton = document.querySelector('.login-button');
  var signupButton = document.querySelector('.unselected-button');
  if (loginButton) {
    loginButton.addEventListener('click', async function (event) {
      event.preventDefault();
      await handleLogin();
    });
  }
  if (signupButton) {
    signupButton.addEventListener('click', async function (event) {
      event.preventDefault();
      // Toggle the visibility of the name input
      // var nameInput = document.querySelector('.login-input[type="text"]');
      //nameInput.classList.toggle('is-hidden');

      // Call the signup logic
      await handleSignup();
    });
  }
 });