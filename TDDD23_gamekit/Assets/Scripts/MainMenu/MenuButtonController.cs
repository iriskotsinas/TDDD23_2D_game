using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonController : MonoBehaviour {

	// Use this for initialization
	public int index;
	[SerializeField] bool keyDown;
	[SerializeField] int maxIndex;
	public AudioSource audioSource;

	
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		//IsPointerOverUIButton(GetEventSystemRaycastResults());

		if (Input.GetAxis ("Vertical") != 0){
			if(!keyDown){
				if (Input.GetAxis ("Vertical") < 0) {
					if(index < maxIndex){
						index++;
					}else{
						index = 0;
					}
				} else if(Input.GetAxis ("Vertical") > 0){
					if(index > 0){
						index --; 
					}else{
						index = maxIndex;
					}
				}
				keyDown = true;
			}
		}else{
			keyDown = false;
		}
	}

	///Returns 'true' if we touched or hovering on Unity UI element.
	//public static bool IsPointerOverUIElement()
	//{
	//	return IsPointerOverUIButton(GetEventSystemRaycastResults());
	//}
	///Returns 'true' if we touched or hovering on Unity UI element.
	//public static void IsPointerOverUIButton(List<RaycastResult> eventSystemRaysastResults)
	//{
	//	for (int index = 0; index < eventSystemRaysastResults.Count; index++)
	//	{
	//		RaycastResult curRaysastResult = eventSystemRaysastResults[index];

	//		Debug.Log(curRaysastResult.gameObject.name);

	//		if (curRaysastResult.gameObject.tag == "MenuButton")
 //           {
	//			index = curRaysastResult.gameObject.GetComponentInParent<MenuButton>().thisIndex;
	//			Debug.Log("Updated index to " + index);
	//		}		
	//	}
	//}

	/////Gets all event systen raycast results of current mouse or touch position.
	//static List<RaycastResult> GetEventSystemRaycastResults()
	//{
	//	PointerEventData eventData = new PointerEventData(EventSystem.current);
	//	eventData.position = Input.mousePosition;
	//	List<RaycastResult> raysastResults = new List<RaycastResult>();
	//	EventSystem.current.RaycastAll(eventData, raysastResults);
	//	return raysastResults;
	//}


	


	//   private void OnTriggerEnter2D(Collider2D collision)
	//   {
	//	index = collision.gameObject.GetComponent<MenuButton>().thisIndex;
	//	MenuButton.canClick = true;
	//   }
	//private void OnTriggerExit2D(Collider2D collision)
	//{
	//	MenuButton.canClick = false;
	//}
}
