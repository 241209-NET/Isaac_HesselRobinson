function addArcanePoster()
{
    const newImg = document.createElement('img');
    const posterArray = [];
    posterArray.push("https://mir-s3-cdn-cf.behance.net/project_modules/1400/443b18186095503.656f58fc0b6e1.png","https://image.tmdb.org/t/p/original/mOUV08EAZ86mXvN8fNUowYAnmLy.jpg","https://cafecomnerd.com.br/wp-content/uploads/2021/11/ARCANE-Ato-3-Netflix-divulga-trailer-da-serie-animada-baseada-no-game-LEAGUE-OF-LEGENDS-poster.jpg");
    const posterOutlineColorArray = ['#FFFFFF', '#FF00FF', '#25FF99'];
    let newImageIndex = generateRandomInteger(posterArray.length);
    newImg.src = posterArray[newImageIndex];
    newImg.style.outlineColor = posterOutlineColorArray[newImageIndex];
    const ul = document.getElementById('arcane-poster-list');
    ul.appendChild(newImg);
}

function generateRandomInteger(max) {
    return Math.floor(Math.random() * max) + 1;
}