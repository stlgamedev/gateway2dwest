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
	[Category("Damage Dealer Test")]
	internal class DamageDealerTest 
	{
		private UnityScriptTestHelper testHelper = new UnityScriptTestHelper();
		
		public DamageDealer testObject;
		
		[SetUp]
		public void Setup() {

			testObject = testHelper.InstantiateScript<DamageDealer>();
		}
		
		[TearDown]
		public void TearDown() {
			testHelper.CleanUp ();
		}
		
		[Test]
		[Category("Player Movement")]
		public void OnCollision ()
		{
			//Arrange
			var collider2D = testHelper.InstantiateScript<BoxCollider2D> ();
			var playerStatus = collider2D.gameObject.AddComponent<PlayerStatusStub> ();
			var playerMovement = collider2D.gameObject.AddComponent<PlayerMovementStub> ();
			var enemyPosition = new Vector2 (10, 10);
			collider2D.transform.position = enemyPosition;
			var damageToDeal = 20.0f;
			testObject.damageToDeal = damageToDeal;

			var yourPosition = new Vector2(3,5);
			testObject.transform.position = yourPosition;

			//Act
			testHelper.OnTriggerEnter2D (testObject, collider2D);

			//Assert
			var expectedKnockbackDir = (enemyPosition - yourPosition).normalized;
			Assert.AreEqual (expectedKnockbackDir, playerMovement.KnockedBackDirection);
			Assert.AreEqual (damageToDeal, playerStatus.DamageDelt);
		}

		public class PlayerStatusStub:PlayerStatus
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
