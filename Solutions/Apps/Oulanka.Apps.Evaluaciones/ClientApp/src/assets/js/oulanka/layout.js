var Layout = function () {

  var resBreakpointMd = OulankaApp.getResponsiveBreakpoint('md');

  var handleSidebarAndContentHeight = function () {
    var content = $('.page-content');
    var sidebar = $('.page-sidebar');
    var body = $('body');
    var height;

    if (body.hasClass("page-footer-fixed") === true && body.hasClass("page-sidebar-fixed") === false) {
      var available_height = OulankaApp.getViewPort().height - $('.page-footer').outerHeight() - $('.page-header').outerHeight();
      var sidebar_height = sidebar.outerHeight();
      if (sidebar_height > available_height) {
        available_height = sidebar_height + $('.page-footer').outerHeight();
      }
      if (content.height() < available_height) {
        content.css('min-height', available_height);
      }
    } else {
      if (body.hasClass('page-sidebar-fixed')) {
        height = _calculateFixedSidebarViewportHeight();
        if (body.hasClass('page-footer-fixed') === false) {
          height = height - $('.page-footer').outerHeight();
        }
      } else {
        var headerHeight = $('.page-header').outerHeight();
        var footerHeight = $('.page-footer').outerHeight();

        if (OulankaApp.getViewPort().width < resBreakpointMd) {
          height = OulankaApp.getViewPort().height - headerHeight - footerHeight;
        } else {
          height = sidebar.height() + 20;
        }

        if ((height + headerHeight + footerHeight) <= OulankaApp.getViewPort().height) {
          height = OulankaApp.getViewPort().height - headerHeight - footerHeight;
        }
      }
      content.css('min-height', height);
    }
  };

  var handleGoTop = function () {
    var offset = 300;
    var duration = 500;

    if (navigator.userAgent.match(/iPhone|iPad|iPod/i)) { // ios supported
      $(window).bind("touchend touchcancel touchleave", function (e) {
        if ($(this).scrollTop() > offset) {
          $('.scroll-to-top').fadeIn(duration);
        } else {
          $('.scroll-to-top').fadeOut(duration);
        }
      });
    } else { // general 
      $(window).scroll(function () {
        if ($(this).scrollTop() > offset) {
          $('.scroll-to-top').fadeIn(duration);
        } else {
          $('.scroll-to-top').fadeOut(duration);
        }
      });
    }

    $('.scroll-to-top').click(function (e) {
      e.preventDefault();
      $('html, body').animate({
        scrollTop: 0
      }, duration);
      return false;
    });
  };

  var handle100HeightContent = function () {

    $('.full-height-content').each(function () {
      var target = $(this);
      var height;

      height = OulankaApp.getViewPort().height -
        $('.page-header').outerHeight(true) -
        $('.page-footer').outerHeight(true) -
        $('.page-title').outerHeight(true) -
        $('.page-bar').outerHeight(true);

      if (target.hasClass('portlet')) {
        var portletBody = target.find('.portlet-body');

        // AmeliaApp.destroySlimScroll(portletBody.find('.full-height-content-body')); // destroy slimscroll 

        height = height -
          target.find('.portlet-title').outerHeight(true) -
          parseInt(target.find('.portlet-body').css('padding-top')) -
          parseInt(target.find('.portlet-body').css('padding-bottom')) - 5;

        if (OulankaApp.getViewPort().width >= resBreakpointMd && target.hasClass("full-height-content-scrollable")) {
          height = height - 35;
          portletBody.find('.full-height-content-body').css('height', height);
          // AmeliaApp.initSlimScroll(portletBody.find('.full-height-content-body'));
        } else {
          portletBody.css('min-height', height);
        }
      } else {
        // AmeliaApp.destroySlimScroll(target.find('.full-height-content-body')); // destroy slimscroll 

        if (OulankaApp.getViewPort().width >= resBreakpointMd && target.hasClass("full-height-content-scrollable")) {
          height = height - 35;
          target.find('.full-height-content-body').css('height', height);
          //  AmeliaApp.initSlimScroll(target.find('.full-height-content-body'));
        } else {
          target.css('min-height', height);
        }
      }
    });
  };

  var handleFixedSidebar = function () {
    var menu = $('.page-sidebar-menu');

    handleSidebarAndContentHeight();

    if ($('.page-sidebar-fixed').size() === 0) {
      //AmeliaApp.destroySlimScroll(menu);
      return;
    }

    if (OulankaApp.getViewPort().width >= resBreakpointMd && !$('body').hasClass('page-sidebar-menu-not-fixed')) {
      menu.attr("data-height", _calculateFixedSidebarViewportHeight());
      //AmeliaApp.destroySlimScroll(menu);
      //AmeliaApp.initSlimScroll(menu);
      handleSidebarAndContentHeight();
    }
  };
  return {
    initSidebar: function ($state) {
      handleFixedSidebar();
      OulankaApp.addResizeHandler(handleFixedSidebar);
    },

    initContent: function () {
      handle100HeightContent();
      OulankaApp.addResizeHandler(handleSidebarAndContentHeight);
      OulankaApp.addResizeHandler(handle100HeightContent);
    },

    initFooter: function () {
      handleGoTop();
    },

    init: function () {
      this.initSidebar(null);
      this.initContent();
      this.initFooter();
    },
    fixContentHeight: function () {
      handleSidebarAndContentHeight();
    },
    initFixedSidebar: function () {
      handleFixedSidebar();
    },
  };

}();

$(document).ready(function () {
  Layout.init();
  Layout.fixContentHeight();
  Layout.initFixedSidebar();
});
