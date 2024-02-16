// // tang so luong khi bam them vao gio
// const count = document.querySelector(".countItem");

// const countCart = document.querySelectorAll(".countCart");

// const btnAdd = document.querySelector(".btn-add-cart");

// const btnLeft = document.querySelector(".leftBtn");

// const btnRight = document.querySelector(".rightBtn");

// let totalCount = 1;
// const btnAddClick = (e) => {
//     totalCount++;
//     count.innerHTML = totalCount;
//     localStorage.removeItem("total");
//     localStorage.setItem("total", totalCount.toString());
// };

// function btnEdit(numberBtn) {
//     if (numberBtn == 1) {
//         var total = Number(localStorage.getItem("total"));
//         total--;
//         countCart.forEach((item) => {
//             item.innerHTML = total;
//         });
//         count.innerHTML = total;
//         localStorage.removeItem("total");
//         localStorage.setItem("total", total.toString());
//     }
//     if (numberBtn == 2) {
//         var total = Number(localStorage.getItem("total"));
//         total++;
//         countCart.forEach((item) => {
//             item.innerHTML = total;
//         });
//         count.innerHTML = total;
//         localStorage.removeItem("total");
//         localStorage.setItem("total", total.toString());
//     }
// }

// if (countCart.length > 0) {
//     var totalLocal = localStorage.getItem("total");
//     countCart.forEach((item) => {
//         item.innerHTML = totalLocal;
//     });
//     count.innerHTML = totalLocal;
// }

// if (btnAdd != undefined && btnAdd != null) {
//     btnAdd.addEventListener("click", btnAddClick);
// }

// // console.log(btnAdd);
// // count[0].innerHTML = 2;

// điều hướng theo sp
var products = [
    "Tinh Chất Dưỡng Sáng Da AHC Niacin Biome Mela Scissors Max Essence 30ml",
    "Nước Cân Bằng Chống Lão Hóa Làm Sáng Da AHC 365 Red Toner 100Ml",
    "Combo 5 Mặt Nạ Dưỡng Ẩm, Săn Chắc Da AHC (nhiều loại)",
    "Phấn Nước Che Khuyết Điểm Hoàn Hảo Clio Kill Cover The New Founwear Cushion SPF50+ PA+++ (15gx2) (Tặng 1 lõi refill)",
    "Thực Phẩm Bảo Vệ Sức Khỏe Elasten Collagen Germany (28 Ống 25Ml)",
    "Sữa Rửa Mặt Nghệ Hưng Yên Cocoon Hung Yen Turmeric Cleanser",
];

var arr = [
    [
        "Tinh Chất Dưỡng Sáng Da AHC Niacin Biome Mela Scissors Max Essence 30ml",
        "Nước Cân Bằng Chống Lão Hóa Làm Sáng Da AHC 365 Red Toner 100Ml",
        "Combo 5 Mặt Nạ Dưỡng Ẩm, Săn Chắc Da AHC (nhiều loại)",
        "Phấn Nước Che Khuyết Điểm Hoàn Hảo Clio Kill Cover The New Founwear Cushion SPF50+ PA+++ (15gx2) (Tặng 1 lõi refill)",
        "Thực Phẩm Bảo Vệ Sức Khỏe Elasten Collagen Germany (28 Ống 25Ml)",
        "Sữa Rửa Mặt Nghệ Hưng Yên Cocoon Hung Yen Turmeric Cleanser",
    ],
    ["10000", "2", "3", "4", "5", "6"],
];

// Lấy URL hiện tại
const url = new URL(window.location.href);

// Lấy đối tượng URLSearchParams từ các tham số truy vấn của URL
const searchParams = url.searchParams;

