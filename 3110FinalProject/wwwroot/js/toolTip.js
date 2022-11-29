//Credit: https://github.com/ArchwayEon/CSCI3110BootstrapIntro/blob/master/CSCI3110BootstrapIntro/wwwroot/js/toolTipDemo.js
(function _homeTooltipsMain() {
    let allTooltipDOMElementList =
        document.querySelectorAll('[data-bs-toggle="tooltip"]');
    // Convert the NodeList to an Array
    let allTooltipDOMElements = Array.from(allTooltipDOMElementList);
    let allTooltipElements = allTooltipDOMElements.map(tt => {
        return new bootstrap.Tooltip(tt);
    });
})();