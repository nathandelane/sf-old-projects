/**
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

package com.nathandelane.mud.server;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.net.InetAddress;
import java.net.Socket;
import java.util.Date;

public class TestClient {

	public static void main(String[] args) {
		String host = "localhost";
		int port = 19999;
		StringBuffer stringBuffer = new StringBuffer();
		String timeStamp;
		
		System.out.println("Mud Test Client initialized...");
		
		try {
			InetAddress address = InetAddress.getByName(host);
			Socket connection = new Socket(address, port);
			
			BufferedOutputStream outputStream = new BufferedOutputStream(connection.getOutputStream());
			OutputStreamWriter outputWriter = new OutputStreamWriter(outputStream, "US-ASCII");
			
			timeStamp = (new Date()).toString();
			
			String process = "Calling Mud Server on " + host + " port " + port + " at " + timeStamp + "\n";
			
			outputWriter.write(process);
			outputWriter.flush();
			
			BufferedInputStream inputStream = new BufferedInputStream(connection.getInputStream());
			InputStreamReader inputReader = new InputStreamReader(inputStream, "US-ASCII");
			
			int nextCharacter;
			
			while ((nextCharacter = inputReader.read()) != 10) {
				stringBuffer.append((char)nextCharacter);
			}
			
			connection.close();
			
			System.out.println(stringBuffer);
		} catch(IOException ioe) {
			ioe.printStackTrace();
		} catch(Exception e) {
			e.printStackTrace();
		}
	}

}
