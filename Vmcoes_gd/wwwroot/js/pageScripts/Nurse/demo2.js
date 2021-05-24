jQuery(function () {
    'use strict';

    var ajaxdata = {}, delopt;

    //绑定事件
    _bind();
    //初始化
    _init();
    _getgdname();
    _ychang();

    function _ychang() {
        $(".shijianuxanzhe").hide();
        $(".gdxuanzhekuang").hide();
    }

    function _init() {
        $('#option').hide();
        //_initData();
    }
    function _bind() {
        //提交按钮事件
        //$('#btnSave').on('click', function () {
        //    _save();
        //});

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
       
         //创建行时间
        $('#addRow').on('click', function () {
            $(".shijianuxanzhe").show();
            $(".gdxuanzhekuang").show();
            var cls = $("#indexTab thead th").length;//列
            var ros = $("#indexTab tbody tr").length;//行
            if (ros == 0) {
                $('#option').show();
            } 
            _crtImprotDataRow(cls - 1, ros);
        });
        //业务重置表单
        $('#resetBtn').on('click', function () {
            $("#indexTab tbody tr").remove();
            $('#option').hide();
            $('#addRow').show();
            $('.gdxuanzhekuang').hide();
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

    








    //创建导入行和列
    function _crtImprotDataRow(len, rowNo) {
        //var currDate = moment().format("MM");
        var htm = '<tr>';
        for (var i = 0; i < len; i++) {

            if (i == 0) {
                //var time = $("#stDate1").attr("value");
                htm += '<td><span class="timerq"><input class="nowtimes" style="width: 96%;" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd\'});" value="" required/></span>';
            } else if (i == 1) {
                console.log($('#mes1').html());
                htm += '<td align="center" style="border-color: #e1e1e1;width: 5%;" ><input type="text" class="times" /></td>';
            }
            else if (i == 2) {
                htm += '<td align="center"  style="border-color: #e1e1e1;width: 15%;" ><span class="jiuge3" ></span></td>';
            }
            else if (i == 3) {
                htm += '<td align="center" style="border-color: #e1e1e1;width: 15%;" ><span class="gddz"></span></td>';
            }
            else if (i == 4) {
                htm += '<td align="center"><input class="yscl" /></td>';
            }

            else if (i == 5) {
                htm += '<td>   <select class="opt5" style="border-color: #e1e1e1;" ><option text="0" value="0"  selected>是</option><option text="1" value="1" >否</option></td>';
            }
            else if (i == 6) {
                htm += '<td align="center">   <select class="opt6" style="border-color: #e1e1e1;" ><option text="0" value="0" selected>是</option><option text="1" value="1">否</option></td>';
            }
            else if (i == 7) {
                htm += '<td align="center">   <select class="opt7" style="border-color: #e1e1e1;width: 100%;" ><option text="" value="" selected></option><option text="0" value="0" >是</option><option text="1" value="1">否</option></td>';
            }
            else if (i == 8) {
                htm += '<td align="center" style="border-color: #e1e1e1;width: 15%;" ><select class="opt8" style="border-color: #e1e1e1;width:90%;"><option text = "" value = "" ></option ><option text="0" value="0">营润</option> <option text="1" value="1">禹程</option><option text="2" value="2">勤顺</option><option text="3" value="3">健徽</option> <option text ="4" value ="4">世滨</option ><option text="5" value="5">陆通</option><option text="6" value="6">普环</option><option text="7" value="7">明路</option><option text="8" value="8">儒林</option></select ></td>';
            }  
            else {
                htm += '<td><input style="width: 96%;" /></td>';
            }
        }
        htm += _crtDelBtn();
        htm += '</tr>';
        $("#indexTab tbody").append(htm);
        $("#indexTab tbody").append(htm);
        $("#indexTab tbody").append(htm);
        $("#indexTab tbody").append(htm);
        $("#indexTab tbody").append(htm);
        _bindDelRowEvent(rowNo);
        _bindTimeEvent();
        _NowTime();
    }

    function _bindDelRowEvent(rowNo) {
        $('#indexTab tbody tr').find('.opt-del').on('click', function () {
            $('#addRow').show();
            var rows = $("#indexTab tbody tr").length;
            if (rows == 1) {
                $('#option').hide();
            } 
            $(this).parents('tr').remove();
        });
    }

    //表格的删除行功能
    function _crtDelBtn() {
        if (typeof (delopt) == 'undefined') {
            delopt = '<td>';
            delopt += $('#delTd').html();
            delopt += '</td>';
        }
        return delopt;
    }

    function _bindTimeEvent() {
        //时间控件
        $('.times').timepicker({
            defaultTime: '',
            showInputs: true,
            minuteStep: 1,
            showMeridian: false,
            maxHours: 24,
        });
    }

    function _NowTime() {
            $(function () {
        var myDate = new Date;
        var year = myDate.getFullYear(); //获取当前年
        var mon = myDate.getMonth() + 1; //获取当前月
        var date = myDate.getDate(); //获取当前日
        var timeing = year + "-" + mon + "-" + date
        $(".nowtimes").attr("value", timeing);
    });

    }




});
