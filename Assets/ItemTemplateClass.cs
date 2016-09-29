using UnityEngine;
using System.Collections;

public class ItemTemplateClass : MonoBehaviour {

	[SerializeField]
	private Sprite _spriteToChange;
	[SerializeField]
	private string _itemName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeImageOnUI()
	{
		UIImageChangingScript.instance.ChangeSpriteImage(_spriteToChange);
	}

}
