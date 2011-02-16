<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * Button
 * This class renders a button.
 * @author lanathan
 *
 */
class Button implements IRenderable {
	
	private $_htmlId;
	private $_text;
	private $_linkHref;
	
	/**
	 * Constructor
	 * @return Button
	 */
	public function Button(/*string*/ $htmlId, /*string*/ $text, /*string*/ $linkHref) {
		$this->_htmlId = $htmlId;
		$this->_text = $text;
		$this->_linkHref = $linkHref;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div id="<?php echo "$this->_htmlId"; ?>" class="customButton">
	<a href="<?php echo "$this->_linkHref"; ?>">
		<span class="left"></span>
		<span class="content">
			<?php echo "$this->_text"; ?>
		</span>
		<span class="right"></span>
	</a>
</div>
<?php
		
	}
	
}

?>