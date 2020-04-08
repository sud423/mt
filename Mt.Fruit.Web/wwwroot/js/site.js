/**
 * 网站脚本
 */


var categoryId = 0;

$(function () {
    $("#btn_preview").click(function () {
        if ($("#Title").val() === '') {
            alert("标题不能为空");
            return;
        }
        $("#editor_id").val(editor.html());
        $.post("/my/save?isPerview=true", $("#from1").serialize(), function (e) {
            window.open("/detail/" + e.msg);
            $("#Id").val(e.msg);
        });
        
    });
});


//分页功能
var setPaginator = function (data,t,s) {
    $('.pagination').bootstrapPaginator({
        bootstrapMajorVersion: 3, //对应bootstrap版本
        size: 'small', //分页大小
        currentPage: data.currentPage, //当前页
        numberOfPages: 3, //显示的页数
        totalPages: data.totalPage, // 总页数
        /**
         * 分页点击事件
         * @param {any} event [jquery对象]
         * @param {any} originalEvent [dom原生对象]
         * @param {any} type [按钮类型]
         * @param {any} page [点击按钮对应的页码]
         */
        onPageClicked: function (event, originalEvent, type, page) {
            switch (t) {
                case 1:
                    getResource(page,s);
                    break;
                case 2:
                    getCategoryPage(page);
                    break;
                case 3:
                    getMyArticles(page);
                    break;
                default:
                    getArticles(page);//根据点击页数渲染页面
                    break;
            }
            
        }
    });
};

function getArticles(page) {
    $.get("/list/" + categoryId + "?page=" + page, function (res) {
        switch (categoryId) {
            case 43:
                render1(res.data);
                break;
            default:
                render(res.data);
                break;
        }

        setPaginator(res);
    });
}

function render(data) {

    $(".dl-list").empty();
    $(".table-bordered tbody").empty();
    var html = [];
    var arr = [];
    $.each(data, function (i) {
        arr.push("<dt title='" + this.title + "'><a href='/detail/" + this.id + "'>");
        arr.push(this.title + ' ' );
        arr.push("</a><span>" + this.replys + "/" + this.clicks+"</span></dt>");
        var row = $("#trExample").html();
        if (row) {
            html.push("<tr>");
            row = row.replace("{4}", this.id);
            row = row.replace("{0}", this.title);
            row = row.replace("{1}", this.author);
            row = row.replace("{2}", this.replys + "/" + this.clicks);
            row = row.replace("{3}", this.lastReplyUser == null ? "" : this.lastReplyUser);
            html.push(row);
            html.push("</tr>");
        }
    });
    $(".dl-list").html(arr.join(""));
    $(".table-bordered tbody").html(html.join(""));
}

function render1(data) {
    $("#list").empty();
    var html = [];
    $.each(data, function (i) {
        html.push("<div class='row mb-3'>");
        var row = $("#example").html();

        if (this.cover) {
            row = row.replace("{4}", this.cover);
        }
        else {
            row = row.replace("{4}", "data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiIHN0YW5kYWxvbmU9InllcyI/PjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB3aWR0aD0iNjQiIGhlaWdodD0iNjQiIHZpZXdCb3g9IjAgMCA2NCA2NCIgcHJlc2VydmVBc3BlY3RSYXRpbz0ibm9uZSI+PCEtLQpTb3VyY2UgVVJMOiBob2xkZXIuanMvNjR4NjQKQ3JlYXRlZCB3aXRoIEhvbGRlci5qcyAyLjYuMC4KTGVhcm4gbW9yZSBhdCBodHRwOi8vaG9sZGVyanMuY29tCihjKSAyMDEyLTIwMTUgSXZhbiBNYWxvcGluc2t5IC0gaHR0cDovL2ltc2t5LmNvCi0tPjxkZWZzPjxzdHlsZSB0eXBlPSJ0ZXh0L2NzcyI+PCFbQ0RBVEFbI2hvbGRlcl8xNmRiYTFmMzk0MSB0ZXh0IHsgZmlsbDojQUFBQUFBO2ZvbnQtd2VpZ2h0OmJvbGQ7Zm9udC1mYW1pbHk6QXJpYWwsIEhlbHZldGljYSwgT3BlbiBTYW5zLCBzYW5zLXNlcmlmLCBtb25vc3BhY2U7Zm9udC1zaXplOjEwcHQgfSBdXT48L3N0eWxlPjwvZGVmcz48ZyBpZD0iaG9sZGVyXzE2ZGJhMWYzOTQxIj48cmVjdCB3aWR0aD0iNjQiIGhlaWdodD0iNjQiIGZpbGw9IiNFRUVFRUUiLz48Zz48dGV4dCB4PSIxMi41IiB5PSIzNi41Ij42NHg2NDwvdGV4dD48L2c+PC9nPjwvc3ZnPg==");
        }
        row = row.replace("{0}", this.title);
        row = row.replace("{1}", this.replys);
        row = row.replace("{2}", this.likes);
        row = row.replace("{3}", this.lead);
        row = row.replace("{5}", this.id);
        html.push(row);

        html.push("</div>");
    });

    $("#list").html(html.join(""));
}


