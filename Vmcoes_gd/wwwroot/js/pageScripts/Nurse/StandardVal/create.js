jQuery(function () {
    'use strict';

    var ajaxData = {}, ysgs = true;;

    //绑定
    _bind();

    _init();

    function _init() {
     
    }

    function _bind() {
        /**  保存 */
        $('#btnSave1').click(function () {
            _save($(this), $(this).text());
            //_buildSubData();
        });
    }
   
      
     





    //提交表单
    function _save($target, btnText) {
        _buildSubData();
        bootbox.dialog({
            title: "",
            message: '<div class="row">  ' +
                '<div class="col-xs-12"> ' +
                '请确认是否提交表单记录？' +
                '</div></div>',
            buttons: {
                cancel: {
                    label: "取消",
                    className: "btn-cancel",
                    callback: $.noop
                },
                confirm: {
                    label: "确定提交",
                    className: "btn-success",
                    callback: function () {
                        jQuery.ajax({
                            dataType: "json",
                            url: "/Home/Save",
                            data: ajaxData,
                            type: "POST",
                            success: function (result) {
                                var that = $('#indexTab tbody').find("tr").eq(0);
                                var c = that.find("td:eq(0) input").val();//取第一个日期
                                if (result.code == 1) {
                                    FOXKEEPER_UTILS.alert('success', '操作成功');
                                    setTimeout(function () {
                                        location.replace('/Home/Index?u=' + c);
                                    }, 1500);
                                //} else if (result.code == -1) {
                                //    FOXKEEPER_UTILS.alert('warning', '同一天不能添加相同的地址');
                                //    $target.html(btnText).attr("disabled", false);
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
                        return true;
                    }
                }
            }
        })
    }

    function _buildSubData() {
        var tag = false;
        //获取table的每一行
        var $rows = $('#indexTab tbody').find("tr");
        var items = [];
        $rows.each(function (i,v) {
            var $that = $(this);
            var t1 = $that.find("td:eq(0) input").val();//获取日期
            var t2 = $that.find("td:eq(1) input").val();//获取时间
            var t3 = $that.find("td:eq(2) span").text();//获取工地名称
            var t4 = $that.find("td:eq(2) span").attr("code");//获取工地代码
            var t5 = $that.find("td:eq(3) span").text();//获取工地地址
            var t6 = $that.find("td:eq(4) input").val();//获取车辆
            var t7 = $that.find("td:eq(5) select").val();//是否冲洗
            var t8 = $that.find("td:eq(6) select").val();//是否密闭
            var t9 = $that.find("td:eq(7) select").val();//获取办证
            var t10 = $that.find("td:eq(8) select").find("option:selected").text();//获取公司
            //var tt10 = $that.find("td:eq(8) select").find("option:selected").val("value")
            var item = {};//一个什么也没有值的对象
            //item.inputRiqi = t1;
            item.inputTime = t1 +" "+ t2;//日期加时间=完整的日期时间
            item.date = t1;
            item.time = t2;
            item.constCode = t4;
            item.constName = t3;
            item.constAddr = t5;
            item.trans = t6;
            item.rinse = t7;
            item.airtight = t8;
            item.certificate = t9;
            item.transportcompanyName = t10;
            items.push(item);
        });
        console.log(JSON.stringify(items));
        ajaxData.vms = items;
    }

       
    
});