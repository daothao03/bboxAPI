const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);

const tb = $$(".prod-tab__item");
const ps = $$(".prod-tab__content");

const tabActive = $(".prod-tab__item--current");

tb.forEach((tab, index) => {
    const p = ps[index];

    tab.onclick = function () {
        $(".prod-tab__item--current").classList.remove(
            "prod-tab__item--current"
        );
        $(".prod-tab__content--current").classList.remove(
            "prod-tab__content--current"
        );

        this.classList.add("prod-tab__item--current");
        p.classList.add("prod-tab__content--current");
    };
});
