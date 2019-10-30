
/* Copyright: liudi
 *
 * Autor: xx
 * Create Time: xx
 *
 * Rewriter: liudi
 * Rewriter Time: 2019.10.12
 * Description: 
 *      弹框提示框架
 *      框架依赖于layer，使用该框架之前，必须引用layer
 *
 * Rewriter: xx
 * Rewriter Time: xx
 * Description: xx
 *
 *
 * **/


/**
 * 说明：
 *     弹出框，提示信息
 * power by 刘迪 2019.10.12
 * 
 * 参数说明：
 * @param {any} message：提示信息
 * @param {any} timeout：提示框显示时间，单位毫秒，默认值为3000，即3秒；超出该时间之后，提示框消失
 * 
 */
function AlertMsg(message, timeout = 3000) {
    layer.msg(message, {
        time: timeout //timeout秒之后自动关闭
    });
}

/**
 * 说明：
 *     弹出框，警告信息
 * power by 刘迪 2019.10.12
 * 
 * 参数说明：
 * @param {any} message：提示信息
 * @param {any} timeout：提示框显示时间，单位毫秒，默认值为3000，即3秒；超出该时间之后，提示框消失
 * 
 * icon值: 
 * 		1：操作成功，绿色 √
 * 		2：操作失败，红色 x 
 * 		3：黄色 ？
 * 		4：黑色 锁
 * 		5：红色悲伤脸
 * 		6：绿色笑脸
 * 		7：橘色感叹号
 * 		大于7：橘色感叹号
 */
function AlertWarnMsg(message, timeout = 0) {
    layer.alert(message, {
        title: '警 告'
        , time: timeout //timeout秒之后自动关闭
        , icon: 7
    });
}

/**
 * 说明：
 *     弹出框，错误信息
 * power by 刘迪 2019.10.12
 * 
 * 参数说明：
 * @param {any} message：提示信息
 * @param {any} timeout：提示框显示时间，单位毫秒，默认值为0，不会自动关闭；超出该时间之后，提示框消失
 * 
 * icon值: 
 * 		1：操作成功，绿色 √
 * 		2：操作失败，红色 x 
 * 		3：黄色 ？
 * 		4：黑色 锁
 * 		5：红色悲伤脸
 * 		6：绿色笑脸
 * 		7：橘色感叹号
 * 		大于7：橘色感叹号
 */
function AlertErrorMsg(message, timeout = 0) {
    layer.alert(message, {
        title: '错 误'
        , skin: 'layui-layer-molv' //样式类名
        , time: timeout //timeout秒之后自动关闭
        , icon: 5
    });
}

/**
 * 说明：
 *     弹出框，操作成功信息
 * power by 刘迪 2019.10.12
 * 
 * 参数说明：
 * @param {any} message：提示信息
 * @param {any} timeout：提示框显示时间，单位毫秒，默认值为0，不会自动关闭；超出该时间之后，提示框消失
 * 
 * icon值: 
 * 		1：操作成功，绿色 √
 * 		2：操作失败，红色 x 
 * 		3：黄色 ？
 * 		4：黑色 锁
 * 		5：红色悲伤脸
 * 		6：绿色笑脸
 * 		7：橘色感叹号
 * 		大于7：橘色感叹号
 */
function AlertSuccessMsg(message, timeout = 3000) {
    // layer.alert(message, {
    //     title: '成 功'
    //     , skin: 'layui-layer-molv' //样式类名
    //     , time: timeout //timeout秒之后自动关闭
    //     , icon: 1
    // });
    layer.msg(message, {
        time: timeout //timeout秒之后自动关闭
        , icon: 1
    });
}

/**
 * 说明：
 *     弹出框，操作失败信息
 * power by 刘迪 2019.10.12
 * 
 * 参数说明：
 * @param {any} message：提示信息
 * @param {any} timeout：提示框显示时间，单位毫秒，默认值为0，不会自动关闭；超出该时间之后，提示框消失
 * 
 * icon值: 
 * 		1：操作成功，绿色 √
 * 		2：操作失败，红色 x 
 * 		3：黄色 ？
 * 		4：黑色 锁
 * 		5：红色悲伤脸
 * 		6：绿色笑脸
 * 		7：橘色感叹号
 * 		大于7：橘色感叹号
 */
function AlertFaiureMsg(message, timeout = 0) {
    layer.alert(message, {
        title: '失 败'
        , skin: 'layui-layer-molv' //样式类名
        , time: timeout //timeout秒之后自动关闭
        , icon: 2
    });
}
