function search() {
    var searchText = document.getElementById('searchBox').value;
    var links = document.querySelectorAll('.tree a');

    try {
        var regex = new RegExp(searchText, 'i'); // 'i' für case-insensitive
    } catch (e) {
        alert("Ungültiger Regex-Ausdruck");
        return;
    }

    links.forEach(function(link) {
        var linkText = link.textContent;
        if (searchText === "" || regex.test(linkText)) {
            link.parentElement.style.display = "list-item";
            showParentDetails(link);
        } else {
            link.parentElement.style.display = "none";
        }
    });
}

function showParentDetails(element) {
    while (element) {
        if (element.tagName === 'DETAILS') {
            element.open = true;
        }
        element = element.parentElement;
    }
}