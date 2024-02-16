// let slideIndex = 0;
// showSlides();

// function showSlides() {
//     let i;
//     let slides = document.getElementsByClassName("slideshow");
//     for (i = 0; i < slides.length; i++) {
//         slides[i].style.display = "none";
//     }
//     slideIndex++;
//     if (slideIndex > slides.length) {
//         slideIndex = 1;
//     }
//     slides[slideIndex - 1].style.display = "block";
//     setTimeout(showSlides, 7000); // Change image every 2 seconds
// }

// var slideIndex = 0;
// showSlides();

// function plusSlides(n) {
//     showSlides((slideIndex += n));
// }

// function currentSlide(n) {
//     showSlides((slideIndex = n));
// }

// function showSlides(n) {
//     var i;
//     var slides = document.getElementsByClassName("slideshow");
//     var dots = document.getElementsByClassName("dot");
//     if (n > slides.length) {
//         slideIndex = 1;
//     }
//     if (n < 1) {
//         slideIndex = slides.length;
//     }
//     for (i = 0; i < slides.length; i++) {
//         slides[i].style.display = "none";
//     }
//     for (i = 0; i < dots.length; i++) {
//         dots[i].className = dots[i].className.replace(" active", "");
//     }
//     slides[slideIndex - 1].style.display = "block";
//     dots[slideIndex - 1].className += " active";
// }

console.log("test");

const listImage = document.querySelector('.slide')
const images = document.querySelectorAll(".slide img")
const length = images.length
let current = 0

setInterval(() => {
    let width = images[0].offsetWidth;
    if (current == length - 1) {
        current = 0
        let width = images[0].offsetWidth
        listImage.style.transform = `translateX(0px)`
    } else {
        current ++
        let width = images[0].offsetWidth
        listImage.style.transform = `translateX(${width * -1 * current}px)`
    }
},4000)

let handleEventChangeSlide = setInterval(handleChangeSlide, 4000)

