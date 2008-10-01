<div id="innerPageBody" class="innerPageBody">
	<h1>Highlights</h1>
	<?php
		$highlightsXml = new DOMDocument();
		$highlightsXml->load('_xml\highlights.xml');
		$xmlHighlights = simplexml_import_dom($highlightsXml);
		
		foreach($xmlHighlights->item as $item)
		{
	?>
	<p>
		<div class="stamp">
			<span><?php echo $item['date']; ?></span>
			<span class="itemTitle">|</span>
			<span class="itemTitle"><?php echo $item['title']; ?></span>
		</div>
		<p><?php echo $item; ?></p>
		<div class="dashedSeparator"></div>
	</p>
	<?php
		}
	?>
</div>
