

jQuery(function () {
    'use strict';
    var ajaxData = {}, ajaxdatac = {}, ysgs = true;
    var queryParams = {
        pageNo: 1,
        pageSize: 100
    };

    var tempGrp1;
    var tempLen;
    
    //渲染
    _init();
    //绑定
    _bind();
    //绑定事件
    //_getgdname();
    function _init() {

        //$('#dataList tbody').find("tr").find("td:eq(0) ").text();
        _initData();
    }
    //查看当天的数据
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
                        var datas = [];
                        var temp = [];//分组后的数组
                        var prev;
                        var len = list.length, lastLen = list.length - 1;
                        for (var i = 0; i < len; i++) {
                            var data = list[i];
                            var curr = data.date + '' + data.constCode; //根据时间和工地字符串相同
                            //&& i != lastLen
                            if (i == 0) {
                                temp.push(data);
                            } else if (i > 0) {
                                if (curr == prev) {
                                    temp.push(data);
                                    if (i == lastLen) {//最后一条的时候
                                        datas.push(temp);
                                    }
                                } else {
                                    datas.push(temp);//说明上个组已经，存起来
                                    temp = [];//清空上一组的数据
                                    temp.push(data);
                                }
                            }
                            prev = curr;
                        }
                        //console.log(datas);//打印分组数据
                        var grpedDatas = datas;
                        var htm = "";
                        for (var i = 0; i < grpedDatas.length; i++) {
                            var grp = grpedDatas[i];
                            for (var k = 0; k < grp.length; k++) {
                                if (k == 0) {
                                    var grpHtm = '<tr align="center" style="">';
                                    grpHtm += '<td class="t1" width="4%"   rowspan="' + grp.length + '" style="  vertical-align: middle; font-size:5px; ">' + (i + 1) + '</td>';
                                    if (grp[0].datein == null) { grpHtm += '<td width="5%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px; "></td>'; }
                                    else { grpHtm += '<td width="5%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px;  ">' + grp[0].datein + '</td>'; }

                                    if (grp[k].time == null) { grpHtm += '<td width="6%" style="vertical-align: middle; font-size:5px;  "></td>'; }
                                    else { grpHtm += '<td width="6%" style="vertical-align: middle; font-size:5px;  ">' + grp[k].time + '</td>'; }
                                    if (grp[0].constName == null) { grpHtm += '<td width="15%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px; "></td>'; }
                                    else { grpHtm += '<td width="15%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px;  ">' + grp[0].constName + '</td>'; }

                                    if (grp[0].constAddr == null) { grpHtm += '<td width="15%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px; "></td>'; }
                                    else { grpHtm += '<td width="15%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px;  ">' + grp[0].constAddr + '</td>'; }


                                    if (grp[k].trans == null) { grpHtm += '<td width="12%" style=" vertical-align: middle; text-align: center;font-size:5px;  " ></td>'; }
                                    else { grpHtm += '<td width="12%" style=" vertical-align: middle; text-align: center;font-size:5px;  " >' + grp[k].trans + '</td>'; }

                                    if (grp[k].rinse == 0) { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px;  ">是</td>'; }
                                    else { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px;  ">否</td>'; }
                                    if (grp[k].airtight == 0) { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px; ">是</td>'; }
                                    else { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px;">否</td>'; }
                                    if (grp[k].certificate == 0) { grpHtm += '<td width="8%" style=" vertical-align: middle; text-align: center;font-size:5px; ">是</td>'; }
                                    else if (grp[k].certificate == 1) { grpHtm += '<td width="8%" style=" vertical-align: middle; text-align: center;font-size:5px;  ">否</td>'; }
                                    else { grpHtm += '<td width="8%"></td>' }

                                    if (grp[k].transportcompanyName == null) { grpHtm += '<td width="13%"></td>'; }
                                    else { grpHtm += '<td width="13%" style=" vertical-align: middle; text-align: center;font-size:5px;">' + grp[k].transportcompanyName + '</td>'; }
                                    grpHtm += '<td  id="niana" hidden >' + grp[0].nian + '</td>';//年度
                                    grpHtm += '</tr>';

                                } /*else if (i==3) { grpHtm += '<tr style="page -break-after: always;"><td colspan="10"><div style="page-break-after:always;"></div></td></tr>'; }*/
                                else {
                                    var grpHtm = '<tr align="center" style=" ">';
                                    if (grp[k].time == null) { grpHtm += '<td width="6%" style="vertical-align: middle; font-size:5px;"></td>'; }
                                    else { grpHtm += '<td width="6%" style="vertical-align: middle; font-size:5px;">' + grp[k].time + '</td>'; }
                                    if (grp[k].trans == null) { grpHtm += '<td width="12%" style=" vertical-align: middle; text-align: center;font-size:5px;" ></td>'; }
                                    else { grpHtm += '<td width="12%" style=" vertical-align: middle; text-align: center;font-size:5px;" >' + grp[k].trans + '</td>'; }
                                    if (grp[k].rinse == 0) { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px;">是</td>'; }
                                    else { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px;">否</td>'; }

                                    if (grp[k].airtight == 0) { grpHtm += '<td width="10%" style=" vertical-align: middle; text-align: center;font-size:5px;">是</td>'; }
                                    else { grpHtm += '<td width="10%" style=" vertical-align: middle; text-align: center;font-size:5px;">否</td>'; }

                                    if (grp[k].certificate == 0) { grpHtm += '<td width="8%" style=" vertical-align: middle; text-align: center;font-size:5px;">是</td>'; }
                                    else if (grp[k].certificate == 1) { grpHtm += '<td width="8%" style=" vertical-align: middle; text-align: center;font-size:5px;">否</td>'; }
                                    else { grpHtm += '<td width="8%"></td>' }

                                    if (grp[k].transportcompanyName == null) { grpHtm += '<td width="13%"></td>'; }
                                    else { grpHtm += '<td width="13%" style=" vertical-align: middle; text-align: center;font-size:5px;">' + grp[k].transportcompanyName + '</td>'; }
                                    grpHtm += '</tr>';
                                }
                                htm += grpHtm;
                            }
                            if (i == 0) {
                                tempGrp1 = htm;
                                tempLen = grp.length;
                            }
                            //if (i == 2) {
                            //    grpHtm += '<tr style="border:none"><td class="fenye" colspan="10"></td></tr>'
                            //    htm += grpHtm;
                            //}

                        }
                        $('#dayindataList tbody').html(htm);
                        //_setWidth();
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
                    label: "提交",
                    className: "btn-success",
                    callback: function () {
                        jQuery.ajax({
                            dataType: "json",
                            url: "/Home/Updateall",
                            data: ajaxData,
                            async: false,
                            type: "POST",
                            success: function (result) {
                                if (result.code == 1) {
                                    FOXKEEPER_UTILS.alert('success', '提交成功');
                                    setTimeout(function () {
                                        var $dataList = $('#dataList');
                                        $dataList.find('tbody').html('');
                                        _initTData();
                                    }, 100);
                                } else if (result.code == 0) {
                                    FOXKEEPER_UTILS.alert('warning', '操作失败');
                                    $target.html(btnText).attr("disabled", false);
                                } else {
                                    FOXKEEPER_UTILS.alert('warning', '操作失败');
                                    $target.html(btnText).attr("disabled", false);
                                }
                            },
                            beforeSend: function () {


                            },
                            error: function () {
                                FOXKEEPER_UTILS.alert('danger', '系统异常');
                            }
                        });
                        return true;
                    }
                }
            }
        });
    }

  


    function _bind() {
        //根据时间和工地名搜索
        $('#btnSearch').click(function () {
            //$('#stDate1').val();
            _initTData();
        });
        //导出
        $('#btnExport').on('click', function () {
            console.log('---input date: ' + $('#stDate1').val());
            var form = document.getElementById("form_query");
            form.action = "/Home/ExportExcel";
            form.submit();
        });

        $('#btnPrint').on('click', function () {
            _initTData();
            dayin();
        });

        /**提交保存 **/
        $('#btnSave').click(function () {
            _save($(this), $(this).text());
            //_bang1();
        });
    }


    function _setAjaxData() {
        ajaxdatac.gdrq = $('#stDate1').val()
    }

    function dayin() {
        $('#biaotou tbody').html('');
        $('#biaotou tbody').append(tempGrp1);
        if (tempLen > 0 && typeof (tempLen) != 'undefined') {
            $('#dayindataList tbody tr:lt(' + tempLen + ')').remove();
        }
        var nian = $("#niana").text();
        $("#nianid").text(nian);
        $("#dayinlist").jqprint({
            debug: false,//如果是true则可以显示iframe查看效果（iframe默认高和宽都很小，可以再源码中调大），默认是false
            importCSS: true,//true表示引进原来的页面的css，默认是true。（如果是true，先会找$("link[media=print]")，若没有会去找$("link")中的css文件）
            printContainer: true,//表示如果原来选择的对象必须被纳入打印（注意：设置为false可能会打破你的CSS规则）
            operaSupport: false //表示如果插件也必须支持歌opera浏览器，在这种情况下，它提供了建立一个临时的打印选项卡。默认是true
        });
      
    }

    function _shuaxin() {
    }


   
    //动态设置表头宽度
    function _setWidth() {
        var $rows = $('#dayindataList  tbody tr')
        //var tempThead = $('#dayindataList thead').html();
        //$('#biaotou').remove('thead');
        //var $sttr = $('#biaotou').append('<thead>' + tempThead + '</thead>');
        //$('#dayindataList').remove('thread');

        var ta = $rows.find("td:eq(0)");
        console.log(ta.hasClass('t1') + '---------' + ta.html() + '------' + ta.width());
        //获取第一行td宽度
        var t1 = $rows.find("td:eq(0)").width();//获取宽度
        console.log(t1);
        var t = $sttr.find("td:eq(0)").width(t1);
        console.log(t.width());
        
        var t2 = $rows.find("td:eq(1)").width();//获取宽度
        console.log(t2);
        var tt = $sttr.find("td:eq(1)").width(t2);
        console.log(tt.width());
        var t3 = $rows.find("td:eq(2)").width();//获取宽度
        $sttr.find("td:eq(2)").width(t3);
        var t4 = $rows.find("td:eq(3)").width();//获取宽度
        $sttr.find("td:eq(3)").width(t4);
        var t5 = $rows.find("td:eq(4)").width();//获取宽度
        $sttr.find("td:eq(4)").width(t5);
        var t6 = $rows.find("td:eq(5)").width();//获取宽度
        $sttr.find("td:eq(5)").width(t6);
        var t7 = $rows.find("td:eq(6)").width();//获取宽度
        $sttr.find("td:eq(6)").width(t7);
        var t8 = $rows.find("td:eq(7)").width();//获取宽度
        $sttr.find("td:eq(7)").width(t8);
        var t9 = $rows.find("td:eq(8)").width();//获取宽度
        $sttr.find("td:eq(8)").width(t9);
        var t10 = $rows.find("td:eq(9)").width();//获取宽度
        $sttr.find("td:eq(9)").width(t10);
      


    }




    

    //var vms = ajaxData.UpVms;
    //if (ysgs == false) {
    //    alert('运输公司存在没有选择的选项,请填写完整');
    //    for (var i = 0; i < vms.length; i++) {
    //        var data = vms[i];
    //        if (data.transportcompanyName != '--请选择--') {
    //            ysgs = true;
    //            break;
    //        }
    //    }
    //    return;
    //}

    //bootbox.dialog({
    //    title: "",
    //    message: '<div class="row">  ' +
    //        '<div class="col-xs-12"> ' +
    //        '请确认是否提交表单记录？' +
    //        '</div></div>',
    //    buttons: {
    //        cancel: {
    //            label: "取消",
    //            classname: "btn-cancel",
    //            callback: $.noop
    //        },
    //        confirm: {
    //            label: "确定提交",
    //            classname: "btn-success",
    //            callback: function () {

    //            }
    //        })
    //}



    //function _initData() {
    //    jQuery.ajax({
    //        dataType: "json",
    //        url: "/Home/FindforPage",
    //        data: queryParams,
    //        type: "POST",
    //        success: function (rs) {
    //            if (rs.code == 1) {
    //                var $dataList = $('#dataList');
    //                var list = rs.data.data;
    //                $dataList.find('tbody').html('');
    //                if (list != null && list.length > 0) {
    //                    $("#listTpl").tmpl(list).appendTo('#dataList tbody');
    //                    _bindDelEvent();

    //                } else {
    //                }
    //                $('html').scrollTop(0);
    //            } else {
    //                //异常
    //            }
    //        },
    //        beforeSend: function () {
    //        },
    //        error: function () {
    //            alert('系统异常');
    //        }

    //    });
    //}


    function _buildSubData() {
        var tag = false;
        //获取table的每一行
        var $rows = $('#dataList tbody').find("tr");
        var items = [];
        $rows.each(function (i, v) {
            var $that = $(this);
            var t1 = $that.find("td:eq(0) ").text();//获取日期
            var t2 = $that.find("td:eq(1) ").text();//获取时间
            var t3 = $that.find("td:eq(2) ").text();//获取工地名称
            var t5 = $that.find("td:eq(3) ").text();//获取工地地址
            var t6 = $that.find("td:eq(4) ").text();//获取车辆
            var t7 = $that.find("td:eq(5) input").val();//是否冲洗
            var t8 = $that.find("td:eq(6) input").val();//是否密闭
            var t9 = $that.find("td:eq(7) select").val();//获取办证
            var t10 = $that.find("td:eq(8) select").find("option:selected").text();//获取公司
            //var tt10 = $that.find("td:eq(8) select").find("option:selected").val("value")
            var tid = $that.find("td:eq(9) ").text();//获取id
            var item = {};//一个什么也没有值的对象
            //item.inputRiqi = t1;
            item.inputTime = t1 + " " + t2;//日期加时间=完整的日期时间
            item.date = t1;
            item.time = t2;
            item.constName = t3;
            item.constAddr = t5;
            item.trans = t6;
            item.rinse = t7;
            item.airtight = t8;
            item.certificate = t9;
            item.transportcompanyName = t10;
            item.status = 1;
            item.id = tid;
            items.push(item);
        });
        //console.log(JSON.stringify(items));
        ajaxData.UpVms = items;
    }



    ////动态获取下拉框值和地址
    //function _getgdname() {
    //            jQuery.ajax({
    //                dataType: "json",
    //                url: "/Home/fingd",
    //                type: "POST",
    //                success: function (rs) {
    //                    var $gdname = $('#wokao1');
    //                    var data = rs.data;
    //                    for (var i = 0; i < data.length; i++) {
    //                        var name = data[i].constName;
    //                        var cord = data[i].constCode;
    //                        var addr = data[i].constAddr;
    //                        $gdname.append('<option adr="' + addr + '" value="' + cord + '">' + name + '</option>');
    //                    }
    //                },
    //                beforeSend: function () {
    //                },
    //                error: function () {
    //                    alert('系统异常');
    //                }
    //            });
    //        }


    //按日期搜索查询
    function _initTData() {
        _setAjaxData();
        jQuery.ajax({
            dataType: "json",
            url: "/Home/FindVmcoesGdorTimes",
            data: ajaxdatac,
            type: "POST",
            success: function (rs) {
                if (rs.code == 1) {
                    var $dataList = $('#dataList');
                    var $dayindataList = $('#dayindataList');
                    var list = rs.data.data;
                    //console.log(JSON.stringify(list)); //打印当前月份数据
                    $dataList.find('tbody').html('');
                    $dayindataList.find('tbody').html('');
                    if (list != null && list.length > 0) {
                        $("#listTpl").tmpl(list).appendTo('#dataList tbody');
                        var datas = [];
                        var temp = [];//分组后的数组
                        var prev;
                        var len = list.length, lastLen = list.length - 1;
                        for (var i = 0; i < len; i++) {
                            var data = list[i];
                            var curr = data.date + '' + data.constCode; //根据时间和工地好字符串相同
                            //&& i != lastLen
                            if (i == 0) {
                                temp.push(data);
                            } else if (i > 0) {
                                if (curr == prev) {
                                    temp.push(data);
                                    if (i == lastLen) {//最后一条的时候
                                        datas.push(temp);
                                    }
                                } else {
                                    datas.push(temp);//说明上个组已经，存起来
                                    temp = [];//清空上一组的数据
                                    temp.push(data);
                                }
                            }
                            prev = curr;
                        }
                        //console.log(datas);//打印分组数据
                        var grpedDatas = datas;
                        var htm = "";
                        for (var i = 0; i < grpedDatas.length; i++) {
                            var grp = grpedDatas[i];
                            for (var k = 0; k < grp.length; k++) {
                                var grpHtm = '<tr align="center" style=" ">';
                                if (k == 0) {
                                    grpHtm += '<td  width="4%" class="t1"  rowspan="' + grp.length + '" style="  vertical-align: middle; font-size:5px; ">' + (i + 1) + '</td>';
                                    if (grp[0].datein == null) { grpHtm += '<td width="5%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px; "></td>'; }
                                    else { grpHtm += '<td width="5%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px;  ">' + grp[0].datein + '</td>'; }

                                    if (grp[k].time == null) { grpHtm += '<td width="6%" style="vertical-align: middle; font-size:5px;  "></td>'; }
                                    else { grpHtm += '<td width="6%" style="vertical-align: middle; font-size:5px;  ">' + grp[k].time + '</td>'; }
                                    if (grp[0].constName == null) { grpHtm += '<td width="15%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px; "></td>'; }
                                    else { grpHtm += '<td width="15%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px;  ">' + grp[0].constName + '</td>'; }

                                    if (grp[0].constAddr == null) { grpHtm += '<td width="15%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px; "></td>'; }
                                    else { grpHtm += '<td width="15%" rowspan="' + grp.length + '" style="vertical-align: middle; font-size:5px;  ">' + grp[0].constAddr + '</td>'; }


                                    if (grp[k].trans == null) { grpHtm += '<td width="12%" style=" vertical-align: middle; text-align: center;font-size:5px;  " ></td>'; }
                                    else { grpHtm += '<td width="12%" style=" vertical-align: middle; text-align: center;font-size:5px;  " >' + grp[k].trans + '</td>'; }

                                    if (grp[k].rinse == 0) { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px;  ">是</td>'; }
                                    else { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px;  ">否</td>'; }
                                    if (grp[k].airtight == 0) { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px; ">是</td>'; }
                                    else { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px;">否</td>'; }
                                    if (grp[k].certificate == 0) { grpHtm += '<td width="8%" style=" vertical-align: middle; text-align: center;font-size:5px; ">是</td>'; }
                                    else if (grp[k].certificate == 1) { grpHtm += '<td width="8%" style=" vertical-align: middle; text-align: center;font-size:5px;  ">否</td>'; }
                                    else { grpHtm += '<td width="8%"></td>' }

                                    if (grp[k].transportcompanyName == null) { grpHtm += '<td width="13%"></td>'; }
                                    else { grpHtm += '<td width="13%" style=" vertical-align: middle; text-align: center;font-size:5px;">' + grp[k].transportcompanyName + '</td>'; }
                                    grpHtm += '<td  id="niana" hidden >' + grp[0].nian + '</td>';//年度
                                    grpHtm += '</tr>';
                                } else {

                                    if (grp[k].time == null) { grpHtm += '<td width="6%" style="vertical-align: middle; font-size:5px;"></td>'; }
                                    else { grpHtm += '<td width="6%" style="vertical-align: middle; font-size:5px;">' + grp[k].time + '</td>'; }
                                    if (grp[k].trans == null) { grpHtm += '<td width="12%" style=" vertical-align: middle; text-align: center;font-size:5px;" ></td>'; }
                                    else { grpHtm += '<td width="12%" style=" vertical-align: middle; text-align: center;font-size:5px;" >' + grp[k].trans + '</td>'; }
                                    if (grp[k].rinse == 0) { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px;">是</td>'; }
                                    else { grpHtm += '<td width="11%" style=" vertical-align: middle; text-align: center;font-size:5px;">否</td>'; }

                                    if (grp[k].airtight == 0) { grpHtm += '<td width="10%" style=" vertical-align: middle; text-align: center;font-size:5px;">是</td>'; }
                                    else { grpHtm += '<td width="10%" style=" vertical-align: middle; text-align: center;font-size:5px;">否</td>'; }

                                    if (grp[k].certificate == 0) { grpHtm += '<td width="8%" style=" vertical-align: middle; text-align: center;font-size:5px;">是</td>'; }
                                    else if (grp[k].certificate == 1) { grpHtm += '<td width="8%" style=" vertical-align: middle; text-align: center;font-size:5px;">否</td>'; }
                                    else { grpHtm += '<td width="8%"></td>' }

                                    if (grp[k].transportcompanyName == null) { grpHtm += '<td width="13%"></td>'; }
                                    else { grpHtm += '<td width="13%" style=" vertical-align: middle; text-align: center;font-size:5px;">' + grp[k].transportcompanyName + '</td>'; }
                                    grpHtm += '</tr>';
                                }
                                htm += grpHtm;
                            }
                           
                            if (i == 0) {
                                tempGrp1 = htm;
                                tempLen = grp.length;
                            }
                        }
                        $('#dayindataList tbody').html(htm);


                        //_setWidth();
                    } else {
                    }
                    $('html').scrollTop(0);
                } else if (rs.code == 0) {
                    alert('请选择工地名和时间')
                }
            },
            beforeSend: function () {
            },
            error: function () {
                alert('系统异常');
            }

        });
    }

  
});
