using UnityEngine;
using System.Collections;

public class DetectInventory : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
	}
		
	void DetectPlayerInventory(ItemTemplateClass holdingItem)
	{
		string holdingItemName = holdingItem.name;

		switch(holdingItemName)
		{
			case "Apple":
			HappyFunction();
				break;
			case "Fish":
			SadFunction();
				break;
			case "PocketLint":
				break;
			case "BeefJerky":
			KickSomebodyFunction();
				break;
		}
	}

	void HappyFunction()
	{
	}

	void SadFunction()
	{
	}

	void KickSomebodyFunction()
	{
	}


}
