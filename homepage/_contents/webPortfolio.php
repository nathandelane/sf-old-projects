<div id="innerPageBody" class="innerPageBody">
	<h1>Web Applications Portfolio</h1>
	<?php
		$webappsXml = new DOMDocument();
		$webappsXml->load('_xml\webapps.xml');
		$xmlWebapps = simplexml_import_dom($webappsXml);
		
		foreach($xmlWebapps->item as $item)
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