// Lấy giá trị của tham số 'keyProduct'
const keyProduct = searchParams.get("keyProduct");
console.log(keyProduct);
const sp = document.querySelector(".product");
sp.innerHTML = `
                    <!-- Product info -->
    <div class="product-container prod-ctrl" data-key="1">
        <div class="product-preview">
            <div class="prod-preview__list">
                <div class="prod-preview__item">
                    <img
                        src="./assets/img/prod-1.svg"
                        alt=""
                        class="prod-preview__img"
                    />
                </div>
                <!-- <div class="prod-preview__item">
                    <img
                        src="./assets/img/prod-1.svg"
                        alt=""
                        class="prod-preview__img"
                    />
                </div>
                <div class="prod-preview__item">
                    <img
                        src="./assets/img/prod-1.svg"
                        alt=""
                        class="prod-preview__img"
                    />
                </div>
                <div class="prod-preview__item">
                    <img
                        src="./assets/img/prod-1.svg"
                        alt=""
                        class="prod-preview__img"
                    />
                </div> -->
            </div>
    
            <div class="prod-preview__thumbs">
                <img
                    src="./assets/img/prod-1.svg"
                    alt=""
                    class="prod-preview__thumb-img prod-preview__thumb-img--current"
                />
                <img
                    src="./assets/img/prod-1.svg"
                    alt=""
                    class="prod-preview__thumb-img"
                />
                <img
                    src="./assets/img/prod-1.svg"
                    alt=""
                    class="prod-preview__thumb-img"
                />
                <img
                    src="./assets/img/prod-1.svg"
                    alt=""
                    class="prod-preview__thumb-img"
                />
            </div>
        </div>
        <div class="product-info">
            <section class="prod-info">
                <a href="#!" class="prod-link">AHC</a>
                <h1 class="prod-heading">
                    ${arr[0][keyProduct]}
                </h1>
    
                <div class="prod-review">
                    <div class="prod-star">
                        <img
                            src="./assets/img/star.svg"
                            alt=""
                            class="prod-review-img"
                        />
                        <img
                            src="./assets/img/star.svg"
                            alt=""
                            class="prod-review-img"
                        />
                        <img
                            src="./assets/img/star.svg"
                            alt=""
                            class="prod-review-img"
                        />
                        <img
                            src="./assets/img/star.svg"
                            alt=""
                            class="prod-review-img"
                        />
                        <img
                            src="./assets/img/star.svg"
                            alt=""
                            class="prod-review-img"
                        />
                    </div>
                    <div class="prod-origin">
                        <span>Xuất xứ: </span>
                        Hàn Quốc
                    </div>
                </div>
    
                <span class="price">${arr[1][keyProduct]}</span>
    
                <div class="prod-choose-delivery">
                    <div class="prod-deli">
                        <div class="prod-choose">
                            <img
                                src="./assets/img/cart.svg"
                                alt=""
                                class="prod-choose-img"
                            />
                            <span>Giao hàng</span>
                        </div>
    
                        <div class="prod-choose">
                            <img
                                src="./assets/img/pick-up.svg"
                                alt=""
                                class="prod-choose-img"
                            />
                            <span>Mua và lấy tại cửa hàng</span>
                        </div>
    
                        <div class="prod-choose">
                            <img
                                src="./assets/img/cart.svg"
                                alt=""
                                class="prod-choose-img"
                            />
                            <span
                                >Nhận HSV Point cho mỗi lần
                                mua</span
                            >
                        </div>
                    </div>
                    <div class="prod-dis">
                        <div class="prod-choose">
                            <img
                                src="./assets/img/star.svg"
                                alt=""
                                class="prod-choose-img"
                            />
                            <span>Cam kết hàng chính hãng</span>
                        </div>
    
                        <div class="prod-choose">
                            <img
                                src="./assets/img/pick-up.svg"
                                alt=""
                                class="prod-choose-img"
                            />
                            <span>Đổi/ trả trong vòng 7 ngày</span>
                        </div>
    
                        <div class="prod-choose">
                            <img
                                src="./assets/img/pick-up.svg"
                                alt=""
                                class="prod-choose-img"
                            />
                            <span
                                >Miễn phí giao hàng, tối đa
                                44k</span
                            >
                        </div>
                    </div>
                </div>
    
                <!-- <button class="btn-add-cart button btn-primary">
                    Thêm vào giỏ hàng
                </button> -->
    
                <div class="prod-button">
                    <button class="btn-add-cart button btn-primary" onclick="btnAddClick(${keyProduct})">
                        Thêm vào giỏ hàng
                    </button>
                    <button class="button btn-outline btn-mua">
                        Mua ngay
                    </button>
                    <button class="button btn-outline btn-heart">
                        <img src="./assets/img/heart.svg" alt="" />
                    </button>
                </div>
            </section>
        </div>
    </div>
    
    <!-- Product content -->
    <div class="product-content">
        <div class="prod-tab js-tabs">
            <ul class="prod-tab__list">
                <li class="prod-tab__item prod-tab__item--current">
                    Mô tả
                </li>
                <li class="prod-tab__item">Đánh giá</li>
                <li class="prod-tab__item">Sản phẩm tương tự</li>
            </ul>
            <div class="prod-tab__contents">
                <div
                    class="prod-tab__content prod-tab__content--current"
                >
                    <div
                        class="text-content prod-tab__text-content"
                    >
                        <h2>Lorem ipsum dolor sit amet.</h2>
                        <p>
                            Lorem ipsum dolor sit amet
                            <a href="#!">consectetur</a> adipisicing
                            elit. Aliquid, cupiditate. Modi, quidem,
                            ullam sint dolorum recusandae voluptates
                            dignissimos similique animi assumenda
                            <a href="#!">praesentium</a> et! Illum
                            dolorem est rem voluptas nam!
                            Voluptatem.
                        </p>
                        <h3>Lorem ipsum dolor sit amet.</h3>
                        <p>
                            Lorem ipsum dolor sit amet consectetur
                            adipisicing elit. Aliquid, cupiditate.
                            Modi, quidem, ullam sint dolorum
                            recusandae voluptates dignissimos
                            similique animi assumenda praesentium
                            et! Illum dolorem est rem voluptas nam!
                            Voluptatem.
                        </p>
                        <p>
                            <img
                                src="./assets/img/prod-1.svg"
                                alt=""
                            />
                            <em
                                >Lorem ipsum dolor sit amet,
                                consectetur adipisicing elit.</em
                            >
                        </p>
                        <blockquote>
                            <p>
                                Lorem ipsum dolor sit amet
                                <em>consectetur</em>
                                <u>adipisicing</u> elit. Aliquid,
                                cupiditate. Modi, quidem, ullam sint
                                dolorum recusandae voluptates
                                dignissimos similique animi
                                assumenda praesentium et! Illum
                                dolorem est rem voluptas nam!
                                Voluptatem.
                            </p>
                        </blockquote>
                        <h3>Lorem ipsum dolor sit amet.</h3>
                        <p>
                            Lorem ipsum dolor sit amet consectetur
                            adipisicing elit. Aliquid, cupiditate.
                            Modi, quidem, ullam sint dolorum
                            recusandae voluptates dignissimos
                            similique animi assumenda praesentium
                            et! Illum dolorem est rem voluptas nam!
                            Voluptatem.
                        </p>
                        <p>
                            Lorem ipsum dolor sit amet consectetur
                            adipisicing elit. Aliquid, cupiditate.
                            Modi, quidem, ullam sint dolorum
                            recusandae voluptates dignissimos
                            similique animi assumenda praesentium
                            et! Illum dolorem est rem voluptas nam!
                            Voluptatem.
                        </p>
    
                        <hr />
    
                        <h2>Lorem ipsum dolor sit amet.</h2>
                        <p>
                            Lorem ipsum dolor sit amet consectetur
                            adipisicing elit. Aliquid, cupiditate.
                            Modi, quidem, ullam sint dolorum
                            recusandae voluptates dignissimos
                            similique animi assumenda praesentium
                            et! Illum dolorem est rem voluptas nam!
                            Voluptatem.
                        </p>
                        <p>
                            <img
                                src="./assets/img/prod-1.svg"
                                alt=""
                            />
                            <em
                                >Lorem ipsum dolor sit amet,
                                consectetur adipisicing elit.</em
                            >
                        </p>
                        <p>
                            Lorem ipsum dolor sit amet consectetur
                            adipisicing elit. Aliquid, cupiditate.
                            Modi, quidem, ullam sint dolorum
                            recusandae voluptates dignissimos
                            similique animi assumenda praesentium
                            et! Illum dolorem est rem voluptas nam!
                            Voluptatem.
                        </p>
                        <p>
                            Lorem ipsum dolor sit amet consectetur
                            adipisicing elit. Aliquid, cupiditate.
                            Modi, quidem, ullam sint dolorum
                            recusandae voluptates dignissimos
                            similique animi assumenda praesentium
                            et! Illum dolorem est rem voluptas nam!
                            Voluptatem.
                        </p>
    
                        <hr />
    
                        <h2>Lorem ipsum dolor sit amet.</h2>
                        <p>
                            Lorem ipsum dolor sit amet consectetur
                            adipisicing elit. Aliquid, cupiditate.
                            Modi, quidem, ullam sint dolorum
                            recusandae voluptates dignissimos
                            similique animi assumenda praesentium
                            et! Illum dolorem est rem voluptas nam!
                            Voluptatem.
                        </p>
                        <p>
                            Lorem ipsum dolor sit amet consectetur
                            adipisicing elit. Aliquid, cupiditate.
                            Modi, quidem, ullam sint dolorum
                            recusandae voluptates dignissimos
                            similique animi assumenda praesentium
                            et! Illum dolorem est rem voluptas nam!
                            Voluptatem.
                        </p>
                        <p>
                            <img
                                src="./assets/img/prod-1.svg"
                                alt=""
                            />
                            <em
                                >Lorem ipsum dolor sit amet,
                                consectetur adipisicing elit.</em
                            >
                        </p>
                        <p>
                            Lorem ipsum dolor sit amet consectetur
                            adipisicing elit. Aliquid, cupiditate.
                            Modi, quidem, ullam sint dolorum
                            recusandae voluptates dignissimos
                            similique animi assumenda praesentium
                            et! Illum dolorem est rem voluptas nam!
                            Voluptatem.
                        </p>
                    </div>
                </div>
                <div class="prod-tab__content">
                    <div class="prod-content">
                        <h2 class="prod-content__heading">
                            Khách hàng của chúng tôi đang nhận xét
                            gì?
                        </h2>
    
                        <div class="row">
                            <div class="row-3">
                                <div class="user-review">
                                    <div class="user-avatar">
                                        <img
                                            src="./assets/img/user.png"
                                            alt=""
                                        />
                                    </div>
                                    <div class="user-info">
                                        <span class="user-name"
                                            >Jakir Hussen</span
                                        >
                                        <p class="review">
                                            Great product, I love
                                            this Coffee Beans
                                        </p>
                                    </div>
                                </div>
                                <div class="user-star-review">
                                    <div class="user-star">
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                    </div>
                                    <span>(3.5) Review</span>
                                </div>
                            </div>
                            <div class="row-3">
                                <div class="user-review">
                                    <div class="user-avatar">
                                        <img
                                            src="./assets/img/user.png"
                                            alt=""
                                        />
                                    </div>
                                    <div class="user-info">
                                        <span class="user-name"
                                            >Jakir Hussen</span
                                        >
                                        <p class="review">
                                            Great product, I love
                                            this Coffee Beans
                                        </p>
                                    </div>
                                </div>
                                <div class="user-star-review">
                                    <div class="user-star">
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                    </div>
                                    <span>(3.5) Review</span>
                                </div>
                            </div>
                            <div class="row-3">
                                <div class="user-review">
                                    <div class="user-avatar">
                                        <img
                                            src="./assets/img/user.png"
                                            alt=""
                                        />
                                    </div>
                                    <div class="user-info">
                                        <span class="user-name"
                                            >Jakir Hussen</span
                                        >
                                        <p class="review">
                                            Great product, I love
                                            this Coffee Beans
                                        </p>
                                    </div>
                                </div>
                                <div class="user-star-review">
                                    <div class="user-star">
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                            class="star-img"
                                        />
                                    </div>
                                    <span>(3.5) Review</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="prod-tab__content">
                    <div class="prod-content">
                        <h2 class="prod-content__heading">
                            Sản phẩm tương tự
                        </h2>
    
                        <div class="row">
                            <div class="row-6">
                                <img
                                    src="./assets/img/prod-1.svg"
                                    alt=""
                                    class="prod-img"
                                />
                                <h3 class="prod-name">
                                    Tinh Chất Chống Lão Hóa AHC
                                    Youth Focus Essence 30Ml
                                </h3>
    
                                <a href="#!" class="link">AHC</a>
    
                                <div class="price-star">
                                    <span class="price"
                                        >350.000đ</span
                                    >
                                    <div class="star">
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                        />
                                        <span>3.5</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row-6">
                                <img
                                    src="./assets/img/prod-1.svg"
                                    alt=""
                                    class="prod-img"
                                />
                                <h3 class="prod-name">
                                    Tinh Chất Chống Lão Hóa AHC
                                    Youth Focus Essence 30Ml
                                </h3>
    
                                <a href="#!" class="link">AHC</a>
    
                                <div class="price-star">
                                    <span class="price"
                                        >350.000đ</span
                                    >
                                    <div class="star">
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                        />
                                        <span>3.5</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row-6">
                                <img
                                    src="./assets/img/prod-1.svg"
                                    alt=""
                                    class="prod-img"
                                />
                                <h3 class="prod-name">
                                    Tinh Chất Chống Lão Hóa AHC
                                    Youth Focus Essence 30Ml
                                </h3>
    
                                <a href="#!" class="link">AHC</a>
    
                                <div class="price-star">
                                    <span class="price"
                                        >350.000đ</span
                                    >
                                    <div class="star">
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                        />
                                        <span>3.5</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row-6">
                                <img
                                    src="./assets/img/prod-1.svg"
                                    alt=""
                                    class="prod-img"
                                />
                                <h3 class="prod-name">
                                    Tinh Chất Chống Lão Hóa AHC
                                    Youth Focus Essence 30Ml
                                </h3>
    
                                <a href="#!" class="link">AHC</a>
    
                                <div class="price-star">
                                    <span class="price"
                                        >350.000đ</span
                                    >
                                    <div class="star">
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                        />
                                        <span>3.5</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row-6">
                                <img
                                    src="./assets/img/prod-1.svg"
                                    alt=""
                                    class="prod-img"
                                />
                                <h3 class="prod-name">
                                    Tinh Chất Chống Lão Hóa AHC
                                    Youth Focus Essence 30Ml
                                </h3>
    
                                <a href="#!" class="link">AHC</a>
    
                                <div class="price-star">
                                    <span class="price"
                                        >350.000đ</span
                                    >
                                    <div class="star">
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                        />
                                        <span>3.5</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row-6">
                                <img
                                    src="./assets/img/prod-1.svg"
                                    alt=""
                                    class="prod-img"
                                />
                                <h3 class="prod-name">
                                    Tinh Chất Chống Lão Hóa AHC
                                    Youth Focus Essence 30Ml
                                </h3>
    
                                <a href="#!" class="link">AHC</a>
    
                                <div class="price-star">
                                    <span class="price"
                                        >350.000đ</span
                                    >
                                    <div class="star">
                                        <img
                                            src="./assets/img/star.svg"
                                            alt=""
                                        />
                                        <span>3.5</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>`;

