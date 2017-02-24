

// if the code is not in iffy, the values become global and variables like ele get overriden --> wont execute in few browsers
(function () {

   //document.getElementbyid is replaced by jquery(jquery uses css selectors)
    //var ele = $("#username");

//    var main = $("#main");
//    main.on("mouseenter", function () {
//        main.style = "background: #888;";
//    });

//    main.on("mouseleave", function () {
//        main.style = "";
//    });

//    var menuItems = $("ul.menu li a");
//    menuItems.on("click",function(){
//        var me = $(this);
//        alert(me.text());
    //});
    
    //to return 2 ele: seperate by ';'
    var $sidebarAndWrapper = $("#sidebar,#wrapper");

    //toggleclass will add/hide class to sidebar and wrapper divs
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        } else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }
    });
})();