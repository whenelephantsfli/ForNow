using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RaycastToObject : MonoBehaviour {

	[SerializeField]
	private Image _crosshairImage;

	[SerializeField]
	private Sprite _onHoverSprite;
	[SerializeField]
	private Sprite _nonHoverSprite;

	[SerializeField]
	private CurrentInventory _inventory;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ManageRaycast();
	}

	void ManageRaycast()
	{
		RaycastHit objectHit;        
		// Shoot raycast
		if (Physics.Raycast(transform.position, transform.forward, out objectHit, 50)) {
			//Debug.Log("Raycast hitted to: " + objectHit.collider);
			if (objectHit.transform.gameObject.tag == "InventoryObject")
			{
				_crosshairImage.sprite = _onHoverSprite;
				//Pick up object
				if (Input.GetMouseButtonDown(0))
				{
					objectHit.transform.GetComponent<ItemTemplateClass>().ChangeImageOnUI();
					UIImageChangingScript.instance.DisplayImage();
				}

				if (Input.GetMouseButtonDown(1))
				{
					_inventory.AddItemToRightHand(objectHit.transform.GetComponent<ItemTemplateClass>());
				}

				if (Input.GetMouseButtonDown(2))
				{
					_inventory.AddItemToLeftHand(objectHit.transform.GetComponent<ItemTemplateClass>());
				}


			}
			else
			{
				if (Input.GetMouseButtonDown(0))
				{
					UIImageChangingScript.instance.HideImage();
				}
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				UIImageChangingScript.instance.HideImage();
			}
		}

	}
}
