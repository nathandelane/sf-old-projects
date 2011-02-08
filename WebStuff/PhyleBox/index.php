<?php

require_once(dirname(__FILE__) . "/_index.inc.php");

$page = new _Index_Page();
$page->openDocument();
?>
<div class="column left">
	<p>PhyerNet offers hosting services for many types of artists. Our services give these artists a centralized location where they can store and showcase their work in a manner that is pleasing to them. They can display their works, advertise, and meet other artists. Here is an example of what you could get if you decided to host with us:</p>
	<ul>
		<li>250Mb+ personal storage space*</li>
		<li>Easy to use and secure file management tools</li>
		<li>Fast and stable chat services</li>
		<li>Friendly 24-hour support</li>
		<li>Zero bandwidth limits</li>
	</ul>
	<span class="disclaimer">* Storage space may vary in size depending on service type</span>
	<?php $page->buttons["signUpNow"]->render(); ?>
</div>
<div class="column">
	<div id="newsItems">
		<div class="containerTop"></div>
		<div class="containerMiddle">
			<h2>PhyerNet News</h2>
			<div id="newsItem0" class="newsItem">
				<h4>30 July 2010</h4>
				Mandatory team meeting on Saturday, 31 July 2010, starting at 6:00 pm EST.
			</div>
			<div id="newsItem1" class="hide newsItem">
				<h4>24 July 2010</h4>
				Brace yourselves! The new and improved PhyerNet is almost ready!
			</div>
			<div id="newsItem2" class="hide newsItem">
				<h4>20 June 2010</h4>
				Chat system restored. Beta phase is nearly ended. We will give an update later.
			</div>
			<div id="newsItem3" class="hide newsItem">
				<h4>19 March 2010</h4>
				We are still restoring services. Updates can also be found here: 
				<a href="http://www.twitter.com/phyernet">www.twitter.com/phyernet</a>
				.
			</div>
		</div>
		<div class="containerBottom"></div>
	</div>
</div>
<script type="text/javascript">
	$(document).ready(function() {
		if ($Phyer) {
			$Phyer.News.showNewsItem(0);
			$Phyer.News._startRotation();
		}
	});
</script>
<?php

$page->closeDocument();

?>