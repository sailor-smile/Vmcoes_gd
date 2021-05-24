jQuery(function () {
    'use strict';
    

    var queryParams = {
        pageNo: 1,
        pageSize: 20
    };

    //绑定事件
    _bind();
    //初始化
    _init();

    function _init() {
        _initData();
    }

    function _bind() {
     
    }

    function _initData() {
        _setAjaxData();
        jQuery.ajax({
            dataType: "json",
            url: "/Grade/FindGredNumber",
            data: queryParams,
            type: "POST",
            success: function (rs) {
                if (rs != null) {
                    var $dateList = $('#dataList');
                    //alert("成功")
                    $dateList.find('tbody').html("");
                    //$('html').scrollTop(0);
                    $("#listTpl").tmpl(rs).appendTo('#dataList tbody');
                    _bindDelEvent();
                } 
            }
        });
    }

    //提交保存数据
    function _bindDelEvent() {
        //删除

        $('.opt-del').on('click', function () {
            alert($(this).attr("ref"));
            _del($(this).attr("ref"));

        });
        //跳转到修改页面
        $('.opt-edit').on('click', function () {
            var bid = $(this).attr('ref');
            window.location.href = "/Grade/Edit?d=" + window.btoa(bid);
        });
    }

    //删除字典
    function _del(id) {
        bootbox.dialog({
            title: "",
            message: '<div class="row">  ' +
                '<div class="col-xs-12"> ' +
                '请确认是否删除此条记录吗?？' +
                '</div></div>',
            buttons: {
                cancel: {
                    label: "取消",
                    className: "btn-cancel",
                    callback: $.noop
                },
                confirm: {
                    label: "确定",
                    className: "btn-success",
                    callback: function () {
                        jQuery.ajax({
                            dataType: "json",
                            url: '/Grade/Delete?id=' + id,
                            data: null,
                            type: "POST",
                            success: function (result) {
                                if (result.code == 1) {
                                    FOXKEEPER_UTILS.alert('success', '删除成功');
                                    setTimeout(function () {
                                        location.replace('/Grade/Index');
                                    }, 1500);
                                } else {
                                    alert("删除成功")
                                    location.replace('/Grade/Index');
                                }
                            }
                        });
                        return true;
                    }
                }
            }
        })
    }


    function _setAjaxData() {
        
    }

    
});
