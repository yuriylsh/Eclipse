var resultsPageSize = 10;

function initHomePage() {
    populateResultsGrid();
    setupNameEdit();
    var selected = [];
    var testSelectionPubSub = $.Callbacks();
    testSelectionPubSub.add(onTestSelectionChanged);
    setupAddButtons(selected, testSelectionPubSub);
    setupSelectedGrid(selected, testSelectionPubSub);
    google.charts.load("current", { "packages": ["corechart"] });
}

var resultsPager = (function pager() {
    var pagerNext = document.getElementById("pagerNext"),
        pagerPrevious = document.getElementById("pagerPrevious"),
        summary = document.getElementById("pagerSummary"),
        totalCount = 0,
        totalNumberOfPages = 0,
        page = 0;

    function setTotal(total) {
        totalCount = total;
        totalNumberOfPages = Math.ceil(total / resultsPageSize);
    }

    function setCurrentPageData(pageNumber, itemsCount) {
        page = pageNumber;
        if (itemsCount === 0 || pageNumber * resultsPageSize >= totalCount) {
            pagerNext.className = "disabled";
        } else {
            pagerNext.className = "";
        }

        pagerPrevious.className = pageNumber === 1 ? "disabled" : "";
        summary.innerText = "page " + page + " of " + totalNumberOfPages + " (" + totalCount + ")";
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

    var headerRow = document.getElementById("resultsGridHeaderRow"),
        gridRowTemplate = $.templates("#resultsGridRow");

    function clearGrid() {
        while (headerRow.nextSibling) {
            headerRow.nextSibling.remove();
        }
    }

    function populateGridWithResults(results) {
        results.forEach(function(r) {
            r.nameOrPrompt = r.name === null ? "[click to enter name]" : r.name;
        });
        var rowsHtml = gridRowTemplate.render({ rows: results });
        headerRow.insertAdjacentHTML("afterend", rowsHtml);
    }
}

function setupNameEdit() {
    $("#resultsGrid").on("click", "a.resultName", onNameEdit);

    function onNameEdit(evt) {
        var data = evt.target.dataset;
        var id = data["id"];
        var newName = prompt("Enter new name for the result " + id, data["name"]);
        if (newName.length) {
            processNameEdit(newName, id, evt.target);
        }
    }

    function processNameEdit(newName, resultId, elem) {
        $.ajax({
            type: "POST",
            url: "/SetResultName",
            data: {name: newName, id: resultId}
        }).done(function () {
            elem.dataset["name"] = newName;
            elem.innerText = newName;
        });
    }
}

function findTestById(tests, id) {
    return _.find(tests, function(test) { return test.id === id; });
}

function setupAddButtons(selected, testSelectionPubSub) {
    var headerRow = document.getElementById("selectedGridHeaderRow"),
        gridRowTemplate = $.templates("#selectedGridRow");

    $("#resultsGrid").on("click", "button.btnAdd", onAddButtonClicked);

    function onAddButtonClicked(evt) {
        var data = evt.target.dataset,
            id = data["id"],
            name = data["name"];
        if (!findTestById(selected, id)) {
            var newRow = { id: id, name: name };
            selected.push(newRow);
            appendNewRow(newRow);
            testSelectionPubSub.fire(selected);
        };
    }

    function appendNewRow(rowData) {
        var rowHtml = gridRowTemplate.render(rowData);
        headerRow.parentNode.lastElementChild.insertAdjacentHTML("afterend", rowHtml);
    }
}

function setupSelectedGrid(selected, testSelectionPubSub) {
    var grid = $("#selectedGrid");

    grid.on("click", "button.btnRemove", onRemoveButtonClicked);

    function onRemoveButtonClicked(evt) {
        var data = evt.target.dataset,
            id = data["id"];
        _.remove(selected, function(test) { return test.id === id; });
        var removeButton = evt.target;
        var row = removeButton.parentNode.parentNode;
        row.remove();
        testSelectionPubSub.fire(selected);
    }
}

function onTestSelectionChanged(selected) {
    var ids = _.map(selected,
        function getId(test) {
            return test.id;
        });
    $.ajax({
        type: "GET",
        url: "/GetTestData",
        traditional: true,
        data: { ids: ids }
    }).done(processComparisonData);

    function processComparisonData(data) {
        clearCharts();
        data.charts.forEach(chartTest);
    };

    function clearCharts() {
        var chartsContainer = getChartsContainer();
        while(chartsContainer.firstChild) {
            chartsContainer.removeChild(chartsContainer.firstChild);
        }
    }

    function chartTest(chart) {
        var data = new google.visualization.DataTable();

        chart.columns.forEach(function addColumnToChart(column) {
            data.addColumn(column.dataType, column.name);
        });

        data.addRows(chart.rows);

        var options = {
            title: chart.title,
            hAxis: {
                title: chart.hAxisTitle
            },
            vAxis: {
                title: chart.vAxisTitle
            },
            height: "300"
        };

        var columnChart = new google.visualization.ColumnChart(getElementForChart());

        columnChart.draw(data, options);
    }

    function getElementForChart() {
        var chartsContainer = getChartsContainer();
        chartsContainer.insertAdjacentHTML("beforeend", "<div class='chartContainer'></div>");
        return chartsContainer.lastElementChild;
    }

    function getChartsContainer() { return document.getElementById("chartsContainer"); }
}