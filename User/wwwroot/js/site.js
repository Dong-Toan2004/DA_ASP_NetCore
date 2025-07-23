// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification

// hàm để ẩn và hiện header khi cuộn trang
document.addEventListener("DOMContentLoaded", function () {
    var header = document.querySelector("header");
    var searchBar = document.querySelector(".search-container");
    var lastScrollY = window.scrollY;
    var ticking = false;
    var isHeaderHidden = false;
    var isSearchHidden = false;

    function onScroll() {
        var currentScrollY = window.scrollY;
        if (currentScrollY > lastScrollY && currentScrollY > 70) {
            if (!isHeaderHidden) {
                header.style.transform = "translateY(-100%)";
                isHeaderHidden = true;
            }
            if (searchBar && !isSearchHidden) {
                searchBar.style.transform = "translateY(-200%)";
                isSearchHidden = true;
            }
        }
        else if (currentScrollY < lastScrollY) {
            if (isHeaderHidden) {
                header.style.transform = "translateY(0)";
                isHeaderHidden = false;
            }
            if (searchBar && isSearchHidden) {
                searchBar.style.transform = "translateY(0)";
                isSearchHidden = false;
            }
        }
        lastScrollY = currentScrollY;
        ticking = false;
    }
    window.addEventListener("scroll", function () {
        if (!ticking) {
            window.requestAnimationFrame(onScroll);
            ticking = true;
        }
    });
});