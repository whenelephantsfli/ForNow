using UnityEngine;
using System.Collections;

public class ClockManager : MonoBehaviour
{
    public enum TimeControl
    {
        Forward, Backward, Stop
    }

    public int EventsCount;
    public string[] EventName = new string[50];
    public int[] Events = new int[50];
    public int[] EventHours = new int[50];
    public int[] EventMinutes = new int[50];
    public int[] EventSeconds = new int[50];

    public TimeControl CurrentTimeControl;
    public bool UseSystemTime;
    public bool AlarmClock;
    public int Hours;
    public int Minutes;
    public int Seconds;
    public int SetAlarmHours;
    public int SetAlarmMinutes;
    public int SetAlarmSeconds;
    public int AlarmDuration;
    public int TimeSpeed;
    public Color TextColor;

    private Light _clockGlow;
    private bool _startAlarm;
    private int _alarmDurationTemp;
    private TextMesh _clockTextColons;
    private TextMesh _clockTextHours;
    private TextMesh _clockTextMinutes;
    private TextMesh _clockTextSeconds;
    private Transform _myTransform;

	void Start ()
	{
	    _myTransform = transform;
        _clockTextColons = _myTransform.Find("ClockTextColons").gameObject.GetComponent<TextMesh>();
        _clockTextHours = _myTransform.Find("ClockTextHours").gameObject.GetComponent<TextMesh>();
        _clockTextMinutes = _myTransform.Find("ClockTextMinutes").gameObject.GetComponent<TextMesh>();
        _clockTextSeconds = _myTransform.Find("ClockTextSeconds").gameObject.GetComponent<TextMesh>();
	    _clockGlow = _myTransform.Find("ClockLight").gameObject.GetComponent<Light>();
	    StartCoroutine(Timer());
        _alarmDurationTemp = AlarmDuration;
	}
	
	void Update ()
	{
        _clockGlow.color = TextColor;
        _clockTextColons.GetComponent<Renderer>().material.color = TextColor;
        _clockTextHours.GetComponent<Renderer>().material.color = TextColor;
        _clockTextMinutes.GetComponent<Renderer>().material.color = TextColor;
        _clockTextSeconds.GetComponent<Renderer>().material.color = TextColor;
        _clockTextHours.text = Hours.ToString("00");
	    _clockTextMinutes.text = Minutes.ToString("00");
        _clockTextSeconds.text = Seconds.ToString("00");

        if (!UseSystemTime)
        {
            if (CurrentTimeControl == TimeControl.Forward)
            {
                if (Seconds == 60)
                {
                    Minutes++;
                    Seconds = 0;
                }
                if (Minutes == 60)
                {
                    Hours++;
                    Minutes = 0;
                }
                if (Hours == 24)
                    Hours = 0;
            }
            else if (CurrentTimeControl == TimeControl.Backward)
            {
                if (Seconds == -1)
                {
                    Minutes--;
                    Seconds = 60;
                }
                if (Minutes == -1)
                {
                    Hours--;
                    Minutes = 60;
                }
                if (Hours == -1)
                    Hours = 24;
            }
        }
        else
        {
            Hours = System.DateTime.Now.Hour;
            Minutes = System.DateTime.Now.Minute;
            Seconds = System.DateTime.Now.Second;
        }

	    if (Hours == SetAlarmHours && Minutes == SetAlarmMinutes && Seconds == SetAlarmSeconds & AlarmClock)
        {
            _startAlarm = true;
        }

        if (_startAlarm)
        {
            if (Seconds == SetAlarmSeconds)
                GetComponent<AudioSource>().Play();
            
            if (Seconds % 2 == 0)
            {
                _clockGlow.intensity = 0.5f;
                _clockTextColons.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                _clockGlow.intensity = 0.9f;
                _clockTextColons.GetComponent<Renderer>().enabled = true;
            }
            if (AlarmDuration == 0)
            {
                GetComponent<AudioSource>().Stop();
                AlarmDuration = _alarmDurationTemp;
                _clockTextColons.GetComponent<Renderer>().enabled = true;
                _startAlarm = false;
            }
        }
	}

    IEnumerator Timer()
    {
        while (true)
        {
            if (!UseSystemTime)
            {
                if (CurrentTimeControl == TimeControl.Forward)
                    Seconds++;
                else if (CurrentTimeControl == TimeControl.Backward)
                    Seconds--;
            }
            if (_startAlarm)
                AlarmDuration--;
            yield return new WaitForSeconds((float)1 / TimeSpeed);
        }
    }

    
    public string GetCurrentTime()
    {
        return Hours.ToString("00") + ":" + Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }

    public void SetCurrentTime(int hours, int minutes, int seconds)
    {
        if (UseSystemTime)
            return;
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }

    public int GetHours()
    {
        return Hours;
    }

    public int GetMinutes()
    {
        return Minutes;
    }

    public int GetSeconds()
    {
        return Seconds;
    }
    
    public void SetAlarmTime(int hours, int minutes, int seconds)
    {
        SetAlarmHours = hours;
        SetAlarmMinutes = minutes;
        SetAlarmSeconds = seconds;
    }

    public void Switch(bool enableClock)
    {
        if (enableClock)
        {
            _clockGlow.enabled = true;
            _clockTextColons.GetComponent<Renderer>().enabled = true;
            _clockTextHours.GetComponent<Renderer>().enabled = true;
            _clockTextMinutes.GetComponent<Renderer>().enabled = true;
            _clockTextSeconds.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            _clockGlow.enabled = false;
            _clockTextColons.GetComponent<Renderer>().enabled = false;
            _clockTextHours.GetComponent<Renderer>().enabled = false;
            _clockTextMinutes.GetComponent<Renderer>().enabled = false;
            _clockTextSeconds.GetComponent<Renderer>().enabled = false;
        }
    }

    public void SetTimeControl(TimeControl type, int speed)
    {
        CurrentTimeControl = type;
        TimeSpeed = speed;
        SetTimeControl(TimeControl.Backward, 3);
    }

    public bool GetEvent(string eventName)
    {
        for (int i = 0; i < EventsCount; i++)
        {
            if (EventName[i] == eventName)
            {
                if (EventHours[i] == Hours && EventMinutes[i] == Minutes && EventSeconds[i] == Seconds)
                    return true;
            }
        }
        return false;
    }
}
