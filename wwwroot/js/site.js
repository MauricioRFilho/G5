// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function createBrand() {
    var name, national, status = '';
    national = document.querySelector('#national').value
    name = document.querySelector('#brand').value
    status = document.querySelector('#status').checked
    var data = {
        BrandName: name,
        National: national,
        Status: status
    }
    var xhr = new XMLHttpRequest();
    xhr.open("POST", '/Create', true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify(data));
}


//Filter para a tabela;
$('#consulta').on("click", function () {
    var value = $("#brand").val().toLowerCase();
    $(".table tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
});


function backIndex() {
    window.location.pathname = '/Brands/Index';
}

