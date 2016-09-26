using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ClockManager))]
public class ClockManagerEditor : Editor
{
    private ClockManager _clockManager;
	
	void OnEnable()
	{
	    _clockManager = target as ClockManager;
        
	}

    public override void OnInspectorGUI()
    {
        _clockManager.UseSystemTime = EditorGUILayout.Toggle("System time", _clockManager.UseSystemTime);
        if (!_clockManager.UseSystemTime)
        {
            _clockManager.CurrentTimeControl =
                   (ClockManager.TimeControl)EditorGUILayout.EnumPopup("Time control", _clockManager.CurrentTimeControl);
            EditorGUILayout.LabelField("Current time:", "");
            EditorGUI.indentLevel = 1;
            _clockManager.Hours = EditorGUILayout.IntSlider("Hours", _clockManager.Hours, 0, 23);
            _clockManager.Minutes = EditorGUILayout.IntSlider("Minutes", _clockManager.Minutes, 0, 59);
            _clockManager.Seconds = EditorGUILayout.IntSlider("Seconds", _clockManager.Seconds, 0, 59);
            _clockManager.TimeSpeed = EditorGUILayout.IntSlider("Time speed", _clockManager.TimeSpeed, 1, 100);
            EditorGUI.indentLevel = 0;
        }
        _clockManager.AlarmClock = EditorGUILayout.Toggle("Alarm clock", _clockManager.AlarmClock);
        if (_clockManager.AlarmClock)
        {
            EditorGUI.indentLevel = 1;
            _clockManager.SetAlarmHours = EditorGUILayout.IntSlider("Hours", _clockManager.SetAlarmHours, 0, 23);
            _clockManager.SetAlarmMinutes = EditorGUILayout.IntSlider("Minutes", _clockManager.SetAlarmMinutes, 0, 59);
            _clockManager.SetAlarmSeconds = EditorGUILayout.IntSlider("Seconds", _clockManager.SetAlarmSeconds, 0, 59);
            _clockManager.AlarmDuration = EditorGUILayout.IntSlider("Duration (sec)", _clockManager.AlarmDuration, 1,
                                                                    300);
            EditorGUI.indentLevel = 0;
        }
        _clockManager.TextColor = EditorGUILayout.ColorField("Text color", _clockManager.TextColor);
        _clockManager.transform.Find("ClockTextHours").gameObject.GetComponent<TextMesh>().text = _clockManager.Hours.ToString("00");
        _clockManager.transform.Find("ClockTextMinutes").gameObject.GetComponent<TextMesh>().text = _clockManager.Minutes.ToString("00");
        _clockManager.transform.Find("ClockTextSeconds").gameObject.GetComponent<TextMesh>().text = _clockManager.Seconds.ToString("00");
        _clockManager.transform.Find("ClockTextColons").gameObject.GetComponent<TextMesh>().GetComponent<Renderer>().sharedMaterial.color = _clockManager.TextColor;
        _clockManager.transform.Find("ClockTextHours").gameObject.GetComponent<TextMesh>().GetComponent<Renderer>().sharedMaterial.color = _clockManager.TextColor;
        _clockManager.transform.Find("ClockTextMinutes").gameObject.GetComponent<TextMesh>().GetComponent<Renderer>().sharedMaterial.color = _clockManager.TextColor;
        _clockManager.transform.Find("ClockTextSeconds").gameObject.GetComponent<TextMesh>().GetComponent<Renderer>().sharedMaterial.color = _clockManager.TextColor;
        _clockManager.transform.Find("ClockLight").gameObject.GetComponent<Light>().color = _clockManager.TextColor;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Events");
        if (GUILayout.Button("Add"))
            _clockManager.EventsCount++;
        if (GUILayout.Button("Delete") & _clockManager.EventsCount >= 0)
            _clockManager.EventsCount--;
        EditorGUILayout.EndHorizontal();
        EditorGUI.indentLevel = 1;

        for (var i = 0; i < _clockManager.EventsCount; i++)
        {
            if (_clockManager.EventsCount >= 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("", "Event " + (i + 1));
                _clockManager.EventName[i] = EditorGUILayout.TextField("Name", _clockManager.EventName[i]);
                _clockManager.EventHours[i] = EditorGUILayout.IntSlider("Hours", _clockManager.EventHours[i], 0, 23);
                _clockManager.EventMinutes[i] = EditorGUILayout.IntSlider("Minutes", _clockManager.EventMinutes[i], 0, 59);
                _clockManager.EventSeconds[i] = EditorGUILayout.IntSlider("Seconds", _clockManager.EventSeconds[i], 0, 59);
            }
        }

        EditorGUI.indentLevel = 0;


        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
