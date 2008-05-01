<!--
	PageHeaderBar.php contains the header bar for every page using the default page template.
	Author: Nathan Lane
	Date: 01.05.2008
-->
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
					<div id="languageTextMenu" class="languageTextMenu hidden">
						<div id="usLanguage" class="languageText" onclick="selectLanguage('en');">EN</div>
						<div id="spLanguage" class="languageText" onclick="selectLanguage('sp');">SP</div>
						<div id="deLanguage" class="languageText" onclick="selectLanguage('de');">DE</div>
						<div id="daLanguage" class="languageText" onclick="selectLanguage('da');">DA</div>
						<div id="nlLanguage" class="languageText" onclick="selectLanguage('nl');">NL</div>
						<div id="frLanguage" class="languageText" onclick="selectLanguage('fr');">FR</div>
						<div id="huLanguage" class="languageText" onclick="selectLanguage('hu');">HU</div>
						<div id="jaLanguage" class="languageText" onclick="selectLanguage('ja');">JA</div>
					</div>
				</div>
