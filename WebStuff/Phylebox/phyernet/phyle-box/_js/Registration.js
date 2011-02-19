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
				$("#username").attr("class", "error");
				
				isValid = false;
			}
			
			if ($("#password").val() === "" || $("#repeatPassword").val() === "" || $("#password").val() != $("#repeatPassword").val()) {
				$("#password").attr("class", "error");
				$("#repeatPassword").attr("class", "error");
				
				isValid = false;
			}
			
			if ($("input[name='explicitness']:checked").val() == undefined) {
				isValid = false;
			}
			
			if ($("#dateOfBirth").val() === "") {
				$("#dateOfBirth").attr("class", "error");
				
				isValid = false;
			}
			
			if ($("#firstRealName").val() === "" || $("#lastRealName").val() === "") {
				$("#firstRealName").attr("class", "error");
				$("#lastRealName").attr("class", "error");
				
				isValid = false;
			}
			
			if ($("#emailAddress").val() === "" || $("#repeatEmailAddress").val() === "" || ($("#emailAddress").val() != $("#repeatEmailAddress").val())) {
				$("#emailAddress").attr("class", "error");
				$("#repeatEmailAddress").attr("class", "error");
				
				isValid = false;
			}
			
			if ($("#country").val() === "0" || ($("#country").val() === "10001" && $("#otherCountry").val() === "")) {
				$("#country").attr("class", "error");
				$("#country").attr("class", "error");
				
				isValid = false;
			}
			
			if ($("#state").val() === "") {
				$("#state").attr("class", "error");
				
				isValid = false;
			}
			
			if ($("#city").val() === "") {
				$("#city").attr("class", "error");
				
				isValid = false;
			}
			
			if ($("#iagreetobetarules:checked").val() == undefined) {
				$("label[for='iagreetobetarules']").attr("class", "error");
				
				isValid = false;
			}
			
			return isValid;
		}
	}
	
	Phyer.prototype.Registration = new Registration();
	
}

window.$Phyer = new Phyer();
