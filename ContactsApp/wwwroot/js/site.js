$(document).ready(function () {
    getDatatable('#table-contacts');
    getDatatable('#table-users');
});

function getDatatable(id) {
    $(id).DataTable();
}


$('.close-alert').click(function () {
    $('.alert').hide('hide');
});