using System;
using System.Collections.Generic;
using Multiplayer;
using UnityEngine;

namespace Guiaaaaaa
{
	// Token: 0x02000799 RID: 1945
	public class KillerGame
	{
		// Token: 0x06002BFF RID: 11263 RVA: 0x0012E334 File Offset: 0x0012C534
		public KillerGame()
		{
			this.time = 60f;
			this.tpTime = 15f;
			this._restTime = 15f;
			this.speak_over = false;
			this.speak_start = false;
			this.playerList = new List<NetPlayer>();
			this.peopleList = new List<Human>();
			for (int i = 0; i < NetGame.instance.players.Count; i++)
			{
				NetGame.instance.players[i].human.killerGameVo = new KillerGameVo();
				NetGame.instance.players[i].human.killerGameVo.issurvive = true;
				this.playerList.Add(NetGame.instance.players[i]);
				this.playerList[i].human.killerGameVo.id = i + 1;
				this.playerList[i].human.killerGameVo.fyTime = 30f;
				this.playerList[i].human.killerGameVo.thisturn_vote = true;
			}
			this.surviveNum = this.playerList.Count;
			int num = Random.Range(0, this.playerList.Count);
			this.killer = this.playerList[num];
			this.playerList[num].human.killerGameVo.identity = Identity.凶手;
			this.Msg_Pertinence(this.playerList[num].host, "<size=25>你的身份是 凶手</size>", "");
			for (int j = 0; j < this.playerList.Count; j++)
			{
				if (j != num)
				{
					this.peopleList.Add(this.playerList[j].human);
					this.playerList[j].human.killerGameVo.identity = Identity.好人;
					this.Msg_Pertinence(this.playerList[j].host, "<size=25>你的身份是 好人</size>", "");
				}
			}
			this.CkPlayer();
			this.ready = true;
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x0012E554 File Offset: 0x0012C754
		public void Msg_Pertinence(NetHost netHost, string name, string msg)
		{
			if (netHost == NetGame.instance.local)
			{
				NetChat.Print(name);
				return;
			}
			NetStream netStream = NetGame.BeginMessage(NetMsgId.Chat);
			try
			{
				netStream.WriteNetId(netHost.hostId);
				netStream.Write(name);
				netStream.Write(msg);
				if (NetGame.isServer)
				{
					NetGame.instance.SendReliable(netHost, netStream);
				}
			}
			finally
			{
				if (netStream != null)
				{
					netStream = netStream.Release();
				}
			}
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x0012E5C8 File Offset: 0x0012C7C8
		public void RandomKillerTask(Human human)
		{
			switch (Random.Range(0, 5))
			{
			case 1:
				human.killerGameVo.killerTask = "由于你长期闭关苦练，你终于练成了吸星大法,可以摄人魂魄";
				human.killerGameVo.TaskID = 1;
				return;
			case 2:
				human.killerGameVo.killerTask = "你成为杀手前,你出生于少林寺,擅长铁头功,但目标也不是吃素的,需找到其死穴";
				human.killerGameVo.TaskID = 2;
				return;
			case 3:
				human.killerGameVo.killerTask = "你的目标据说有金钟罩护身,你需要击中他的命门,破掉他的罩子才行";
				human.killerGameVo.TaskID = 3;
				return;
			case 4:
				human.killerGameVo.killerTask = "你的目标一身横练功夫,无比豪横，但据不知道哪来的消息,早年他练麒麟臂的落下暗病，这可能是他的死穴";
				human.killerGameVo.TaskID = 4;
				return;
			default:
				return;
			}
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x0012E66C File Offset: 0x0012C86C
		public void KillerPerform(Human human)
		{
			switch (human.killerGameVo.TaskID)
			{
			case 1:
				this.TouchTask(this.killerTarget.ragdoll.partHead, human);
				return;
			case 2:
				this.TouchTask(this.killerTarget.ragdoll.partLeftForearm, human);
				return;
			case 3:
				this.TouchTask(this.killerTarget.ragdoll.partLeftHand, human);
				return;
			case 4:
				this.TouchTask(this.killerTarget.ragdoll.partRightArm, human);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x0012E700 File Offset: 0x0012C900
		public void RestTime()
		{
			if (this.time < 60f)
			{
				return;
			}
			for (int i = 0; i < this.playerList.Count; i++)
			{
				if (this.playerList[i].human.killerGameVo.issurvive)
				{
					this.playerList[i].human.disableInput = true;
				}
				if (this.playerList[i].human.killerGameVo.identity == Identity.凶手 && this.playerList[i].human.killerGameVo.issurvive)
				{
					this.Msg_Pertinence(this.playerList[i].host, "<size=25>输入kill 编号 选择击杀目标 </size>", "");
				}
			}
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x0012E7C8 File Offset: 0x0012C9C8
		public void KillerTarget(int number)
		{
			this.killerTarget = this.playerList[number - 1].human;
			this.Msg_Pertinence(this.killer.host, "已确定击杀目标为" + this.killerTarget.player.host.name, "");
			this.RandomKillerTask(this.killer.human);
			this.Msg_Pertinence(this.killer.host, "<size=25>你的任务是" + this.killer.human.killerGameVo.killerTask + "</size>", "");
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x0012E870 File Offset: 0x0012CA70
		public void CkPlayer()
		{
			for (int i = 0; i < this.playerList.Count; i++)
			{
				string arg;
				if (this.playerList[i].human.killerGameVo.issurvive)
				{
					arg = "存活";
				}
				else
				{
					arg = "倒地";
				}
				Human_Mod_GraduateSchool.getins().Custom_Message(0u, string.Format("编号:{0}  名字:{1}   是否存活:{2}", this.playerList[i].human.killerGameVo.id, this.playerList[i].human.player.host.name, arg), "");
			}
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x0012E920 File Offset: 0x0012CB20
		public void TouchTask(HumanSegment mem, Human human)
		{
			if ((double)(this.killer.human.ragdoll.partLeftHand.transform.position - mem.transform.position).sqrMagnitude < 0.2 || (double)(this.killer.human.ragdoll.partRightHand.transform.position - mem.transform.position).sqrMagnitude < 0.2)
			{
				human.killerGameVo.taskIsYes = true;
				this.peopleList.Remove(this.killerTarget);
				this.Msg_Pertinence(this.killer.host, "<size=25>任务完成</size>", "");
			}
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x0012E9EC File Offset: 0x0012CBEC
		public void PeopleTaskAchieve()
		{
			int index = Random.Range(0, this.peopleList.Count);
			Human human = this.peopleList[index];
			Human_Mod_GraduateSchool.getins().Custom_Message(0u, "任务完成:" + human.player.host.name + "不是凶手", "");
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x0012EA48 File Offset: 0x0012CC48
		public void PeoplePerform()
		{
			switch (this.peopleTask_id)
			{
			case 1:
				for (int i = 0; i < this.playerList.Count; i++)
				{
					if (this.playerList[i].human.killerGameVo.id != this.peopleTask_Target.killerGameVo.id && ((double)(this.playerList[i].human.ragdoll.partLeftHand.transform.position - this.peopleTask_Target.ragdoll.partHead.transform.position).sqrMagnitude < 0.2 || (double)(this.playerList[i].human.ragdoll.partRightHand.transform.position - this.peopleTask_Target.ragdoll.partHead.transform.position).sqrMagnitude < 0.2))
					{
						this.peopleTask_Yes = true;
					}
				}
				return;
			case 2:
				for (int j = 0; j < this.playerList.Count; j++)
				{
					if (this.playerList[j].human.killerGameVo.id != this.peopleTask_Target.killerGameVo.id && ((double)(this.playerList[j].human.ragdoll.partLeftHand.transform.position - this.peopleTask_Target.ragdoll.partHead.transform.position).sqrMagnitude < 0.2 || (double)(this.playerList[j].human.ragdoll.partRightHand.transform.position - this.peopleTask_Target.ragdoll.partHead.transform.position).sqrMagnitude < 0.2))
					{
						this.peopleTask_Yes = true;
					}
				}
				return;
			case 3:
				for (int k = 0; k < this.playerList.Count; k++)
				{
					if (this.playerList[k].human.killerGameVo.id != this.peopleTask_Target.killerGameVo.id && ((double)(this.playerList[k].human.ragdoll.partLeftHand.transform.position - this.peopleTask_Target.ragdoll.partHead.transform.position).sqrMagnitude < 0.2 || (double)(this.playerList[k].human.ragdoll.partRightHand.transform.position - this.peopleTask_Target.ragdoll.partHead.transform.position).sqrMagnitude < 0.2))
					{
						this.peopleTask_Yes = true;
					}
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x0012ED84 File Offset: 0x0012CF84
		public void RandomPeopleTask()
		{
			switch (Random.Range(0, 4))
			{
			case 1:
			{
				this.peopleTask = "天机老人:魔道猖獗,天下将乱,你们之中有天命之人,请替我找到他,必有重谢,据说他头上有个胎记,切记你们只有两次机会";
				int index = Random.Range(0, this.playerList.Count);
				this.peopleTask_Target = this.playerList[index].human;
				this.peopleTask_id = 1;
				return;
			}
			case 2:
			{
				this.peopleTask = "天机老人:魔道猖獗,天下将乱,你们之中有天命之人,请替我找到他,必有重谢,据说他头上有个胎记,切记你们只有两次机会";
				int index2 = Random.Range(0, this.playerList.Count);
				this.peopleTask_Target = this.playerList[index2].human;
				this.peopleTask_id = 2;
				return;
			}
			case 3:
			{
				this.peopleTask = "天机老人:魔道猖獗,天下将乱,你们之中有天命之人,请替我找到他,必有重谢,据说他头上有个胎记,切记你们只有两次机会";
				int index3 = Random.Range(0, this.playerList.Count);
				this.peopleTask_Target = this.playerList[index3].human;
				this.peopleTask_id = 3;
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x04002A9F RID: 10911
		public float time;

		// Token: 0x04002AA0 RID: 10912
		public List<NetPlayer> playerList;

		// Token: 0x04002AA1 RID: 10913
		public bool ready;

		// Token: 0x04002AA2 RID: 10914
		public float tpTime;

		// Token: 0x04002AA3 RID: 10915
		public int surviveNum;

		// Token: 0x04002AA4 RID: 10916
		public float _restTime;

		// Token: 0x04002AA5 RID: 10917
		public Human killerTarget;

		// Token: 0x04002AA6 RID: 10918
		public NetPlayer killer;

		// Token: 0x04002AA7 RID: 10919
		public bool speak_over;

		// Token: 0x04002AA8 RID: 10920
		public bool speak_start;

		// Token: 0x04002AA9 RID: 10921
		public bool peopleTask_Yes;

		// Token: 0x04002AAA RID: 10922
		public Human peopleTask_Target;

		// Token: 0x04002AAB RID: 10923
		private List<Human> peopleList;

		// Token: 0x04002AAC RID: 10924
		public int peopleTask_id;

		// Token: 0x04002AAD RID: 10925
		public string peopleTask;
	}
}
