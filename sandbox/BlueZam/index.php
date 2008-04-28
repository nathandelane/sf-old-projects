<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<?php
	include_once("lib/PhpSession.php");
	
	$userSession = new PhpSession();
	$userSession->setCurrentPage("index.php");
?>
<html>
	<head>
		<title>Project</title>
		<link rel="stylesheet" type="text/css" href="_stylesheets/main.css"/>
	</head>
	<body>
		<div class="pageContainer" id="pageContainer">
			<div class="innerContainer" id="innerContainer">
				<div class="banner" id="banner">
					<div class="siteMapBar backgroundRed" id="siteMapBar">
						<div id="siteMap" class="siteMap">
							<ul>
								<li>OVERVIEW</li>
								<li>|</li>
								<li>COMMUNITY</li>
								<li>|</li>
								<li>WEBLOG</li>
								<li>|</li>
								<li>ONLINE STORE</li>
								<li>|</li>
								<li>LOGIN</li>
							</ul>
							<div id="createYourAccountButton" class="createYourAccountButton backgroundBlueZamBlue">
								<div class="headingMajor" id="createYourAccountButtonHeading">CREATE YOUR ACCOUNT!</div>
							</div>
							<div id="languageSelectorButton" class="languageSelectorButton">
								<div id="arrowImage" class="arrowImage"></div>
								<div id="languageSetting" class="languageSetting">EN</div>
							</div>
							<div id="flagContainer" class="flagContainer" style="background-position: 0px 0px;"></div>
						</div>
					</div>
					<div class="logoBar backgroundOrange" id="logoBar">logo bar</div>
				</div>
				<div id="buttonAndHeaderContainer" class="buttonAndHeaderContainer backgroundRed">
					<div id="chooseYourArtButton" class="chooseYourArtButton backgroundBlueZamBlue">
						<div class="headingMajor" id="chooseYourArtButtonHeading">- CHOOSE YOUR ART -</div>
					</div>
					<div id="blueZamGives" class="blueZamGives backgroundBlueZamBlue">
						<div class="headingMajor" id="chooseYourArtButtonHeading">- BLUE ZAM GIVES ALL ARTISTS A HOME IN WHICH TO DISPLAY THEIR ART -</div>
					</div>
				</div>
				<div id="leftColumn" class="leftColumn backgroundRed">
					<div id="auctionBlockHeader" class="auctionBlockHeader backgroundBlueZamBlue">
						<div id="auctionBlockHeaderText" class="headingMinor">AUCTION BLOCK</div>
					</div>
					<div id="auctionBlock" class="auctionBlock backgroundOrange"></div>
					<div id="zamTonesHeader" class="zamTonesHeader backgroundBlueZamBlue">
						<div id="zamTonesHeaderText" class="headingMinor">ZAM TONES</div>
					</div>
					<div id="zamTones" class="zamTones backgroundOrange"></div>
					<div id="verticalAdleft" class="verticalAdleft backgroundOrange">
						<iframe id="verticalAdLeftIFrame" class="verticalAdLeftIFrame" frameborder="0"></iframe>
					</div>
				</div>
				<div id="rightColumn" class="rightColumn backgroundRed">
					<div id="artisticCommunity" class="smallContainer">
						<div id="artisticCommunityInner" class="smallContainerInner backgroundRed"></div>
					</div>
					<div id="artisticCommunity" class="smallContainer">
						<div id="artisticCommunityInner" class="smallContainerInner backgroundRed"></div>
					</div>
					<div id="artisticCommunity" class="smallContainer">
						<div id="artisticCommunityInner" class="smallContainerInner backgroundRed"></div>
					</div>
				</div>
			</div>
		</div>
	</body>
</html>