﻿using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;
using System.Reflection;

namespace UnityTest
{
	
	[TestFixture]
	[Category("Damage Dealer Test")]
	internal class DamageDealerTest 
	{
		private UnityScriptTestHelper testHelper = new UnityScriptTestHelper();
		
		public DamageDealer testObject;

		BoxCollider2D collider2D;
		PlayerStatusStub playerStatus;
		PlayerMovementStub playerMovement;
		
		[SetUp]
		public void Setup() {

			testObject = testHelper.InstantiateScript<DamageDealer>();
			collider2D = testHelper.InstantiateScript<BoxCollider2D> ();
			playerStatus = collider2D.gameObject.AddComponent<PlayerStatusStub> ();
			playerMovement = collider2D.gameObject.AddComponent<PlayerMovementStub> ();
		}
		
		[TearDown]
		public void TearDown() {
			testHelper.CleanUp ();
		}
		
		[Test]
		[Category("Player Movement")]
		public void OnTriggerEnter2D ()
		{
			var enemyPosition = new Vector3 (10, 10, 0);
			collider2D.transform.position = enemyPosition;
			var damageToDeal = 20.0f;
			testObject.damageToDeal = damageToDeal;
			var yourPosition = new Vector3(3, 5, 0);
			testObject.transform.position = yourPosition;

			//Act
			testHelper.OnTriggerEnter2D (testObject, collider2D);

			//Assert
			var expectedKnockbackDir = (enemyPosition - yourPosition);
			var expectedKnockbackDir2D = new Vector2 (expectedKnockbackDir.x, expectedKnockbackDir.y);
			expectedKnockbackDir2D.Normalize ();
			Assert.AreEqual (expectedKnockbackDir2D, playerMovement.KnockedBackDirection);
			Assert.AreEqual (damageToDeal, playerStatus.DamageDelt);
		}

		[Test]
		[Category("Player Movement")]
		public void OnCollisionStay2D ()
		{
			var enemyPosition = new Vector3 (10, 10, 0);
			collider2D.transform.position = enemyPosition;
			var damageToDeal = 20.0f;
			testObject.damageToDeal = damageToDeal;
			var yourPosition = new Vector3(9, 9, 0);
			testObject.transform.position = yourPosition;

			//Act
			testHelper.OnCollisionStay2D (testObject, collider2D);
			
			//Assert
			var expectedKnockbackDir = (enemyPosition - yourPosition);
			var expectedKnockbackDir2D = new Vector2 (expectedKnockbackDir.x, expectedKnockbackDir.y);
			expectedKnockbackDir2D.Normalize ();
			Assert.AreEqual (expectedKnockbackDir2D, playerMovement.KnockedBackDirection);
			Assert.AreEqual (damageToDeal, playerStatus.DamageDelt);
		}

		[Test]
		[Category("Player Movement")]
		public void OnTriggerStay2D ()
		{
			var enemyPosition = new Vector3 (10, 10, 0);
			collider2D.transform.position = enemyPosition;
			var damageToDeal = 20.0f;
			testObject.damageToDeal = damageToDeal;
			var yourPosition = new Vector3(9,10, 0);
			testObject.transform.position = yourPosition;
			
			//Act
			testHelper.OnTriggerStay2D (testObject, collider2D);
			
			//Assert
			var expectedKnockbackDir = (enemyPosition - yourPosition);
			var expectedKnockbackDir2D = new Vector2 (expectedKnockbackDir.x, expectedKnockbackDir.y);
			expectedKnockbackDir2D.Normalize ();
			Assert.AreEqual (expectedKnockbackDir2D, playerMovement.KnockedBackDirection);
			Assert.AreEqual (damageToDeal, playerStatus.DamageDelt);
		}
		[Test]
		[Category("Player Movement")]
		public void OnCollisionEnter2D ()
		{
			var enemyPosition = new Vector3 (10, 10, 0);
			collider2D.transform.position = enemyPosition;
			var damageToDeal = 20.0f;
			testObject.damageToDeal = damageToDeal;
			var yourPosition = new Vector3(10,11, 0);
			testObject.transform.position = yourPosition;
			
			//Act
			testHelper.OnCollisionEnter2D (testObject, collider2D);
			
			//Assert
			var expectedKnockbackDir = (enemyPosition - yourPosition);
			var expectedKnockbackDir2D = new Vector2 (expectedKnockbackDir.x, expectedKnockbackDir.y);
			expectedKnockbackDir2D.Normalize ();
			Assert.AreEqual (expectedKnockbackDir2D, playerMovement.KnockedBackDirection);
			Assert.AreEqual (damageToDeal, playerStatus.DamageDelt);
		}


		public class PlayerStatusStub:Status
		{
			public float DamageDelt { get; private set;}

			public override void TakeDamage (float damageToDeal)
			{
				DamageDelt = damageToDeal;
			}
		}

		public class PlayerMovementStub:PlayerMovement
		{
			public Vector2 KnockedBackDirection { get; private set;}

			public override void KnockBack (Vector2 direction)
			{
				KnockedBackDirection = direction;
			}
		}
	}
}
