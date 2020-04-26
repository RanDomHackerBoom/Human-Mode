using System;
using System.Collections.Generic;
using Guiaaaaaa;
using HumanAPI;
using Multiplayer;
using UnityEngine;

// Token: 0x0200034C RID: 844
public class Human : HumanBase
{
	// Token: 0x06000EF5 RID: 3829 RVA: 0x0008C574 File Offset: 0x0008A774
	public Human()
	{
		this.mod_vo = new Human_Mod_GraduateSchool_vo();
		this.killerGameVo = new KillerGameVo();
	}
	public KillerGameVo killerGameVo;

	// Token: 0x04000ECB RID: 3787
	public Human_Mod_GraduateSchool_vo mod_vo;
}