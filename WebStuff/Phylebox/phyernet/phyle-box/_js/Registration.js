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
		EMAIL_ADDRESS: "emailAddress",
		USER_NAME_IN_USE: "userNameIsInUse",
		EMAIL_ADDRESS_IN_USE: "emailAddressIsInUse",
		USER_NAME_REGEX: /[A-Za-z\d-_\.]{4}[A-Za-z\d-_\.]*/,
		EMAIL_ADDRESS_REGEX: /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/,
		
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
		 * checkEmailAddressAvailability
		 * Checks whether an email address is available or not.
		 */
		checkEmailAddressAvailability: function() {
			var emailAddress = $("#emailAddress").val();
			
			if (emailAddress != "") {
				var data = "{ \"" + $Phyer.Registration.USER_NAME_IN_USE + "\": \"" + emailAddress + "\" }";
				
				
				$Phyer.postJson($Phyer.PHYER_ROOT + "phyle-box/business/RegistrationService.php?checkEmailAddressUsage", data, 
					null, 
					function(json) {
						$Phyer.setJson(json);
						
						if (json.userNameIsInUse) {
							$("#emailAddressIsAvailableMessage").text("Email address is already in use");
						} else {
							$("#emailAddressIsAvailableMessage").text("Email address is already in use");
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
			
			if ($("#username").val() === "" || !$Phyer_json.userNameIsInUse || !$Phyer.Registration.USER_NAME_REGEX.test($("#username").val())) {
				$("#username").attr("class", "error");
				
				isValid = false;
			}
			
			if ($("#password").val() === "" || $("#repeatPassword").val() === "" || ($("#password").val() != "" && $("#password").val() != $("#repeatPassword").val())) {
				$("#password").attr("class", "error");
				$("#repeatPassword").attr("class", "error");
				
				isValid = false;
			}
			
			if ($("accountType").val() == "5") {
				if ($("input[name='explicitness']:checked").val() == undefined) {
					$("#contentTypeDiv").attr("class", "error");
					
					isValid = false;
				}
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
			
			$Phyer.Registration.checkEmailAddressAvailability();
			
			if ($("#emailAddress").val() === "" || $("#repeatEmailAddress").val() === "" || ($("#emailAddress").val() != $("#repeatEmailAddress").val()) || !$Phyer_json.emailAddressIsInUse || !$Phyer.Registration.EMAIL_ADDRESS_REGEX.test($("#emailAddress"))) {
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
		},
		
		/**
		 * accountTypeFormIsValid
		 * Determines whether an account type has been chosen.
		 * @return bool
		 */
		accountTypeFormIsValid: function() {
			var isValid = true;
			
			if ($("#accountType:checked").val() == undefined) {
				isValid = false;
			}
			
			return isValid;
		}
	}
	
	Phyer.prototype.Registration = new Registration();
	
}

window.$Phyer = new Phyer();
