package main;

import javax.swing.JFrame;

import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

public class GameEventHandler implements KeyListener, MouseListener {
	
	private JFrame frame;
	
	public GameEventHandler(JFrame frame) {
		this.frame = frame;
		this.frame.addKeyListener(this);
		this.frame.getContentPane().addKeyListener(this);
	}

	@Override
	public void keyPressed(KeyEvent event) {
		System.out.println("Key was pressed: " + event.getModifiersEx() + " + "  + event.getKeyCode());
		
		int keyCode = event.getKeyCode();
		
		if (keyCode == KeyEvent.VK_ESCAPE) {
			this.frame.dispose();
		}
	}

	@Override
	public void keyReleased(KeyEvent event) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void keyTyped(KeyEvent event) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mouseClicked(MouseEvent arg0) {
		System.out.println("Mouse was clicked.");		
		
	}

	@Override
	public void mouseEntered(MouseEvent arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mouseExited(MouseEvent arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mousePressed(MouseEvent arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mouseReleased(MouseEvent arg0) {
		// TODO Auto-generated method stub
		
	}
	
}