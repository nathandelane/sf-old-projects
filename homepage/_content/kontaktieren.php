<?php
	if($_POST["nameField"] != "" && $_POST["emailField"] != "" && $_POST["commentsTextArea"] != "")
	{
?>
<div id="introductionTextContainer" class="innerContainer">
	Thank you for your comments.
</div>
<?php
		$ip;
		if (getenv("HTTP_CLIENT_IP"))
		{
			$ip = getenv("HTTP_CLIENT_IP");
		}
		else if(getenv("HTTP_X_FORWARDED_FOR"))
		{
			$ip = getenv("HTTP_X_FORWARDED_FOR");
		}
		else if(getenv("REMOTE_ADDR"))
		{
			$ip = getenv("REMOTE_ADDR");
		}
		else
		{
			$ip = "UNKNOWN";
		}
		
		$pathToMailFolder = "C:\\var\\mail";
		$dateStamp = date("Y.m.d");
		$timeStamp = time();
		$fileName = "Mail_" . $ip . "-" . $dateStamp . "_" . $timeStamp . ".mail";
		$fullPath = $pathToMailFolder . "\\" . $fileName;

		$fp = fopen($fullPath, "w");
		fwrite($fp, "$" . $ip . "; " . $_POST["nameField"] . "; " . $_POST["emailField"] . "; " . $_POST["commentsTextArea"] . "$");
		fclose($fp);
	}
	else
	{
?>
<div id="introductionTextContainer" class="innerContainer">
	This is one method of contacting me. Though I'm sure you could find my contact information some other way. Thanks. Nathan Lane.
</div>
<div class="hrDiv"></div>
<div id="contactFormContainer" class="contactFormContainer">
	<form id="contactForm" name="contactForm" action="/?page=kontaktieren" method="post">
		<div>This form should be used to contact me. It contacts me directly, so there is no need to send an email to me in any other way.</div>
		<div class="hrSeparator"></div>
		<div>Fields with an asterisk (*) next to their labels are required by this form. <span class="textRed" style="display: <?php if(($_POST["nameField"] != "" || $_POST["emailField"] != "" || $_POST["commentsTextArea"] != "")) { echo "inline"; } else { echo "none"; } ?>;">All Fields Are Required.</span></div>
		<div class="hrSeparator"></div>
		<div>
			<label for="nameField" <?php if(($_POST["nameField"] != "" || $_POST["emailField"] != "" || $_POST["commentsTextArea"] != "") && $_POST["nameField"] == "") { echo "class=\"textRed\""; } ?>>Your Name*&nbsp;</label>
			<input id="nameField" name="nameField" type="text" value="<?php if($_POST["nameField"]) { echo $_POST["nameField"]; } ?>"/>
		</div>
		<div class="formItemSeparator"></div>
		<div>
			<label for="emailField" <?php if(($_POST["nameField"] != "" || $_POST["emailField"] != "" || $_POST["commentsTextArea"] != "") && $_POST["emailField"] == "") { echo "class=\"textRed\""; } ?>>Your Email Address*&nbsp;</label>
			<input id="emailField" name="emailField" type="text" value="<?php if($_POST["emailField"]) { echo $_POST["emailField"]; } ?>"/>
		</div>
		<div class="formItemSeparator"></div>
		<div>
			<label for="commentsTextArea" <?php if(($_POST["nameField"] != "" || $_POST["emailField"] != "" || $_POST["commentsTextArea"] != "") && $_POST["commentsTextArea"] == "") { echo "class=\"textRed\""; } ?>>Your Comments To Me*&nbsp;</label>
			<textarea id="commentsTextArea" name="commentsTextArea"><?php if($_POST["commentsTextArea"]) { echo $_POST["commentsTextArea"]; } ?></textarea>
		</div>
		<div class="formItemSeparator"></div>
		<div>
			<button id="commentsSubmittalButton" name="commentsSubmittalButton" type="submit" value="Submit">Send</button>
		</div>
	</form>
</div>
<?php
	}
?>