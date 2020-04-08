/**
 * 判断手机端
 * @type {{BlackBerry: (function(): boolean), Windows: (function(): boolean), iOS: (function(): boolean), any: (function(): (*|boolean)), Android: (function(): boolean)}}
 */
var isMobile = {
	Android : function() {
		return navigator.userAgent.match(/Android/i) ? true : false;
	},
	BlackBerry : function() {
		return navigator.userAgent.match(/BlackBerry/i) ? true : false;
	},
	iOS : function() {
		return navigator.userAgent.match(/iPhone|iPad|iPod/i) ? true : false;
	},
	Windows : function() {
		return navigator.userAgent.match(/IEMobile/i) ? true : false;
	},
	any : function() {
		return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Windows());
	}
};

function openPhotoSwipe(_img) {
    var items = [{ src: _img.src, w: _img.naturalWidth, h: _img.naturalHeight }];
    var imgs = $("#jcsj").find("img");
    if (imgs.length > 0) {
        // build items array
        $.each(imgs, function (i, img) {
            if (img.src != _img.src) {
                items.push({
                    src: img.src,
                    w: img.naturalWidth,
                    h: img.naturalHeight
                });
            }
        });
    }
    var pswpElement = document.querySelectorAll('.pswp')[0];

    // define options (if needed)
    var options = {
        // history & focus options are disabled on CodePen
        history: false,
        focus: false,

        showAnimationDuration: 0,
        hideAnimationDuration: 0

    };

    var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
    gallery.init();
}