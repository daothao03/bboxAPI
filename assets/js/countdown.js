var timeStart = new Date("Dec 27,2023 00:00:00").getTime();

setInterval(function () {
    var noW = new Date().getTime();
    var D = timeStart - noW;
    var days = Math.floor(D / (24 * 60 * 60 * 1000));
    var hours = Math.floor(D / (1000 * 60 * 60));
    var minutes = Math.floor(D / (1000 * 60));
    var seconds = Math.floor(D / 1000);

    hours %= 24;
    minutes %= 60;
    seconds %= 60;

    document.getElementById("days").innerText = days;
    document.getElementById("hours").innerText = hours;
    document.getElementById("minutes").innerText = minutes;
    document.getElementById("seconds").innerText = seconds;
}, 1000);
