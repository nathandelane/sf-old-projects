<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * GoogleAnalytics
 * This class renders the requisite Google Analytics.
 * @author lanathan
 *
 */
class GoogleAnalytics implements IRenderable {
	
	private $_webPropertyId;
	
	/**
	 * Constructor
	 * @param string $webPropertyId
	 * @return GoogleAnalytics
	 */
	public function GoogleAnalytics(/*string*/ $webPropertyId) {
		ArgumentTypeValidator::isString($webPropertyId, "WebPropertyId must be a string.");
		
		$this->_webPropertyId = $webPropertyId;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		if (isset($this->_webPropertyId)) {
		
?>
<script type="text/javascript">
  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', '<?php echo "$webPropertyId"; ?>']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();
</script>
<?php

		}
	}
	
}

?>