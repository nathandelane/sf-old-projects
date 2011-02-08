<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_Once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_Once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * Breadcrumb
 * This class represents a breadcrumb for PhyleBox.
 * @author lanathan
 *
 */
class Breadcrumb implements IRenderable {
	
	private $_breadcrumbComponents;
	
	/**
	 * Constructor
	 * @return Breadcrumb
	 */
	public function Breadcrumb() {
		$this->_breadcrumbComponents = array();
	}
	
	/**
	 * setBreadcrumb
	 * Sets the breadcrumb's components.
	 * @param array $breadcrumbComponents
	 */
	public function setBreadcrumb(array $breadcrumbComponents) {
		$this->_breadcrumbComponents = $breadcrumbComponents;
	}
	
	/**
	 * addComponent
	 * Adds a component to the breadcrumb.
	 * @param string $label
	 * @param string $href
	 */
	public function addComponent(/*string*/ $label, /*string*/ $href) {
		ArgumentTypeValidator::isString($label, "Label must be a string.");
		ArgumentTypeValidator::isString($href, "Href must be a string.");
		
		if (!array_key_exists($label, $this->_breadcrumbComponents)) {
			$this->_breadcrumbComponents["$label"] = $href;
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		$keys = array_keys($this->_breadcrumbComponents);
				
?>
<div class="breadcrumb">
	<ul>
<?php

		if (count($keys) > 0) {
			$counter = 0;
			
			foreach ($this->_breadcrumbComponents as $label => $href) {
				if ($counter > 0) {
					
?>
		<li>
			&gt;
		</li>
<?php
					
				}
				
				if (is_null($href)) {
					
?>
		<li>
			<?php echo "$label"; ?>
		</li>
<?php
					
				} else {
				
?>
		<li>
			<a href="<?php echo "$href"; ?>">
				<?php echo "$label"; ?>
			</a>
		</li>
<?php

				}

				$counter++;
			}
		}
			
?>
	</ul>
</div>
<?php

	}
	
}

?>