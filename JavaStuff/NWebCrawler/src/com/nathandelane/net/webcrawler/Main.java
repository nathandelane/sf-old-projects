package com.nathandelane.net.webcrawler;

import java.net.*;

import com.nathandelane.net.Agent;

public class Main {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		try {
			Agent agent = new Agent(new URL(args[0]));
			agent.run();
		} catch (MalformedURLException mue) {
			mue.printStackTrace();
		}
	}

}
