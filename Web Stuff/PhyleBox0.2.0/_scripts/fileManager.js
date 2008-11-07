var windowWidth = 0;
var windowHeight = 0;
var currentlySelectedView = "details";

function padRowNumber(number) {
	rowNumber = "" + number;
	
	while(rowNumber.length < 3)
	{
		rowNumber = '0' + rowNumber;
	}
	
	return rowNumber;
}

function initializeEventHandlers() {
	//window.onresize = scaleScalableElements;
	//setWidthHeight();
	//document.getElementById('rightPanel').style.width = (windowWidth - 150) + "px";
	
	if(totalRows) {
		for(i = 0; i < totalRows; i++) {
			rowName = "row" + padRowNumber(i);
			
			if(document.attachEvent) {
				document.getElementById(rowName).attachEvent('onclick', checkOrUncheck);
			} else {
				document.getElementById(rowName).addEventListener('click', checkOrUncheck, false);
			}
		}
	}

	selectCurrentlySelectedView();

	if(document.attachEvent) {
		document.getElementById('permissionsItem').attachEvent('onclick', expandOrCollapseUserItem);
		document.getElementById('logoutItem').attachEvent('onclick', expandOrCollapseUserItem);
		document.getElementById('profileItem').attachEvent('onclick', expandOrCollapseUserItem);
		document.getElementById('servicesItem').attachEvent('onclick', expandOrCollapseUserItem);
		document.getElementById('iconsViewRadioButton').attachEvent('onclick', selectRadioButton);
		document.getElementById('detailsViewRadioButton').attachEvent('onclick', selectRadioButton);
		document.getElementById('thumbsViewRadioButton').attachEvent('onclick', selectRadioButton);
		document.getElementById('treeViewRadioButton').attachEvent('onclick', selectRadioButton);
		document.getElementById('yesLogoutButton').attachEvent('onmouseover', activatePseudoButton);
		document.getElementById('noDontLogoutButton').attachEvent('onmouseover', activatePseudoButton);
		document.getElementById('yesLogoutButton').attachEvent('onmouseout', deactivatePseudoButton);
		document.getElementById('noDontLogoutButton').attachEvent('onmouseout', deactivatePseudoButton);
		document.getElementById('helpButton').attachEvent('onmouseover', activatePseudoButton);
		document.getElementById('uploadFilesButton').attachEvent('onmouseover', activatePseudoButton);
		document.getElementById('helpButton').attachEvent('onmouseout', deactivatePseudoButton);
		document.getElementById('uploadFilesButton').attachEvent('onmouseout', deactivatePseudoButton);
		document.getElementById('editProfileButton').attachEvent('onmouseover', activatePseudoButton);
		document.getElementById('editProfileButton').attachEvent('onmouseout', deactivatePseudoButton);
		document.getElementById('settingsButton').attachEvent('onmouseover', activatePseudoButton);
		document.getElementById('settingsButton').attachEvent('onmouseout', deactivatePseudoButton);
	} else {
		document.getElementById('permissionsItem').addEventListener('click', expandOrCollapseUserItem, false);
		document.getElementById('logoutItem').addEventListener('click', expandOrCollapseUserItem, false);
		document.getElementById('servicesItem').addEventListener('click', expandOrCollapseUserItem, false);
		document.getElementById('profileItem').addEventListener('click', expandOrCollapseUserItem, false);
		document.getElementById('iconsViewRadioButton').addEventListener('click', selectRadioButton, false);
		document.getElementById('detailsViewRadioButton').addEventListener('click', selectRadioButton, false);
		document.getElementById('thumbsViewRadioButton').addEventListener('click', selectRadioButton, false);
		document.getElementById('treeViewRadioButton').addEventListener('click', selectRadioButton, false);
		document.getElementById('yesLogoutButton').addEventListener('mouseover', activatePseudoButton, false);
		document.getElementById('noDontLogoutButton').addEventListener('mouseover', activatePseudoButton, false);
		document.getElementById('yesLogoutButton').addEventListener('mouseout', deactivatePseudoButton, false);
		document.getElementById('noDontLogoutButton').addEventListener('mouseout', deactivatePseudoButton, false);
		document.getElementById('helpButton').addEventListener('mouseover', activatePseudoButton, false);
		document.getElementById('uploadFilesButton').addEventListener('mouseover', activatePseudoButton, false);
		document.getElementById('helpButton').addEventListener('mouseout', deactivatePseudoButton, false);
		document.getElementById('uploadFilesButton').addEventListener('mouseout', deactivatePseudoButton, false);
		document.getElementById('editProfileButton').addEventListener('mouseover', activatePseudoButton, false);
		document.getElementById('editProfileButton').addEventListener('mouseout', deactivatePseudoButton, false);
		document.getElementById('settingsButton').addEventListener('mouseover', activatePseudoButton, false);
		document.getElementById('settingsButton').addEventListener('mouseout', deactivatePseudoButton, false);
	}
}

function checkOrUncheck(e) {
	var target;

	if(!e) var e = window.event;
	if(e.target) {
		target = e.target;
	} else if(e.srcElement) {
		target = e.srcElement;
	}
	
	if(target.nodeName !== "INPUT")
	{
		checkBoxId = target.parentNode.parentNode.id + "CheckBox";
		objCheckBox = document.getElementById(checkBoxId);
		
		if(objCheckBox.checked) {
			objCheckBox.checked = false;
		} else {
			objCheckBox.checked = true;
		}
	}
}

