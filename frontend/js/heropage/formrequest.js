const loginEndpoint = "https://your-server.com/api/login";
const registerEndpoint = "https://your-server.com/api/register";

const loginForm = document.querySelector(".input-form");
const registerForm = document.querySelector(".unselected-form");

loginForm.addEventListener("submit", function (event) {
  event.preventDefault();

  // Gather login form data
  const email = loginForm.querySelector('input[type="email"]').value;
  const password = loginForm.querySelector('input[type="password"]').value;

  // Make a login request
  fetch(loginEndpoint, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  })
    .then((response) => response.json())
    .then((data) => {
      // Check the response from the server
      if (data.status === "OK") {
        // Successfully logged in, you can redirect or perform other actions
        window.location.href = "profile.html";
      } else {
        // Handle login error, display a message, or do other actions
        console.error("Login failed:", data.message);
      }
    })
    .catch((error) => {
      console.error("Error during login request:", error);
    });
});