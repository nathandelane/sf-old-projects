/**
 * Phyer.js
 * 
 * This script defines the Phyer JavaScript object model.
 * 
 * @author lanathan
 */
if (jQuery) {
	Phyer = function() { }
	
	Phyer.prototype = {
		_json: null,

		/* Constants */
		PHYER_ROOT: "/",
		
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
		 * @param string url The url to the webservice method.
		 * @param string data JSON encoded string. Many of the methods require an array, like '{ "productId": 2450 }'.
		 * @param function beforeSendCallback A function to call before the postback.
		 * @param function successCallback A function to call after the postback.
		 * @param function errorCallback A function to call when there is an error.
		 */
		postJson: function(url, data, beforeSendCallback, successCallback, errorCallback) {
			actualSuccessCallback = successCallback;
			
			if(!actualSuccessCallback) {
				actualSuccessCallback = function(json) { $Phyer.setJson(json); };
			}
		
			$.ajax( { type: "post", contentType: "application/json; charset=utf-8", url: url, data: data, dataType: "json", beforeSend: beforeSendCallback, success: actualSuccessCallback, error: errorCallback } );
		},
		
		/**
		 * setJson is the default success callback function for postJson. It sets this._json to the returned JSON object.
		 * @param object json
		 */
		setJson: function(json) {
			if(json) {
				this._json = json;
			}
		},
		
		/**
		 * getJson returns this._json.
		 * @return object The JSON object in this._json.
		 */
		getJson: function() {
			return this._json;
		}
	}
}

window.$Phyer = new Phyer();
