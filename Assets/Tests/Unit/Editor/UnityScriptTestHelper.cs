using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using System.Reflection;

namespace UnityTest
{
	class UnityScriptTestHelper
	{
		private ScriptInstantiator si = new ScriptInstantiator();

		public T InstantiateScript<T>() where T : Behaviour
		{
			return si.InstantiateScript<T>();
		}

		public void Start (MonoBehaviour behavior)
		{
			RunInstanceMethod (behavior.GetType (), "Start", behavior, null);
		}

		public void Update(MonoBehaviour behavior) 
		{
			RunInstanceMethod (behavior.GetType(), "Update", behavior, null);
		}

		public void FixedUpdate (MonoBehaviour behavior)
		{
			RunInstanceMethod (behavior.GetType(), "FixedUpdate", behavior, null);
		}

		public void OnTriggerEnter2D (MonoBehaviour behavior, Component collider2D)
		{
			RunInstanceMethod (behavior.GetType (), "OnTriggerEnter2D", behavior, new object[] {collider2D});
		}
		
		public void OnCollisionStay2D (MonoBehaviour behavior, Collider2D objectCollidingWith)
		{
			var collision = createCollisionWithGameObject (objectCollidingWith);
			RunInstanceMethod (behavior.GetType (), "OnCollisionStay2D", behavior, new object[] {collision});
		}

		public void OnTriggerStay2D (MonoBehaviour behavior, Component collider2D)
		{
			RunInstanceMethod (behavior.GetType (), "OnTriggerStay2D", behavior, new object[] {collider2D});
		}

		public void OnCollisionEnter2D (MonoBehaviour behavior, Collider2D objectCollidingWith)
		{
			var collision = createCollisionWithGameObject (objectCollidingWith);
			RunInstanceMethod (behavior.GetType (), "OnCollisionEnter2D", behavior, new object[] {collision});
		}
		
		public void CleanUp ()
		{
			si.CleanUp ();
		}
		
		public static object RunInstanceMethod(System.Type t, string strMethod, 
		                                       object objInstance, object [] aobjParams) 
		{
			BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public | 
				BindingFlags.NonPublic;
			return RunMethod(t, strMethod, 
			                 objInstance, aobjParams, eFlags);
		} //end of method
		
		private static object RunMethod(System.Type t, string 
		                                strMethod, object objInstance, object [] aobjParams, BindingFlags eFlags) 
		{
			MethodInfo m;
			try 
			{
				m = t.GetMethod(strMethod, eFlags);
				if (m == null)
				{
					throw new ArgumentException("There is no method '" + 
					                            strMethod + "' for type '" + t.ToString() + "'.");
				}
				
				object objRet = m.Invoke(objInstance, aobjParams);
				return objRet;
			}
			catch
			{
				throw;
			}
		} //end of method

		public static object RunStaticMethod(System.Type t, string strMethod, 
		                                     object [] aobjParams) 
		{
			BindingFlags eFlags = 
				BindingFlags.Static | BindingFlags.Public | 
					BindingFlags.NonPublic;
			return RunMethod(t, strMethod, 
			                 null, aobjParams, eFlags);
		} //end of method

		private static Collision2D createCollisionWithGameObject (Collider2D objectCollidingWith)
		{
			var collision = new Collision2D ();
			var prop = collision.GetType ().GetField ("m_Collider", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			prop.SetValue (collision, objectCollidingWith);
			return collision;
		}
	}

}
