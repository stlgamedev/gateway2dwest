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

		
		[Test]
		[Category("Player Movement")]
		public void PlayerMovesAtNormalizedSpeedWhenMovingDiagonal ()
		{
			//Arrange
			inputHelper.mockAxisRaw = new Vector2 (1, 1);
			inputHelper.customDeltaTime = 1.0f;
			//Act
			testHelper.Update(testObject);
			
			//Assert
			Vector2 expectedResult = new Vector2 (1, 1);
			expectedResult.Normalize ();
			expectedResult = expectedResult * testObject.movementSpeed;

			Assert.AreEqual (new Vector3(expectedResult.x, expectedResult.y, 0), testObject.transform.position);
		}
		
		[Test]
		[Category("Player Movement")]
		public void PlayerMovesAtAnalogSpeedWhenMovementIsInAnalog ()
		{
			//Arrange
			inputHelper.mockAxisRaw = new Vector2 (0.5f, 0.5f);
			inputHelper.customDeltaTime = 1.0f;
			//Act
			testHelper.Update(testObject);
			
			//Assert
			Vector2 expectedResult = new Vector2 (0.5f, 0.5f);
			expectedResult = expectedResult * testObject.movementSpeed;
			
			Assert.AreEqual (new Vector3(expectedResult.x, expectedResult.y, 0), testObject.transform.position);
		}

	}
}
