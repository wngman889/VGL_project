    import axios from "axios";
    let gamesData = [];

    async function fetchData() {      
      const mostplayedGamesEndpoint = "http://localhost:5077/api/Games/get-most-played-owned-games";

      try {
        const response = await axios.post(mostplayedGamesEndpoint, {
          "steamId": localStorage.getItem("SteamId"),
          "count": 30
        });
        gamesData = response.data;
        let first = true;
        gamesData.forEach(game => {

          if(first){
            var openingContainer = document.getElementById('already-played-opening');

            openingContainer.innerHTML = `
                <img class="already-played-img" src="https://cdn.cloudflare.steamstatic.com/steam/apps/${game.appId}/header.jpg" alt="${game.name}"/>
                <div class="opening-overlay"></div>
                <div class="section-opening-container">
                    <div class="section-title">${game.name}</div>
                    <div class="section-update">
                        Release Date: <span class="section-date">${game.releaseDate}</span>
                    </div>
                </div>
            `;
            first = false;
          }else{
            const container = document.getElementById("already-played-cards");

            const card = document.createElement("div");
            card.className = "card";

            const gameContainer = document.createElement("div");
            gameContainer.className = "card-game-container";
        
            const gameCover = document.createElement("img");
            gameCover.className = "card-game-cover";
            gameCover.src = `https://cdn.cloudflare.steamstatic.com/steam/apps/${game.appId}/header.jpg`;
            gameCover.alt = game.name;
        
            gameContainer.appendChild(gameCover);
        
            const gameInfoContainer = document.createElement("div");
            gameInfoContainer.className = "card-game-info-container";
        
            const mainInfoContainer = document.createElement("div");
            mainInfoContainer.className = "card-game-main-info-container";
        
            const title = document.createElement("div");
            title.className = "card-game-title";
            title.textContent = game.name;

            const studio = document.createElement("div");
            studio.className = "card-game-studio";
            studio.textContent = game.developer;
        
            mainInfoContainer.appendChild(title);
            mainInfoContainer.appendChild(studio);
        

            const release = document.createElement("div");
            release.className = "card-game-release";
            release.textContent = game.releaseDate;
        
            gameInfoContainer.appendChild(mainInfoContainer);
            gameInfoContainer.appendChild(release);
        
            card.appendChild(gameContainer);
            card.appendChild(gameInfoContainer);
        
            container.appendChild(card);
          }
        });
      } catch (error) {
        console.error(error.message);
      }
    }
    fetchData();
