document.addEventListener("DOMContentLoaded", function () {
    AddEventListenersToButtons();
    AddEvenListenersToBackToMenuButtons();
});

function AddEventListenersToButtons() {
    var buttons = document.querySelectorAll(".view-button");
    buttons.forEach((button) => {
        button.addEventListener("click", OpenView);
    });
}

function HideAllViews() {
    var views = document.querySelectorAll(".view")
    views.forEach((view) => {
        view.style.display = "none";
    });
    var mainView = document.querySelector("#main-view");
    mainView.style.display = "none";
}

function OpenView() {
    HideAllViews();
    console.log(this)
    console.log(this.dataset.view);
    var view = document.querySelector("#" + this.dataset.view);
    view.style.display = "block";
}

function AddEvenListenersToBackToMenuButtons() {
    var buttons = document.querySelectorAll(".tomenu-button");
    buttons.forEach((button) => {
        button.addEventListener("click", OpenMainView);
    });
}

function OpenMainView() {
    var mainView = document.querySelector("#main-view");
    HideAllViews();
    mainView.style.display = "block";
}

function ViewGraph(viewId, char, data, titleText) {
    var chart = new CanvasJS.Chart(viewId, {
        theme: "light2",
        exportEnabled: true,
        animationEnabled: true,
        title: {
            text: titleText,
            fontSize: 21
        },
        data: [{
            type: char,
            startAngle: 160,
            toolTipContent: "<b>{label}</b>: {y}%",
            indexLabel: "{label} - {y}%",
            dataPoints: data
        }]
    });
chart.render();
}
