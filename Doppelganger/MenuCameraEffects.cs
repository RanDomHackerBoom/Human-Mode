//File path: Assembly-CSharp.dll/-/MenuCameraEffects.cs
//Only mod code is here, NOT game code.
using System;
using System.Collections.Generic;
using Multiplayer;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;


public class MenuCameraEffects : MonoBehaviour, IDependency
{
	public void SetupViewports()
	{
		for (int i = 0; i < this.gameCameras.Count; i++)
		{
			this.gameCameras[i].SetViewport(0, 1);//Fix lens bug
		}
	}
}