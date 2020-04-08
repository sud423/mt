var pageIndex=1;

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
		return isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Windows();
	}
};

/**
 * 我的帖子页面
 */
$(function(){


	$("#btnComment").click(function () {
		var toast = $(document).dialog({
			type : 'toast',
			infoIcon: '/images/loading.gif',
			infoText: '正在提交……'
		});
		if($("#content").val().trim()!=="") {
            $.post("/forum/reply", {
                replyId: $("#txtId").val(),
                articleId: $("#txtForumId").val(),
                content: $("#content").val(),
                topic: $("strong").text(),
                toUser: $("#lbluser").attr("rel"),
                url: $(".breadcrumb .active").attr("rel")
            }, function (data) {
                if (data.succeed) {
                    $("#content").val("");
                    getReplyList();

                    toast.update({
                        infoIcon: '/images/success.png',
                        infoText: data.result,
                        autoClose: 2500
                    });
                }
                else {

                    toast.update({
                        infoIcon: '/images/fail.png',
                        infoText: data.result,
                        autoClose: 2500
                    });
                }
            });
		}
	});

    if (!isMobile.any()) { //判断是否为android,BlackBerry,ios,windows
        //要执行的代码
        var tophtml = "<div id=\"izl_rmenu\" class=\"izl-rmenu\">"
            + "<div class=\"btn btn-logo\"><!--<img class=\"pic\" src=\"~/images/r_logo.png\" >--></div>"
            + "<a href=\"tencent://Message/?Uin=105339175&websiteName=www.5-ask.com=&Menu=yes\" class=\"btn btn-qq\"></a>"
            + "<div class=\"btn btn-phone\"><div class=\"phone\">15221256681</div></div>" + "<div class=\"btn btn-top\"></div></div>";
        $("#top").html(tophtml);
        $("#izl_rmenu").each(function () {
            $(this).find(".btn-wx").mouseenter(function () {
                $(this).find(".pic").fadeIn("fast");
            });
            $(this).find(".btn-wx").mouseleave(function () {
                $(this).find(".pic").fadeOut("fast");
            });
            $(this).find(".btn-phone").mouseenter(function () {
                $(this).find(".phone").fadeIn("fast");
            });
            $(this).find(".btn-phone").mouseleave(function () {
                $(this).find(".phone").fadeOut("fast");
            });
            $(this).find(".btn-top").click(function () {
                $("html, body").animate({
                    "scroll-top": 0
                }, "fast");
            });
        });
        var lastRmenuStatus = false;
        $(window).scroll(function () {//bug
            var _top = $(window).scrollTop();
            if (_top > 200) {
                $("#izl_rmenu").data("expanded", true);
            } else {
                $("#izl_rmenu").data("expanded", false);
            }
            if ($("#izl_rmenu").data("expanded") !== lastRmenuStatus) {
                lastRmenuStatus = $("#izl_rmenu").data("expanded");
                if (lastRmenuStatus) {
                    $("#izl_rmenu .btn-top").slideDown();
                } else {
                    $("#izl_rmenu .btn-top").slideUp();
                }
            }
        });
    }
});


var pages=0;
function getReplyList(){
    $.get("/forum/getReplies?id=" + $("#txtForumId").val() + "&page=" + pageIndex, function (data) {

        if (data.Count === 0)
            return;

        pages = data.totalPage;

        $(".page_num").text((data.currentPage) + "/" + data.totalPage);

        if (data.hasNextPage) {
            $("#btnNext").removeClass("disabled");
            $("#btnLast").removeClass("disabled");
        }
        else {
            $("#btnNext").addClass("disabled");
            $("#btnLast").addClass("disabled");
        }
        if (data.hasPreviousPage) {
            $("#btnFirst").removeClass("disabled");
            $("#btnPrevious").removeClass("disabled");
        }
        else {
            $("#btnFirst").addClass("disabled");
            $("#btnPrevious").addClass("disabled");
        }
        $("#J_reply_container").empty();
        $.each(data.data, function (index) {

            var arr = [];
            arr.push("<dl class=\"reply-list J-reply-" + this.id + "\" id=\"" + this.id + "\">");
            arr.push("<dd class=\"operations-user\">");
            arr.push("<div class=\"user-info\">");
            var nick = "我爱上课会员";
            var headImg = "/images/r_logo.png";
            if (this.user) {
                nick = this.user.nickName;
                headImg = this.user.headImgUrl;
            }

            arr.push("<div class=\"avatar\"><img style=\"width: 30px;\" src=\"" + headImg + "\" alt=\"用户头像\" class=\"img-circle\"></div>");
            arr.push("<a class=\"user-name\">" + nick + "</a>");
            arr.push("<div class=\"user-other\">");
            arr.push(this.createdAt);
            arr.push("</div>");
            arr.push("</div>");
            arr.push("<div class=\"operations\">");

            var agree = this.replyLikes.some(item => {
                return item.userId == $("#txtLoginId").val();
            });

            if (agree)
                arr.push("<a style='margin-right:5px;' onclick='agree(" + this.id + ",0)'>取消同意(" + this.likes + ")</a>&nbsp;");//亮了(100)
            else
                arr.push("<a style='margin-right:5px;' onclick='agree(" + this.id + ",1,\"" + this.user.externalLogin.openId+"\",this)'>同意(" + this.likes + ")</a>&nbsp;");//亮了(100)

            if (this.userId==$("#txtLoginId").val() || $("#txtUserId").val() == $("#txtLoginId").val()) {
                arr.push("<a style='margin-right:5px;' onclick='delReply(" + this.id + "," + this.forumId + ")'>删除</a>");//删除我的评论
            }

            if (this.userId == $("#txtLoginId").val()) {
                arr.push("<a href='#comment' style='margin-right:0px;' onclick='editReply(" + this.id + ",\"" + this.content + "\")'>编辑</a>");//删除我的评论
            }

            arr.push("<a></a>");//引用
            arr.push("</div>");
            arr.push("</dd>");
            arr.push("<dt class=\"reply-content\">");
            arr.push("<div class=\"current-content\">");
            arr.push("<span class=\"short-content\">" + this.content + "</span>");
            arr.push("</div>");
            arr.push("</dt>");
            arr.push("<dd class=\"reply-bt\"></dd>");
            arr.push("</dl>");

            $("#J_reply_container").append(arr.join(""));
        });

        // console.log(data);
    });
}

