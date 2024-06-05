using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DragDropManager : MonoBehaviour {

	// platforms
	public enum Platforms { PC, Mobile };
	public Platforms TargetPlatform;

	// dragging modes
	public enum DragModes { ChangeToMousePos, DoNotChange };

	public static DragDropManager DDM;

	// panels
	public List<PanelSettings> AllPanels;
	// objects
	public List<ObjectSettings> AllObjects;
	// canvases
	public Canvas FirstCanvas;
	public Canvas SecondCanvas;

	[Header ("Save scene states")]
	public bool SaveStates = false;

	[Header ("Object dragging modes")]
	public DragModes DraggingModes;

	[Header("Events Management")]
	public UnityEvent BeforeSetup;
	public UnityEvent AfterSetup;

	void Start () {
		UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();

		// Getting current DDM GameObject
		DDM = this;

		if (BeforeSetup != null)
        {
			BeforeSetup.Invoke();
        }

		// Order Objects by sibling index
		AllObjects.Sort((objA, objB) =>
		objA.GetComponent<RectTransform>().GetSiblingIndex().CompareTo(objB.GetComponent<RectTransform>().GetSiblingIndex()));

		// Initialize Drag & Drop system
		Init ();
		
		if (AfterSetup != null)
		{
			AfterSetup.Invoke();
		}
	}
	
	public void Init () {
		for (int i = 0; i < AllObjects.Count; i++) {
			// Setting the first position of objects
			AllObjects [i].FirstPos = AllObjects [i].GetComponent<RectTransform> ().position;
			// Setting the first scale of objects
			AllObjects [i].FirstScale = AllObjects [i].GetComponent<RectTransform> ().localScale;

			SetupDefaultPanel (i, 's');
			if (SaveStates && PlayerPrefs.HasKey(AllObjects[i].Id + "X"))
			{
				LoadSavedPositions(i);
			}
		}
		if (SaveStates)
		{
			// Load Positions of Multi Objects
			for (int j = 0; j < AllPanels.Count; j++)
			{
				if (AllPanels[j].ObjectReplacement == PanelSettings.ObjectReplace.MultiObjectMode)
				{
					AllPanels[j].LoadObjectsList();
				}
			}
		}
	}

	void SetupDefaultPanel (int i, char mode) {
		for (int j = 0; j < AllPanels.Count; j++) {
			// Check if there is any object on any panel (Default Panel)
			if (RectTransformUtility.RectangleContainsScreenPoint (AllPanels [j].GetComponent<RectTransform> (), AllObjects[i].GetComponent<RectTransform> ().position)) {
				// Check if the panel is blocked
				if (AllPanels[j].Ignore || (AllObjects[i].FilterPanels && !Array.Exists(AllObjects[i].AllowedPanels, panel => panel == AllPanels[j].Id)))
					return;
					
				AllObjects [i].DefaultPanel = true;
				if (SaveStates && PlayerPrefs.HasKey (AllObjects[i].Id + "X") && mode == 's') {
					// Object detection must be done through LoadSavedPositions method
					return;
				}

				if (AllObjects[i].ScaleOnDropped) {
					AllObjects[i].GetComponent<RectTransform> ().localScale = AllObjects[i].DropScale;
				}
				// Setting the Id of object for Panel Object detection
				if (AllPanels [j].ObjectReplacement == PanelSettings.ObjectReplace.MultiObjectMode) {
					AllPanels [j].SetMultiObject (AllObjects[i].Id);
				}
				AllObjects[i].Dropped = true;
				SetPanelObject (j, AllObjects[i].Id);
			}
		}
	}

	void LoadSavedPositions (int i) {
		if (AllObjects[i].Id != "")
		{
			// Setting the position of object to last saved position
			AllObjects[i].GetComponent<RectTransform>().position = new Vector3(PlayerPrefs.GetFloat(AllObjects[i].Id + "X"), PlayerPrefs.GetFloat(AllObjects[i].Id + "Y"), AllObjects[i].GetComponent<RectTransform>().position.z);
			for (int j = 0; j < AllPanels.Count; j++)
			{
				// Check if the object is on any panel
				if (RectTransformUtility.RectangleContainsScreenPoint(AllPanels[j].GetComponent<RectTransform>(), AllObjects[i].GetComponent<RectTransform>().position))
				{
					// Check if the panel is blocked
					if (AllPanels[j].Ignore || (AllObjects[i].FilterPanels && !Array.Exists(AllObjects[i].AllowedPanels, panel => panel == AllPanels[j].Id)))
						return;
					
					if (AllObjects[i].ScaleOnDropped)
					{
						AllObjects[i].GetComponent<RectTransform>().localScale = AllObjects[i].DropScale;
					}
					// Setting the Id of object for Panel Object detection
					SetPanelObject(j, AllObjects[i].Id);
					AllObjects[i].Dropped = true;
				}
			}
		}
		else
		{
			Debug.LogError("Set the Id of <" + AllObjects[i].gameObject.name + "> Object to use save system with it!");
		}
	}

	public void SetPanelObject (int PanelIndex, string ObjectId) {
		AllPanels [PanelIndex].ObjectId = ObjectId;
	}

	public static void ResetScene()
	{
		// Reset Objects
		for (int i = 0; i < DDM.AllObjects.Count; i++)
		{
			DDM.AllObjects[i].Dropped = false;
			DDM.AllObjects[i].GetComponent<RectTransform>().SetAsLastSibling();
			DDM.AllObjects[i].GetComponent<RectTransform>().position = DDM.AllObjects[i].FirstPos;
			if (DDM.SaveStates)
			{
				PlayerPrefs.DeleteKey(DDM.AllObjects[i].Id + "X");
				PlayerPrefs.DeleteKey(DDM.AllObjects[i].Id + "Y");
			}
			if (DDM.AllObjects[i].ScaleOnDropped)
				DDM.AllObjects[i].GetComponent<RectTransform>().localScale = DDM.AllObjects[i].FirstScale;
		}
		// Reset Panels
		for (int i = 0; i < DDM.AllPanels.Count; i++)
		{
			DDM.AllPanels[i].ObjectId = "";
			if (DDM.AllPanels[i].ObjectReplacement == PanelSettings.ObjectReplace.MultiObjectMode)
			{
				DDM.AllPanels[i].PanelIdManager.Clear();
				DDM.AllPanels[i].DeleteObjectsList();
			}
		}

		DDM.Init();
	}

	// Finds the index of the panel that holds the given object
	public int IndexOfObjectId(string objId)
	{
		for (int i = 0; i < AllPanels.Count; i++)
		{
			if (AllPanels[i].ObjectId == objId)
				return i;
		}
		return -1;
	}

	public static string GetPanelObjectId (string PanelId) {
		return GetPanelById(PanelId).ObjectId;
	}

	public static string[] GetPanelObjectsIds (string PanelId) {
		List<string> IdStatus = new List<string> ();

		for (int i = 0; i < DDM.AllPanels.Count; i++) {
			if (PanelId == DDM.AllPanels [i].Id) {
				IdStatus = DDM.AllPanels [i].PanelIdManager;
			}
		}
		return IdStatus.ToArray();
	}

	public static string GetObjectPanelId(string ObjectId)
	{
		string IdStatus = "";

		for (int i = 0; i < DDM.AllPanels.Count; i++)
		{
			if (DDM.AllPanels[i].ObjectReplacement != PanelSettings.ObjectReplace.MultiObjectMode)
			{
				if (ObjectId == DDM.AllPanels[i].ObjectId)
				{
					IdStatus = DDM.AllPanels[i].Id;
				}
			}
			else
			{
				if (DDM.AllPanels[i].PanelIdManager.Contains(ObjectId))
				{
					IdStatus = DDM.AllPanels[i].Id;
				}
			}
		}
		return IdStatus;
	}

	public static ObjectSettings GetObjectById(string ObjectId)
	{
		foreach (ObjectSettings obj in DDM.AllObjects)
		{
			if (obj.Id == ObjectId)
				return obj;
		}
		return null;
	}

	public static PanelSettings GetPanelById(string PanelId)
	{
		foreach (PanelSettings pnl in DDM.AllPanels)
		{
			if (pnl.Id == PanelId)
				return pnl;
		}
		return null;
	}

	public static void AddObject (ObjectSettings obj)
    {
		DDM.AllObjects.Add (obj);

		obj.GetComponent<RectTransform>().SetAsLastSibling();

		// Setting the first position of objects
		obj.FirstPos = obj.GetComponent<RectTransform>().position;
		// Setting the first scale of objects
		obj.FirstScale = obj.GetComponent<RectTransform>().localScale;

		DDM.SetupDefaultPanel(DDM.AllObjects.Count - 1, 'd');
	}

	public static void RemoveObject(ObjectSettings obj)
	{
		DDM.AllObjects.Remove (obj);

		if (DDM.SaveStates && PlayerPrefs.HasKey(obj.Id + "X")) {
			PlayerPrefs.DeleteKey(obj.Id + "X");
			PlayerPrefs.DeleteKey(obj.Id + "Y");
        }

		string panelId = GetObjectPanelId (obj.Id);
		// Removing panel object detection records
		if (panelId != "")
        {
			for (int i = 0; i < DDM.AllPanels.Count; i++)
            {
				if (DDM.AllPanels[i].Id == panelId)
                {
					if (DDM.AllPanels[i].ObjectId == obj.Id)
						DDM.AllPanels[i].ObjectId = "";
					if (DDM.AllPanels[i].ObjectReplacement == PanelSettings.ObjectReplace.MultiObjectMode)
                    {
						DDM.AllPanels[i].PanelIdManager.Remove(obj.Id);
						if (DDM.AllPanels[i].PanelIdManager.Count > 0)
							DDM.AllPanels[i].ObjectId = DDM.AllPanels[i].PanelIdManager[DDM.AllPanels[i].PanelIdManager.Count - 1];
						if (DDM.SaveStates)
                        {
							DDM.AllPanels[i].SaveObjectsList();
							// Collecting garbage
							PlayerPrefs.DeleteKey(DDM.AllPanels[i].Id + "&&" + DDM.AllPanels[i].PanelIdManager.Count.ToString());
						}
					}
                }
            }
        }
	}

	public void SmoothMoveStarter (string state, RectTransform Target, Vector3 TargetPos, float Speed)
    {
		StartCoroutine(SmoothMove(state, Target, TargetPos, Speed));
    }

	bool Approximately (float valueA, float valueB)
	{
		return Mathf.Abs (valueA - valueB) < 0.04f;
	}

	// Smooth Movement tool
	IEnumerator SmoothMove(string state, RectTransform Target, Vector3 TargetPos, float Speed)
	{
		Target.GetComponent<ObjectSettings>().OnReturning = true;
		float t = 0.0f;
		TargetPos.z = Target.position.z;

		// Save last position of target object
		if (SaveStates)
		{
			PlayerPrefs.SetFloat(Target.GetComponent<ObjectSettings>().Id + "X", TargetPos.x);
			PlayerPrefs.SetFloat(Target.GetComponent<ObjectSettings>().Id + "Y", TargetPos.y);
		}

		while (!Approximately(Target.position.x, TargetPos.x) || !Approximately(Target.position.y, TargetPos.y))
		{
			t += Time.deltaTime * Speed;
			Target.position = Vector3.Lerp(Target.position, TargetPos, Mathf.SmoothStep(0.0f, 1.0f, t));
			yield return null;
		}
		Target.position = TargetPos;
		Target.GetComponent<ObjectSettings>().OnReturning = false;

		if (state.StartsWith("AI"))
		{
			Target.GetComponent<ObjectSettings>().PointerUp(state);
			AIDragDrop.ReservedPanel = "";
			AIDragDrop.ReservedObject = "";
		}
	}

	// Event Starter
	public void CallEvent (UnityEvent _event)
    {
		if (_event != null)
			StartCoroutine(StartEvent(_event));
    }
	IEnumerator StartEvent (UnityEvent _event) {
		yield return new WaitForFixedUpdate ();
		_event.Invoke ();
    }
}