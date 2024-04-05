var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    // Check if DataTables script is loaded successfully
    console.log($.fn.DataTable);

    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/quiz/getall' },
        "columns": [
            { data: 'title', "width": "15%" },
            { data: 'section.title', "width": "10%" },
            { data: 'points', "width": "10%" },
            { data: 'coins', "width": "10%" },
            // Add a comma after the previous column configuration

            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group"> 
                        <a href="/admin/quiz/upsert?id=${data}" class="btn btn-primary mx-2" onclick="Edit(${data})"> <i class="bi bi-pencil-square"></i> Edytuj</a>   
                        <a onClick=Delete('/admin/quiz/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Usuń</a>
                    </div>`;
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Jesteś pewien?',
        text: "Nie będzie można tego cofnąć!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Tak, usuń!',
        cancelButtonText: 'Anuluj'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            });
        }
    });
}