/**
 * 首页
 */
function first() {
    if ($("#btnFirst").attr("class") !== "disabled") {
        pageIndex = 1;
        getReplyList();
    }
}

/**
 * 上一页
 */
function pre() {
    if ($("#btnPrevious").attr("class") !== "disabled") {
        pageIndex +=1;
        getReplyList();
    }
}

/**
 * 下一页
 */
function next() {
    if ($("#btnNext").attr("class") !== "disabled") {
        pageIndex -= 1;
        getReplyList();
    }
}

/**
 * 最后一页
 */
function last() {
    if ($("#btnLast").attr("class") !== "disabled") {
        pageIndex = pages;
        getReplyList();
    }
}

/**
 * 删除发布
 * @param {any} id 帖子编号
 */
function del(id){

	$(document).dialog({
		type : 'confirm',
		style: 'android',
		titleText: '信息',
		content: '您确定要删除数据？',
		closeBtnShow: true,
		onClickConfirmBtn: function () {
			var toast = $(document).dialog({
				type : 'toast',
				infoIcon: '/images/loading.gif',
				infoText: '正在提交……'
			});
			$.post("/forum/delete?id="+id,function (data) {

				if(data.succeed){
					toast.update({
						infoIcon: '/images/success.png',
						infoText: "删除成功，正在跳转到列表",
						autoClose: 2500,
						onClosed:function () {
							window.location.href="/forum/list";
						}
					});
				}
				else{

					toast.update({
						infoIcon: '/images/fail.png',
						infoText: "删除失败，原因："+data.msg,
						autoClose: 2500
					});
				}

			});
		}
	});


}

/**
 * 同意
 * @param {any} id 回复编号
 * @param {any} tag 0:取消，1：同意
 * @param {any} toUser 通知回复人
 * @param {any} obj 点击的dom节点对象
 */
function agree(id, tag,toUser,obj) {
    var d = {
        url: $(".breadcrumb .active").attr("rel"),
        toUser: toUser,
        replyId: id,
        content: $("#" + id +" .short-content").text(),
        topic: $("strong").text(),
        nickName: $("#"+id+" .user-name").text()
    };
    $.post("/forum/agree",d, function (data) {

        if (data.succeed && tag===1) {
            $.tipsBox({
                obj: $(obj),
                str: "+1",
                callback: function () {

                }
            });
        }

        if (data.succeed) {
            getReplyList();
        }

    });
}

(function ($) {
	$.extend({
		tipsBox: function (options) {
			options = $.extend({
				obj: null,  //jq对象，要在那个html标签上显示
				str: "+1",  //字符串，要显示的内容;也可以传一段html，如: "<b style='font-family:Microsoft YaHei;'>+1</b>"
				startSize: "12px",  //动画开始的文字大小
				endSize: "30px",    //动画结束的文字大小
				interval: 600,  //动画时间间隔
				color: "red",    //文字颜色
				callback: function () { }    //回调函数
			}, options);
			$("body").append("<span class='num'>" + options.str + "</span>");
			var box = $(".num");
			var left = options.obj.offset().left + options.obj.width() / 2;
			var top = options.obj.offset().top - options.obj.height();
			box.css({
				"position": "absolute",
				"left": left + "px",
				"top": top + "px",
				"z-index": 9999,
				"font-size": options.startSize,
				"line-height": options.endSize,
				"color": options.color
			});
			box.animate({
				"font-size": options.endSize,
				"opacity": "0",
				"top": top - parseInt(options.endSize) + "px"
			}, options.interval, function () {
				box.remove();
				options.callback();
			});
		}
	});
})(jQuery);

/**
 * 删除回复
 * @param {any} id 回复编号
 * @param {any} forumId 帖子编号
 */
function delReply(id,forumId) {
	$(document).dialog({
		type : 'confirm',
		style: 'android',
		titleText: '信息',
		content: '您确定要删除评论？',
		closeBtnShow: true,
		onClickConfirmBtn: function () {
			var toast = $(document).dialog({
				type : 'toast',
				infoIcon: '/images/loading.gif',
				infoText: '正在提交……'
			});
			$.post("/forum/delreply?id="+id,function (data) {

				toast.update({
					infoIcon: '/images/success.png',
					infoText: data.result,
					autoClose: 2500,
					onClosed:function () {
						if(data.succeed)
							getReplyList();
					}
				});

			});
		}
	});
}

/**
 * 编辑回复
 * @param {any} id 帖子编号
 * @param {any} content 回复内容
 */
function editReply(id, content) {
	$("#txtId").val(id);
	$("#content").val(content);
}