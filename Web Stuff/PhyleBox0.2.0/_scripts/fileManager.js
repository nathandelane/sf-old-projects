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
	if(totalRows) {
		for(i = 1; i < totalRows; i++) {
			rowName = "row" + padRowNumber(i) + "CheckBox";
			
			if(document.attachEvent) {
				document.getElementById(rowName).attachEvent('onclick', isChecked);
			} else {
				document.getElementById(rowName).addEventListener('click', isChecked, false);
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
		document.getElementById('downloadAllButton').attachEvent('onclick', downloadFiles);
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
		document.getElementById('downloadAllButton').addEventListener('click', downloadFiles, false);
	}
}

function downloadFiles(e) {
	var target;
	
	if(!e) var e = window.event;
	if(e.target) {
		target = e.target;
	} else if(e.srcElement) {
		target = e.srcElement;
	}
	
	if(target.className == "downloadAllButton downloadButtonActive") {
		alert("Run AJAX to start downloading all of the files!");
	}	
}

function isChecked(e) {
	var target;
	
	if(!e) var e = window.event;
	if(e.target) {
		target = e.target;
	} else if(e.srcElement) {
		target = e.srcElement;
	}
	
	numberChecked = 0;
	for(j = 1; j < totalRows; j++) {
		checkBoxName = "row" + padRowNumber(j) + "CheckBox";
		checkBox = document.getElementById(checkBoxName);
		if(checkBox.checked) {
			numberChecked++;
		}
	}
	
	if(numberChecked > 0) {
		document.getElementById('downloadAllButton').className = "downloadAllButton downloadButtonActive";
	} else {
		document.getElementById('downloadAllButton').className = "downloadAllButton downloadButtonInactive";
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

	target.className = "pseudoButton pbActive";
}

function deactivatePseudoButton(e) {
	var target;

	if(!e) var e = window.event;
	if(e.target) {
		target = e.target;
	} else if(e.srcElement) {
		target = e.srcElement;
	}

	target.className = "pseudoButton pbInactive";
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
