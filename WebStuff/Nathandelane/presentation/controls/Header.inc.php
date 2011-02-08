<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * Header
 * The global header for all pages on www.nathandelane.com
 * @author lanathan
 *
 */
final class Header implements IRenderable {
	
	/**
	 * Constructor
	 * @return Header
	 */
	public function Header() {
		// TODO: Whatever needs to be done in the header.
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div id="header">
	<div class="top">
		<a href="/">
			<img alt="Nathandelane.com" src="/_img/Nathandelane-Green.png" />
		</a>
		<ul id="navigation">
			<li>
				<a <?php if ($this->_isSelected("resume") === true) { echo "class=\"selected\""; } ?>href="/resume.php">Resume</a>
			</li>
			<li>
				<a <?php if ($this->_isSelected("projects") === true) { echo "class=\"selected\""; } ?>href="/projects/">Projects</a>
			</li>
			<li>
				<a <?php if ($this->_isSelected("web-reference") === true) { echo "class=\"selected\""; } ?>href="/web-references/">Web Reference</a>
			</li>
		</ul>
	</div>
	<div class="bottom">
	</div>
</div>
<?php
		
	}
	
	/**
	 * _isSelected
	 * Renders something if the section is selected.
	 * @param string $section
	 * @return bool
	 */
	private function _isSelected(/*string*/ $section) {
		ArgumentTypeValidator::isString($section, "Section must be a string.");
		
		$isSelected = false;
		
		if (Strings::contains($_SERVER["REQUEST_URI"], $section)) {
			$isSelected = true;
		}
		
		return $isSelected;
	}
	
}

?>