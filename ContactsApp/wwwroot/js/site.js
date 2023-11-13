$(document).ready(function () {
    getDatatable('#table-contacts');
    getDatatable('#table-users');

    $('.btn-contacts-total').click(function () {
        var userId = $(this).attr('user-id');

        $.ajax({
            type: 'GET',
            url: '/User/UserContactListById/' + userId,
            success: function (result) {
                $('#UserContactList').html(result);
                $('#userContactsModal').modal('show');
                getDatatable('#table-contacts-user');
            }
        });
    });
});

function getDatatable(id) {
    $(id).DataTable();
}


$('.close-alert').click(function () {
    $('.alert').hide('hide');
});