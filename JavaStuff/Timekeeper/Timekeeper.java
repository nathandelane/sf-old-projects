import javax.swing.*;
import java.awt.*;
import java.util.*;

public class Timekeeper extends JFrame
{
	private int _numScreens;
	private int _width;
	private int _height;

	public Timekeeper(int numScreens)
	{
		_numScreens = numScreens;
		_width = 250;
		_height = 20;

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

		while(true)
		{
			Calendar cal = Calendar.getInstance();
			int hour = cal.get(Calendar.HOUR_OF_DAY);
			int minute = cal.get(Calendar.MINUTE);
			int second = cal.get(Calendar.SECOND);
			int date = cal.get(Calendar.DAY_OF_MONTH);
			timeLabel.setText("" + timeToBool(hour) + ":" + timeToBool(minute) + ":" + timeToBool(second) + " [" + timeToBool(date) + "]");

			try {
				Thread.sleep(250);
			} catch(InterruptedException e) {
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
