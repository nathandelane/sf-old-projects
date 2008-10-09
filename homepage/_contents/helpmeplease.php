<div id="innerPageBody" class="innerPageBody">
	<h1>Do You Need Help With my Web Site?</h1>
	<p>
		Well you've come to the right place. Here I describe some of the things I've done and give mostly obvious reasons for it. You can also discover some good practices to pursue while browsing the web. You will probably also come to realize that I have strong opinions, but they are very well founded. Anyway, enjoy.
	</p>
	<?php
		$faqXml = new DOMDocument();
		$faqXml->load('_xml\faq.xml');
		$xmlFaq = simplexml_import_dom($faqXml);
		
		foreach($xmlFaq->item as $item)
		{
	?>
	<p>
		<span style="font-weight: bold;">Q:&nbsp;</span>
		<span><?php echo $item->question; ?></span>
		<p>
			<span style="font-weight: bold;">A:&nbsp;</span>
			<span><?php echo $item->faqanswer; ?></span>
		</p>
		<div class="dashedSeparator"></div>
	</p>
	<?php
		}
	?>
</div>
