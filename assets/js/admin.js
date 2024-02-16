const $$$$ = document.querySelector.bind(document);
const $$$ = document.querySelectorAll.bind(document);

const tbs = $$$(".manager-items");
const psa = $$$(".manager-content");

// const tabActive = $$$$(".manager-items-current");


tbs.forEach((tab, index) => {
    const pa = psa[index];

    tab.onclick = function () {
        $$$$(".manager-items-current").classList.remove(
            "manager-items-current"
        );
        $$$$(".manager-content-current").classList.remove(
            "manager-content-current"
        );

        this.classList.add("manager-items-current");
        pa.classList.add("manager-content-current");
    };
   
});
