using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using Direct3D = Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.DirectInput;
using DirectInput = Microsoft.DirectX.DirectInput;

namespace Nathandelane.Game
{
	public partial class GameForm : Form
	{
		private Direct3D.Device _graphicsDevice;
		private DirectInput.Device _inputDevice;
		private Direct3D.DeviceType _deviceType;
		private CustomVertex.PositionColored[] _vertices;
		private VertexBuffer _vertexBuffer;
		private IndexBuffer _indexBuffer;
		private float _angle;
		private int[] _indices;
		private int[,] _heightData;
		private int _width;
		private int _height;

		public GameForm()
		{
			_angle = 0f;
			_deviceType = Direct3D.DeviceType.Reference;//DeviceType.Hardware;
			_width = 64;
			_height = 64;

			InitializeForm();
			LoadHeightDataFromFile();
			InitializeDevice();
			CameraPositioning();
			HeightVertexDeclaration();
			HeightIndicesDeclaration();

			this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
		}

		private void LoadHeightDataFromFile()
		{
			_heightData = new int[_width, _height];

			using (FileStream fileStream = new FileStream("heightdata.raw", FileMode.Open, FileAccess.Read))
			{
				BinaryReader reader = new BinaryReader(fileStream);

				for (int y = 0; y < _height; y++)
				{
					for (int x = 0; x < _width; x++)
					{
						int pixelHeight = (int)(reader.ReadByte() / 50);
						_heightData[_width - 1 - x, _height - 1 - y] = pixelHeight;
					}
				}
			}
		}

		private void HeightIndicesDeclaration()
		{
			_indexBuffer = new IndexBuffer(typeof(int), (_width - 1) * (_height - 1) * 6, _graphicsDevice, Usage.WriteOnly, Pool.Default);
			_indices = new int[(_width - 1) * (_height - 1) * 6];

			for (int x = 0; x < _width - 1; x++)
			{
				for (int y = 0; y < _height - 1; y++)
				{
					_indices[(x + y * (_width - 1)) * 6] = (x + 1) + (y + 1) * _width;
					_indices[(x + y * (_width - 1)) * 6 + 1] = (x + 1) + y * _width;
					_indices[(x + y * (_width - 1)) * 6 + 2] = x + y * _width;

					_indices[(x + y * (_width - 1)) * 6 + 3] = (x + 1) + (y + 1) * _width;
					_indices[(x + y * (_width - 1)) * 6 + 4] = x + y * _width;
					_indices[(x + y * (_width - 1)) * 6 + 5] = x + (y + 1) * _width;
				}
			}

			_indexBuffer.SetData(_indices, 0, LockFlags.None);
		}

		private void HeightVertexDeclaration()
		{
			_vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored), _width * _height, _graphicsDevice, Usage.Dynamic | Usage.WriteOnly, CustomVertex.PositionColored.Format, Pool.Default);
			_vertices = new CustomVertex.PositionColored[_width * _height];

			for (int x = 0; x < _width; x++)
			{
				for (int y = 0; y < _height; y++)
				{
					_vertices[x + y * _width].Position = new Vector3(x, y, _heightData[x, y]);
					_vertices[x + y * _width].Color = Color.White.ToArgb();
				}
			}

			_vertexBuffer.SetData(_vertices, 0, LockFlags.None);
		}

		private void InitializeForm()
		{
			this.Size = new Size(640, 480);
			this.FormBorderStyle = FormBorderStyle.Fixed3D;
		}

		private void InitializeDevice()
		{
			PresentParameters presentationParameters = new PresentParameters();
			presentationParameters.Windowed = true;
			presentationParameters.SwapEffect = SwapEffect.Discard;

			_graphicsDevice = new Direct3D.Device(0, _deviceType, this, CreateFlags.SoftwareVertexProcessing, presentationParameters);
			_graphicsDevice.RenderState.FillMode = FillMode.WireFrame;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			// Lights definition
			_graphicsDevice.RenderState.Lighting = false;

			_graphicsDevice.Clear(ClearFlags.Target, Color.DarkSlateBlue, 1.0f, 0); // Clear the viewport

			_graphicsDevice.BeginScene(); // Beginning of scene rendering
			_graphicsDevice.VertexFormat = CustomVertex.PositionColored.Format;

			_graphicsDevice.SetStreamSource(0, _vertexBuffer, 0);
			_graphicsDevice.Indices = _indexBuffer;

			_graphicsDevice.Transform.World = Matrix.Translation(-_height / 2, -_width / 2, 0);
			_graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, _width * _height/*5*/, 0, _indices.Length / 3/*2*/);
			_graphicsDevice.EndScene(); // Ending of scene rendering

			_graphicsDevice.Present(); // Present or paint the viewport content

			this.Invalidate(); // Forget any automatic event validation calls

			_angle += 0.05f;
		}

		private void CameraPositioning()
		{
			_graphicsDevice.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, this.Width / this.Height, 1f, 150f);
			_graphicsDevice.Transform.View = Matrix.LookAtLH(new Vector3(0, -40, 50), new Vector3(0, -5, 0), new Vector3(0, 1, 0));
			_graphicsDevice.RenderState.Lighting = false;
			_graphicsDevice.RenderState.CullMode = Cull.None;
		}

	}
}