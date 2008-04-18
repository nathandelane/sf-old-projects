using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectDraw;

namespace Uniprogamer
{
	public class Program : Form
	{
		private Device _device;
		private Surface _primarySurface;
		private Surface _secondarySurface;
		private Clipper _clip;

		public Program()
			: base()
		{
			InitializeComponent();
			Show();

			_device = new Device();
			_device.SetCooperativeLevel(this, CooperativeLevelFlags.FullscreenExclusive);

			InitGame();
			StartGameLoop();
		}

		private void StartGameLoop()
		{
			do
			{
				if (!this.Focused)
				{
					Application.DoEvents();
				}
				else
				{
					ShowSurface();
				}

				Application.DoEvents();
			}
			while (this.Created);

			StartEnding();
		}

		private void StartEnding()
		{
			_device.Dispose();
			_primarySurface.Dispose();
			_secondarySurface.Dispose();
		}

		private void ShowSurface()
		{
			_secondarySurface.ColorFill(Color.White);
			_primarySurface.Flip(_secondarySurface, FlipFlags.Wait);
		}

		private void InitGame()
		{
			SurfaceDescription surfaceDescription = new SurfaceDescription();

			_clip = new Clipper(_device);
			_clip.Window = this;

			surfaceDescription.SurfaceCaps.PrimarySurface = true;
			surfaceDescription.SurfaceCaps.Flip = true;
			surfaceDescription.SurfaceCaps.Complex = true;
			surfaceDescription.BackBufferCount = 1;

			_primarySurface = new Surface(surfaceDescription, _device);
			_primarySurface.Clipper = _clip;

			surfaceDescription.Clear();
			surfaceDescription.SurfaceCaps.BackBuffer = true;

			_secondarySurface = _primarySurface.GetAttachedSurface(surfaceDescription.SurfaceCaps);

			SetEventListeners();
		}

		private void SetEventListeners()
		{
			this.KeyDown += new KeyEventHandler(HandleKeyDown);
		}

		private void InitializeComponent()
		{
			//throw new Exception("The method or operation is not implemented.");
		}

		private void HandleKeyDown(Object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				StartEnding();
				Application.Exit();
			}
		}

		public static void Main(string[] args)
		{
			Program game = new Program();
			Application.Exit();
		}
	}
}
