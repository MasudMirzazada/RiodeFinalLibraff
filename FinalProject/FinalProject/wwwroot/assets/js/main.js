
$(document).ready(function(){

	//Header
// Search div open-close
$(".open-search-div").click(function(){
	$("#search").slideDown("slow");
})
$(".close-inp").click(function(){
	$("#search").slideUp("slow");
})
// Cart click overlay
$(".shopping-cart-header").click(function(){
	$(".overlay-cart").css({
		"opacity" : "1",
		"visibility" : "visible"
	})
	$(".header-cart").css({
		"transform": "translateX(0%)"

































	})
})
$(".overlay-cart").click(function(){
	$(".overlay-cart").css({
		"opacity" : "0",
		"visibility" : "hidden"
	})
	$(".header-cart").css({
		"transform": "translateX(100%) translatex(30px)"
	})
	$("#modal-prod").css({
		"opacity" : "0",
		"visibility" : "hidden"
	})
})
$(".cart-close-icon").click(function(){
	$(".overlay-cart").css({
		"opacity" : "0",
		"visibility" : "hidden"
	})
	$(".header-cart").css({
		"transform": "translateX(100%) translatex(30px)"
	})
})

	//Arrival
//Open product Modal
 $(".open-modal").parent().click(function(e){
	 e.preventDefault()
	 $("#modal-prod").css({
		 "opacity" : "1",
		 "visibility" : "visible"
	 })
	 $(".overlay-cart").css({
		"opacity" : "1",
		"visibility" : "visible"
	})
 })
// // Sizes change active class
// $(".sizes a").click(function(e){
//	 e.preventDefault()
//	 $(this).addClass("active-det");
//	 $(this).siblings().removeClass("active-det")
// })
// $(".colors a").click(function(e){
//	e.preventDefault()
//	$(this).addClass("active-det");
//	$(this).siblings().removeClass("active-det")
//})
	// SHOP---->
 // Grid change
	$(".equal").click(function(){
		//self change
		$(this).addClass("active-grid");
		$(".nonequal").removeClass("active-grid");
		// product list change
		$(this).parent().parent().next().find(".product-card").removeClass("product-card-row");

	})
	$(".nonequal").click(function(){
		//self change
		$(this).addClass("active-grid");
		$(".equal").removeClass("active-grid");
		// product list change
		$(this).parent().parent().next().find(".product-card").addClass("product-card-row");
	})
 // Dropdown sidebar menu
	$(".fa-plus").click(function(e){
		e.preventDefault();
		$(this).parent().parent().siblings().find(".sub-list").slideUp();
		//$(".fa-minus").css({
		//	"opacity" : "0",
		//	"visibility" : "hidden"
		//})
		$(".fa-plus").css({
			"opacity" : "1",
			"visibility" : "visible"
		})
		$(this).parent().next(".sub-list").slideDown();

		$(this).css({
			"opacity": "0 ",
			"visibility": "hidden"
		});
		$(this).prev().css({
			"opacity": "1 !important",
			"visibility": "visible !important"
		});
	})
	$(".fa-minus").click(function(e){
		e.preventDefault();
		$(this).parent().parent().siblings().find(".sub-list").slideUp();
		$(this).parent().next(".sub-list").slideUp();
		//$(this).css({
		//	"opacity": "0 ",
		//	"visibility": "hidden "
		//});
		$(this).next().css({
			"opacity" : "1",
			"visibility" : "visible"
		});
	})
	$('ul.tabs li').click(function(){
		var tab_id = $(this).attr('data-tab');
	
		$('ul.tabs li').removeClass('current');
		$('.tab-content').removeClass('current');
	
		$(this).addClass('current');
		$("#"+tab_id).addClass('current');
	})


	// BASKET---->
//Check out
	$(".estimates").click(function(){
		$("#shipping").slideToggle();
	})
	$(".addNote").click(function(){
		$(this).css({"display" : "none"});
		$("#addNoteInput").css({"display" : "block"})
		$(".addNoteLabel").css({"display" : "block"})
	})

	// Window
// To top function
document.querySelector(".topBtn").addEventListener("click",function(){
  window.scrollTo({
    top: 0,
    behavior: 'smooth'
  });
})
	// Parallax background
	function backImgParallax() {
		let offSetY = window.pageYOffset;
		$("#maker").css({
			"background-position-y": `${-(offSetY - 750) * 0.2 + "px"}`
		})
		$("#offer").css({
			"background-position-y": `${-(offSetY - 750) * 0.2 + "px"}`
		})
	}
	backImgParallax();
	$(window).scroll(function () {
		backImgParallax()
    })
	//Profile page

	$(".myaccount-tab-menu a").click(function (e) {
		e.preventDefault();
		let name = $(this).attr("data-toggle");
		$(this).parent().find("a").removeClass("active");
		$(this).addClass("active");
		$(".tab-content div").css({
			"display": "none",
			"opacity": "0"
		})
		$(`#${name}`).css({
			"display": "block",
			"opacity": "1"
		})
		$(`#${name} .myaccount-content`).css({
			"display": "block",
			"opacity": "1"
		})
		$(`#${name} .myaccount-table`).css({
			"display": "block",
			"opacity": "1"
		})
		$(`#${name}`).addClass("show active")
    })
})		

