<?php
	include("_php/doctype.php");
	$page = "info";
?>
<html>
	<head>
		<title>Nathandelane, info about me</title>
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
				Info
			</div>
			<div class="innerContent" id="info">
				The information on this page reflects my experience as a computer programmer. It is purposely not laid out in 
				a fashion resembling a resume. However if this information leads one to believe that my skills may be useful to 
				them, then I invite that person to contact me.
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule0"></div>
			<div class="innerContent" id="goals">
				<h3>Goals</h3>
				My goals are varied. For school, I'd like to at least attain my Bachelor's of Science in Computer Science. I'm 
				not really certain about which school I'd like to go to, but for now it looks like it's going to be <a id="weberStateLink" href="https://www.weber.edu/">
				Weber State</a> University. For work, I'd like to be a software engineer and at some point program video games 
				for a living, though the video game part isn't as important. On my spiritual side, I'd like to become a 
				<a id="spiritualGiantLink" href="http://speeches.byu.edu/reader/reader.php?id=11017">spiritual giant</a>.
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule1"></div>
			<div class="copyrightContainer" id="copyright">
				<?php include("_php/copyright.php"); ?>
			</div>
		</div>
	</body>
</html>