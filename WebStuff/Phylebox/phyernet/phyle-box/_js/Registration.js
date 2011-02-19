if (Phyer && $Phyer) {
	
	var Registration = function() { }
	
	Registration.prototype = {
		/**
		 * Static variables
		 */
		/**
		 * Constants
		 */
		USER_NAME: "username",
		USER_NAME_IN_USE: "userNameIsInUse",
		
		/**
		 *  checkUserNameAvailability
		 *  Checks whether a user name is available or not.
		 */
		checkUserNameAvailability: function() {
			var chosenUserName = $("#username").val();
			
			if (chosenUserName != "") {
				var data = "{ \"" + $Phyer.Registration.USER_NAME + "\": \"" + chosenUserName + "\" }";
				
				$Phyer.postJson($Phyer.PHYER_ROOT + "phyle-box/business/RegistrationService.php?checkUserNameAvailability", data, 
					null, 
					function(json) {
						$Phyer.setJson(json);
						
						if (json.userNameIsInUse) {
							$("#userNameIsAvailableMessage").text("Username is not available");
						} else {
							$("#userNameIsAvailableMessage").text("Username is available");
						}
					}
				);
			}
		},
		
		/**
		 * informationFormIsValid
		 * Determines whether all of the information on the information form is valid.
		 * @return bool
		 */
		informationFormIsValid: function() {
			var isValid = true;
			
			$Phyer.Registration.checkUserNameAvailability();
			
			if ($("#username").val() === "" || !$Phyer_json.userNameIsInUse) {
				isValid = false;
			}
			
			if ($("#password").val() === "" || $("#repeatPassword").val() === "" || $("#password").val() != $("#repeatPassword").val()) {
				isValid = false;
			}
			
			if ($("input[name='explicitness']:checked").val() == undefined) {
				isValid = false;
			}
			
			if ($("#dateOfBirth").val() === "") {
				isValid = false;
			}
			
			if ($("#firstRealName").val() === "" || $("#lastRealName").val() === "") {
				isValid = false;
			}
			
			if ($("#emailAddress").val() === "" || $("#repeatEmailAddress").val() === "") {
				isValid = false;
			}
			
			if ($("#country").val() === "0" || ($("#country").val() === "10001" && $("#otherCountry").val() === "")) {
				isValid = false;
			}
			
			if ($("#state").val() === "") {
				isValid = false;
			}
			
			if ($("#city").val() === "") {
				isValid = false;
			}
			
			if (!($("#iagreetobetarules:checked").val() == undefined)) {
				isValid = false;
			}
			
			return isValid;
		}
	}
	
	Phyer.prototype.Registration = new Registration();
	
}

window.$Phyer = new Phyer();
