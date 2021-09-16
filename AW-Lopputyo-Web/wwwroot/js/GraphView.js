document.addEventListener("DOMContentLoaded", function () {
    ToggleAnimationOfBackButton();
});

function ToggleAnimationOfBackButton() {
    // when animation ends, remove the animation
    var backButton = document.querySelectorAll(".tomenu-button");
    backButton.forEach(el => {
        el.addEventListener("mouseover", function () {
        if (event.target.parentElement.className == "tomenu-button") {
            event.target.parentElement.className = "tomenu-button-no-animation";
            // when button is clicked, add animation back for next time
            event.target.addEventListener("click", function () {
                event.target.parentElement.className = "tomenu-button";
            });
        }
    });
    })
    
}

