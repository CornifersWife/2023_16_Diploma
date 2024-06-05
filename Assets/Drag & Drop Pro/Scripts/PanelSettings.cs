using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PanelSettings : MonoBehaviour
{
	[HideInInspector]
	public string ObjectId = "";

	// Enums
	public enum ObjectPosStates { UseObjectSettings, DroppedPosition, PanelPosition };
	public enum ObjectLockStates { UseObjectSettings, LockObject, DoNotLockObject };
	public enum ObjectReplace { Allowed, NotAllowed, MultiObjectMode };
	//

	// Customization Tools
	public string Id;   // the Id of this panel

	[Tooltip("Objects will ignore this panel")]
	public bool Ignore = false;

	[Header("Object Position")]
	[Tooltip("Customize the position of the object when it gets dropped on this panel")]
	public ObjectPosStates ObjectPosition;

	[Header("Lock Object")]
	[Tooltip("Customize Object Locking")]
	public ObjectLockStates LockObject;

	[Header("Replacement & Multi Object")]
	[Tooltip("Allow Object to Replace & Switch or use Multi Object Mode")]
	public ObjectReplace ObjectReplacement;
	//

	[Header("Events Management")]
	[Tooltip("When any object gets dropped on the panel, the functions that you added to this event trigger will be called")]
	public UnityEvent OnObjectDropped;

	[Tooltip("When the object of this panel, gets dropped on another panel")]
	public UnityEvent OnObjectExit;

	[HideInInspector]
	// Used for Multi Object tool
	public List<string> PanelIdManager;

	public void SetMultiObject(string ObjectId)
	{
		// Adding new object to the list of dropped objects
		PanelIdManager.Add(ObjectId);
		if (DragDropManager.DDM.SaveStates)
		{
			PlayerPrefs.SetString(Id + "&&" + (PanelIdManager.Count - 1).ToString(), ObjectId);
		}
	}

	public void RemoveMultiObject(string ObjectId)
	{
		// Removing an object from list of dropped objects
		if (DragDropManager.DDM.SaveStates)
		{
			PlayerPrefs.DeleteKey(Id + "&&" + (PanelIdManager.Count - 1).ToString());
		}
		PanelIdManager.Remove(ObjectId);
	}

	public void SaveObjectsList()
	{
		for (int i = 0; i < PanelIdManager.Count; i++)
		{
			PlayerPrefs.SetString(Id + "&&" + i.ToString(), PanelIdManager[i]);
		}
	}

	public void DeleteObjectsList()
	{
		for (int counter = 0; PlayerPrefs.HasKey(Id + "&&" + counter.ToString()); counter++)
		{
			PlayerPrefs.DeleteKey(Id + "&&" + counter.ToString());
		}
	}

	public void LoadObjectsList()
	{
		PanelIdManager.Clear();
		// Loading the list of dropped objects
		for (int counter = 0; PlayerPrefs.HasKey(Id + "&&" + counter.ToString()); counter++)
		{
			PanelIdManager.Add(PlayerPrefs.GetString(Id + "&&" + counter.ToString()));
			for (int i = 0; i < DragDropManager.DDM.AllObjects.Count; i++)
			{
				if (DragDropManager.DDM.AllObjects[i].Id == PanelIdManager[counter])
				{
					DragDropManager.DDM.AllObjects[i].GetComponent<RectTransform>().SetAsLastSibling();
					DragDropManager.DDM.SetPanelObject(DragDropManager.DDM.AllPanels.IndexOf(this), DragDropManager.DDM.AllObjects[i].Id);
					break;
				}
			}
		}
	}
}