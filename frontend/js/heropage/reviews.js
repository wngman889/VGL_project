document.addEventListener("DOMContentLoaded", function () {
  const reviewsData = [
    { username: "Olympic57", imageSrc: "imgs/recommendations/game1.jpg" },
    { username: "Beastry", imageSrc: "imgs/recommendations/game2.jpg" },
    { username: "Stef", imageSrc: "imgs/recommendations/game3.jpg" },
    { username: "Gali", imageSrc: "imgs/recommendations/game4.webp" },
    { username: "Hyport77", imageSrc: "imgs/recommendations/game7.webp" },
    { username: "Featurelynk", imageSrc: "imgs/recommendations/game6.jpg" },
    { username: "Anderha38", imageSrc: "imgs/recommendations/game8.jpg" },
    { username: "iiChelagii", imageSrc: "imgs/recommendations/game9.jpg" },
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
