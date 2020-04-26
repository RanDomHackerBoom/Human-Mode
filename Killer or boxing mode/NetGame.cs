using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Guiaaaaaa;
using HumanAPI;
using Steamworks;
using UnityEngine;

namespace Multiplayer
{
	// Token: 0x02000536 RID: 1334
	public class NetGame : MonoBehaviour, IDependency
	{
	private void OnReceiveChatServer(NetHost client, NetStream stream)
		{
			uint clientId = stream.ReadNetId();
			string nick = stream.ReadString();
			string text = stream.ReadString();
			if (text.ToLower().Substring(0, 4).Equals("toup") || text.ToLower().Substring(0, 4).Equals("kill"))
			{
				Human_Mod_GraduateSchool.getins().Init_Client_mod(client, text);
				return;
			}
			NetChat.OnReceive(clientId, nick, text);
			for (int i = 0; i < this.readyclients.Count; i++)
			{
				if (this.readyclients[i] != client)
				{
					this.SendReliable(this.readyclients[i], stream);
				}
			}
			Human_Mod_GraduateSchool.getins().Init_Client_mod(client, text);
		}

public List<NetHost> readyclients = new List<NetHost>();
	}