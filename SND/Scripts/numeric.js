
//	Author: Joshua De Leon - File: numericInput.js - Description: Allows only numeric input in an element. - If you happen upon this code, enjoy it, learn from it, and if possible please credit me: www.transtatic.com
(function (b) {
    var c = {
        allowFloat: false,
        allowNegative: false,
        showComma: false
    };
    b.fn.numericInput = function (e) {
        var f = b.extend({}, c, e);
        var d = f.allowFloat;
        var g = f.allowNegative;
        var comma = f.showComma;

        //컨트롤 구분을 위한 클래스 삽입
        this.removeClass("numericInput");
        this.addClass("numericInput");

        this.keypress(function (j) {
            var i = j.which;
            var h = b(this).val();
            if (i > 0 && (i < 48 || i > 57)) {
                if (d == true && i == 46) {
                    if (g == true && a(this) == 0 && h.charAt(0) == "-") {
                        return false
                    }
                    if (h.match(/[.]/)) {
                        return false
                    }
                } else {
                    if (g == true && i == 45) {
                        if (h.charAt(0) == "-") {
                            return false
                        } if (a(this) != 0) {
                            return false
                        }
                    } else {
                        if (i == 8) {
                            return true
                        } else {
                            return false
                        }
                    }
                }
            } else {
                if (i > 0 && (i >= 48 && i <= 57)) {
                    if (g == true && h.charAt(0) == "-" && a(this) == 0) {
                        return false
                    }
                }
            }
        });

        //한글입력, 콤마 처리
        this.keyup(function (j) {
            var i = j.which;
            var h = b(this).val();


            //한글처리
            if (h.length > 0 && isNaN(h) == true) {
                var result = '';
                for (i = 0; i < h.length; i++) {
                    var tmpVal = h.charAt(i);
                    if (isNaN(tmpVal) == false) {
                        result += tmpVal;
                    }
                }

                h = result;
            }

            //콤마처리
            if (comma == true) {
                h = (h * 1).toLocaleString('en-US');
            }

            b(this).val(h);
        });

        return this
    };
    function a(d) {
        if (d.selectionStart) {
            return d.selectionStart
        } else {
            if (document.selection) {
                d.focus();
                var f = document.selection.createRange();
                if (f == null) {
                    return 0
                }
                var e = d.createTextRange(), g = e.duplicate(); e.moveToBookmark(f.getBookmark()); g.setEndPoint("EndToStart", e); return g.text.length
            }
        } return 0
    }
}(jQuery));