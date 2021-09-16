document.addEventListener("DOMContentLoaded", function () {
    IfPreviousVisitNoAnimation();
    AddVisitToLocalStorage();
});

function SetWelcomeAnimationOff() {
    var welcomeAnimation = document.querySelector("#welcome-animation");
    welcomeAnimation.style.display = "none";
    var loginContainer = document.querySelector("#login-outer-container");
    loginContainer.id = "login-outer-container-nodelay";
}

function AddVisitToLocalStorage() {
    localStorage.setItem("hasPreviouslyVisitedSite", true)
}

function IfPreviousVisitNoAnimation() {
    var previousVisit = localStorage.getItem("hasPreviouslyVisitedSite");
    if (previousVisit) {
        SetWelcomeAnimationOff();
    }
}