function activatePseudoButton(e) {
	var target;

	if(!e) var e = window.event;
	if(e.target) {
		target = e.target;
	} else if(e.srcElement) {
		target = e.srcElement;
	}

	target.style.color = "#ffffff";
}

function deactivatePseudoButton(e) {
	var target;

	if(!e) var e = window.event;
	if(e.target) {
		target = e.target;
	} else if(e.srcElement) {
		target = e.srcElement;
	}

	target.style.color = "#9fcbf8";
}

function selectCurrentlySelectedView() {
	document.getElementById('iconsViewRadioButton').className = "radioButtonDeselected";
	document.getElementById('detailsViewRadioButton').className = "radioButtonDeselected";
	document.getElementById('thumbsViewRadioButton').className = "radioButtonDeselected";
	document.getElementById('treeViewRadioButton').className = "radioButtonDeselected";

	if(currentlySelectedView == "icons") {
		document.getElementById('iconsViewRadioButton').className = "radioButtonSelected";
	} else if(currentlySelectedView == "details") {
		document.getElementById('detailsViewRadioButton').className = "radioButtonSelected";
	} else if(currentlySelectedView == "thumbs") {
		document.getElementById('thumbsViewRadioButton').className = "radioButtonSelected";
	} else if(currentlySelectedView == "tree") {
		document.getElementById('treeViewRadioButton').className = "radioButtonSelected";
	}
}

function setWidthHeight() {
	if(window.innerWidth) {
		if(window.innerWidth > minimumWidth) {
			windowWidth = window.innerWidth;
		} else {
			widowWidth = minimumWidth;
		}
		windowHeight = window.innerHeight;
	} else if(document.body.offsetWidth) {
		if(document.body.offsetWidth > minimumWidth) {
			windowWidth = document.body.offsetWidth;
		} else {
			widowWidth = minimumWidth;
		}
		windowHeight = document.body.offsetHeight;
	}
}

function scaleScalableElements(e) {
	var target;

	if(!e) var e = window.event;
	if(e.target) {
		target = e.target;
	} else if(e.srcElement) {
		target = e.srcElement;
	}

	setWidthHeight();

	document.getElementById('rightPanel').style.width = (windowWidth - 150) + "px";
	document.getElementById('rightPanel').style.height = (windowHeight - 40) + "px";
}

function expandOrCollapseUserItem(e) {
	var target;

	if(!e) var e = window.event;
	if(e.target) {
		target = e.target;
	} else if(e.srcElement) {
		target = e.srcElement;
	}

	if(target.className == "collapsed") {
		target.className = "expanded";
	} else if(target.className == "expanded") {
		target.className = "collapsed";
	}
}

function selectRadioButton(e) {
	var target;

	if(!e) var e = window.event;
	if(e.target) {
		target = e.target;
	} else if(e.srcElement) {
		target = e.srcElement;
	}

	if(target.id == "iconsViewRadioButton") {
		document.getElementById('detailsViewRadioButton').className = "radioButtonDeselected";
		document.getElementById('thumbsViewRadioButton').className = "radioButtonDeselected";
		document.getElementById('treeViewRadioButton').className = "radioButtonDeselected";
		target.className = "radioButtonSelected";
		currentlySelectedView = "icons";
		document.getElementById('iconsView').style.display = "block";
		document.getElementById('detailsView').style.display = "none";
		document.getElementById('thumbnailsView').style.display = "none";
		document.getElementById('treeView').style.display = "none";
	} else if(target.id == "detailsViewRadioButton") {
		document.getElementById('iconsViewRadioButton').className = "radioButtonDeselected";
		document.getElementById('thumbsViewRadioButton').className = "radioButtonDeselected";
		document.getElementById('treeViewRadioButton').className = "radioButtonDeselected";
		target.className = "radioButtonSelected";
		currentlySelectedView = "details";
		document.getElementById('detailsView').style.display = "block";
		document.getElementById('iconsView').style.display = "none";
		document.getElementById('thumbnailsView').style.display = "none";
		document.getElementById('treeView').style.display = "none";
	} else if(target.id == "thumbsViewRadioButton") {
		document.getElementById('iconsViewRadioButton').className = "radioButtonDeselected";
		document.getElementById('detailsViewRadioButton').className = "radioButtonDeselected";
		document.getElementById('treeViewRadioButton').className = "radioButtonDeselected";
		target.className = "radioButtonSelected";
		currentlySelectedView = "thumbs";
		document.getElementById('thumbnailsView').style.display = "block";
		document.getElementById('detailsView').style.display = "none";
		document.getElementById('iconsView').style.display = "none";
		document.getElementById('treeView').style.display = "none";
	} else if(target.id == "treeViewRadioButton") {
		document.getElementById('iconsViewRadioButton').className = "radioButtonDeselected";
		document.getElementById('thumbsViewRadioButton').className = "radioButtonDeselected";
		document.getElementById('detailsViewRadioButton').className = "radioButtonDeselected";
		target.className = "radioButtonSelected";
		currentlySelectedView = "tree";
		document.getElementById('treeView').style.display = "block";
		document.getElementById('detailsView').style.display = "none";
		document.getElementById('thumbnailsView').style.display = "none";
		document.getElementById('iconsView').style.display = "none";
	}
}
