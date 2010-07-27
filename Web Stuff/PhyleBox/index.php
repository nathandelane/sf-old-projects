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
	<div id="services">test</div>
</div>
<?php

$page->closeDocument();

?>