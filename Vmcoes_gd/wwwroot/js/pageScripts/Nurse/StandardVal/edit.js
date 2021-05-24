jQuery(function () {
    'use strict';
    var ajaxData = {};
    //绑定事件
    _bind();
    _bind1();
    //初始化
    _getgdname();
        //加载内容
    _initData($('#dataId').val());
    function _bind() {
        ////时间输入框数据同步
        //$("#stDate1").on("change", function () {
        //    var $vue = $(this).text();
        //    $("#indexTab").$(".timerq").text($vue);
        //});

        //工地和工地地址输入同步
        
        $("#wokao1").on("change", function () {
            if (!$(this).val()) return;
            var constName = $(this).find("option:selected").text();//获取选中的工地名
            //获取工地名里面的span标签元素
            var $gname_span = $("#indexTab").find(".jiuge3");
            var consCode = $(this).val();//获取工地代码
            $gname_span.attr('code', consCode);
            $gname_span.text(constName);
            var shux = $(this).find("option:selected").attr("adr");//获取工地地址
            $("#indexTab").find(".gddz").text(shux);
        });

    }

   
    //动态获取下拉框值和地址
    function _getgdname() {
        jQuery.ajax({
            dataType: "json",
            url: "/Home/fingd",
            type: "POST",
            success: function (rs) {
                var $gdname = $('#wokao1');
                var data = rs.data;
                for (var i = 0; i < data.length; i++) {
                    var name = data[i].constName;
                    var cord = data[i].constCode;
                    var addr = data[i].constAddr;
                    $gdname.append('<option adr="' + addr + '" value="' + cord + '">' + name + '</option>');
                }
            },
            beforeSend: function () {
            },
            error: function () {
                alert('系统异常');
            }
        });
    }

    //根据id加载一行数据
    function _initData(id) {
        jQuery.ajax({
            dataType: "json",
            url: "/Home/GetVmcogd?dataId=" + id,
            data: null,
            type: "GET",
            success: function (result) {
                if (result.code == 1) {
                    var o = result.data;

                    $('#rqtime').val(o.date);
                    $('#sjtime').val(o.time);
                    $('#indexTab').find(".jiuge3").text(o.constName);
                    $('#indexTab').find(".gddz").text(o.constAddr);
                    $('#trans').val(o.trans);
                    $('#rinse').val(o.rinse);
                    $('#airtight').val(o.airtight);
                    $('#certificate').val(o.certificate);
                    $('#transportcompanyName').val(o.transportcompanyName);
                } else {
                    FOXKEEPER_UTILS.alert('danger', '程序出错.');
                }
            },
            error: function () {
                FOXKEEPER_UTILS.alert('danger', '程序出错.');
            }
        });
    }

    function _setAjaxData() {
        ajaxData.date = $('#rqtime').val();
        ajaxData.time = $('#sjtime').val();
        ajaxData.constName = $('#indexTab').find(".jiuge3").text();
        ajaxData.constAddr = $('#indexTab').find(".gddz").text();
        ajaxData.trans = $('#trans').val();
        ajaxData.rinse = $('#rinse').val();
        ajaxData.airtight = $('#airtight').val();
        ajaxData.certificate = $('#certificate').val();
        ajaxData.id = $('#dataId').val();
        ajaxData.transportcompanyName = $('#transportcompanyName').val();

    }

    function _bind1() {
        /**  保存 */
        $('#btnSave1').click(function () {
            _save($(this), $(this).text());
        });
    }


    //提交表单
    function _save($target, btnText) {
        _setAjaxData();
        jQuery.ajax({
            dataType: "json",
            url: "/Home/Update",
            data: ajaxData,
            type: "POST",
            success: function (result) {
                if (result.code == 1) {
                    var that = $('#indexTab tbody').find("tr").eq(0);
                    var u = that.find("td:eq(0) input").val();//取第一个日期
                    FOXKEEPER_UTILS.alert('success', '操作成功');
                    setTimeout(function () {
                        location.replace('/Home/Index?u=' + u);
                    }, 1000);
                } else if (result.code == -1) {
                    FOXKEEPER_UTILS.alert('warning', '状态为-1');
                    $target.html(btnText).attr("disabled", false);
                } else {
                    FOXKEEPER_UTILS.alert('warning', '操作失败');
                    $target.html(btnText).attr("disabled", false);
                }
            },
            beforeSend: function () {
                $target.html('<i class=\"fa fa-spinner\"></i>&nbsp;正在保存').attr("disabled", "disabled");
            },
            error: function () {
                FOXKEEPER_UTILS.alert('danger', '系统异常');
            }
        });
    }




});


