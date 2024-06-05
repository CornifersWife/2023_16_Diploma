using UnityEngine;

public class AIDragDrop : MonoBehaviour
{
	// By using this component, you can simulate the drag and drop system.

	[Range(0.1f, 2.0f)]
	public float MovementSpeed = 1.0f;

	static DragDropManager DDM;

	public static string ReservedPanel = "";
	public static string ReservedObject = "";

	void Awake()
	{
		// Getting DDM GameObject
		DDM = FindObjectOfType<DragDropManager>();
	}

	public static void DragDrop(string ObjectId, string PanelId, bool InstantMove = false)
	{
		for (int i = 0; i < DDM.AllObjects.Count; i++)
		{
			if (ObjectId == DDM.AllObjects[i].Id)
			{
				if (!DDM.AllObjects[i].OnReturning)
				{
					ReservedPanel = PanelId;
					PanelSettings AIPanel = DragDropManager.GetPanelById(PanelId);
					if (!InstantMove)
					{
						ReservedObject = DragDropManager.GetPanelObjectId(PanelId);
						// Smooth movement
						DDM.AllObjects[i].PointerDown("AI0", AIPanel);
					}
					else
					{
						// Instant movement
						DDM.AllObjects[i].PointerDown("AI1", AIPanel);
					}
				}
				break;
			}
		}
	}
}