(function ($) {
    $.extend({
        Request: function (m) {
            var sValue = location.search.match(new RegExp("[\?\&]" + m + "=([^\&]*)(\&?)", "i"));
            return sValue ? sValue[1] : sValue;
        },
        UrlUpdateParams: function (url, name, value) {
            var r = url;
            if (r != null && r != 'undefined' && r != "") {
                value = encodeURIComponent(value);
                var reg = new RegExp("(^|)" + name + "=([^&]*)(|$)");
                var tmp = name + "=" + value;
                if (url.match(reg) != null) {
                    r = url.replace(eval(reg), tmp);
                }
                else {
                    if (url.match("[\?]")) {
                        r = url + "&" + tmp;
                    } else {
                        r = url + "?" + tmp;
                    }
                }
            }
            return r;
        }

    });
})(jQuery);

//获取单个参数值
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg); //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}

//返回的是字符串形式的参数，例如：class_id=3&id=2&  
function getUrlArgStr() {
    var q = location.search.substr(1);
    var qs = q.split('&');
    var argStr = '';
    if (qs) {
        for (var i = 0; i < qs.length; i++) {
            argStr += qs[i].substring(0, qs[i].indexOf('=')) + '=' + qs[i].substring(qs[i].indexOf('=') + 1) + '&';
        }
    }
    return argStr;
}

//参对象形式返回URL参数
function urlArgs() {
    var args = {};                             // Start with an empty object
    var query = location.search.substring(1);  // Get query string, minus '?'
    var pairs = query.split("&");              // Split at ampersands
    for (var i = 0; i < pairs.length; i++) {    // For each fragment
        var pos = pairs[i].indexOf('=');       // Look for "name=value"
        if (pos == -1) continue;               // If not found, skip it

        var name = pairs[i].substring(0, pos);  // Extract the name
        var value = pairs[i].substring(pos + 1); // Extract the value
        value = decodeURIComponent(value);     // Decode the value
        args[name] = value;                    // Store as a property
    }
    return args;                               // Return the parsed arguments
}

////参对象形式返回URL参数
function getQueryStringArgs() {

    //get query string without the initial ?
    var qs = (location.search.length > 0 ? location.search.substring(1) : ""),

        //object to hold data
        args = {},

        //get individual items
        items = qs.length ? qs.split("&") : [],
        item = null,
        name = null,
        value = null,

        //used in for loop
        i = 0,
        len = items.length;

    //assign each item onto the args object
    for (i = 0; i < len; i++) {
        item = items[i].split("=");
        name = decodeURIComponent(item[0]);
        value = decodeURIComponent(item[1]);

        if (name.length) {
            args[name] = value;
        }
    }

    return args;
}

//设置URL参数的方法  
function setParmsValue(parms, parmsValue) {
    var urlstrings = document.URL;
    var args = GetUrlParms();
    var values = args[parms];
    //如果参数不存在，则添加参数         
    if (values == undefined) {
        var query = location.search.substring(1); //获取查询串   
        //如果Url中已经有参数，则附加参数  
        if (query) {
            urlstrings += ("&" + parms + "=" + parmsValue);
        }
        else {
            urlstrings += ("?" + parms + "=" + parmsValue);  //向Url中添加第一个参数  
        }
        window.location = urlstrings;
    }
    else {
        window.location = updateParms(parms, parmsValue);  //修改参数  
    }
}

//修改URL参数，parms：参数名，parmsValue：参数值，return：修改后的URL  
function updateParms(parms, parmsValue) {
    var newUrlParms = "";
    var newUrlBase = location.href.substring(0, location.href.indexOf("?") + 1); //截取查询字符串前面的url  
    var query = location.search.substring(1); //获取查询串     
    var pairs = query.split("&"); //在逗号处断开     
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('='); //查找name=value     
        if (pos == -1) continue; //如果没有找到就跳过     
        var argname = pairs[i].substring(0, pos); //提取name     
        var value = pairs[i].substring(pos + 1); //提取value   
        //如果找到了要修改的参数  
        if (findText(argname, parms)) {
            newUrlParms = newUrlParms + (argname + "=" + parmsValue + "&");
        }
        else {
            newUrlParms += (argname + "=" + value + "&");
        }
    }
    return newUrlBase + newUrlParms.substring(0, newUrlParms.length - 1);
}

//辅助方法  
function findText(urlString, keyWord) {
    return urlString.toLowerCase().indexOf(keyWord.toLowerCase()) != -1 ? true : false;
}

//得到查询字符串参数集合  
function GetUrlParms() {
    var args = new Object();
    var query = location.search.substring(1); //获取查询串     
    var pairs = query.split("&"); //在逗号处断开     
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('='); //查找name=value     
        if (pos == -1) continue; //如果没有找到就跳过     
        var argname = pairs[i].substring(0, pos); //提取name     
        var value = pairs[i].substring(pos + 1); //提取value     
        args[argname] = unescape(value); //存为属性     
    }
    return args;
}