// tang so luong khi bam them vao gio
const count = document.querySelector(".countItem");

const countCart = document.querySelectorAll(".countCart");

const btnAdd = document.querySelector(".btn-add-cart");

const btnLeft = document.querySelector(".leftBtn");

const btnRight = document.querySelector(".rightBtn");

const cart = document.querySelector(".cart-preview");
console.log(cart);

let totalCount = 1;
const btnAddClick = (e) => {
    if (keyProduct == 0 && count != null && count != undefined) {
        totalCount++;
        count.innerHTML = totalCount;
        localStorage.removeItem("total");
        localStorage.setItem("total", totalCount.toString());
    } else {
        const cartItem = document.createElement("article");
        cartItem.classList.add("cart-preview-item");
        cartItem.innerHTML = `<img
                                    src="./assets/img/prod-1.svg"
                                    alt=""
                                    class="cart-previewitem__thumb"
                                />
                                <div class="cart-preview-item__content">
                                    <h2 class="cart-preview-item__title">
                                        ${products[keyProduct]}
                                    </h2>
                                    <span class="cart-preview-item__price">
                                        Giá: ${arr[1][keyProduct]}
                                    </span>
                                    <span>
                                        Số lượng: <span class="countItem">1</span>
                                    </span>
                                </div>`;
        cart.appendChild(cartItem);
    }
};

if (countCart.length > 0) {
    var totalLocal = localStorage.getItem("total");
    countCart.forEach((item) => {
        item.innerHTML = totalLocal;
    });
    count.innerHTML = totalLocal;
}

function btnEdit(numberBtn) {
    if (numberBtn == 1) {
        var total = Number(localStorage.getItem("total"));
        total--;
        countCart.forEach((item) => {
            item.innerHTML = total;
        });
        count.innerHTML = total;
        localStorage.removeItem("total");
        localStorage.setItem("total", total.toString());
    }
    if (numberBtn == 2) {
        var total = Number(localStorage.getItem("total"));
        total++;
        countCart.forEach((item) => {
            item.innerHTML = total;
        });
        count.innerHTML = total;
        localStorage.removeItem("total");
        localStorage.setItem("total", total.toString());
    }
}
