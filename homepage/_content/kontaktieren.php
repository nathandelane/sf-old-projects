<?php
	if($_POST["nameField"] && $_POST["emailField"] && $_POST["commentsTextArea"])
	{
?>
<div id="introductionTextContainer" class="innerContainer">
	Thank you for your comments.
</div>
<?php
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
		<div>Fields with an asterisk (*) next to their labels are required by this form</div>
		<div class="hrSeparator"></div>
		<?php
			if($_POST["hiddenField"] == 'contactForm')
			{
		?>
		<div>You must enter data into the required fields.</div>
		<div class="hrSeparator"></div>
		<?php
			}
		?>
		<hidden id="hiddenField" name="hiddenField" value="contactForm"></hidden>
		<div>
			<label for="nameField">Your Name*&nbsp;</label>
			<input id="nameField" name="nameField" type="text" value=""/>
		</div>
		<div class="formItemSeparator"></div>
		<div>
			<label for="emailField">Your Email Address*&nbsp;</label>
			<input id="emailField" name="emailField" type="text" value=""/>
		</div>
		<div class="formItemSeparator"></div>
		<div>
			<label for="commentsTextArea">Your Comments To Me*&nbsp;</label>
			<textarea id="commentsTextArea" name="commentsTextArea"></textarea>
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