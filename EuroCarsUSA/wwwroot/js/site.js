// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function likeCar(carId, event, buttonElement) {
    event.preventDefault();
    event.stopPropagation();
    $(buttonElement).blur();

    var likes = getCookie("likes");
    var likesArray = likes ? JSON.parse(decodeURIComponent(likes)) : [];

    var isLiked = likesArray.includes(carId);

    var partialViewUrl = isLiked ? '/Home/UnpressedLikeButton' : '/Home/PressedLikeButton';
    var actionUrl = isLiked ? '/Home/UnlikeCar' : '/Home/LikeCar';

    $.ajax({
        url: partialViewUrl,
        type: 'GET',
        data: { carId: carId },
        success: function (response) {
            $(`button[data-car-id="${carId}"]`).replaceWith(response.html);
        },
        error: function () {
            console.error('Error loading button state');
        }
    });

    $.ajax({
        url: actionUrl,
        type: 'POST',
        data: { carId: carId },
        success: function (response) {
            if (response.success) {
                console.log('Car liked/unliked successfully');
            }
        },
        error: function () {
            console.error('Error liking/unliking car');
        }
    });
}

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/; Secure; SameSite=Lax";
}

function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function toggleDetails(uniqueId, event) {
    event.preventDefault();
    event.stopPropagation();

    var detailsDiv = document.getElementById("details-" + uniqueId);
    var iconDiv = document.getElementById("icon-" + uniqueId);

    if (detailsDiv.style.maxHeight == "0px") {
        detailsDiv.style.maxHeight = "500px"
        iconDiv.style.transform = "scaleY(1)";
    } else {
        detailsDiv.style.maxHeight = "0px"
        iconDiv.style.transform = "scaleY(-1)";
    }
}
function toggleOrderVisibility(formId) {
    var popupDiv = document.getElementById("expanded-" + formId);
    var iconDiv = document.getElementById("icon-" + formId);

    if (popupDiv.style.maxHeight === "320px") {
        popupDiv.style.maxHeight = "0px";
        iconDiv.style.transform = "scaleY(-1)";
    } else {
        popupDiv.style.maxHeight = "320px";
        iconDiv.style.transform = "scaleY(1)";
    }
}
function toggleSidebar() {
    const sidebar = document.getElementById("mobile-sidebar");
    sidebar.classList.toggle("open");
}

