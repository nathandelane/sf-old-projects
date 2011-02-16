<?php

/**
 * IRenderable
 * This interface represents any renderable component of an XHTML webpage.
 * @author lanathan
 *
 */
interface IRenderable {
	
	/**
	 * render
	 * Renders the component. All rendering should take place in here.
	 * @return void
	 */
	public function render();
	
}

?>