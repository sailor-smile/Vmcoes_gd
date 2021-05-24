jQuery(function () {
    'use strict';
    var ajaxData = {};
    //绑定
    _bind();

    _init();

    function _init() {

    }

    function _bind() {
        /**  保存 */
        $('#btnSave').click(function () {
            _save($(this), $(this).text());
        });
    }

    //提交表单
    function _save($target, btnText) {
        _setAjaxData();
        jQuery.ajax({
            dataType: "json",
            url: "/Users/Save",
            data: ajaxData,
            type: "POST",
            success: function (result) {
                if (result.code == 1) {
                    FOXKEEPER_UTILS.alert('success', '操作成功');
                    setTimeout(function () {
                        location.replace('/Users/Index');
                    }, 1500);
                }  else {
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

    function _setAjaxData() {
        ajaxData.userName = $('#insv').val();
        ajaxData.userPassword = $('#insv2').val();
        ajaxData.userId = $('#insv2').val();
        ajaxData.dptId ="300"
    }
    
});