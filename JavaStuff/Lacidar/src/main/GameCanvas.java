package main;

import java.awt.Dimension;

import com.nathandelane.lacidar.DesktopCanvas;

/**
 * Our implementation of DekstopCanvas.
 * @author lanathan
 *
 */
public class GameCanvas extends DesktopCanvas {

	private static final long serialVersionUID = 6147816380499199392L;
	
	/**
	 * Main constructor for GameCanvas.
	 * @param dimension
	 */
	protected GameCanvas() {
		super(new Dimension(1024, 768));
	}

}
