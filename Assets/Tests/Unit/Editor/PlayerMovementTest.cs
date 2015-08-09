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
			testObject.gameObject.AddComponent<Rigidbody2D>();
			testObject.gameObject.AddComponent<Animator> ();

			testHelper.Start(testObject);
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
			//Act
			testHelper.Update(testObject);
			testHelper.FixedUpdate (testObject);

			//Assert
			var rb = testObject.GetComponent<Rigidbody2D> ();
			Assert.AreEqual (new Vector2(2, 0), rb.velocity);
		}

		
		[Test]
		[Category("Player Movement")]
		public void PlayerMovesAtNormalizedSpeedWhenMovingDiagonal ()
		{
			//Arrange
			inputHelper.mockAxisRaw = new Vector2 (1, 1);
			//Act
			testHelper.Update(testObject);
			testHelper.FixedUpdate(testObject);
			
			//Assert
			Vector2 expectedResult = new Vector2 (1, 1);
			expectedResult.Normalize ();
			expectedResult = expectedResult * testObject.movementSpeed;
			
			var rb = testObject.GetComponent<Rigidbody2D> ();
			Assert.AreEqual (expectedResult, rb.velocity);
		}
		
		[Test]
		[Category("Player Movement")]
		public void PlayerMovesAtAnalogSpeedWhenMovementIsInAnalog ()
		{
			//Arrange
			inputHelper.mockAxisRaw = new Vector2 (0.5f, 0.5f);
			//Act
			testHelper.Update(testObject);
			testHelper.FixedUpdate (testObject);
			
			//Assert
			Vector2 expectedResult = new Vector2 (0.5f, 0.5f);
			expectedResult = expectedResult * testObject.movementSpeed;
			
			var rb = testObject.GetComponent<Rigidbody2D> ();
			Assert.AreEqual (new Vector2(1, 1), rb.velocity);
		}

	}
}
