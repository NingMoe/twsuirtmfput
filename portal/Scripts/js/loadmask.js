function createMask(p) {
    var __m, __msg;
    if (p.selector && $(p.selector).length != 0) {
        __m = $('<div ></div>').css({
            position: 'absolute',
            top: $(p.selector).offset().top + 'px',
            left: $(p.selector).offset().left + 'px',
            width: $(p.selector).width(),
            height: $(p.selector).height(),
            'z-index': 1000,
            'background': 'repeat scroll 0 0 white',
            opacity: p.opacity ? p.opacity : 0.5
        });
        //高度40px，宽度100px.距离上边缘150px,水平居中
        __msg = $('<div ></div>').css({
            position: 'relative',
            top: '150px',
            left: __m.width() / 2 - 100 / 2 + 'px',
            width: 100,
            height: 40,
            'line-height': '40px',
            'font': 'tohama',
            'color': 'blue',
            'font-size': '12px',
            border: '2px solid gray'
        }).html('正在装载......');
        $('body').append(__m);
        __m.append(__msg);
    }
    return __m;
}