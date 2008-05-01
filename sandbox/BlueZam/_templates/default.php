<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<?php
	$userSession;
	
	include_once("lib/PhpSession.php");
	
	if(!isset($userSession))
	{
		$userSession = new PhpSession();
	}
?>
<html>
	<head>
		<title>
			<?php 
				if(isset($title))
				{
					echo $title;
				}
				else
				{
					echo "Untitled Page | Blue Zam";
				}
			?>
		</title>
		<link rel="stylesheet" type="text/css" href="_stylesheets/containers.css"/>
		<link rel="stylesheet" type="text/css" href="_stylesheets/main.css"/>
		<link rel="stylesheet" type="text/css" href="_stylesheets/colors.css"/>
		<?php
			if($requirePageStylesheet)
			{
				$file = "_stylesheets/".$page.".css";
				if(file_exists($file))
				{
		?>
		<link rel="stylesheet" type="text/css" href="_stylesheets/<?php echo $page; ?>.css"/>
		<?php
				}
				else
				{
					header("HTTP/1.0 404 Not Found");
					header("File-not-found: ".$file);
				}
			}
		?>
		<script src="_scripts/menu.js" type="text/javascript"></script>
	</head>
	<body onload="init();">
		<div class="pageContainer" id="pageContainer">
			<div class="innerContainer" id="innerContainer">
				<?php
					// Get the header bar
					if(file_exists("_userControls/pageHeaderBar.php"))
					{
						require_once("_userControls/pageHeaderBar.php");
					}
					else
					{
						echo "Error Occurred. Could not retrieve _userControls/pageHeaderBar.php.";
					}
					
					// Get the page content
					if(isset($page))
					{
						require_once("pages/".$page.".php");
					}
					else
					{
						echo "Error Occurred. Could not retrieve ".$page.".php";
					}
					
					// Get the footer bar
					if(file_exists("_userControls/pageFooterBar.php"))
					{
						require_once("_userControls/pageFooterBar.php");
					}
					else
					{
						echo "Error Occurred. Could not retrieve _userControls/pageFooterBar.php.";
					}					
				?>
				<div id="copyrightBar" class="copyrightBar copyright">Copyright (C) 2008 BlueZam. All Rights Reserved.</div>
			</div>
		</div>
	</body>
</html>