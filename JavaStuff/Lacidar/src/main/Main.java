package main;

import java.awt.Color;

import com.nathandelane.lacidar.*;

/**
 * @author lanathan
 *
 */
public class Main {

	private DesktopCanvas canvas;
	private FullScreenHandler fullScreenHandler;
	
	private Main() throws InterruptedException {
		this.canvas = new GameCanvas();
		this.canvas.setVisible(true);
		this.canvas.setBackground(Color.BLACK);		

		new GameEventHandler(this.canvas);

		this.fullScreenHandler = new FullScreenHandler(this.canvas, 32, 60);
		this.fullScreenHandler.setFullScreen();
	}
	/**
	 * @param args
	 */
	public static void main(String[] args) {
		try {
			new Main();
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

}
