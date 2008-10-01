<?php
	if($_POST["nameField"] != "" && $_POST["emailField"] != "" && $_POST["commentsTextArea"] != "")
	{
?>
<div id="introductionTextContainer" class="innerContainer">
	<h1>Thank you for your comments. Click <a href="<?php echo $sitePath; ?>">here</a> to return to my home page.</h1>
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
<div id="innerPageBody" class="innerPageBody">
	<h1>Contact Me</h1>
	<p>
		Please use this form to contact me. If you are in urgent need of contacting me and don't currenlt have my email address or some other more instant method of contacting me, then I'm certain you could find it by <a id="googleLink" href="http://www.google.com/search?hl=en&rlz=1C1GGLS_enUS291&q=nathandelane&btnG=Search">googling me</a>.
		<div class="dashedSeparator"></div>
	</p>
	<p>
		<form id="contactForm" method="post" action="<?php echo $sitePath; ?>?page=kontaktieren" name="contactForm">
			<div>
				Fields with an asterisk (*) next to their labels are required by this form.
				<span class="textRed" style="display:  <?php if(($_POST["nameField"] != "" || $_POST["emailField"] != "" || $_POST["commentsTextArea"] != "")) { echo "inline"; } else { echo "none"; } ?>;">All Fields Are Required.</span>
			</div>
			<div class="hrSeparator"></div>
			<div>
				<label for="nameField" <?php if(($_POST["nameField"] != "" || $_POST["emailField"] != "" || $_POST["commentsTextArea"] != "") && $_POST["nameField"] == "") { echo "class=\"textRed\""; } ?>>Your Name*&nbsp;</label>
				<input id="nameField" type="text" value="<?php if($_POST["nameField"]) { echo $_POST["nameField"]; } ?>" name="nameField"/>
			</div>
			<div class="hrSeparator"></div>
			<div>
				<label for="emailField" <?php if(($_POST["nameField"] != "" || $_POST["emailField"] != "" || $_POST["commentsTextArea"] != "") && $_POST["emailField"] == "") { echo "class=\"textRed\""; } ?>>Your Email Address*&nbsp;</label>
				<input id="emailField" type="text" value="<?php if($_POST["emailField"]) { echo $_POST["emailField"]; } ?>" name="emailField"/>
			</div>
			<div class="hrSeparator"></div>
			<div>
				<label for="commentsTextArea" <?php if(($_POST["nameField"] != "" || $_POST["emailField"] != "" || $_POST["commentsTextArea"] != "") && $_POST["commentsTextArea"] == "") { echo "class=\"textRed\""; } ?>>Your Comments To Me*&nbsp;</label>
				<textarea id="commentsTextArea" name="commentsTextArea"><?php if($_POST["commentsTextArea"]) { echo $_POST["commentsTextArea"]; } ?></textarea>
			</div>
			<div class="hrSeparator"></div>
			<div>
				<button id="commentsSubmittalButton" value="Submit" type="submit" name="commentsSubmittalButton">Send</button>
			</div>
		</form>
	</p>
</div>
<?php
	}
?>
