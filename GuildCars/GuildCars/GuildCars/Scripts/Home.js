

$('#searchButton').on('click', function () {
    $('#errorList').empty();
    var term = $('#searchTerm').val();
    var minPrice = $('#minPrice').val();
    var maxPrice = $('#maxPrice').val();
    var minYear = $('#minYear').val();
    var maxYear = $('#maxYear').val();

    if (term == "") {
        $('#errorList').append('<li>Please enter Term to search for.</li>')
    } if (minPrice < 0 || maxPrice < 0) {
        alert("Prices must be positive.")
    } if (minPrice > maxPrice) {
        alert("Min Price must be lower than Max Price.")
    } if (minYear > maxYear) {
        alert("Min Year must be lower than Max Year.")
    }
    else {
        $('#tableBody').text('')
        // $('#saveDeleteButton').val("Delete")
        // $('#saveEditButton').val("Edit")

        $.ajax({
            type: 'GET',
            url: 'http://localhost:61683/dvds/' + category + '/' + term,
            success: function (data) {

                $.each(data, function (index, item) {
                    $('#tableBody').append('<tr>')
                        .append('<td><a href=# style="color:Black;" onClick="return movieDetails(' + item.dvdId + ');"><u>' + item.title + '</u></a></td>')
                        .append('<td class=text-center>' + data[index].realeaseYear + '</td>')
                        .append('<td>' + data[index].director + '</td>')
                        .append('<td class=text-center>' + data[index].rating + '</td>')
                        .append('<td><a href=# onClick="return showEditForm(' + item.dvdId + ');">Edit</a> | <a href=# style="color:red;" onClick="return showDeleteForm(' + item.dvdId + ');">Delete</a></td></tr>')
                })
                if (data.length == 0) {
                    alert("No Results Found With Selected Parameters.");
                    displayList();
                }
                else {
                    alert("Data Length: " + data.length)
                }
            },
            error: function () {
                alert(string);
                displayList();
            }
        });

        $('#addForm').hide()
        $('#editForm').hide()
        $('#deleteForm').hide()
        $('#listForm').show()
    }
});

