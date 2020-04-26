//File path: Assembly-CSharp.dll/-/CheatCodes.cs
//Only mod code is here, NOT game code.
using System;
using System.Collections.Generic;
using Multiplayer;
using Steamworks;
using UnityEngine;


public class CheatCodes : MonoBehaviour
{

	private void Start()
	{
		Shell.RegisterCommand("+", new Action<string>(this.Add_Local_Player), "+ ---<Number of avatars> --- Add batch avatars");
		Shell.RegisterCommand("-", new Action<string>(this.Remove_Local_Player), "- ---Delete all parts. -<Number of avatars> --- Add batch avatars");
		Shell.RegisterCommand("kz", new Action<string>(this.Local_Control), "kz ---All avatar control (switch). Kz <avatar number> --- custom avatar control");
		Shell.RegisterCommand("zy", new Action<string>(this.Change_The_Lens), "zy <Avatar number> --- transfer avatar");
	}
	public void Add_Local_Player(string txt)
	{
		if (!string.IsNullOrEmpty(txt))
		{
			string[] array = txt.Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 1)
			{
				NetChat.Print("格式错误!");
				return;
			}
			int num = 0;
			if (!int.TryParse(array[0], out num))
			{
				NetChat.Print("格式错误!");
				return;
			}
			for (int i = 0; i < num; i++)
			{
				NetGame.instance.AddLocalPlayer();
			}
			Shell.Print(string.Format("已发送{0}个添加模型信息!\n当前本地玩家数量为{1}", num, NetGame.instance.local.players.Count));
			this.FangJianTiShi(string.Format("玩家{0}发动秘技分身术,尝试召唤{1}个分身!!!", NetGame.instance.local.name, num));
		}
	}
	private void Remove_Local_Player(string txt)
	{
		if (string.IsNullOrEmpty(txt))
		{
			for (int i = NetGame.instance.local.players.Count - 2; i >= 0; i--)
			{
				NetGame.instance.RemoveLocalPlayer(NetGame.instance.local.players[i]);
			}
			NetChat.Print("正在全部删除!");
			return;
		}
		string[] array = txt.Split(new char[]
		{
			' '
		}, StringSplitOptions.RemoveEmptyEntries);
		if (array.Length == 1)
		{
			int num = 0;
			if (int.TryParse(array[0], out num))
			{
				if (num > NetGame.instance.local.players.Count)
				{
					NetChat.Print("超出本地玩家当前数量!当前玩家数量为" + NetGame.instance.local.players.Count.ToString());
					return;
				}
				MenuCameraEffects.instance.RemoveHuman(NetGame.instance.local.players[num - 1]);
				NetGame.instance.RemoveLocalPlayer(NetGame.instance.local.players[num - 1]);
				NetChat.Print(string.Format("当前玩家{0}人,正在发送删除消息!", NetGame.instance.local.players.Count));
			}
		}
	}
	{
		if (string.IsNullOrEmpty(txt))
		{
			bool flag = false;
			for (int i = NetGame.instance.local.players.Count - 1; i >= 0; i--)
			{
				if (NetGame.instance.local.players[i].human.disableInput)
				{
					flag = true;
				}
			}
			if (flag)
			{
				for (int j = NetGame.instance.local.players.Count - 1; j >= 0; j--)
				{
					NetGame.instance.local.players[j].human.disableInput = false;
					NetChat.Print("已还原所有分身控制!");
				}
				return;
			}
			for (int k = NetGame.instance.local.players.Count - 1; k >= 0; k--)
			{
				NetGame.instance.local.players[k].human.disableInput = true;
				NetChat.Print("已取消所有分身控制!");
			}
			return;
		}
		else
		{
			string[] array = txt.Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length != 1)
			{
				NetChat.Print("格式错误!");
				return;
			}
			int num = 0;
			if (!int.TryParse(array[0], out num))
			{
				NetChat.Print("格式错误!");
				return;
			}
			if (num < 0 || num > NetGame.instance.local.players.Count)
			{
				NetChat.Print("错误玩家编号!当前玩家总数" + NetGame.instance.local.players.Count.ToString());
				return;
			}
			num--;
			if (NetGame.instance.local.players[num].human.disableInput)
			{
				NetGame.instance.local.players[num].human.disableInput = false;
				NetChat.Print(string.Format("{0}号玩家已恢复控制!", num + 1));
				return;
			}
			NetGame.instance.local.players[num].human.disableInput = true;
			NetChat.Print(string.Format("{0}号玩家取消控制!", num + 1));
			return;
		}
	}
		private void Change_The_Lens(string txt)
	{
		if (!string.IsNullOrEmpty(txt))
		{
			string[] array = txt.Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 1)
			{
				int num = 0;
				if (int.TryParse(array[0], out num))
				{
					if (num <= 0 || num > NetGame.instance.local.players.Count)
					{
						MonoBehaviour.print("玩家编号错误!当前本地玩家总数" + NetGame.instance.local.players.Count.ToString());
						return;
					}
					if (NetGame.instance.local.players.Count >= num)
					{
						MenuCameraEffects.instance.AddHuman(NetGame.instance.local.players[num - 1]);
						NetGame.instance.local.players[num - 1].human.disableInput = false;
						MonoBehaviour.print("视角已变更!");
						this.FangJianTiShi(string.Format("玩家{0}发动秘技:转移主体,转移目标{1}号", NetGame.instance.local.name, num));
					}
				}
			}
		}
	}
}