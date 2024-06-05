using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	// Object Setting
	ObjectSettings OS;
	// DDM Game Object
	DragDropManager DDM;

	int SuccessfulTouch = 0;
	int TouchCount = 0;

	void Awake () {
		// Getting the Object Setting that assigned to this GameObject
		OS = GetComponent<ObjectSettings> ();
		// Getting DDM GameObject
		DDM = FindObjectOfType<DragDropManager>();
	}

	public void OnPointerDown (PointerEventData eventData) {
		if (!OS.OnReturning) {
			if (DDM.TargetPlatform == DragDropManager.Platforms.PC) {
				// for PC
				if (eventData.button == PointerEventData.InputButton.Left) {
					OS.PointerDown ("User", null);
				}
			} else {
				// for Mobile
				if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count == 1)
				{
					SuccessfulTouch++;
					OS.PointerDown("User", null);
				}
				TouchCount++;
			}
		}
	}

	public void OnPointerUp (PointerEventData eventData) {
		if (!OS.OnReturning) {
			if (DDM.TargetPlatform == DragDropManager.Platforms.PC)
			{
				// for PC
				if (eventData.button == PointerEventData.InputButton.Left)
				{
					OS.PointerUp("User");
				}
			}
			else
			{
				// for Mobile
				if (SuccessfulTouch == TouchCount)
				{
					SuccessfulTouch--;
					OS.PointerUp("User");
				}
				TouchCount--;
			}
		}
	}
}
