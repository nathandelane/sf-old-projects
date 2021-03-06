var oddOrEvenClassName = "";

function setupEventHandlers() {
	document.getElementById("contentContainer").oncontextmenu = function() {
		return false;
	}
	
	return true;
}

function parseClick(clickedObject, event) {
	var ctrlKey = document.layers ? event.modifiers & Event.CONTROL_MASK : event.ctrlKey;
	var button = ((event.which)?event.which:event.button);
	var nodeIndex = 3;
	
	event.cancelBubble = true;
	if (event.stopPropagation) event.stopPropagation();
	
	if(document.getElementById('phyleboxContextMenu').style.display == "block") {
		document.getElementById('phyleboxContextMenu').style.display = "none"
	} else if(clickedObject.nodeName == "TR") {
		if(button == 1 && ctrlKey == true || button > 1) {
			try {
				var fi = document.getElementById('fileItem');
				//alert("children:" + clickedObject.childNodes[nodeIndex].childNodes[3] + ";" + clickedObject.childNodes[nodeIndex].childNodes[3].innerHTML);
				if(fi.childNodes.length > 0) {
					fi.removeChild(fi.firstChild);
				}
				
				if(clickedObject.childNodes[nodeIndex].childNodes[3].innerHTML != null) {
					newTextNode = document.createTextNode(clickedObject.childNodes[nodeIndex].childNodes[3].innerHTML);
					fi.appendChild(newTextNode);
				}
			} catch(err) {
				alert("Exception occurred trying to get file name.");
			}
			
			var xLoc = "0px";
			var yLoc = "0px";
			
			if(window.event) {
				xLoc = (event.clientX - 160) + "px";
				yLoc = (event.clientY - 100) + "px";
			} else {
				xLoc = (event.pageX - 160) + "px";
				yLoc = (event.pageY - 100) + "px";
			}
			
			document.getElementById('phyleboxContextMenu').style.left = xLoc;
			document.getElementById('phyleboxContextMenu').style.top = yLoc;
			document.getElementById('phyleboxContextMenu').style.display = "block";
		} else if(button == 1 && ctrlKey == false) {
			if(clickedObject.childNodes[1].firstChild.checked) {
				clickedObject.childNodes[1].firstChild.checked = false;
			} else {
				clickedObject.childNodes[1].firstChild.checked = true;
			}
		}
	}
	
	return false;
}

function showPageCover() {
	document.getElementById('pageCover').style.display = "block";
}

function hidePageCover() {
	document.getElementById('pageCover').style.display = "none";
}

function openRecycleBin() {
	showPageCover();
	document.getElementById('recycleBinContents').style.display = "block";
}

function closeRecycleBin() {
	hidePageCover();
	document.getElementById('recycleBinContents').style.display = "none";
}