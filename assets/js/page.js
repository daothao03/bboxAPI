function changeProductList(type, element) {
    let tabs = document.getElementsByClassName("btn-trends");
    for (i = 0; i < tabs.length; ++i) {
        tabs[i].style.color = "#000";
    }
    element.style.color = "#f7aca6";

    document.getElementById(type).style.display = "block";
    switch (type) {
        case "makeup":
            document.getElementById("skincare").style.display = "none";
            document.getElementById("routine").style.display = "none";
            break;
        case "skincare":
            document.getElementById("makeup").style.display = "none";
            document.getElementById("routine").style.display = "none";
            break;
        case "routine":
            document.getElementById("skincare").style.display = "none";
            document.getElementById("makeup").style.display = "none";
            break;
    }
}
