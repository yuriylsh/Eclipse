var nameLinkTemplate;

function initHomePage() {
    populateResultsGrid();
    setupNameEdit();
    nameLinkTemplate = $.templates("#nameEditLink");
}

function populateResultsGrid() {

    var resultsGridController = {
        loadData: function (filter) {
            return $.ajax({
                type: "GET",
                url: "/GetResults",
                data: filter
            });
        },
        updateItem: function(item) {
            $.ajax({
                type: "POST",
                url: "/SetResultName",
                data: item
            });
        }
    }

    $("#resultsGrid").jsGrid({
        width: "100%",
        filtering: false,
        editing: false,
        sorting: false,
        paging: true,
        autoload: true,
        pageLoading: true,
        pageSize: 10,
        pageButtonCount: 5,
        controller: resultsGridController,
        fields: [
            { name: "date", type: "text", width: "25%"},
            { name: "id", type: "text", width: "25%"},
            { name: "name", type: "text", width: "50%", itemTemplate: editNameTemplate}
        ]
    });

    function editNameTemplate(value, item) {
        var name = item.name == null ? "[click to enter name]" : item.name;
        return nameLinkTemplate.render({id: item.id, name: (item.name || ""), nameOrPrompt: name});
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
        var row = $(elem).closest("tr");
        var resultItem = row.data("JSGridItem");
        $("#resultsGrid").jsGrid("updateItem", row, { id: resultId, name: newName, date: resultItem.date });
    }
}



