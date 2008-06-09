<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>Home | Nathandelane, software architect, programmer, designer</title>
		<style type="text/css">
			body {
				margin: 0px;
				padding: 0px;
				background-color: #ffffff;
				color: #003e66;
				font-family: Arial,sans-serif;
				font-size: 14px;
				font-weight: normal;
				line-height: 16px;
			}

			img {
				border: 0px;
			}

			h1 {
				padding: 0px;
				margin: 0px 0px 10px 0px;
				font-size: 20px;
			}

			.pageContainer {
				width: 740px;
				margin: 0px auto;
			}

			.pageHeader {
				width: 740px;
				margin: 0px;
				float: left;
			}

			.pageBodyContainer {
				width: 740px;
				float: left;
			}

			.innerContainer {
				width: 740px;
				float: left;
				margin: 5px;
			}

			a.nathandelaneLink, a:link.nathandelaneLink, a:active.nathandelaneLink, a:visited.nathandelaneLink {
				font-size: 16px;
				font-weight: bold;
				text-decoration: none;
				color: #3696bb;
			}

			a:hover.nathandelaneLink {
				font-size: 16px;
				font-weight: bold;
				text-decoration: underline;
				color: #3696bb;
			}

			.hrDiv {
				width: 100%;
				height: 2px;
				background-color: #003e66;
				overflow: hidden;
				float: left;
				margin: 5px 0px 5px 0px;
			}

			.hrSeparator {
				width: 100%;
				height: 20px;
				overflow: hidden;
				float: left;
			}

			.footer {
				width: 100%;
				float: left;
				text-align: center;
				margin: 0px 0px 10px 0px;
			}

			/* Image classes */

			.pageHeaderImage {
				height: 20px;
				width: 200px;
				display: block;
				float: left;
				margin: 10px 0px 10px 0px;
				overflow: hidden;
			}

			.projectLogo {
				width: 150px;
				height: 150px;
				overflow: hidden;
				display: block;
				float: left;
				margin: 0px 10px 0px 0px;
			}

			.projectContentContainer {
				width: 580px;
				float: left;
				margin: 0px 0px 0px 10px;
			}
		</style>
		<link rel="icon" href="favicon.ico"/>
		<link rel="shortcut icon" href="favicon.ico"/>
	</head>
	<body>
		<div id="pageContainer" class="pageContainer">
			<div id="pageHeader" class="pageHeader">
				<img id="pageHeaderImage" class="pageHeaderImage" alt="Nathan de lane logo" src="_images/nathandelanewithlogo20.png"/>
			</div>
			<div id="pageBodyContainer" class="pageBodyContainer">
				<div id="introductionTextContainer" class="innerContainer">
					These pages outline some of the features and status of my latest programming projects. I have also linked to my blog posts from here. I reserve thes pages as part of an online portfolio. Currently the projects are hosted by different public project hosting domains. All projects are licensed under the <a id="gplLinkIntro" class="nathandelaneLink" href="http://www.gnu.org/licenses/gpl-3.0.html">GNU GPL Version 3.0</a> unless otherwise specified.
				</div>
				<div class="hrDiv"></div>
				<div id="personalCalculatorOuterContainer" class="innerContainer">
					<a id="personalCalculatorLogoLink" href="https://personalcalculator.dev.java.net/">
						<img id="personalCalculatorLogo" class="projectLogo" alt="Personal Calculator logo" src="_images/personalcalculatorlogo.png"/>
					</a>
					<?php //https://personalcalculator.dev.java.net/ ?>
					<h1>Personal Calculator</h1>
					Personal Calculator or "PC" was developed in the tradition of other text-mode calculator programs. As of yet it doesn't include a full set of features that perhaps bc includes, but the direction is similar. One of the major differences in this calculator is that it is developed in Java.
					<br/><br/>
					PC utilizes Polish Postscript to perform calculations, as is described on the homepage for it. In some cases I hack the usage of Polish Postscript in order to allow non-arithmetic operations such as creating matrices or running functions.
					<br/><br/>
					Check out more about PC <a id="linkToPcOnJavaDotNet" class="nathandelaneLink" href="https://personalcalculator.dev.java.net/">here</a>.
				</div>
				<div class="hrSeparator"></div>
				<div id="adannaOuterContainer" class="innerContainer">
					<a id="adannaLogoLink" href="http://code.google.com/p/adanna-scheduler/">
						<img id="adannaLogo" class="projectLogo" alt="ADAnna - The Agent Database Agent Test Scheduler logo" src="_images/adannascheduler.png" style="background-image: url('_images/stripe_diagonalrightgray.png');"/>
					</a>
					<h1>Adanna</h1>
					Adanna is a framework written in Ruby for running Watir tests written in Ruby. The framework includes three components, namely an interface to a database (both MySQL and MSSQL are currently supported), a server agent, and a client agent.
					<br/><br/>
					Adanna was designed to fill in the pieces that Mercury Quick Test Professional provided but that Watir was never meant to provide. It is one of the frameworks talked about in the Watir forums and user groups and on the Watir podcasts.
					<br/><br/>
					Check out more about Adanna <a id="linkToAdannaOnGoogleCode" class="nathandelaneLink" href="http://code.google.com/p/adanna-scheduler/">here</a>.
				</div>
				<div class="hrSeparator"></div>
				<div id="ufooarOuterContainer" class="innerContainer">
					<a id="adannaCalculatorLogoLink" href="http://code.google.com/p/adanna-scheduler/">
						<img id="adannaLogo" class="projectLogo" alt="U.F.O.O.A.R. - Uno For OpenOffice And Ruby" src="_images/ufooar.png"/>
					</a>
					<h1>Ufooar</h1>
					U.F.O.O.A.R. stands for Uno For OpenOffice And Ruby. It is essentially an Uno-Ruby bridge for OpenOffice.org. This project stemmed out of my endeavors as a Software Automation Architect. Ufooar came as a result of some fellow testers wanting a way to use OpenOffice.org's Spreadsheet program as a data source for some of their tests. Ufooar has since grown into much more, and has become its own automation project.
					<br/><br/>
					Check out more about Ufooar <a id="linkToUfooarOnRubyForge" class="nathandelaneLink" href="http://rubyforge.org/projects/ufooar/">here</a>.
				</div>
				<div class="hrDiv"></div>
				<div id="footer" class="footer">
					Copyright (C) 2008, Nathan Lane. All Rights Reserved.
				</div>
			</div>
		</div>
	</body>
</html>