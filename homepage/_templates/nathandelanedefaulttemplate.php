<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>Home | Nathandelane, software architect, programmer, designer</title>
		<link rel="stylesheet" type="text/css" href="_stylesheets/nathandelanemain.css"/>
		<link rel="icon" href="favicon.ico"/>
		<link rel="shortcut icon" href="favicon.ico"/>
	</head>
	<body>
		<div id="pageContainer" class="pageContainer">
			<div id="pageHeader" class="pageHeader">
				<img id="pageHeaderImage" class="pageHeaderImage" alt="Nathan de lane logo" src="_images/nathandelanewithlogo20.png"/>
			</div>
			<div id="pageBodyContainer" class="pageBodyContainer">
				<?php
					require_once("_content/" . $pageContent);
				?>
				<div class="hrDiv"></div>
				<div id="footer" class="footer">
					Copyright (C) 2008, Nathan Lane. All Rights Reserved.
				</div>
			</div>
		</div>
	</body>
</html>