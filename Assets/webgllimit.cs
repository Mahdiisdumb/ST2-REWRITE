using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class webgllimit : MonoBehaviour {
	public GameObject[] whattoDisable;
	//for to direct dissables
    public bool webgl = false;
	//some more testing
	void Start() {
        //if on webgl dissable the object
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            webgl = true;
        }
        else
        {
            webgl = false;
        }
        //this should set the webgl bool to true if on webgl and false if not
        if (webgl == true)
		{
            //for each object in the array disable it
            foreach (GameObject whattoDisable in whattoDisable)
            {
                whattoDisable.SetActive(false);
            }
            //exactly like that
		}
		//somthing like that
	}
}