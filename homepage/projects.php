<?php
	include("_php/doctype.php");
	$page = "projects";
?>
<html>
	<head>
		<title>Nathandelane, my projects</title>
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
				Projects
			</div>
			<div class="innerContent" id="ufooar">
				<h3>Ufooar</h3>
				<a href="http://rubyforge.org/projects/ufooar/" target="_blank">UFOOAR (Uno For OpenOffice And Ruby)</a> is a Ruby-UNO 
				Bridge library so that Ruby scripters can also access the UNO COM-like interface in OpenOffice.org. The 
				main support for this is that of Spreadsheets, however support for Documents, XML, and HTML pages has 
				been requested so heavy work is being performed on those as well.
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule0"></div>
			<div class="innerContent" id="personalCalculator">
				<h3>Personal Calculator</h3>
				<a href="https://personalcalculator.dev.java.net/" target="_blank">Personal Calculator</a> is a command-line based calculator 
				that accepts as input a mathematical equation string. The value of having it as a command-line tool is that you 
				can edit your entry before submitting it, or automatically recall your last equation based on your terminal 
				settings. In a DOS or Windows command-line the Up Arrow key performs the recall action.
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule1"></div>
			<div class="innerContent" id="basicConsole">
				<h3>Basic Console</h3>
				<a href="http://code.google.com/p/jbasicconsole/" target="_blank">Basic Console</a> is a Java-based console application that has 
				been developed with pluggability and extendibility in mind. It is very basic and includes information pertaining 
				to how I developed it. As there were no readily available resources on developing a console application in Java, 
				I feel this is a great step for beginners in the Java software development community.
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule2"></div>
			<div class="copyrightContainer" id="copyright">
				<?php include("_php/copyright.php"); ?>
			</div>
		</div>
	</body>
</html>