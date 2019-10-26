

/* Copyright:dx.com
 *
 * Autor: xx
 * Create Time: xx
 *
 * Rewriter: liudi
 * Rewriter Time: 2019.10.12
 * Description: xx
 *
 * Rewriter: xx
 * Rewriter Time: xx
 * Description: xx
 *
 *
 * **/

/**
 * 发送 GET 请求
 * @param {any} url：发送url地址
 * @param {any} data：发送数据
 * @param {any} successCallback：操作执行成功之后，执行该函数（必须有一个参数可接收返回结果）
 * @param {any} failureCallback：操作失败之后，执行该函数（必须有一个参数可接收返回结果）
 */
function SendGetRequest(url, data, successCallback, failureCallback = null) {
    $.ajax({
        url: url
        , type: 'GET'
        , contentType: 'application/json'       //默认值: "application/x-www-form-urlencoded"。发送信息至服务器时内容编码类型。
        , data: JSON.stringify(data)
        , dataType: 'json'     //预期服务器返回的数据类型。如果不指定，jQuery 将自动根据 HTTP 包 MIME 信息来智能判断，比如 XML MIME 类型就被识别为 XML
        , timeout: 30000        //设置本地的请求超时时间（以毫秒计）。
        , error: function (xhr, status, error) {
            if (undefined != failureCallback && null != failureCallback)
                failureCallback("Error message： " + xhr.status + " " + xhr.statusText);
        }
        , success: function (result, status, xhr) {
            successCallback(result);
        }
    });
}


/**
 * 发送 POST 请求
 * @param {any} url：发送url地址
 * @param {any} data：发送数据
 * @param {any} successCallback：操作执行成功之后，执行该函数（必须有一个参数可接收返回结果）
 * @param {any} failureCallback：操作失败之后，执行该函数（必须有一个参数可接收返回结果）
 */
function SendPostRequest(url, data, successCallback, failureCallback = null) {
    if (undefined == data) {
        data = null;
    } else if (null == data) {
        data = null;
    } else {
        data = JSON.stringify(data);
    }

    $.ajax({
        url: url
        , type: 'POST'
        , contentType: 'application/json'       //默认值: "application/x-www-form-urlencoded"。发送信息至服务器时内容编码类型。
        , data: data
        , dataType: 'json'     //预期服务器返回的数据类型。如果不指定，jQuery 将自动根据 HTTP 包 MIME 信息来智能判断，比如 XML MIME 类型就被识别为 XML
        , timeout: 30000        //设置本地的请求超时时间（以毫秒计）。
        , error: function (xhr, status, error) {
            if (undefined != failureCallback && null != failureCallback)
                failureCallback("Error message： " + xhr.status + " " + xhr.statusText);
        }
        , success: function (result, status, xhr) {
            if (undefined != successCallback && null != successCallback)
                successCallback(result);
        }
    });
}