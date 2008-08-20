using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace Nathandelane.Game
{
	class Program
	{
		static void Main(string[] args)
		{
			using (GameForm gameFrame = new GameForm())
			{
				Application.Run(gameFrame);
			}
		}
	}
}
