using UnityEngine;
using System.Collections;

public class CurrentInventory : MonoBehaviour {

	[SerializeField]
	private ItemTemplateClass _leftHandItem;
	[SerializeField]
	private ItemTemplateClass _rightHandItem;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddItemToLeftHand(ItemTemplateClass item)
	{
		_leftHandItem = item;
	}

	public void AddItemToRightHand(ItemTemplateClass item)
	{
		_rightHandItem = item;
	}

}
