using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Guiaaaaaa;
using HumanAPI;
using I2.Loc;
using InControl;
using Multiplayer;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

// Token: 0x02000313 RID: 787
public class Game : MonoBehaviour, IGame, IDependency
{
	private void Awake()
	{
		PostProcessLayer[] componentsInChildren = base.GetComponentsInChildren<PostProcessLayer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = true;
		}
		base.gameObject.AddComponent<Gui_mono>();
		Human_Mod_GraduateSchool.getins().Human_mod_Init();
	}

}