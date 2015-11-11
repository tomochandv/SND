var Utill = {
    //TextBox 빈값 체크
    Nullcheck: function (id) {
        var text = $('#' + id).val();
        if (text == '') {
            return false;
        }
        else {
            return true;
        }
    },
    NumbercheckSelector: function (selector) {
        var regular = /[^0-9]/;
        var text = selector.val();
        if (text.search(regular) != -1) {
            return false;
        }
        else {
            return true;
        }
    },
    ReplaceAll: function (fulltxt, oTxt, nTxt) {
        return fulltxt.split(oTxt).join(nTxt);
    },
    //Ajax
    Ajax: function (url, data) {
        var returnData = '';
        $.ajax({
            url: url,
            data: data,
            type: 'POST',
            async: false,
            success: function (data) {
                returndata = data;
            },
            error: function (e) {
                console.log(e);
                alert("통신에러가 발생했습니다.(" + url + ")");
            }
        });
        return returndata;
    },
    //check 박스 전체 선택
    CheckAll: function (id) {
        $("#" + id).click(function () {
            if ($("#" + id + ":checked").length > 0) {
                $("input[type='checkbox']").prop("checked", true);
            }
            else {
                $("input[type='checkbox']").prop("checked", false);
            }
        });
    }
}
