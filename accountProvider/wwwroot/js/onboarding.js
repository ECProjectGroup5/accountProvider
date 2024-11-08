let currentIndex = 0;
const slider = document.querySelector('.slider');
const totalItems = document.querySelectorAll('.slider-item').length;

function showNextSlide() {
    currentIndex = (currentIndex + 1) % totalItems;  
    slider.style.transform = `translateX(-${currentIndex * 100}%)`;  
}

function showPrevSlide() {
    currentIndex = (currentIndex - 1 + totalItems) % totalItems;  
    slider.style.transform = `translateX(-${currentIndex * 100}%)`;  
}

setInterval(showNextSlide, 5000);
