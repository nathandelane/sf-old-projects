function setupEventHandlers() {
	document.getElementById("folderContentsTable").oncontextmenu = function() {
		return false;
	}
	
	return true;
}

function parseClick(clickedObject, event) {
	var ctrlKey = document.layers ? event.modifiers & Event.CONTROL_MASK : event.ctrlKey;
	event.cancelBubble = true;
	if (event.stopPropagation) event.stopPropagation();
	alert(clickedObject.tagName + ";" + clickedObject.id + "; button=" + ((event.which)?event.which:event.button) + ";ctrl=" + ctrlKey);
	return false;
}