// Sticky header
window.onscroll = function () {
	var phoneHeader = document.querySelector(".sticky-header");
	if(window.scrollY >= 240){
		phoneHeader.style.transform = "translateY(0px)";
		phoneHeader.style.transition = "transition: all .5s";
		phoneHeader.style.opacity = "1";
		if (window.scrollY > 1000) {
			const counters = document.querySelectorAll('.value');
			const speed = 200;

			counters.forEach(counter => {
				var animate = () => {
					const value = +counter.getAttribute('akhi');
					const data = +counter.innerText;

					const time = value / speed;
					if (data < value) {
						counter.innerText = Math.ceil(data + time);
						setTimeout(animate, 300);
					} else {
						counter.innerText = value;
					}
				}
				setTimeout(animate, 200);
			});
		}
		}
		else{
		phoneHeader.style.transition = "transition: all .5s";
		phoneHeader.style.transform = "translateY(-80px)";
		phoneHeader.style.opacity = "0";
	
		}
		ScrollToTop();
	}
// Scroll to top button visibility
function ScrollToTop(){
  let scBtn= document.querySelector(".toTopBtn");
  if(window.scrollY > 350){
    scBtn.style.opacity = "1"
    scBtn.style.visibility = "visible"
  }
  else{
    scBtn.style.opacity = "0"
    scBtn.style.visibility = "hidden"
  }
}
// Intro slider
var swiperIntro = new Swiper(".mySwiperIntro", {
	loop : true,
	navigation: {
		nextEl: ".swiper-button-next",
		prevEl: ".swiper-button-prev",
	},
});
	
// Swipe slider
var swipert1 = new Swiper(".mySwiperTab1", {
	slidesPerView: 4,
	grid: {
		rows: 2,
	},
	navigation: {
		nextEl: ".swiper-button-next-t1",
		prevEl: ".swiper-button-prev-t1",
	},
});
var swipert2 = new Swiper(".mySwiperTab2", {
	slidesPerView: 4,
	grid: {
		rows: 2,
	},
	navigation: {
		nextEl: ".swiper-button-next-t2",
		prevEl: ".swiper-button-prev-t2",
	},
});
var swipert3 = new Swiper(".mySwiperTab3", {
	slidesPerView: 4,
	grid: {
		rows: 2,
	},
	navigation: {
		nextEl: ".swiper-button-next-t3",
		prevEl: ".swiper-button-prev-t3",
	},
});

var swiper2 = new Swiper(".mySwiper2", {
	slidesPerView: 4,
	spaceBetween: 30,
	pagination: {
	  el: ".swiper-pagination",
	  clickable: true,
	},
	});
// Shop left bottom slider
var swiper3 = new Swiper(".mySwiper3", {
	navigation: {
		nextEl: ".swiper-button-next",
		prevEl: ".swiper-button-prev",
	},
});
var swiperModal = new Swiper(".mySwiperModal", {
  navigation: {
	nextEl: ".swiper-button-next-modal",
	prevEl: ".swiper-button-prev-modal",
  },
});