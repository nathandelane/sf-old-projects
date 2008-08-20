package com.nathandelane.util.timekeeper;

import javax.swing.*;
import java.awt.*;
import java.util.*;
import java.io.*;
import java.security.*;

public class Timekeeper extends JFrame
{
	private int _numScreens;
	private int _width;
	private int _height;
	private Hashtable<String, String> _events;

	public Timekeeper(int numScreens)
	{
		_numScreens = numScreens;
		_width = 250;
		_height = 20;
		_events = new Hashtable<String, String>();
		
		int length = getEvents("Timekeeper.events");
		
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setSize(_width, _height);
		setUndecorated(true);
		
		Rectangle screen = getGraphicsConfiguration().getBounds();
		
		setLocation(screen.width * _numScreens - _width, screen.height - _height);
		setLayout(new BorderLayout());
		
		JLabel timeLabel = new JLabel("00:00");
		getContentPane().add(timeLabel, BorderLayout.CENTER);
		timeLabel.setFont(new Font("Arial", Font.PLAIN, 14));
		timeLabel.setHorizontalAlignment(JLabel.CENTER);
		
		setVisible(true);
		
		String eventsMD5Sum = "";
		
		try {
			eventsMD5Sum = getMD5Checksum("Timekeeper.events");
		} catch(Exception e) {
		}
		
		while(true)
		{
			Calendar cal = Calendar.getInstance();
			int hour = cal.get(Calendar.HOUR_OF_DAY);
			int minute = cal.get(Calendar.MINUTE);
			int second = cal.get(Calendar.SECOND);
			int date = cal.get(Calendar.DAY_OF_MONTH);
			String timestamp = "" + timeToBool(hour) + ":" + timeToBool(minute) + ":" + timeToBool(second);
			timeLabel.setText(timestamp  + " [" + timeToBool(date) + "]");
			
			for(int i = 0; i < length; i++) {
				System.out.print(timestamp + " == ");
				System.out.println(_events.get(timestamp));
				if(_events.containsKey(timestamp) || _events.containsKey(timestamp + " [" + timeToBool(date) + "]")) {
					JOptionPane.showMessageDialog(null, _events.get(timestamp));
				}
			}
			
			try {
				if(!getMD5Checksum("Timekeeper.events").equals(eventsMD5Sum)) {
					length = getEvents("Timekeeper.events");
					eventsMD5Sum = getMD5Checksum("Timekeeper.events");
				}
				
				Thread.sleep(250);
			} catch(InterruptedException e) {
			} catch(Exception e) {
			}
		}
	}

	private String timeToBool(int time)
	{
		String retVal = "";
		int modInt = 32;

		while(modInt > 0)
		{
			if(time % modInt == time)
			{
				retVal = retVal + "0";
			}
			else
			{
				retVal = retVal + "1";
				time -= modInt;
			}

			modInt /= 2;
		}		

		return retVal;
	}
	
	private byte[] createChecksum(String filename) throws IOException, NoSuchAlgorithmException {
		FileInputStream fis = new FileInputStream(filename);
		byte[] buffer = new byte[1024];
		MessageDigest md = MessageDigest.getInstance("MD5");
		int numRead;
		
		do {
			numRead = fis.read(buffer);
			
			if(numRead > 0) {
				md.update(buffer, 0, numRead);
			}
		} while(numRead != -1);
		
		fis.close();
		
		return md.digest();
	}
	
	private String getMD5Checksum(String filename) throws IOException, NoSuchAlgorithmException {
		String retVal = "";
		byte[] bytes = createChecksum(filename);
		
		for(int i = 0; i < bytes.length; i++) {
			retVal += Integer.toString(bytes[i], 16);
		}
		
		return retVal;
	}
	
	private int getEvents(String filename) {
		int length = 0;
		
		try {
			FileInputStream fis = new FileInputStream("Timekeeper.events");
			BufferedReader reader = new BufferedReader(new InputStreamReader(fis));
			String line = "0";
			
			while((line = reader.readLine()) != null) {
				String parts[] = line.split("~=");
				
				if(parts.length == 2) {
					_events.put(parts[0], parts[1]);
				}
			}
			
			length = _events.size();
			
			System.out.println(length);
			
			reader.close();
			fis.close();
		} catch(IOException e) {
		}
		
		return length;
	}

	public static void main(String[] args)
	{
		if(args.length < 1)
		{
			System.out.println("Number of screens required.");
		}
		else
		{
			new Timekeeper(Integer.parseInt(args[0]));
		}
	}
}
