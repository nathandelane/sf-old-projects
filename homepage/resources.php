<?php
	include("_php/doctype.php");
	$page = "resources";
?>
<html>
	<head>
		<title>Nathandelane, resources for the masses</title>
		<?php include("_php/resources.php"); ?>
	</head>
	<body>
		<div class="mysteryMeatLinkBox" id="mysteryMeatLinkBox">
			<?php include("_php/mysteryMeatLinks.php"); ?>
		</div>
		<?php include("_php/ldsgems.php"); ?>
		<div class="navBar backgroundBlack backgroundBlackGrad" id="navigation">
			<?php include("_php/menus.php"); ?>
		</div>
		<div class="contentContainer" id="content">
			<div class="containerHeader backgroundBlack" id="contentheader">
				Resources
			</div>
			<div class="innerContent" id="gimprehueing">
				<h3>Gimp Re-hue-ing Tutorial</h3>
				<ol id="gimpRehueingSteps">
					<li>Load your image</li>
					<li>Using the dropper tool, click on the color on the image the was the old text color</li>
					<li>Click on the Color in the Gimp Toolbox to open the color chooser</li>
					<li>Note the H value (Hue at the top right of the color chooser)</li>
					<li>Now type the hex value of the new text color into the text field on the color chooser</li>
					<li>Note the H value (Hue) again</li>
					<li>Subtract the old H-value from the new H-value</li>
					<li>If the result is positive, subtract that amount from the Hue in the Hue-Saturation dialog under Colors, if it is negative, add that amount to the Hue in the Hue-Saturation dialog.</li>
				</ol>
				<span id="gimpRehueingTags">
					<a href="#">gimp</a>,&nbsp;
					<a href="#">art</a>,&nbsp;
				</span>
			</div>
			<!--
			<div class="horizontalRule backgroundWhite" id="horizRule1"></div>
			<div class="innerContent" id="family">
			</div>-->
			<div class="horizontalRule backgroundWhite" id="horizRule1"></div>
			<div class="copyrightContainer" id="copyright">
				<?php include("_php/copyright.php"); ?>
			</div>
		</div>
	</body>
</html>