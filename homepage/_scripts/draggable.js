function init() {
	/*
	makeClickable(document.getElementById("black"));
	makeClickable(document.getElementById("red"));
	makeClickable(document.getElementById("yellow"));
	makeDraggable(document.getElementById("black"));
	makeDraggable(document.getElementById("red"));
	makeDraggable(document.getElementById("yellow"));
	addDropTarget(document.getElementById("dropTarget"));
	*/
}

document.onmousemove = mouseMove;

function mouseCoords(e) {
	if(e.pageX || e.pageY) {
		return {x:e.pageX, y:e.pageY};
	} else {
		return {
			x:e.clientX + document.body.scrollLeft - document.body.clientLeft,
			y:e.clientY + document.body.scrollTop - document.body.clientTop
		};
	}	
}

document.onmouseup = mouseUp;
var dragObject = null;
var mouseOffset = null;

function makeClickable(object) {
	object.onmousedown = function() {
		dragObject = this;
	}
}

function mouseUp(e) {
	e = e || window.event;
	var mousePos = mouseCoords(e);
	
	for(var i = 0; i < dropTargets.length; i++) {
		var curTarget = dropTargets[i];
		var targetPos = getPosition(curTarget);
		var targWidth = parseInt(curTarget.offsetWidth);
		var targHeight = parseInt(curTarget.offsetHeight);
		
		if(
			(mousePos.x > targetPos.x) &&
			(mousePos.x < (targetPos.x + targWidth) &&
			(mousePos.y > targetPos.y) &&
			(mousePos.y < (targetPos.y + targHeight)))) {
			
			document.getElementById("dragObj").value = e.target.id;
		}		
	}
	
	dragObject = null;
}

document.onmousemove = mouseMove;
document.onmouseup = mouseUp;

function getMouseOffset(target, e) {
	e = e || window.event;
	
	var docPos = getPosition(target);
	var mousePos = mouseCoords(e);
	return {x:mousePos.x - docPos.x, y:mousePos.y - docPos.y};
}

function getPosition(e) {
	var left = 0;
	var top = 0;
	
	while(e.offsetParent) {
		left += e.offsetLeft;
		top += e.offsetTop;
		e = e.offsetParent;
	}
	
	left += e.offsetLeft;
	top += e.offsetTop;
	
	return {x:left, y:top};
}

function mouseMove(e) {
	e = e || window.event;
	var mousePos = mouseCoords(e);
	
	el = document.getElementById("xCoord");
	el.value = mousePos.x;
	el = document.getElementById("yCoord");
	el.value = mousePos.y;

	if(dragObject) {
		dragObject.style.position = "absolute";
		dragObject.style.top = mousePos.y - mouseOffset.y;
		dragObject.style.left = mousePos.x - mouseOffset.x;
		
		return false;
	}
}

function makeDraggable(object) {
	if(!object) return;
	
	object.onmousedown = function(e) {
		dragObject = this;
		mouseOffset = getMouseOffset(this, e);
		
		return false;
	}
}

var dropTargets = [];

function addDropTarget(object) {
	dropTargets.push(object);
}

function removeDropTarget(object) {
	dropTargets.pop(object);
}
