//This is a UI framework.
//Institute Ghost Provided.
//Only code added/modified by the mod is here.
namespace Human_Function
{
	public class UI_Test : MonoBehaviour
	{

		public UI_Test()
		{
			this.isTest = "关";
			this.isTest2 = "关";
		}

	
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Home))
			{
				this.isHome = !this.isHome;
				this.isBang = false;
			}
		}

	
		private void OnGUI()
		{
			GUI.BeginGroup(new Rect(0f, 0f, this.x, this.y));
			GUI.Box(new Rect(0f, 0f, this.x, this.y), "功能菜单");
			if (GUI.Button(new Rect(10f, 30f, 100f, 30f), "打开帮助"))
			{
				this.isBang = true;
				this.isHome = false;
			}
			if (GUI.Button(new Rect(10f, 70f, 100f, 30f), "中途加入:" + this.isTest))
			{
				if (this.isTest == "关")
				{
					this.isTest = "开";
				}
				else
				{
					this.isTest = "关";
				}
			}
			if (GUI.Button(new Rect(10f, 110f, 100f, 30f), "仅限邀请:" + this.isTest2))
			{
				if (this.isTest2 == "关")
				{
					this.isTest2 = "开";
				}
				else
				{
					this.isTest2 = "关";
				}
			}
			if (GUI.Button(new Rect(10f, 150f, 50f, 30f), "+"))
			{
				this.num++;
			}
			if (GUI.Button(new Rect(180f, 150f, 50f, 30f), "-"))
			{
				if (this.num <= 0)
				{
					return;
				}
				this.num--;
			}
			GUI.Label(new Rect(70f, 150f, 100f, 30f), "当前房间人数：" + this.num.ToString());
			GUI.EndGroup();
			GUI.BeginGroup(new Rect(0f, 0f, this.bang_x, this.bang_y));
			GUI.Box(new Rect(0f, 0f, this.bang_x, this.bang_y), "<size=20>使用说明</size>");
			GUI.color = new Color(178f, 34f, 34f);
			GUI.Label(new Rect(0f, 30f, 300f, 300f), "1./shan+编号:开启编号玩家闪现功能被指定玩家输入pau暂停/恢复 闪现 (已删除)\r\n2./n+数值:修改房间人数上限\r\n3./dy 按键 取点文字————定义在他人房间的取点快捷键(一位参数:单键Q.两位参数:单键,三位参数:组合键,请按顺序输入)\r\n");
			if (GUI.Button(new Rect(10f, this.bang_y - 40f, 100f, 30f), "关闭帮助"))
			{
				this.isBang = false;
				this.isHome = true;
			}
			GUI.EndGroup();
			if (this.isHome)
			{
				this.x = 500f;
				this.y = 500f;
			}
			else
			{
				this.x = 0f;
				this.y = 0f;
			}
			if (this.isBang)
			{
				this.bang_x = 500f;
				this.bang_y = 500f;
				return;
			}
			this.bang_x = 0f;
			this.bang_y = 0f;
		}

		private bool isHome;

		private float x;

		private float y;

		private bool isBang;

		private float bang_x;

		private float bang_y;

		private string isTest;

		private int num;
		
		private string isTest2;
	}
}
