using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIImageChangingScript : MonoBehaviour {


	public static UIImageChangingScript instance = null;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void ChangeSpriteImage(Sprite sprite)
	{
		GetComponent<Image>().sprite = sprite;
	}

	public void DisplayImage()
	{
		GetComponent<Image>().enabled = true;
	}

	public void HideImage()
	{
		GetComponent<Image>().enabled = false;
	}

}
