
/**
 * 添加分页信息
 * @param {any} idSelector：需要将分页信息添加在哪个标签中（一般是一个div标签id）
 * @param {any} currentPageIndex：当前页页码（是第几页）
 * @param {any} pageSize：页码（一页多少条）
 * @param {any} totalPage：总页数
 * @param {any} totalRow：总行数
 * @param {any} clickFunction：当点击分页时，执行的函数（该函数需要接收一个页码的参数）
 */
function AddPagination(idSelector, currentPageIndex, pageSize, totalPage, totalRow, clickFunction) {
    //1. 判断 当前页和总页数 是否为正确的数字
    if (undefined == currentPageIndex || null == currentPageIndex || parseInt(currentPageIndex) <= 0) {
        currentPageIndex = 1;
    } else {
        currentPageIndex = parseInt(currentPageIndex);
    }
    if (undefined == totalPage || null == totalPage || parseInt(totalPage) <= 0) {
        totalPage = 1;
    } else {
        totalPage = parseInt(totalPage);
    }

    //添加分页信息显示
    var showPageInfoHtml = '<div class="col-sm-5">';
    showPageInfoHtml += '<div class="dataTables_info" role="status" aria-live="polite">';
    showPageInfoHtml += '<span>';
    showPageInfoHtml += 'Showing ' + currentPageIndex + ' to ' + pageSize + ' of ' + totalRow + ' entries';
    showPageInfoHtml += '</span>';
    showPageInfoHtml += '</div>';
    showPageInfoHtml += '</div>';

    var pageButtonDivId = idSelector + "-pagination";
    var showPageButtonHtml = '<div class="col-sm-7">';
    showPageButtonHtml += '<div class="dataTables_paginate paging_simple_numbers" id="' + pageButtonDivId + '">';

    showPageButtonHtml += '<ul class="pagination pagination-sm no-margin pull-right">';
    var isDisabled = (currentPageIndex == 1);
    //添加上一页
    showPageButtonHtml += GeneratePageNumberHtml(currentPageIndex - 1, "Previous", "previous" + (isDisabled ? " disabled" : ""));
    //添加分页
    var beforeShowNumber = CalculateBeforeShowPageNumber(totalPage, currentPageIndex);
    
    var afterShowNumber = _PaginationShowPageNumber - beforeShowNumber - 1;
    //当总页数少于分页总页数时，计算剩余页数，要使用总页数
    if (totalPage < _PaginationShowPageNumber) {
        afterShowNumber = totalPage - currentPageIndex;
    }

    var isCurrentPagIndex = false;
    var showPageNuber = _PaginationShowPageNumber / 2;
    var startIndex = (beforeShowNumber <= 0 ? 1 : (currentPageIndex - beforeShowNumber));
    
    var endIndex = currentPageIndex + afterShowNumber;

    var isShowHeadMore = false;
    var isShowEndMore = false;

    //计算是否需要显示当前页前面的更多页
    if (currentPageIndex > showPageNuber) {
        isShowHeadMore = true;
    }
    if (isShowHeadMore) {
        showPageButtonHtml += GeneratePageNumberHtml(startIndex - 1);
    }
    //计算是否需要显示当前页后面的更多页
    if ((currentPageIndex + afterShowNumber) < totalPage) {
        isShowEndMore = true;
    }
    //设置中间显示的分页
    for (var page = startIndex; page <= endIndex; page++) {
        isCurrentPagIndex = (currentPageIndex == page);
        showPageButtonHtml += GeneratePageNumberHtml(page, page, (isCurrentPagIndex ? " active" : ""));
    }
    //是否需要显示当前页后面的更多页
    if (isShowEndMore) {
        endIndex = currentPageIndex + showPageNuber;
        showPageButtonHtml += GeneratePageNumberHtml(endIndex + 1);
    }

    //添加下一页
    isDisabled = (currentPageIndex == totalPage);
    var nextPageIndex = (currentPageIndex + 1);
    showPageButtonHtml += GeneratePageNumberHtml(nextPageIndex, "Next", "next" + (isDisabled ? " disabled" : ""));

    showPageButtonHtml += '</ul>';

    showPageButtonHtml += '</div>';
    showPageButtonHtml += '</div>';

    $("#" + idSelector).empty();        //清空原有内容
    $("#" + idSelector).append(showPageInfoHtml + showPageButtonHtml);      //添加内容
    //注册事件
    $("#" + pageButtonDivId + " a").click(function () {
        if (!$(this).parent().hasClass("active") && !$(this).parent().hasClass("disabled")) {
            var page = $(this).data("page-idx");
            clickFunction(page);
        }
    });
}

//分页显示的总页数
var _PaginationShowPageNumber = 10;

/**
 * 计算左侧显示的分页页数
 * @param {any} totalPageNuber：总共多少页
 * @param {any} currentPageIndex：当前页索引
 */
function CalculateBeforeShowPageNumber(totalPageNuber, currentPageIndex) {
    var halfTotalShowPageNumber = _PaginationShowPageNumber / 2;
    var result = 0;
    if (totalPageNuber < _PaginationShowPageNumber) {
        //总页数小于分页数量时，需要显示所有页数
        result = currentPageIndex;  //加一的原因：加上当前页
        //if ((totalPageNuber - currentPageIndex) >= currentPageIndex) {
        //    result = currentPageIndex - 1;
        //} else {
        //    result = totalPageNuber - currentPageIndex + 1;  //加一的原因：加上当前页
        //}        
    } else {
        /**
         * 计算逻辑：
         *      1. 判断还剩余多少页
         *          1.1 如剩余页数超出了总显示页数的一半，则显示总显示页数的一半
         *          1.2 如剩余页数不足总显示页数的一半，则前面显示的分页数=总显示页数-（总页数-当前页索引）
         */
        if ((totalPageNuber - currentPageIndex) >= halfTotalShowPageNumber) {
            //后面也超出 显示总页数的一半 （这里是5）
            result = currentPageIndex <= halfTotalShowPageNumber ? currentPageIndex : halfTotalShowPageNumber;
        } else {
            result = _PaginationShowPageNumber - (totalPageNuber - currentPageIndex);     //总页数-后面还剩的页数
        }
    }
    //result = currentPageIndex - 1;
    result--;       //因为包含当前页，所以这里-1
    if (result <= 0) result = 0;
    return result;
}

/**
 * 生成分页页码的HTML
 * @param {any} pageIndex：页码
 * @param {any} showContent：页码显示内容
 * @param {any} parentAddClass：父级添加样式
 */
function GeneratePageNumberHtml(pageIndex, showContent = "...", parentAddClass = "") {
    var html = "";

    html += '<li class="paginate_button ' + parentAddClass + '" tabindex="0">';
    html += '<a href="#" data-page-idx="' + pageIndex + '" tabindex="0">' + showContent;
    html += '</a>';
    html += '</li>';

    return html;
}