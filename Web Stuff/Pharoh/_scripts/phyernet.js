var mouseX = 0;
var mouseY = 0;
var newsLocation = 0;
var active = true;
var millisecondInterval = 250;
var decrementLength = 5;
var heightDifference = 175;

$().mousemove(function(e) {
	if(!e) var e = window.event;
	
	if(e.clientX || e.clientY) {
		mouseX = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
		mouseY = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
	} else if(e.pageX || e.pageY) {
		mouseX = e.pageX;
		mouseY = e.pageY;
	}
});

$(document).ready(init);

function init() {
	// Start the news items scrolling
	$("div#newsContainerInner").everyTime(millisecondInterval, 'controlled', function(i) {
		height = $("div#newsContainerInner").height() - heightDifference;
		if(newsLocation > (height * -2)) {
			$("div#newsContainerInner").css("top", (newsLocation -= decrementLength) + "px");
		} else {
			newsLocation = 0;
		}
	});
	
	// Stop news items on mouse over
	$("div#newsContainerInner").mouseover(function() {
		if(active) {
			active = false;
			$(this).stopTime('controlled');
		}
	});
	
	// Start news items again on mouse out
	$("div#newsContainerInner").mouseout(function() {
		if(!active) {
			active = true;
			$("div#newsContainerInner").everyTime(millisecondInterval, 'controlled', function(i) {
				height = $("div#newsContainerInner").height() - heightDifference;
				if(newsLocation > (height * -2)) {
					$("div#newsContainerInner").css("top", (newsLocation -= decrementLength) + "px");
				} else {
					newsLocation = 0;
				}
			});
		}
	});
	
	$("h3#phyerNetVisual")
		.hover(
			function() {showInformationFor("visual")}, 
			function(){$("div#popup").find("span:last").remove();$("div#popup").css("display", "none");}
		);
	$("h3#phyerNetAudio")
		.hover(
			function() {showInformationFor("audio")}, 
			function(){$("div#popup").find("span:last").remove();$("div#popup").css("display", "none");}
		);
	$("h3#phyerNetLiterature")
		.hover(
			function() {showInformationFor("literature")}, 
			function(){$("div#popup").find("span:last").remove();$("div#popup").css("display", "none");}
		);
	$("h3#phyerNetLogical")
		.hover(
			function() {showInformationFor("logical")}, 
			function(){$("div#popup").find("span:last").remove();$("div#popup").css("display", "none");}
		);
}

function showInformationFor(infoType) {
	$("div#popup").find("span:last").remove();
	if(infoType == "visual") {
		$("div#popup")
			.append('<span>Creative minds that focus on "visual" forms of artistic expression</span>');
	} else if(infoType == "audio") {
		$("div#popup")
			.append('<span>Artistic souls that express creativity through sound</span>');
	} else if(infoType == "literature") {
		$("div#popup")
			.append('<span>Users that express creativity through words</span>');
	} else if(infoType == "logical") {
		$("div#popup")
			.append('<span>People who express creativity through "technical" means</span>');
	}
	
	$("div#popup")
		.css("left", (mouseX + 10) + "px");
	
	$("div#popup")
		.css("top", (mouseY + 10) + "px");
	
	$("div#popup")
		.css("display", "block");
}
