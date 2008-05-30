<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<?php
/* 
 * @author Nathan Lane
 * @copyright Copyright (c) 2008, Nathan Lane, nathandelane.com
 */
?>
<html>
	<head>
		<title><?php echo "$pageName"; ?> &lt; Nathandelane, software architect and programmer</title>
		<link type="text/css" href="/_stylesheets/nathandelanemain.css" title="default" media="screen" rel="stylesheet"/>
	</head>
	<body>
		<div id="navigationBar" class="navigationBar">
			<div class="bannerBar">
				<div class="bannerBarInner"></div>
			</div>
			<img alt="Nathan de Lane" id="nathandelaneLogo" class="nathandelaneLogo" src="<?php echo "$relativePath"; ?>_images/nathandelanewithlogo20.png"/>
			<div class="navBarInner">
				<div id="menuContainer" class="menuContainer">
					<ul class="menu">
						<li>
							<a id="homeLink" href="/">Home</a>
						</li>
						<li>
							<span>|</span>
						</li>
						<li>
							<a id="resumeLink" href="/resume/">Resume</a>
						</li>
						<li>
							<span>|</span>
						</li>
						<li>
							<a id="resourcesLink" href="/resources/">Resources</a>
						</li>
					</ul>
				</div>
			</div>
			<div id="navBarBottom" class="navBarBottom">
				<div class="navBarBottomInner"></div>
			</div>
		</div>
		<div id="bodyContainer" class="bodyContainer">
			<?php
			if(file_exists($relativePath . "pages/" . $pageContent))
			{
				require_once($relativePath . "pages/" . $pageContent);
			}
			?>
		</div>
		<div id="footer" class="footer">
			<div class="footerInner">
				Copyright &copy; 2008, Nathan Lane (nathandelane at nathandelane dot com)
			</div>
			<div class="footer2">
				<div class="footer2Inner"></div>
			</div>
		</div>
	</body>
</html>