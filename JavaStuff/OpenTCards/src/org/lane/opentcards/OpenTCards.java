package org.lane.opentcards;

import java.util.*;
import org.lane.opentcards.types.*;
import org.lane.opentcards.userinterface.*;

public class OpenTCards {
	
	private static ArrayList<TCardsBoard> boardCollection;

	public static void main(String args[]) {
		new OpenTCardsGUI((boardCollection = new ArrayList<TCardsBoard>()));
	}
}
