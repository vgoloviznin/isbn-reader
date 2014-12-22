(function($) {
    $(function () {
        $('#isbn-form').on('submit', function (e) {
            e.preventDefault();

            var data = $(this).serialize();

            $.ajax({
                url: '/home/books',
                type: 'POST',
                data: data,
                success: function(result) {
                    if (result) {
                        $('.books-result').html(result);
                    }
                }
            });
        });

        $('.books-result').on('change', '.book-checkbox', function () {
            var checkbox = $(this);
            var read = checkbox.is(':checked');
            var isbn = checkbox.data('isbn');

            $.post('/home/read', { isbn: isbn, read: read }, function(result) {
                if (!result) {
                    //handle errors
                    checkbox.removeAttr("checked");
                }
            });
        });
    });
})($);