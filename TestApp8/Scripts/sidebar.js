// サイドバーを取り出しするために使用する
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

$("#side-wrapper").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});