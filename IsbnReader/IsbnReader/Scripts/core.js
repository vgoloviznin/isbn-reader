(function($) {
    $(function () {
        var form = $('#isbn-form');
        form.on('submit', function (e) {
            e.preventDefault();

            if (form.valid()) {
                var data = $(this).serialize();

                form.find(':input:not(:disabled)').prop('disabled', true);

                $.ajax({
                    url: '/home/books',
                    type: 'POST',
                    data: data,
                    success: function (result) {
                        if (result) {
                            $('.books-result').html(result);
                            form.find(':input:disabled').prop('disabled', false);
                        }
                    },
                    error: function () {
                        form.find(':input:disabled').prop('disabled', false);
                        alert('An error has occured.');
                    }
                });
            }
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