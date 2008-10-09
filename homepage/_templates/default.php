<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title><?php echo $pageTitle; ?></title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<link href="_feeds/nathandelane.xml" title="Nathandelane.com RSS Feed" type="application/rss+xml" rel="alternate"/>
		<link media="screen" type="text/css" href="_stylesheets/<?php echo $defaultStylesheetName; ?>" rel="stylesheet" />
		<?php
			if(isset($otherStylesheets))
			{
				foreach($otherStylesheets as $stylesheet)
				{
					echo '<link media="screen" type="text/css" href="' . $stylesheet . '" rel="stylesheet" />';
				}
			}
		?>
		<link rel="shortcut icon" href="favicon.ico"/>
	</head>
	<body>
		<div id="outerPageContainer" class="outerPageContainer">
			<div id="bannerHeader" class="banner">
				<span id="nathandelaneBannerLink">
					<a id="homepageLink" href="<?php echo $sitePath; ?>">Nathandelane.com</a>
					<div id="explanation" class="explanation">
						<span>This web site is about me and what I do, who I am, and where I am headed in life. Feel free to peruse my portfolio and subscribe to my <a id="rssFeedLink" href="<?php echo $sitePath; ?>_xml/nathandelane.xml">RSS feed</a>, as I am always updating it with new projects that I am working on.</span>
					</div>
				</span>
			</div>
			<div class="separator"></div>
			<?php
				require_once('_phpmodules/navigationbar.php');
			?>
			<div id="pageBody" class="pageBody">
			<?php
				if(isset($contents))
				{
					require_once('_contents/' . $contents . '.php');
				}
			?>
			</div>
			<div class="separator"></div>
			<div id="footer" class="footer">
				<span>Nathandelane &copy; 2007-<?php echo date('Y'); ?> by Nathan Lane, Nathandelane.com, All Rights Reserved.</span>
			</div>
		</div>
	</body>
</html>