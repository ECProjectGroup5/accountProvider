document.addEventListener('DOMContentLoaded', function () {
    const companyText = document.querySelector(".company-text");
    const loginOptions = document.querySelector(".login-options");
    const container = document.querySelector(".container");

    companyText.style.opacity = "1";
    setTimeout(() => {
        companyText.style.transform = "translateY(-180%)"; 

        setTimeout(() => {
            loginOptions.style.visibility = "visible";  
            loginOptions.style.opacity = "1"; 
        }, 1000); 
    }, 3000); 

    loginOptions.style.transition = "opacity 1s ease-out"; 
    loginOptions.style = "margin-top: 150%;"
})