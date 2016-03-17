$(".answerLink").click(function() {
    var $item = $(this);
    var $parentId = $item.attr("id");
    var $name = $item.attr("value");
    var $msg = $("#quoteMassage");
    var $hasQuote = $("#NewComment_HasQuote");
    $hasQuote.attr("value", true);
    $msg.html("<b>To " + $name + "</b>");
    $("#NewComment_ParentCommentId").val($parentId);
});


$(".quoteLink").click(function() {
    var $item = $(this);
    var $parentId = $item.attr("id");
    var $name = $item.data("name");
    var $body = $item.attr("value");
    var $msg = $("#quoteMassage");
    var $hasQuote = $("#NewComment_HasQuote");
    $hasQuote.attr("value", true);
    console.log($hasQuote.val());
    $msg.html("<b>" + $name + " said:</b><br/><q>" + $body + "</q>").css("font-style", "italic");
    $("#NewComment_ParentCommentId").val($parentId);
});

$(document).ready(function() {
    $(".deleteLink").click(function() {
        var $item = $(this);
        var $id = $item.attr("id");
        var del = confirm("Do you really want to delete this comment?");
        if (del) {
            $.ajax({
                type: "POST",
                url: "/Comment/Remove/" + $id,
                complete: function() {
                    location.reload();
                }
            });
        }
    });
});

function successFunc(data) {
    $("#gameList").html(data);
};

$("#filterLink").click(function(event) {
    event.preventDefault();
    var $path = $(this).attr("href");
    var idSelector = function() { return this.id; };

    var genreIds = $(".genreBox :checkbox:checked").map(idSelector).get();
    var platformIds = $(".platformBox :checkbox:checked").map(idSelector).get();
    var publisherIds = $(".publisherBox :checkbox:checked").map(idSelector).get();
    var minPrice = $("#MinPrice").val();
    var maxPrice = $("#MaxPrice").val();
    var period = $("#PeriodType > option:selected").val();
    var sort = $("#SortType > option:selected").val();
    var search = $("#SearchString").val();
    var pagination = $("#PagingInfo_PaginationType > option:selected").val();

    $.ajax({
        type: "POST",
        url: $path,
        data: JSON.stringify({
            filters: {
                GenreIds: genreIds,
                PlatformIds: platformIds,
                PublisherIds: publisherIds,
                MinPrice: minPrice,
                MaxPrice: maxPrice,
                PeriodType: period,
                SortType: sort,
                SearchString: search,
                PaginationType: pagination
            }
        }),
        contentType: "application/json; charset=utf-8",
        success: successFunc
    });
});

$(".pageLink").click(function (event) {

    //event.preventDefault();

    event.preventDefault();
    event.stopPropagation();

    var $path = $(this).attr("href");
    var idSelector = function() { return this.id; };

    var genreIds = $(".genreBox :checkbox:checked").map(idSelector).get();
    var platformIds = $(".platformBox :checkbox:checked").map(idSelector).get();
    var publisherIds = $(".publisherBox :checkbox:checked").map(idSelector).get();
    var minPrice = $("#MinPrice").val();
    var maxPrice = $("#MaxPrice").val();
    var period = $("#PeriodType > option:selected").val();
    var sort = $("#SortType > option:selected").val();
    var search = $("#SearchString").val();
    var currentPage = $(this).attr("value");
    var pagination = $("#PagingInfo_PaginationType > option:selected").val();

    $.ajax({
        type: "POST",
        url: $path,
        data: JSON.stringify({
            filters: {
                GenreIds: genreIds,
                PlatformIds: platformIds,
                PublisherIds: publisherIds,
                MinPrice: minPrice,
                MaxPrice: maxPrice,
                PeriodType: period,
                SortType: sort,
                PagingInfo: {
                    SearchString: search,
                    PaginationType: pagination
                }
            },
            page: currentPage
        }),
        contentType: "application/json; charset=utf-8",
        success: successFunc
    });
    event.preventDefault();
});