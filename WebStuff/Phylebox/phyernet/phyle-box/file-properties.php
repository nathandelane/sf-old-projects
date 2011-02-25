<?php

require_once(dirname(__FILE__) . "/_file-properties.inc.php");

$page = new _File_Properties_Page();
$page->openDocument();

?>
<div class="contentInner">
	<h1>Edit Properties</h1>
	<form id="filePropertiesForm" name="filePropertiesForm" action="<?php echo $_SERVER["REQUEST_URI"]; ?>" method="post" enctype="application/x-www-form-urlencoded">
		<div class="panel">
			<div class="panelInner">
				<h2>General Information</h2>
				<input type="hidden" name="token" id="token" value="<?php echo $page->createToken(); ?>" />
				<table id="filePropertiesFormTable">
					<tr>
						<td>
							<div class="type<?php echo $page->getFieldValue("type"); ?>"></div>
						</td>
						<td>
							<input type="text" name="fileName" id="fileName" value="<?php echo $page->getFieldValue("fileName"); ?>" />
						</td>
					</tr>
				</table>
			</div>
		</div>
		<input type="submit" name="submit" id="submitButton" value="Save Properties" />
	</form>
</div>
<?php

$page->closeDocument();

?>