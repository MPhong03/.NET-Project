function initializeJWPlayer(movieLink, backgroundUrl) {
    jwplayer("movie").setup({
        file: movieLink,
        width: "100%",
        autostart: true,
        stretching: "bestfit",
        aspectratio: "16:9",
        image: backgroundUrl,
        controls: true
    });
}