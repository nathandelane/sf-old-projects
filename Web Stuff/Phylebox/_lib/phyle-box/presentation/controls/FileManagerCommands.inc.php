<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

class FileManagerCommands implements IRenderable {
	
	public function FileManagerCommands() {
		
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div id="commands">
	<ul>
		<li>
			<a href="#" title="Create New File" class="smallIcons createNewFile"></a>
		</li>
		<li>
			<a href="#" title="Create New Folder" class="smallIcons createNewFolder"></a>
		</li>
	</ul>
</div>
<?php
		
	}
	
}

?>