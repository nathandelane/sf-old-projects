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
									<a id="overviewLink" href="">OVERVIEW</a>
								</li>
								<li>|</li>
								<li>
									<a id="communityLink" href="">COMMUNITY</a>
								</li>
								<li>|</li>
								<li>
									<a id="weblogLink" href="">WEBLOG</a>
								</li>
								<li>|</li>
								<li>
									<a id="onlineStoreLink" href="">ONLINE STORE</a>
								</li>
								<li>|</li>
								<li>
									<a id="loginLink" href="">LOGIN</a>
								</li>
							</ul>
							<div id="createYourAccountButton" class="createYourAccountButton">
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
				<div id="buttonAndHeaderContainer" class="buttonAndHeaderContainer">
					<div id="chooseYourArtMenuContainer" class="chooseYourArtMenuContainer hidden">
					</div>
					<div id="chooseYourArtButton" class="chooseYourArtButton">
						<div class="headingMajor" id="chooseYourArtButtonHeading"><!--- CHOOSE YOUR ART ---></div>
					</div>
					<div id="chooseYourArtMenuContainer" class="chooseYourArtMenuContainer">
					</div>
					<div id="blueZamGives" class="blueZamGives">
						<div class="headingMajor" id="blueZamGivesHeading"><!--- BLUE ZAM GIVES ALL ARTISTS A HOME IN WHICH TO DISPLAY THEIR ART ---></div>
					</div>
				</div>
				<div id="leftColumn" class="leftColumn">
					<div id="auctionBlockHeader" class="auctionBlockHeader">
						<div id="auctionBlockHeaderText" class="headingMinor"><!--AUCTION BLOCK--></div>
					</div>
					<div id="auctionBlock" class="auctionBlock backgroundBlueZamDarkGray"></div>
					<div id="zamTonesHeader" class="zamTonesHeader backgroundBlueZamBlue">
						<div id="zamTonesHeaderText" class="headingMinor"><!--ZAM TONES--></div>
					</div>
					<div id="zamTones" class="zamTones backgroundBlueZamDarkGray"></div>
					<div id="verticalAdleft" class="verticalAdleft backgroundWhite">
						<iframe id="verticalAdLeftIFrame" class="verticalAdLeftIFrame" frameborder="0" src="advertising/exampleAd.html"></iframe>
					</div>
				</div>
				<div id="rightColumn" class="rightColumn">
					<div id="communityLinks" class="communityLinks">
						<div id="artisticCommunity" class="smallContainer">
							<div id="artisticCommunityInner" class="smallContainerInner artisticCommunity"></div>
							<div id="artisticCommunityButton" class="smallContainerButton">
								<div id="artisticCommunityButtonText" class="headingMinor">- ARTISTIC COMMUNITY -</div>
							</div>
						</div>
						<div id="zamBattleground" class="smallContainer">
							<div id="zamBattlegroundInner" class="smallContainerInner zamBattleGround"></div>
							<div id="zamBattlegroundButton" class="smallContainerButton">
								<div id="artisticCommunityButtonText" class="headingMinor">- ZAM BATTLEGROUND -</div>
							</div>
						</div>
						<div id="publishing" class="smallContainer">
							<div id="publishingInner" class="smallContainerInner publishing"></div>
							<div id="publishingButton" class="smallContainerButton">
								<div id="artisticCommunityButtonText" class="headingMinor">- PUBLISHING -</div>
							</div>
						</div>
					</div>
					<div id="separator1" class="dottedHorizontalSeparator"></div>
					<div id="selectedProfiles" class="selectedProfiles">
						<div id="selectedProfile1" class="selectedProfileOuter backgroundBlueZamDarkGray">
							<div id="selectedProfile1Header" class="selectedProfileHeader">
								<div id="selectedProfileHeaderText" class="headingMinor">- BLUE ZAM PUBLISHING FEATURED AUTHOR -</div>
							</div>
							<div id="selectedProfile1Picture" class="selectedProfilePicture">
								<img id="featuredPublisher" src="_images/profileimages/phillipejones.jpeg"/>
							</div>
							<div id="selectedProfile1Info" class="selectedProfileInfo backgroundBlueZamDarkGray">
								<div id="selectedProfile1Name" class="selectedProfileName headingMinor">PHILLIP E. JONES</div>
								<div id="selectedProfile1Bio" class="selectedProfileBio">
									Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Pellentesque convallis. Maecenas vestibulum enim at odio. Integer non risus nec metus porttitor ornare. Fusce condimentum ipsum quis mauris. Etiam tempus ante non nibh. Vivamus congue, pede ac fermentum facilisis, metus arcu vulputate quam, at vulputate massa nibh et massa. Donec augue. Cras pede. Suspendisse potenti. Quisque porttitor ornare elit. Integer interdum dapibus eros. Aenean vitae orci sed nunc sollicitudin suscipit. Nulla pellentesque. Integer blandit ornare tellus. Morbi ac dui.
								</div>
								<div id="selectedProfile1MoreLink" class="selectedProfileMoreLink">
									<a href="">MORE</a>
								</div>
							</div>
						</div>
						<div id="separator2" class="dottedVerticalSeparator"></div>
						<div id="selectedProfile1" class="selectedProfileOuter backgroundBlueZamDarkGray">
							<div id="selectedProfile1Header" class="selectedProfileHeader">
								<div id="selectedProfileHeaderText" class="headingMinor">- ARTISTIC COMMUNITY FEATURED ARTIST -</div>
							</div>
							<div id="selectedProfile1Picture" class="selectedProfilePicture">
								<img id="featuredArtist" src="_images/profileimages/melodyjmainero.jpeg"/>
							</div>
							<div id="selectedProfile1Info" class="selectedProfileInfo backgroundBlueZamDarkGray">
								<div id="selectedProfile1Name" class="selectedProfileName headingMinor">MELODY J. MAINERO</div>
								<div id="selectedProfile1Bio" class="selectedProfileBio">
									Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Pellentesque convallis. Maecenas vestibulum enim at odio. Integer non risus nec metus porttitor ornare. Fusce condimentum ipsum quis mauris. Etiam tempus ante non nibh. Vivamus congue, pede ac fermentum facilisis, metus arcu vulputate quam, at vulputate massa nibh et massa. Donec augue. Cras pede. Suspendisse potenti. Quisque porttitor ornare elit. Integer interdum dapibus eros. Aenean vitae orci sed nunc sollicitudin suscipit. Nulla pellentesque. Integer blandit ornare tellus. Morbi ac dui.
								</div>
								<div id="selectedProfile1MoreLink" class="selectedProfileMoreLink">
									<a href="">MORE</a>
								</div>
							</div>
						</div>
					</div>
					<div id="separator1" class="dottedHorizontalSeparator"></div>
					<div id="bottomAd" class="bottomAd">
						<iframe id="bottomAdIFrame" class="bottomAdIFrame" frameborder="0" src="advertising/exampleBottomAd.html"></iframe>
					</div>
					<div id="separator1" class="dottedHorizontalSeparator"></div>
				</div>
				<div id="footerBar" class="footerBar">
					<div id="siteMap2" class="siteMap2">
						<ul>
							<li>
								<a id="overviewLink2" href="">OVERVIEW</a>
							</li>
							<li>|</li>
							<li>
								<a id="communityLink2" href="">COMMUNITY</a>
							</li>
							<li>|</li>
							<li>
								<a id="weblogLink2" href="">WEBLOG</a>
							</li>
							<li>|</li>
							<li>
								<a id="onlineStoreLink2" href="">ONLINE STORE</a>
							</li>
							<li>|</li>
							<li>
								<a id="loginLink2" href="">LOGIN</a>
							</li>
						</ul>
					</div>
				</div>
				<div id="copyrightBar" class="copyrightBar copyright">Copyright (C) 2008 BlueZam. All Rights Reserved.</div>
			</div>
		</div>
	</body>
</html>