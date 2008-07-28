package org.lane.opentcards.types;

import java.util.*;

public abstract class TCard {
	
	public TCard() {
		fieldsAndTypes = new Hashtable<String, String>();
	}
	
	public boolean addField(String label, String type) {
		boolean retVal = true;
		
		if(!fieldsAndTypes.containsKey(label)) {
			fieldsAndTypes.put(label, type);
		} else {
			retVal = false;
		}
		
		return retVal;
	}
	
	public boolean removeField(String label) {
		boolean retVal = true;
		
		if(fieldsAndTypes.containsKey(label)) {
			fieldsAndTypes.remove(label);
		} else {
			retVal = false;
		}
		
		return retVal;
	}
	
	private Hashtable<String, String> fieldsAndTypes;

}
