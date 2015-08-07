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

		public T InstantiateScript<T>() where T : MonoBehaviour
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
	}

}
