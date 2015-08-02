using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using System.Reflection;

namespace UnityTest
{

	[TestFixture]
	[Category("Player Movement Test")]
	internal class PlayerMovementTest
	{
		private UnityScriptTestHelper testHelper = new UnityScriptTestHelper();
		public MockInputHelper inputHelper;

		public PlayerMovement testObject;

		[SetUp]
		public void Setup() {
			testObject = testHelper.InstantiateScript<PlayerMovement>();
			inputHelper = testHelper.InstantiateScript<MockInputHelper>();
			testObject.inputHelper = inputHelper;
		}

		[TearDown]
		public void TearDown() {
			testHelper.CleanUp ();
		}

		[Test]
		[Category("Player Movement")]
		public void PlayerMovesRightWhenMovingRight ()
		{
			//Arrange
			inputHelper.mockAxisRaw = new Vector2 (1, 0);
			inputHelper.customDeltaTime = 1.0f;
			//Act
			testHelper.Update(testObject);

			//Assert
			Assert.AreEqual (new Vector3(testObject.movementSpeed, 0, 0), testObject.transform.position);
		}

	}
}
