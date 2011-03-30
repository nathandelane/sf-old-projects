package com.nathandelane.net;

import java.io.*;
import java.net.*;
import java.util.*;

import com.nathandelane.Logger;

import net.htmlparser.jericho.*;

public class Agent implements Runnable {

	private URL url;
	private String responseBody;
	
	/**
	 * Gets the response body.
	 * @return
	 */
	public String getResponseBody() {
		return responseBody;
	}
	
	/**
	 * Creates an instance of Agent.
	 * @param url
	 */
	public Agent(URL url) {
		this.url = url;
	}
	
	/**
	 * Runs this thread.
	 */
	@Override
	public void run() {
		try {
			HttpURLConnection connection = (HttpURLConnection)url.openConnection();
			connection.setRequestMethod("GET");
			connection.setReadTimeout(60000);
			
			setResponseBody();
			
//			System.out.println(responseBody);
		} catch (IOException ioe) {
			Logger.getLogger().error(ioe.getMessage());
		}
	}
	
	/**
	 * Gets the response body from the request.
	 * @throws IOException
	 */
	private void setResponseBody() throws IOException {
		InputStreamReader reader = new InputStreamReader(url.openStream());
		char[] buffer = new char[1024];
		
		while (reader.read(buffer) != -1) {
			responseBody += new String(buffer);
		}
		
		reader.close();
		
		Source jerichoSource = new Source(responseBody);
		List<Element> elementList = jerichoSource.getAllElements(HTMLElementName.A);
		
//		System.out.print(jerichoSource.toString());
//		List<Element> elementList = jerichoSource.getAllElements();
//		
		for (Element nextElement : elementList) {
			System.out.print(nextElement.getName() + ": ");
			
			Attributes attributes = nextElement.getAttributes();
			
			int counter = 0;
			
			for (Attribute nextAttribute : attributes) {
				if (counter > 0) {
					System.out.print(", ");
				}
				
				System.out.print(nextAttribute.getName() + "=" + nextAttribute.getValue());
				
				counter++;
			}
			
			System.out.println();
//			System.out.println("-------------------------------------------------------------------------------");
//			System.out.println(nextElement.getDebugInfo());
//			
//			if (nextElement.getAttributes() != null) {
//				System.out.println("XHTML StartTag:\n" + nextElement.getStartTag().tidy(true));
//			}
//			
//			System.out.println("Source text with content:\n" + nextElement);
		}
	}

}
