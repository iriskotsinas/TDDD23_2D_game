using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctionMenu animatorFunctionsMenu;
	[SerializeField] public int thisIndex;
	const int PLAY_GAME = 0;
	const int SANDBOX = 1;
	const int QUIT = 2;
	public static bool canClick = false;

    // Update is called once per frame
    void Update()
    {	

		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetAxis ("Submit") == 1 || (Input.GetMouseButtonDown(0) && canClick))
			{
				animator.SetBool ("pressed", true);
			}
			else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
				animatorFunctionsMenu.disableOnce = true;

				//Switch to next scene

				if (thisIndex == PLAY_GAME)
                {
					Debug.Log("Switch intro scene");
					SceneManager.LoadScene(sceneName: "CutScene");
				}
				else if( thisIndex == SANDBOX)
                {
					Debug.Log("Switch intro scene");
					SceneManager.LoadScene(sceneName: "Level 0");
				}
				else if (thisIndex == QUIT)
				{
					Application.Quit();
				}
			}
		}else{
			animator.SetBool ("selected", false);
		}
    }
}
