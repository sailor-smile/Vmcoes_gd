jQuery(function () {
    'use strict';

       
      
    var queryParams = {
        pageNo: 1,
        pageSize: 50
    };

    var ajaxdate = {};
  
    _init();
    function _init() {
        var def_date = $('#rqrqrq').val();
        if (!def_date) {
            def_date = _getCurrdate();
        } 
        $('#stDate1').val(def_date);
    }


    function _getCurrdate() {
        // 当前时间
        var myDate = new Date;
        var year = myDate.getFullYear(); //获取当前年
        var mon = myDate.getMonth() + 1; //获取当前月
        var date = myDate.getDate(); //获取当前日
        var timeing = year + "-" + mon + "-" + date;
        return timeing;
    }


    function _setAjaxData() {
        ajaxdate.gdrq = $('#stDate1').val();
    }
  
    _initTData();
      
  
    //搜索
    $('#sousuo').on('click', function () {
        _initTData();
    });


   



 
    //按日期搜索
    function _initTData() {
        _setAjaxData();
            jQuery.ajax({
                dataType: "json",
                url: "/Home/FindVmcoesGdorTimes",
                data: ajaxdate,
                type: "POST",
                success: function (rs) {
                    if (rs.code == 1) {
                        var $dataList = $('#dataList');
                        var list = rs.data.data;
                        $dataList.find('tbody').html('');
                        if (list != null && list.length > 0) {
                            $("#listTpl").tmpl(list).appendTo('#dataList tbody');
                            _bindDelEvent();
                        } else {
                        }
                        $('html').scrollTop(0);
                    } 
                },
                beforeSend: function () {
                },
                error: function () {
                }
            });
        
    }

    /************************************************ */



    //按当天搜索
    function _initData() {
        jQuery.ajax({
            dataType: "json",
            url: "/Home/FindVmcoesGdorNowadaysTime",
            data: queryParams,
            type: "POST",
            success: function (rs) {
                if (rs.code == 1) {
                    var $dataList = $('#dataList');
                    var list = rs.data.data;
                    $dataList.find('tbody').html('');
                    if (list != null && list.length > 0) {
                        $("#listTpl").tmpl(list).appendTo('#dataList tbody');
                        _bindDelEvent();

                    } else {
                    }
                    $('html').scrollTop(0);
                } else {
                    //异常
                }
            },
            beforeSend: function () {
            },
            error: function () {
                alert('系统异常');
            }

        });

    }

    function _huoq() {
        var nameTd = $table.find("tr").eq(0).children("td").eq(0);

    }


    //提交保存数据
    //事件绑定
    function _bindDelEvent() {
   
        //删除
        $('.opt-del').on('click', function () {
            _del($(this).attr("ref"));
        });
        //跳转到修改页面
        $('.opt-edit').on('click', function () {
            var bid = $(this).attr('ref');

            window.location.href = "/Home/modify?d=" + bid;
        });
    }

    //删除字典
    function _del(id) {
        bootbox.dialog({
            title: "",
            message: '<div class="row">  ' +
                '<div class="col-xs-12"> ' +
                '请确认是否删除此条记录吗？' +
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
                            url: '/Home/Delete?id=' + id,
                            data: null,
                            type: "POST",
                            success: function (result) {
                                if (result.code == 1) {
                                    setTimeout(function () {
                                        FOXKEEPER_UTILS.alert('success', '删除成功');
                                    }, 0);
                                    setTimeout(function () {
                                        var $dataList = $('#dataList');
                                        $dataList.find('tbody').html('');
                                        _initTData();
                                    }, 0);
                                } else {
                                    //失败
                                }
                            }
                        });
                        return true;
                    }
                }
            }
        })
    }
   
    


        
        
        });