function getResource(page, size) {
    $.get("/list/" + categoryId + "/" + page + "?size=" + size, function (res) {
        switch (categoryId) {
            case 44:
                render2(res.data);
                break;
            default:
                render3(res.data);
                break;
        }

        setPaginator(res, 1,size);
    });
}

function render2(data) {
    $("#list").empty();
    var html = [];
    $.each(data, function (i) {
       
        var row = $("#example").html();

        row = row.replace("{0}", this.src);
        row = row.replace("{1}", this.title);
        row = row.replace("{2}", this.id);
        row = row.replace("{2}", this.id);
        html.push(row);

    });

    $("#list").html(html.join(""));
}

function render3(data) {
    $("#list").empty();
    var html = [];
    $.each(data, function (index) {
        var clsName = "";

        if (index === 0 || index === 3)
            clsName = "first-child";
        if (index === 2)
            clsName = "last-child";

        html.push("<div class=\"col-lg-4 " + clsName + "\" > ");

        html.push('<img id=' + this.id + ' onclick="openPhotoSwipe(this)" style="max-height:210px;" src="' + this.src + '" alt="' + this.title + '" />');

        html.push("</div>");

    });

    $("#list").html(html.join(""));
}

function read(id) {
    $.get("/read/" + id, function (res) { });
    $("video").each(function () {
        this.pause();
    });
    var src=$("#" + id).attr("src");

    $('.videos').html("<video id=\"video\" src='" + src + "' preload=\"auto\" controls=\"controls\" autoplay=\"autoplay\"></video><img onClick=\"close1()\" class=\"vclose\" src=\"/images/gb.png\" width=\"25\" height=\"25\"/>");
    $('.videos').show();
}


function openPhotoSwipe(_img) {
    var items = [{ src: _img.src, w: _img.naturalWidth, h: _img.naturalHeight, id: _img.id, title:_img.alt }];
    var imgs = $("#list").find("img");
    if (imgs.length > 0) {
        // build items array
        $.each(imgs, function (i, img) {
            if (img.src != _img.src) {
                items.push({
                    src: img.src,
                    w: img.naturalWidth,
                    h: img.naturalHeight,
                    id: img.id,
                    title: img.alt
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

    var pswp = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
    pswp.listen("afterChange", function () {
        var item = pswp.currItem;
        $.get("/read/" + item.id, function (res) { });
        
    });

    pswp.init();

}


function getCategoryPage(page) {
    $.get("/my/categories?page=" + page, function (res) {
        $("#list").empty();
        var html = [];
        $.each(res.data, function () {
            html.push("<tr>");
            html.push('<td style="width:20%">' + this.name + '</td>');
            html.push('<td>' + this.fllows + '</td>');
            html.push('<td style="text-align:left;">' + this.descript + '</td>');
            html.push('<td style="padding-right:25px;"><a href="/my/category/' + this.id +'">编辑</a></td>');
            html.push("</tr>");
        });
        $("#list").html(html.join(""));
        setPaginator(res, 2);
    });
}

var t = "image";

function getResources(page) {
    $.get("/my/getresources?type="+t+"&page=" + page, function (res) {
        $("#list").empty();
        var html = [];
        $.each(res.data, function () {
            html.push("<tr>");
            html.push('<td>' + this.title + '</td>');
            html.push('<td>' + this.src + '</td>');
            html.push('<td><a href="/my/resource?id=' + this.id + '">编辑</a></td>');
            html.push("</tr>");
        });
        $("#list").html(html.join(""));
        setPaginator(res, 2);
    });
}

function getMyArticles(page) {
    $.get("/my/getarticles?page=" + page, function (res) {
        $("#list").empty();
        var html = [];
        $.each(res.data, function () {
            html.push("<tr>");
            html.push('<td>' + this.category.name + '</td>');
            html.push('<td>' + this.title + '</td>');
            if (this.categoryId === 43)
                html.push('<td><a href="/my/post?id=' + this.id + '">编辑</a></td>');
            else
                html.push('<td><a href="/my/create?id=' + this.id + '">编辑</a></td>');
            html.push("</tr>");
        });
        $("#list").html(html.join(""));
        setPaginator(res, 3);
    });
}

function close1() {
    var v = document.getElementById('video');//获取视频节点
    $('.videos').hide();//点击关闭按钮关闭暂停视频
    v.pause();
    $('.videos').html();
}