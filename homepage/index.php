<?php
	include("_php/doctype.php");
	$page = "index";
?>
<html>
	<head>
		<title>Nathandelane, programmer, designer, writer, husband and dad</title>
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
				Home
			</div>
			<div class="innerContent" id="bibleQuote">
				<div class="quote" id="quote0">
					<div class="quoteInner" id="quoteText0">
						"Be not deceived; God is not mocked: for whatsoever a man soweth, that shall he also reap.
						For he that soweth to his flesh shall of the flesh reap corruption;
						but be that soweth to the Spirit shall of the Spirit reap life everlasting."
					</div>
				</div>
				<div class="quoteAuthor" id="quoteAuthor0">
					<a id="quoteAuthorLink" href="http://scriptures.lds.org/en/gal/6">Galatians 6:7, 8</a>
				</div>
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule0"></div>
			<div class="innerContent" id="myWorks">
				This website is devoted to my works. Mainly those works consist of software productions. And of 
				those software productions the most part of them are computer applications. I will also include my 
				writings here, at least the ones that I think are important. Some of my writings are journal-like, 
				while others are more of documentation. Some are fictitious, and others are true. Also on this site 
				you will likely find links to things that please me as well as things that displease me. I'm not much 
				a politician, but you can definitely also find some political ramblings here and there. I'm also not 
				much of an artist, but I'd like to include some art here and there. So anyway, WELCOME.
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule1"></div>
			<div class="innerContent" id="projects">
				<h3>Projects</h3>
				Keeping in line with some of my political beliefs, I have chosen to create much of my software in a 
				manner consistent with the <a href="_licenses/gpl.txt">GNU General Public License, version 3</a>, which all of 
				my software works are distributed under. I am very opposed to software patents as I see that they destroy the 
				economy of software, and they inhibit freedom of thought and freedom of speech in a way that is not consistent 
				with my own personal religious beliefs. In addition I utilize the <a href="_licenses/fdl.txt">GNU Free Documentation</a> 
				Lincense for my documents, documentation, and other writings. In the case of my 
				artistic works, I have chosen to license them under the <a href="_licenses/fal.txt">GNU Free Art License</a>, 
				unless otherwise specified.
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule2"></div>
			<div class="innerContent" id="gnuLicensing">
				<h3>A Brief Explanation on GNU Licensing (Political)</h3>
				GNU Licensing comes from the <a id="gnuLink" href="http://www.gnu.org">GNU Free Software Foundation</a>, which was founded by 
				<a id="stallmanLink" href="http://www.stallman.org/">Richard Stallman</a>. When the word "free" is used in corellation with GNU 
				software, the term is associated with the meaning pertaining to freedom, not price. Thus the freedoms extended 
				by the GNU licenses attend to personal rights and liberty, as such GNU software may be sold, though most softwares 
				are publicly available from their relevant websites or project sites, and you may receive GNU software commercially 
				in this case, but what you may do with the software after you have received it falls completely under the GNU 
				General Public License. Regarding software patents, the <a id="fsfLink" href="http://www.fsf.org/">Free Software Foundation</a>
				does not approve of them, and I don't either. For this reason, the GNU GPL version 3 was developed, which under 
				section 10, paragraph 3 states:
				<div class="spacer" id="spacer0"></div>
				<div class="quote" id="quote1">
					<div class="quoteInner" id="quoteText1">
						"You may not impose any...restrictions on the exercise of the
						rights granted or affirmed under this License.  For example, you may
						not impose a license fee, royalty, or other charge for exercise of
						rights granted under this License, and <b>you may not initiate litigation
						(including a cross-claim or counterclaim in a lawsuit) alleging that
						any patent claim is infringed by making, using, selling, offering for
						sale, or importing the Program or any portion of it</b>."
					</div>
				</div>
				<div class="spacer" id="spacer1"></div>
				There is also a complete section devoted to patents, namely section 11. Do note that these licenses are completely legal 
				and binding in all of the United States of America and in most countries. As such members who have projects that are 
				licensed under the GNU General Public License are strongy urged to upgrade their license to version 3, which may be 
				viewed online <a id="gplLinkLocal" href="http://www.gnu.org/licenses/gpl.html">here</a>.
			</div>
			<div class="horizontalRule backgroundWhite" id="horizRule3"></div>
			<div class="copyrightContainer" id="copyright">
				<?php include("_php/copyright.php"); ?>
			</div>
		</div>
	</body>
</html>