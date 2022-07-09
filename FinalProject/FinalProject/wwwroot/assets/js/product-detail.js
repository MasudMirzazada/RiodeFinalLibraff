var swiperprodDet = new Swiper(".mySwiperProdDetail", {
	slidesPerView: 4,
    loop: true,
	navigation: {
	  nextEl: ".swiper-button-next",
	  prevEl: ".swiper-button-prev",
	},
	});
	
// Swiper slider
var swiperSmallImage = new Swiper(".mySwiperSmallImg", {
		spaceBetween: 10,
		slidesPerView: 2,
		freeMode: true,
		clickable: false,
		watchSlidesProgress: true,
	  });
var swiperBigImage = new Swiper(".mySwiperBigImg", {
	  cssMode: true,
		spaceBetween: 10,
		navigation: {
		  nextEl: ".swiper-button-next",
		  prevEl: ".swiper-button-prev",
		},
		thumbs: {
			swiper: swiperSmallImage,
		},
		});

	$(document).ready(function(){
		// PRODUCT DETAIL---->

 // Image on hover move
 var X = 0;
 var Y = 0;
 $(".image-b img").mouseover(function(en){
	 $(this).css({
	 "opacity" : "0"
	 })
	 X=310;
	 Y=410;
	 $(this).parent().css({
	 "background-image" :`url(${this.src})`,
	 "background-position" : `${X}px ${Y}px`,
	"background-repeat": "no-repeat",
	"background-size": "cover",
	 "transform" : "scale(1.4)",
	 "transition" : "all 0s"
	 })
	 $(this).mousemove(function(e){
	 $(this).parent().css({
		 "background-image" :`url(${this.src})`,
		 "background-position" : `${X-e.pageX/2}px ${Y-e.pageY/2}px`,
		 "transform" : "scale(1.4)",
		 "transition" : "all 0s"
	 })
	 })
	 $(".image-b img").mouseleave(function(){
		 $(this).css({
		 "opacity" : "1"
		 })
		 $(this).parent().css({
		 "background-image" :`url(${this.src})`,
		 "background-position" : `${X}px ${Y}px`,
		 "transform" : "scale(1)",
		 "transition" : "all 0s"
		 })
	 })
 })
	})