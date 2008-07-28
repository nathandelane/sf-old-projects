package org.lane.opentcards.types;

import java.util.*;

public class TCardsBoard {
	
	public TCardsBoard() {
		cardCollection = new ArrayList<TCard>();
	}
	
	public boolean addCard(TCard newCard) {
		boolean retVal = true;
		
		if(!cardCollection.contains(newCard)) {
			cardCollection.add(newCard);
		} else {
			retVal = false;
		}
		
		return retVal;
	}
	
	public boolean removeCard(TCard oldCard) {
		boolean retVal = true;
		
		if(cardCollection.contains(oldCard)) {
			cardCollection.remove(oldCard);
		} else {
			retVal = false;
		}
		
		return retVal;
	}
	
	private ArrayList<TCard> cardCollection;

}
