if(jQuery) {
	var Ultradent = function() { }
	
	Ultradent.prototype = {
		_json: null,
		
		/**
		 * postJson is used to do an asynchronous JSON postback to a web service. The results are returned as a JSON object to the success callback function.
		 * For example, you might define successCallback as:
		 * 		
		 * 		function(json) { populateForm(json); }
		 * 
		 * or
		 * 
		 * 		function(xhr) { value = xhr.productId; document.write(value); }
		 * 
		 * @param {string} url The url to the webservice method.
		 * @param {string} data JSON encoded string. Many of the methods require an array, like '{ "productId": 2450 }'.
		 * @param {function} beforeSendCallback A function to call before the postback.
		 * @param {function} successCallback A function to call after the postback.
		 * @param {function} errorCallback A function to call when there is an error.
		 */
		postJson: function(url, data, beforeSendCallback, successCallback, errorCallback) {
			actualSuccessCallback = successCallback;
			
			if(!actualSuccessCallback) {
				actualSuccessCallback = function(json) { $Ultradent.setJson(json); };
			}
		
			$.ajax( { type: "post", contentType: "application/json; charset=utf-8", url: url, data: data, dataType: "json", beforeSend: beforeSendCallback, success: actualSuccessCallback, error: errorCallback } );
		},
		
		/**
		 * addFieldSet adds a field set to a container.
		 * @param {string} containerId The container's id attribute value where the fieldset will be appended.
		 * @param {string} legend The text for the fieldset legend.
		 * @param {string} fieldSetId The id for the new fieldset.
		 * @param {bool} includeClose Includes the close button if true.
		 */
		addFieldSet: function(containerId, legend, fieldSetId, includeClose) {
			$("<fieldset id=\"" + fieldSetId + "\"><legend>" + legend + "</legend></fieldset>").appendTo("#" + containerId);
			
			if(includeClose) {
				$("<div class=\"deleteItemButton\"><a class=\"deleteButton\" title=\"Delete Item\" href=\"#\" onclick=\"$Ultradent.deleteItem('" + fieldSetId + "', '" + containerId + "');return false;\"></a>").appendTo("#" + fieldSetId);
			}
		},
		
		/**
		 * addHiddenField adds a hidden field to a container.
		 * @param {string} containerId The id of the container.
		 * @param {string} value The value of the hidden field.
		 * @param {string} inputId The id for the new hidden field.
		 */
		addHiddenField: function(containerId, value, inputId) {
			$("<input type=\"hidden\" name=\"" + inputId + "\" id=\"" + inputId + "\" value=\"" + value + "\" />").appendTo("#" + containerId);				
		},
	
		/**
		 * addTestFieldFormRow adds a complete text input form row with label and default value to the container defined by containerId.
		 * @param {string} containerId The container's id attribute value where the form row will be appended.
		 * @param {string} label The text of the label.
		 * @param {string} defaultValue The default value for the text field.
		 * @param {string} inputId This is optional, if left out of the method call, then the input id and name will default to container + (formRow count + 1).
		 * @param {string} title Tool-tip text
		 */
		addTextFieldFormRow: function(containerId, label, defaultValue, inputId, title) {
			numberOfFormRows = $("#" + containerId + " > .formRow").size();
			htmlInputId = inputId;
			
			if(!htmlInputId) {
				htmlInputId = containerId + (numberOfFormRows + 1);
			}
			
			$("<div class=\"formRow\"><label for=\"" + htmlInputId + "\">" + label + "</label><input " + (title ? "title=\"" + title + "\"" : "") + " class=\"long\" type=\"text\" value=\"" + defaultValue + "\" id=\"" + htmlInputId + "\" name=\"" + htmlInputId + "\" /></div>").appendTo("#" + containerId);
		},
		
		/**
		 * addTextAreaFormRow adds a complete text area form row with label and default value to the container defined by containerId.
		 * @param {string} containerId The container's id attribute value where the form row will be appended.
		 * @param {string} label The text of the label.
		 * @param {string} defaultValue The default value for the text field.
		 * @param {string} textAreaId This is optional, if left out of the method call, then the input id and name will default to container + (formRow count + 1).
		 * @param {string} title Tool-tip text
		 */
		addTextAreaFormRow: function(containerId, label, defaultValue, textAreaId, title) {
			numberOfFormRows = $("#" + containerId + " > .formRow").size();
			htmlTextAreaId = textAreaId;
			
			if(!htmlTextAreaId) {
				htmlTextAreaId = containerId + (numberOfFormRows + 1);
			}
			
			$("<div class=\"formRow\"><label for=\"" + htmlTextAreaId + "\">" + label + "</label><textarea " + (title ? "title=\"" + title + "\"" : "") + " id=\"" + htmlTextAreaId + "\" name=\"" + htmlTextAreaId + "\">" + defaultValue + "</textarea></div>").appendTo("#" + containerId);
		},
		
		/**
		 * addFileUploadFormRow adds a complete file form row with label, and a link below it linking to the currently assigned resource.
		 * @param {string} containerId The container's id attribute value where the form row will be appended.
		 * @param {string} label The text of the label.
		 * @param {string} currentLinkUrl The current resource associated with this file upload. If nothing is associated, or there is no association, then enter null.
		 * @param {string} fileUploadId This is optional, if left out of the method call, then the input id and name will default to container + (formRow count + 1).
		 * @param {string} title Tool-tip text.
		 */
		addFileUploadFormRow: function(containerId, label, currentLinkUrl, fileUploadId, title) {
			numberOfFormRows = $("#" + containerId + " > .formRow").size();
			htmlTextAreaId = fileUploadId;
			
			if(!htmlTextAreaId) {
				htmlTextAreaId = containerId + (numberOfFormRows + 1);
			}
			
			$("<div class=\"formRow\"><label for=\"" + htmlTextAreaId + "\">" + label + "</label><input " + (title ? "title=\"" + title + "\"" : "") + " class=\"long\" type=\"file\" value=\"\" id=\"" + htmlInputId + "\" name=\"" + htmlInputId + "\" /></div>").appendTo("#" + containerId);
			
			if(currentLinkUrl) {
				$("<div class=\"formRow\"><a title=\"Click here to view resource.\" href=\"" + currentLinkUrl + "\">" + currentLinkUrl +"</a>").appendTo("#" + containerId);
			}
		},
		
		/**
		 * Removes an element from the DOM based on its id attribute's value.
		 * @param {string} elementId
		 * @param {string} containerId
		 */
		deleteItem: function(elementId, containerId) {
			$("#" + elementId).remove();
			
			if(containerId == "podcasts") {
				num = $("#numPodcasts").attr("value");
				num--;
				$("#numPodcasts").attr("value", num);
			}
		},
		
		/**
		 * setJson is the default success callback function for postJson. It sets this._json to the returned JSON object.
		 * @param {object} json
		 */
		setJson: function(json) {
			if(json) {
				this._json = json;
			}
		},
		
		/**
		 * getJson returns this._json.
		 * @return {object} The JSON object in this._json.
		 */
		getJson: function() {
			return this._json;
		}
	}
}

window.$Ultradent = new Ultradent();