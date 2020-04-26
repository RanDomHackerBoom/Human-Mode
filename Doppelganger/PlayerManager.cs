//File path: Assembly-CSharp.dll/-/PlayerManager.cs
//Only mod code is here, NOT game code.
using System;
using System.Collections.Generic;
using InControl;
using Multiplayer;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public void OnLocalPlayerAdded(NetPlayer player)
	{
		this.ApplyControls();
		if (NetGame.instance.local.players.Count > 1000 && this.activeDevices.Count < 2000)//Unlock the number of avatars
		{
			NetGame.instance.RemoveLocalPlayer(player);
		}
	}
}