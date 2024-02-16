const $$$ = document.querySelector.bind(document);
const $$$$ = document.querySelectorAll.bind(document);

const t = $$$$(".tab-brand");
const brands = $$$$(".brand-product");
const imgs = $$$$(".img-items");

const tabAction = $(".tab-brand.actions");

t.forEach((t, index) => {
    const brand = brands[index];
    const img = imgs[index];

    t.onclick = function () {
        $$$(".tab-brand.actions").classList.remove("actions");
        $$$(".brand-product.actions").classList.remove("actions");
        $$$(".img-items.actions").classList.remove("actions");

        this.classList.add("actions");
        brand.classList.add("actions");
        img.classList.add("actions");
    };
});
