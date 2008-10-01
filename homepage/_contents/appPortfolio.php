<div id="innerPageBody" class="innerPageBody">
	<h1>Application Portfolio</h1>
	<?php
		$appsXml = new DOMDocument();
		$appsXml->load('_xml\applications.xml');
		$xmlApplications = simplexml_import_dom($appsXml);
		
		foreach($xmlApplications->item as $item)
		{
	?>
	<p>
		<div class="stamp">
			<span class="itemTitle">
				<a href="<?php echo $item['link']; ?>"><?php echo $item['name']; ?></a>
			</span>
		</div>
		<p><?php echo $item; ?></p>
		<div class="dashedSeparator"></div>
	</p>
	<?php
		}
	?>
</div>
