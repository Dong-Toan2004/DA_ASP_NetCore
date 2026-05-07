// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification

// hàm để ẩn và hiện header khi cuộn trang
document.addEventListener("DOMContentLoaded", function () {
    const header = document.querySelector("header");
    const searchWrapper = document.querySelector(".search-wrapper");
    const toggleBtn = document.getElementById("toggleCategoryBtn");
    const categoryMenu = document.getElementById("categoryButtons");

    let lastScrollY = window.scrollY;

    // 1. Hiệu ứng cuộn ẩn/hiện Header và Search bar như ban đầu
    window.addEventListener("scroll", function () {
        let currentScrollY = window.scrollY;

        if (currentScrollY > lastScrollY && currentScrollY > 100) {
            // Cuộn xuống: Ẩn cả Header và Search bar
            header.style.transform = "translateY(-100%)";
            if (searchWrapper) {
                searchWrapper.style.transform = "translateY(-150px)"; // Trượt lên ẩn đi
            }
        } else {
            // Cuộn lên: Hiện lại cả hai
            header.style.transform = "translateY(0)";
            if (searchWrapper) {
                searchWrapper.style.transform = "translateY(0)";
            }
        }
        lastScrollY = currentScrollY;
    });

    // 2. Logic Toggle Danh mục
    if (toggleBtn && categoryMenu) {
        toggleBtn.addEventListener("click", function (e) {
            e.preventDefault();
            categoryMenu.classList.toggle("show");
        });
    }
});
function updateCartBadge() {
    fetch('/Cart/GetCartQuantity')
        .then(res => res.json())
        .then(data => {
            document.querySelector('#cartBadge').textContent = data.quantity;
        });
}

document.addEventListener("DOMContentLoaded", function () {
    const toggleBtn = document.getElementById("toggleCategoryBtn");
    const categoryMenu = document.getElementById("categoryButtons");

    if (toggleBtn && categoryMenu) {
        toggleBtn.addEventListener("click", function (e) {
            e.preventDefault(); // Ngăn chặn nhảy trang
            categoryMenu.classList.toggle("show");
        });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const toggleBtn = document.getElementById("toggleCategoryBtn");
    const category = document.getElementById("categoryButtons");

    if (!toggleBtn || !category) return; // không phải Home thì bỏ qua

    toggleBtn.addEventListener("click", function () {
        category.classList.toggle("show");
    });

});