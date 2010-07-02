package com.nathandelane.lacidar;

import java.awt.*;

public class FullScreenHandler {
	
	private DesktopCanvas desktopCanvas;
//	private DisplayMode desiredDisplayMode;
	private DisplayMode originalDisplayMode;
	private GraphicsDevice defaultGraphicsDevice;
	
	/**
	 * Constructor for FullScreenHandler
	 * @param DesktopCanvas desktopCanvas
	 * @param int bitDepth
	 * @param int refreshRate
	 */
	public FullScreenHandler(DesktopCanvas desktopCanvas, int bitDepth, int refreshRate) {
		this.desktopCanvas = desktopCanvas;
//		this.desiredDisplayMode = new DisplayMode(this.desktopCanvas.getWidth(), this.desktopCanvas.getHeight(), bitDepth, refreshRate);

		GraphicsEnvironment graphicsEnvironment = GraphicsEnvironment.getLocalGraphicsEnvironment();
		GraphicsDevice[] graphicsDevices = graphicsEnvironment.getScreenDevices();
		
		this.defaultGraphicsDevice = graphicsDevices[0];
	}
	
	/**
	 * Sets the default graphics device to full-screen-exclusive mode.
	 * @return void
	 */
	public void setFullScreen() {
		System.out.println("Now setting default device to full-screen-exclusive mode.");
		
		this.originalDisplayMode = this.defaultGraphicsDevice.getDisplayMode();
		
		if (this.defaultGraphicsDevice.isFullScreenSupported()) {
			this.defaultGraphicsDevice.setFullScreenWindow(this.desktopCanvas);
		} else {
			System.out.println("Full-screen-exclusive mode is not supported on this device");
		}
	}
	
	/**
	 * Returns the default graphics device to original display mode.
	 * @return void
	 */
	public void unsetFullScreen() {
		System.out.println("Now setting default device to original mode.");
		
		this.defaultGraphicsDevice.setDisplayMode(this.originalDisplayMode);
	}

}
