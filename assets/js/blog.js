const $ = document.querySelector.bind(document);
const $$ = document.querySelectorAll.bind(document);

const tabs = $$(".tab-items");
const panes = $$(".magazine");

const tabActive = $(".tab-items.actions");

tabs.forEach((tab, index) => {
    const pane = panes[index];

    tab.onclick = function () {
        $(".tab-items.actions").classList.remove("actions");
        $(".magazine.actions").classList.remove("actions");

        this.classList.add("actions");
        pane.classList.add("actions");
    };
});
