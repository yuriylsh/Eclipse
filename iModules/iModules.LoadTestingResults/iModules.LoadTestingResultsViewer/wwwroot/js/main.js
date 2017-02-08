function initHomePage() {
    populateResultsGrid();
}

var resultsPageSize = 10;
var resultsPager = (function pager() {
    var pagerNext = document.getElementById("pagerNext"),
        pagerPrevious = document.getElementById("pagerPrevious"),
        totalCount = 0,
        page = 0;
    function setTotal(total) { totalCount = total; }
    function setCurrentPageData(pageNumber, itemsCount) {
        page = pageNumber;
        if (itemsCount === 0 || pageNumber * resultsPageSize >= totalCount) {
            pagerNext.className = "disabled";
        } else {
            pagerNext.className = "";
        }

        pagerPrevious.className = pageNumber === 1 ? "disabled" : "";
    }
    return {
        setTotal: setTotal,
        setCurrentPageData: setCurrentPageData,
        onNext: function onNext(callback) { pagerNext.addEventListener("click", callback); },
        onPrevious: function onPrevious(callback) { pagerPrevious.addEventListener("click", callback); },
        getCurrentPage: function getCurrentPage() { return page; }
    }
})();
function populateResultsGrid() {
    $.ajax({
        type: "GET",
        url: "/GetInitialResults",
        data: {pageIndex: 1, pageSize: resultsPageSize}
    }).done(function(resultsWithTotalCount) {
        resultsPager.setTotal(resultsWithTotalCount.itemsCount);
        resultsPager.setCurrentPageData(1, resultsWithTotalCount.data.length);
        populateGridWithResults(resultsWithTotalCount.data);
    });

    resultsPager.onNext(function onPagerNext() {
        pagerClickHandler(resultsPager.getCurrentPage() + 1);
    });
    resultsPager.onPrevious(function onPagerNext() {
        pagerClickHandler(resultsPager.getCurrentPage() -1);
    });

    function pagerClickHandler(newPageNumber) {
        $.ajax({
            type: "GET",
            url: "/GetResults",
            data: { pageIndex: newPageNumber, pageSize: resultsPageSize }
        }).done(function (results) {
            resultsPager.setCurrentPageData(newPageNumber, results.length);
            clearGrid();
            populateGridWithResults(results);
        });
    }

    var headerRow = document.getElementById("resultsGridHeaderRow");
    var gridRowTemplage = $.templates("#resultsGridRow");

    function clearGrid() {
        while (headerRow.nextSibling) {
            headerRow.nextSibling.remove();
        }
    }

    function populateGridWithResults(results) {
        var rowsHtml = gridRowTemplage.render({ rows: results });
        headerRow.insertAdjacentHTML("afterend", rowsHtml);
    }
}





