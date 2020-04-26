using System;
using UnityEngine;

namespace Guiaaaaaa
{
	
	public class Gui_mono : MonoBehaviour
	{
		
		public event Action preUpdate;

		
		private void Start()
		{
			Gui_mono.getins = this;
		}

		private void FixedUpdate()
		{
			if (this.preUpdate != null)
			{
				this.preUpdate();
			}
		}

		
		public static Gui_mono getins;
	}
}
