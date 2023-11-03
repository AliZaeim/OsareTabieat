(function ($) {
    "use strict";

    /*=============================================
        =    		 Preloader			      =
    =============================================*/
    function preloader() {
        $('#preloader').delay(0).fadeOut();
    };

    $(window).on('load', function () {
        preloader();
        mainSlider();
        aosAnimation();
        wowAnimation();
    });


    /*=============================================
        =            Message Remove           =
    =============================================*/
    $('.top-notify-message .message-remove').on('click', function () {
        $('.header-message-wrap').slideUp(300);
        return false;
    });


    /*=============================================
        =    	   Toggle Active  	         =
    =============================================*/
    $('.cat-toggle').on('click', function () {
        $('.category-menu').slideToggle(500);
        return false;
    });


    /*=============================================
        =    		Mobile Menu			      =
    =============================================*/
    //SubMenu Dropdown Toggle
    if ($('.menu-area li.menu-item-has-children ul').length) {
        $('.menu-area .navigation li.menu-item-has-children').append('<div class="dropdown-btn"><span class="fas fa-angle-down"></span></div>');

    }
    //Mobile Nav Hide Show
    if ($('.mobile-menu').length) {

        var mobileMenuContent = $('.menu-area .main-menu').html();
        $('.mobile-menu .menu-box .menu-outer').append(mobileMenuContent);

        //Dropdown Button
        $('.mobile-menu li.menu-item-has-children .dropdown-btn').on('click', function () {
            $(this).toggleClass('open');
            $(this).prev('ul').slideToggle(500);
        });
        //Menu Toggle Btn
        $('.mobile-nav-toggler').on('click', function () {
            $('body').addClass('mobile-menu-visible');
        });

        //Menu Toggle Btn
        $('.menu-backdrop, .mobile-menu .close-btn').on('click', function () {
            $('body').removeClass('mobile-menu-visible');
        });
    };


    /*=============================================
        =     Menu sticky & Scroll to top      =
    =============================================*/
    $(window).on('scroll', function () {
        var scroll = $(window).scrollTop();
        if (scroll < 245) {
            $("#sticky-header").removeClass("sticky-menu");
            $('.scroll-to-target').removeClass('open');

        } else {
            $("#sticky-header").addClass("sticky-menu");
            $('.scroll-to-target').addClass('open');
        }
    });


    /*=============================================
        =    		 Scroll Up  	         =
    =============================================*/
    if ($('.scroll-to-target').length) {
        $(".scroll-to-target").on('click', function () {
            var target = $(this).attr('data-target');
            // animate
            $('html, body').animate({
                scrollTop: $(target).offset().top
            }, 1000);

        });
    };


    /*=============================================
        =          Data Background               =
    =============================================*/
    $("[data-background]").each(function () {
        $(this).css("background-image", "url(" + $(this).attr("data-background") + ")")
    });



    /*=============================================
        =    		 Main Slider		      =
    =============================================*/
    function mainSlider() {
        var BasicSlider = $('.slider-active');
        BasicSlider.on('init', function (e, slick) {
            var $firstAnimatingElements = $('.single-slider:first-child').find('[data-animation]');
            doAnimations($firstAnimatingElements);
        });
        BasicSlider.on('beforeChange', function (e, slick, currentSlide, nextSlide) {
            var $animatingElements = $('.single-slider[data-slick-index="' + nextSlide + '"]').find('[data-animation]');
            doAnimations($animatingElements);
        });
        BasicSlider.slick({
            autoplay: true,
            autoplaySpeed: 6000,
            dots: true,
            fade: true,
            arrows: false,
            Infinity: true,
            responsive: [
                { breakpoint: 767, settings: { dots: false, arrows: false } }
            ]
        });

        function doAnimations(elements) {
            var animationEndEvents = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
            elements.each(function () {
                var $this = $(this);
                var $animationDelay = $this.data('delay');
                var $animationType = 'animated ' + $this.data('animation');
                $this.css({
                    'animation-delay': $animationDelay,
                    '-webkit-animation-delay': $animationDelay
                });
                $this.addClass($animationType).one(animationEndEvents, function () {
                    $this.removeClass($animationType);
                });
            });
        }
    };


    /*=============================================
        =    	   Active / Remove Class       =
    =============================================*/
    $('.category-item').on('mouseenter', function () {
        $(this).addClass('active').parent().siblings().find('.category-item').removeClass('active');
    })



    /*=============================================
        =    		Category Active		      =
    =============================================*/
    $('.category-active').slick({
        dots: false,
        infinite: true,
        speed: 2000,
        autoplay: true,
        arrows: false,
        slidesToShow: 6,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 5,
                    slidesToScroll: 1,
                    infinite: true,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 767,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                    arrows: false,
                }
            },
            {
                breakpoint: 575,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    arrows: false,
                }
            },
        ]
    });


    /*=============================================
        =          best-deal-active        =
    =============================================*/
    $('.best-deal-active').slick({
        dots: false,
        infinite: false,
        speed: 1000,
        autoplay: true,
        arrows: false,
        slidesToShow: 5,
        slidesToScroll: 1,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1,
                    infinite: true,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 767,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                    arrows: false,
                }
            },
            {
                breakpoint: 575,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    arrows: false,
                }
            },
        ]
    });


    /*=============================================
        =    		Brand Active		      =
    =============================================*/
    $('.brand-active').slick({
        dots: false,
        infinite: true,
        speed: 1000,
        autoplay: true,
        arrows: false,
        slidesToShow: 6,
        slidesToScroll: 2,
        responsive: [
            {
                breakpoint: 1200,
                settings: {
                    slidesToShow: 5,
                    slidesToScroll: 1,
                    infinite: true,
                }
            },
            {
                breakpoint: 992,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 767,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 1,
                    arrows: false,
                }
            },
            {
                breakpoint: 575,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                    arrows: false,
                }
            },
        ]
    });


    /*=============================================
        =    		 Cart Active  	         =
    =============================================*/
    $(".cart-plus-minus").append('<div class="dec qtybutton">-</div><div class="inc qtybutton">+</div>');
    $(".qtybutton").on("click", function () {
        var $button = $(this);
        var oldValue = $button.parent().find("input").val();        
        if ($button.text() == "+") {
            var newVal = parseFloat(oldValue) + 1;
        } else {
            // Don't allow decrementing below zero
            if (oldValue > 1) {
                var newVal = parseFloat(oldValue) - 1;
            } else {
                newVal = 1;
            }
        }
        $button.parent().find("input").val(newVal);
    });
    $(".cart-button, .det-cart-button").on("click", function () {

        var picount = $(this).siblings('.inpcount').val();
        if ($(this).hasClass("det-cart-button")) {
            picount = $(this).siblings('.sd-cart-wrap').find('.inpcount').val();
        }


        var pid = $(this).attr("data-prid");
        var itemid = pid;
        var isproduct = $(this).attr("data-ispro");
        var ptype = "pr";
        if (isproduct == "no") {

            ptype = "pritem";
        }

        $.ajax({
            type: "GET",
            url: "/Shop?handler=AddtoCart",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { "pcount": picount, "itemid": itemid, "type": ptype },
            success: function (response) {
                console.log(response);
                if (response == "yes") {
                    Swal.fire({
                        icon: 'success',
                        title: 'به سبد اضافه شد',
                        showConfirmButton: true,
                        confirmButtonText: 'باشه',
                    }).then((result) => {
                        if (result.isConfirmed) {
                            location.reload();
                        }
                    })
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });
    $(".frmchkinput").on("change", function () {
        
        if ($(this).is(':checked')) {
            var idd = $(this).attr("data-id");            
            $(".det-cart-button").attr("data-ispro", "no");
            $(".det-cart-button").attr("data-prid", idd);
        }
        
    });
    $(".removeCartItem").on("click", function () {
        var crtId = $(this).attr("data-cartid");
        var itmId = $(this).attr("data-itemid");
        var itmname = $(this).attr("data-name");
        console.log("crtId : " + crtId + " itmId : " + itmId);
        var mes = "آیا مطمئن به حذف کالای " + itmname + " از سبد خرید هستید؟"
        Swal.fire({
            text: mes,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'بله',
            cancelButtonText: 'خیر'
        }).then((result) => {
            if (result.isConfirmed) {                
                $.ajax({
                    type: "Get",
                    url: "/Shop/?handler=RemoveItemFromCart",                    
                    data: { "cartId": crtId, "itemId": itmId },
                    success: function (response) {
                        if (response == "yes") {
                            
                            Swal.fire({
                                icon: 'success',
                                title: 'محصول از سبد حذف شد !',
                                showConfirmButton: true,
                                confirmButtonText: 'باشه',
                                timer: 3000
                            });
                            setTimeout(function () { location.reload(); }, 2000);
                        }
                    },
                    failure: function () {
                        alert("خطا رخ داده است !");
                    },
                    error: function () {
                        alert("خطا رخ داده است !");
                    }
                });

            }
        })

    })
    $(".updateCartItem").on("click", function () {
        var crtId = $(this).attr("data-cartid");
        var itmId = $(this).attr("data-itemid");
        var itemcnt = $(this).parent().parent().find(".in-num").val();
        $.ajax({
            type: "Get",
            url: "/Cart/?handler=UpdateCartItemQuantity",
            data: { "crtId": crtId, "cartitemId": itmId, "count": itemcnt },
            success: function (response) {
                if (response == "yes") {

                    Swal.fire({
                        icon: 'success',
                        title: 'سبد خرید اصلاح شد !',
                        showConfirmButton: true,
                        confirmButtonText: 'باشه',
                        timer: 3000
                    });
                    setTimeout(function () { location.reload(); }, 2000);
                }
            },
            failure: function () {
                alert("خطا رخ داده است !");
            },
            error: function () {
                alert("خطا رخ داده است !");
            }
        });
    })
    /*=============================================
        =    		Cart Active Two 	        =
    =============================================*/
    $('.qtybutton-box span').on("click", function () {
        var $input = $(this).parents('.num-block').find('input.in-num');
        
        if ($(this).hasClass('minus')) {
            var count = parseFloat($input.val()) - 1;
            count = count < 1 ? 1 : count;
            if (count < 2) {
                $(this).addClass('dis');
            }
            else {
                $(this).removeClass('dis');
            }
            $input.val(count);
        }
        else {
            var count = parseFloat($input.val()) + 1
            $input.val(count);
            if (count > 1) {
                $(this).parents('.num-block').find(('.minus')).removeClass('dis');
            }
        }
        $input.change();
        return false;
    });


    /*=============================================
        =    		Magnific Popup		      =
    =============================================*/
    $('.popup-image').magnificPopup({
        type: 'image',
        gallery: {
            enabled: true
        }
    });

    /* magnificPopup video view */
    $('.popup-video').magnificPopup({
        type: 'iframe'
    });


    /*=============================================
        =    	 Slider Range Active  	         =
    =============================================*/
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 600,
        values: [100, 480],
        slide: function (event, ui) {
            $("#amount").val(ui.values[0] + " هزار تومان " + " تا " + ui.values[1] + " هزار تومان ");
        }
    });
    $("#amount").val($("#slider-range").slider("values", 0) + " هزار تومان " + " تا " + $("#slider-range").slider("values", 1) + " هزار تومان ");


    /*=============================================
        =    		Isotope	Active  	      =
    =============================================*/
    $('.special--product-active').imagesLoaded(function () {
        // init Isotope
        var $grid = $('.special--product-active').isotope({
            itemSelector: '.grid-item',
            percentPosition: true,
            masonry: {
                columnWidth: '.grid-sizer',
            }
        });
        // filter items on button click
        $('.special--product-nav').on('click', 'button', function () {
            var filterValue = $(this).attr('data-filter');
            $grid.isotope({ filter: filterValue });
        });

    });
    //for menu active class
    $('.special--product-nav button').on('click', function (event) {
        $(this).siblings('.active').removeClass('active');
        $(this).addClass('active');
        event.preventDefault();
    });


    /*=============================================
        =    		 Aos Active  	         =
    =============================================*/
    function aosAnimation() {
        AOS.init({
            duration: 1000,
            mirror: true,
            once: true,
            disable: 'mobile',
        });
    };


    /*=============================================
        =    	  Countdown Active  	         =
    =============================================*/
    $('[data-countdown]').each(function () {
        var $this = $(this), finalDate = $(this).data('countdown');        
        $this.countdown(finalDate, function (event) {
            $this.html(event.strftime('<div class="time-count sec"><span>%S</span><span>ثانیه</span></div><div class="time-count min"><span>%M</span><span>دقیقه</span></div><div class="time-count hour"><span>%H</span><span>ساعت</span></div><div class="time-count day"><span>%D</span><span>روز</span></div>'));
        });
    });


    /*=============================================
        =    		 Wow Active  	         =
    =============================================*/
    function wowAnimation() {
        var wow = new WOW({
            boxClass: 'wow',
            animateClass: 'animated',
            offset: 0,
            mobile: false,
            live: true
        });
        wow.init();
    };


})(jQuery);