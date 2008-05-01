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
		?>
		<link rel="stylesheet" type="text/css" href="_stylesheets/<?php echo $page; ?>.css"/>
		<?php
			}
		?>
		<script src="_scripts/menu.js" type="text/javascript"></script>
	</head>
	<body onload="init();">
		<div class="pageContainer" id="pageContainer">
			<div class="innerContainer" id="innerContainer">
				<div class="banner" id="banner">
					<div class="siteMapBar" id="siteMapBar">
						<div id="siteMap" class="siteMap">
							<ul>
								<li>
									<a id="overviewLink" class="sitemapLink" href="./index.php">OVERVIEW</a>
								</li>
								<li>|</li>
								<li>
									<a id="communityLink" class="sitemapLink" href="">COMMUNITY</a>
								</li>
								<li>|</li>
								<li>
									<a id="weblogLink" class="sitemapLink" href="">WEBLOG</a>
								</li>
								<li>|</li>
								<li>
									<a id="onlineStoreLink" class="sitemapLink" href="./store.php">ONLINE STORE</a>
								</li>
								<li>|</li>
								<li>
									<a id="loginLink" class="sitemapLink" href="">LOGIN</a>
								</li>
							</ul>
							<div id="createYourAccountButton" class="<?php if($createAccountOption) { echo "createYourAccountButton"; } else { echo "createYourAccountButtonGone"; } ?>">
								<div class="headingMajor" id="createYourAccountButtonHeading"><!--CREATE YOUR ACCOUNT!--></div>
							</div>
							<div id="languageSelectorButton" class="languageSelectorButton" onclick="activateMenu('language-text');">
								<div id="arrowImage" class="arrowImage"></div>
								<div id="languageSetting" class="languageSetting" style="background-position: 0px 0px;"></div>
							</div>
							<div id="flagContainer" class="flagContainer" style="background-position: 0px 0px;"></div>
						</div>
					</div>
					<!--<div class="logoBar backgroundOrange" id="logoBar">logo bar</div>-->
					<div id="languageTextMenu" class="languageTextMenu hidden">
						<div id="usLanguage" class="languageText" onclick="selectLanguage('en');">EN</div>
						<div id="spLanguage" class="languageText" onclick="selectLanguage('sp');">SP</div>
						<div id="deLanguage" class="languageText" onclick="selectLanguage('de');">DE</div>
						<div id="daLanguage" class="languageText" onclick="selectLanguage('da');">DA</div>
						<div id="nlLanguage" class="languageText" onclick="selectLanguage('nl');">NL</div>
						<div id="frLanguage" class="languageText" onclick="selectLanguage('fr');">FR</div>
						<div id="huLanguage" class="languageText" onclick="selectLanguage('hu');">HU</div>
						<div id="jaLanguage" class="languageText" onclick="selectLanguage('ja');">JA</div>
					<!--	<ul>
							<li>
								<a href="" onclick="select_language('us');">US
							</li>
						</ul>	-->
					</div>
				</div>
				<?php
					if(isset($page))
					{
						require_once("pages/".$page.".php");
					}
					else
					{
						echo "Error Occurred. Could not retrieve ".$page.".php";
					}
				?>
				<div id="footerBar" class="footerBar">
					<div id="siteMap2" class="siteMap2">
						<ul>
							<li>
								<a id="overviewLink2" class="sitemapLink" href="./index.php">OVERVIEW</a>
							</li>
							<li>|</li>
							<li>
								<a id="communityLink2" class="sitemapLink" href="">COMMUNITY</a>
							</li>
							<li>|</li>
							<li>
								<a id="weblogLink2" class="sitemapLink" href="">WEBLOG</a>
							</li>
							<li>|</li>
							<li>
								<a id="onlineStoreLink2" class="sitemapLink" href="./store.php">ONLINE STORE</a>
							</li>
							<li>|</li>
							<li>
								<a id="loginLink2" class="sitemapLink" href="">LOGIN</a>
							</li>
						</ul>
					</div>
				</div>
				<div id="copyrightBar" class="copyrightBar copyright">Copyright (C) 2008 BlueZam. All Rights Reserved.</div>
			</div>
		</div>
	</body>
</html>