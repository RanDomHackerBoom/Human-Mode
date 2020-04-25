//File path: Assembly-CSharp.dll/-/game.cs
//Only code added/modified by the mod is here.
public class Game : MonoBehaviour, IGame, IDependency
{
	public void Initialize()
	{
		base.gameObject.AddComponent<UI_Test>();
	}
}