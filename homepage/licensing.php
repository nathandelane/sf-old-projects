<?php
	include("_php/doctype.php");
	$page = "licensing";
?>
<html>
	<head>
		<title>Nathandelane, open source licensing</title>
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
				Licensing
			</div>
			<div class="innerContent" id="licensing">
				<h3>Licensing</h3>
				Generally speaking, I govern my works under GNU licensing. Also as a general rule, I always use the latest up 
				to date licensing available from the <a id="licensingLink" href="http://www.fsf.org">Free Software Foundation</a>. In order to 
				comply with licensing standards, all of my licenses, or rather the licenses that I utilize are available on this 
				web site in text format. For consistency I have also provided links below to those licenses online.
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule0"></div>
			<div class="innerContent" id="licenses">
				<h3>Licenses</h3>
				<div class="linkSection" id="gpl3">
					<a id="gpl3HomeLink" href="http://www.gnu.org/licenses/gpl.html">GNU General Public License, version 3.0</a>. I also keep a local copy 
					<a id="gpl3LocalLink" href="_licenses/gpl.txt">here</a>.
				</div>
				<div class="linkSection" id="fdl">
					<a id="fdlHomeLink" href="http://www.gnu.org/licenses/fdl.html">GNU Free Documentation License</a>. I also keep a local copy 
					<a id="fdlLocalLink" href="_licenses/fdl.txt">here</a>.
				</div>
				<div class="linkSection" id="fal">
					<a id="falHomeLink" href="http://artlibre.org/licence/lal/en/">GNU Free Art License</a>. I also keep a local copy 
					<a id="falLocalLink" href="_licenses/fal.txt">here</a>.
				</div>
				<div class="linkSection" id="fsfLicenses">
					<a id="fsfLicensesLink" href="http://www.gnu.org/licenses/license-list.html">Free Software License List</a>. This list shows several other 
					free software licenses that various groups, companies, and individuals use, including whether or not the licenses are 
					compatible with the GNU General Public License.
				</div>
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule1"></div>
			<div class="copyrightContainer" id="copyright">
				<?php include("_php/copyright.php"); ?>
			</div>
		</div>
	</body>
</html>