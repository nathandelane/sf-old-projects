package com.nathandelane.math.evaluation;

import java.util.Enumeration;
import java.util.Hashtable;

public class VariableCollection extends Hashtable<String, String> {

	private static final long serialVersionUID = -558295429673822974L;
	
	public String toString() {
		StringBuilder sb = new StringBuilder();
		Enumeration<String> enumerator = this.keys();
		
		while(enumerator.hasMoreElements()) {
			String key = enumerator.nextElement();
			String value = get(key);
			
			sb.append(String.format("%1$s = %2$s", key, value));
		}		
		
		return sb.toString();
	}

}
