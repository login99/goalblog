block_pos = $('.left-sidebar').offset().top;

wrap_pos = $('.body').offset().top;

block_height = $('.left-sidebar').outerHeight();

wrap_height = $('.body').outerHeight();

block_width = $('.left-sidebar').outerWidth();
pos_absolute = wrap_pos + wrap_height - block_height;

$(window).scroll(function () {
    if ($(window).scrollTop() > pos_absolute) {
        $('.left-sidebar').css({
            'position': 'absolute',
            'top': wrap_height - block_height,
            'width': block_width
        });
    }
    else if ($(window).scrollTop() > block_pos - 100) {
        $('.left-sidebar').css({
            'position': 'fixed',
            'top': '100px',
            'width': block_width
        });

    } else {
        $('.left-sidebar').css({
            'position': 'static'
        });
    }
})