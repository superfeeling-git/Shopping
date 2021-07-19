// JavaScript Document

$(function(){
	
	$(".quanbufle").bind("mousemove",function(){
	$(this).find('dd').fadeIn(200);
    });
	$(".quanbufle").bind("mouseleave",function(){		
	$(this).find('dd').fadeOut(200);
	
    });		
	
/***********************************************首页切换	***/
 $(".tabAuto li:first").addClass("current");
 $(".tgh-box div").not(":first").hide(); 
 $(".tabAuto li").click(function(){
  $(this).addClass("current").siblings().removeClass("current"); 
  var index = $(".tabAuto li").index(this);
  $(".tgh-box div").eq(index).show().siblings().hide();
 });
 
 /******************************************返回顶部*/
	 $(window).scroll(function(){
			 if ($(window).scrollTop()>100){
			 $(".fhtop").fadeIn(1500)
			 }
			 else{
				 $(".fhtop").fadeOut(1500);}
				 });
	$(".fhtop").click(function(){
		$('body,html').animate({scrollTop:0},300);
		return false;
		});
		
/***********************************************product加数量效果 ****/
	$('#content1_2but1').click(function(){
	 var i=	parseInt($("#content1_2text").val());
		$('#content1_2text').val(i+1);
	});
	$('#content1_2but2').click(function(){
	 var i=	parseInt($("#content1_2text").val());
	 if(i<=1){
		$('#content1_2text').val()=1;
	};
		$('#content1_2text').val(i-1);
	}); 
	
/*	$('.shuliang .texta').click(function(){
	 var i=	parseInt($(".textb").val());
		$(this).parents('div.textb').val(i+1);
	});
	$('#content1_2but2').click(function(){
	 var i=	parseInt($("#content1_2text").val());
	 if(i<=1){
		$('#content1_2text').val()=1;
	};
		$('#content1_2text').val(i-1);
	}); */	
	
/*********************************************内页导航展开折叠*/
	var objStr = ".leftnav li h3";
	$(objStr).each(function(i){
		$(this).click(function(){
		$($(".leftnav li h3")[i]).next("p").slideToggle(20);				 
		$(this).toggleClass("cur");  
		$(this).siblings("h3").removeClass("cur");  
		});
	});
	
	
	
})

 /*******************************************首页自动切换代码*/
 $(function(){
  $(".tabAuto li:first").addClass("current");
  $(".tgh-box div:first").css('display','block');
  autoScroll();
  contentHover();
  currentHover(); 
  
 });
 var i=-1; 
 var offset = 3000;  
 var timer = null;
 function autoScroll(){
  var n = $(".tabAuto li").length - 1;
  i++;
  if(i > n)
  i=0;
  slide(i);
  timer = window.setTimeout(autoScroll,offset);
 }
 function slide(i){
  $(".tabAuto li").eq(i).addClass("current").siblings().removeClass("current");
  $(".tgh-box div").eq(i).show().siblings().hide(); 
 }
 function currentHover(){
  $(".tabAuto li").hover(function(){
   if(timer){ 
   clearTimeout(timer);
   i = $(this).prevAll().length;
   slide(i);
   }
   },function(){
    timer = window.setTimeout(autoScroll,offset);
    this.blur();            
                return false; 
   }) 
 }
 function contentHover(){
  $(".tgh-box div").hover(function(){
   if(timer){
    clearTimeout(timer);
    i = $(this).prevAll().length;
          slide(i); 
   } 
  },function(){
   timer = window.setTimeout(autoScroll,offset);
   this.blur();
   return false;
  }) 
 }


/*******************************************失焦获焦*/
function inputTipText(){
$("input[class*=common_text]") 
.each(function(){
   var oldVal=$(this).val();     
   $(this)
   .css({"color":"#a5a5a5"})    
   .focus(function(){
    if($(this).val()!=oldVal){$(this).css({"color":"#333"})}else{$(this).val("").css({"color":"#a5a5a5"})}
	//$(this).css({"border":"1px solid #3b5998"})
   })
   .blur(function(){
    if($(this).val()==""){$(this).val(oldVal).css({"color":"#a5a5a5"})}
	//$(this).css({"border":"1px solid #dfdfdf"})
   })
   .keydown(function(){$(this).css({"color":"#333"})})
  	
})
}
/*******************************************密码*/
$(function(){
inputTipText();    
});
 	
    $(function () {
        $(".input_logina[type=text]").focus(function () {
            $(this).hide();
            $(".input_login[type=password]").show().focus();
        })
        $(".input_login[type=password]").blur(function () {
            if ($(this).val() == "") {
                $(this).hide();
                $(".input_logina[type=text]").show();
            }
        })
       
    });
