using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {
	
	private float _timer;
	private float _minutesToLive;

	[SerializeField]
	private float[] _possibleMinutesToLive;

	private int _randomMinutesToLiveChooser;

	// Use this for initialization
	void Start () {
		Initialize ();
	}

	public void Initialize()
	{
		_randomMinutesToLiveChooser = Random.Range (0, _possibleMinutesToLive.Length - 1);
		_minutesToLive = _possibleMinutesToLive [_randomMinutesToLiveChooser];
		_timer = MinutesToSecondsConversion (_minutesToLive);
	}
	
	// Update is called once per frame
	void Update () {
		ManageDeathTimer ();
	}

	void ManageDeathTimer()
	{
		_timer -= Time.deltaTime;

		if (_timer < 0) 
		{
			// Death;
		}
	}

	float MinutesToSecondsConversion(float minutes)
	{
		return minutes * 60;
	}
}
