document.addEventListener("DOMContentLoaded", function () {
  if(localStorage.getItem("SteamId") != null)
  {
    window.location.href = 'profile.html';
  }
  const reviewsData = [
    { username: "Olympic57", imageSrc: "../public/recommendations/game1.jpg" },
    { username: "Beastry", imageSrc: "../public/recommendations/game2.jpg" },
    { username: "Stef", imageSrc: "../public/recommendations/game3.jpg" },
    { username: "Gali", imageSrc: "../public/recommendations/game4.webp" },
    { username: "Hyport77", imageSrc: "../public/recommendations/game7.webp" },
    { username: "Featurelynk", imageSrc: "../public/recommendations/game6.jpg" },
    { username: "Anderha38", imageSrc: "../public/recommendations/game8.jpg" },
    { username: "iiChelagii", imageSrc: "../public/recommendations/game9.jpg" },
  ];

  function createReviewCard(username, imageSrc) {
    const reviewCard = document.createElement("div");
    reviewCard.classList.add("review-card");

    const img = document.createElement("img");
    img.src = imageSrc;
    img.alt = "";
    img.classList.add("review-img");
    reviewCard.appendChild(img);

    const usernameDiv = document.createElement("div");
    usernameDiv.textContent = username;
    usernameDiv.classList.add("review-card-text", "glass");
    reviewCard.appendChild(usernameDiv);
    return reviewCard;
  }

  function appendReviews(container, reviews, addToStart = false) {
    reviews.forEach((review) => {
      const reviewCard = createReviewCard(review.username, review.imageSrc);
      if (addToStart) {
        container.insertBefore(reviewCard, container.firstChild);
      } else {
        container.appendChild(reviewCard);
      }
    });
  }

  const reviewContainer1 = document.querySelector(".review-container:nth-child(1)");
  const reviewContainer2 = document.querySelector(".review-container:nth-child(2)");

  appendReviews(reviewContainer1, reviewsData.slice(0, 4));
  appendReviews(reviewContainer2, reviewsData.slice(4), true);
});
