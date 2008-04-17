/*
 * Created by SharpDevelop.
 * User: nathanl
 * Date: 4/17/2008
 * Time: 11:18 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using Axiom;
using Axiom.Core;
using Axiom.Math;

namespace ExampleAxiomApplication
{
	public class Program
	{
		public static void Main(string[] args)
		{
			AxiomTutorial app = new AxiomTutorial();
			app.Run();
		}
	}
	
	public class AxiomTutorial : ExampleApplication
	{
		private List<RobotEntity> _robotEntityCollection;
		
		protected override void CreateScene()
		{
			_robotEntityCollection = new List<RobotEntity>();
			
			SceneManager.AmbientLight = new ColorEx(1, 0.8f, 0);
			
			_robotEntityCollection.Add(new RobotEntity(SceneManager.RootSceneNode, SceneManager, new Vector3(-100, 0, 0)));
			_robotEntityCollection[0].Node.Translate(new Vector3(25, 0, 0));
			_robotEntityCollection[0].Node.Yaw(-90);

			_robotEntityCollection.Add(new RobotEntity(_robotEntityCollection[(_robotEntityCollection.Count - 1)].Node, SceneManager));
			_robotEntityCollection[1].Node.Translate(new Vector3(10, 0, 10));
			_robotEntityCollection[1].Node.Pitch(-90);

			_robotEntityCollection.Add(new RobotEntity(_robotEntityCollection[(_robotEntityCollection.Count - 1)].Node, SceneManager, new Vector3(100, 0, 0)));
			_robotEntityCollection[2].Node.Roll(-90);

			/*
			 * The above code does about the same thing as the below code.
			
			Entity robot1 = SceneManager.CreateEntity("Robot1", "robot.mesh");
			
			SceneNode robotNode1 = SceneManager.RootSceneNode.CreateChildSceneNode("RobotNode1");
			robotNode1.AttachObject(robot1);
			
			Entity robot2 = SceneManager.CreateEntity("Robot2", "robot.mesh");
			
			SceneNode robotNode2 = robotNode1.CreateChildSceneNode("RobotNode2", new Vector3(50, 0, 0));
			robotNode2.AttachObject(robot2);
			
			robotNode2.Translate(new Vector3(10, 0, 10));
			robotNode1.Translate(new Vector3(25, 0, 0));
			*/
			
		}
		
		private class RobotEntity
		{
			private static int __currentId = 0;
			
			private readonly string _meshName = "robot.mesh";
			private readonly string _rootName = "Robot";
			private readonly string _rootNodeName = "RobotNode";
			
			private SceneManager _parentSceneManager;
			private SceneNode _parentNode;
			private SceneNode _node;
			private Entity _entity;
			private string _nodeName;
			private string _name;
			
			public RobotEntity(SceneNode parentNode, SceneManager parentSceneManager)
			{
				_parentSceneManager = parentSceneManager;
				_parentNode = parentNode;
				_nodeName = _rootNodeName + RobotEntity.__currentId;
				_name = _rootName + RobotEntity.__currentId;
				
				RobotEntity.IncrementCurrentId();
				InitializeNode();
			}
			
			public RobotEntity(SceneNode parentNode, SceneManager parentSceneManager, Vector3 vector)
			{
				_parentSceneManager = parentSceneManager;
				_parentNode = parentNode;
				_nodeName = _rootNodeName + RobotEntity.__currentId;
				_name = _rootName + RobotEntity.__currentId;
				
				RobotEntity.IncrementCurrentId();
				InitializeNode();
				
				this.Node.Translate(vector);
			}
			
			public SceneManager ParentSceneManager
			{
				get { return _parentSceneManager; }
			}
			
			public SceneNode ParentNode
			{
				get { return _parentNode; }
			}
			
			public SceneNode Node
			{
				get { return _node; }
			}
			
			public int CurrentId
			{
				get { return __currentId; }
			}
			
			public string NodeName
			{
				get { return _nodeName; }
			}
			
			public string Name
			{
				get { return _name; }
			}			
			
			private void InitializeNode()
			{				
				_entity = _parentSceneManager.CreateEntity(_name, _meshName);
				
				_node = _parentNode.CreateChildSceneNode(_nodeName);
				_node.AttachObject(_entity);
			}
			
			private static void IncrementCurrentId()
			{
				RobotEntity.__currentId++;
			}
		}
	}
}