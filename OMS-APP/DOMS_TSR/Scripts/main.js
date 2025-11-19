$('code').each(function() {
    $(this).text($(this).html());
});

$(document).on('click','.table-body ul',function(){
    if(!$('.table-body ul').hasClass('active')){
        $('.table-header').addClass('show-panel');
        $(this).addClass('active');
    } else {
        var selectedItem = $('.table-body ul.active').length
        if($(this).hasClass('active') && selectedItem == 1) {
            $(this).removeClass('active');
            $('.table-header').removeClass('show-panel');
        } else if (!$(this).hasClass('active')){
            $(this).addClass('active');
        } else if ($(this).hasClass('active') && selectedItem > 1) {
            $('.table-body ul').removeClass('active');
            $(this).addClass('active');
        }
    }
});