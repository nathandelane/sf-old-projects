using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics.OpenGL.Enums;

namespace Nathandelane.Otk
{
	public class GameWindow : OpenTK.GameWindow
	{
		public GameWindow(int width, int height)
			: base(width, height, new GraphicsMode(new ColorFormat(16)), "", GameWindowFlags.Fullscreen, DisplayDevice.Default)
		{
			
		}
	}
}
