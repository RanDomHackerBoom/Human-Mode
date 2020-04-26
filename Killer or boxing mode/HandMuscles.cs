using System;
using HumanAPI;
using UnityEngine;

// Token: 0x02000350 RID: 848
[Serializable]
public class HandMuscles
{
public void OnFixedUpdate()
	{
		if (this.human.mod_vo.isHandstand)
		{
			this.human.ReleaseGrab(1f);
			this.maxForce = 1500f;
			this.maxPushForce = 500f;
		}
		else
		{
			this.maxForce = 300f;
			this.maxPushForce = 200f;
		}
		float num;
		float num2;
		float targetPitchAngle;
		float targetYawAngle;
		if (this.human.mod_vo.isBoxing)
		{
			this.human.ReleaseGrab(1f);
			num = this.human.controls.leftExtend * 5f;
			num2 = this.human.controls.rightExtend * 5f;
			targetPitchAngle = this.human.controls.targetPitchAngle;
			targetYawAngle = this.human.controls.targetYawAngle;
		}
		else
		{
			num = this.human.controls.leftExtend;
			num2 = this.human.controls.rightExtend;
			targetPitchAngle = this.human.controls.targetPitchAngle;
			targetYawAngle = this.human.controls.targetYawAngle;
		}
		bool grab = this.human.controls.leftGrab;
		bool grab2 = this.human.controls.rightGrab;
		bool onGround = this.human.onGround;
		if ((this.ragdoll.partLeftHand.transform.position - this.ragdoll.partChest.transform.position).sqrMagnitude > 6f)
		{
			grab = false;
		}
		if ((this.ragdoll.partRightHand.transform.position - this.ragdoll.partChest.transform.position).sqrMagnitude > 6f)
		{
			grab2 = false;
		}
		Quaternion quaternion = Quaternion.Euler(targetPitchAngle, targetYawAngle, 0f);
		Quaternion quaternion2 = Quaternion.Euler(0f, targetYawAngle, 0f);
		Vector3 worldPos = Vector3.zero;
		Vector3 worldPos2 = Vector3.zero;
		float num3 = 0f;
		float num4 = 0f;
		if (targetPitchAngle > 0f && onGround)
		{
			num4 = 0.4f * targetPitchAngle / 90f;
		}
		HandMuscles.TargetingMode targetingMode = (!(this.ragdoll.partLeftHand.sensor.grabJoint != null)) ? this.targetingMode : this.grabTargetingMode;
		HandMuscles.TargetingMode targetingMode2 = (!(this.ragdoll.partRightHand.sensor.grabJoint != null)) ? this.targetingMode : this.grabTargetingMode;
		switch (targetingMode)
		{
		case HandMuscles.TargetingMode.Shoulder:
			worldPos = this.ragdoll.partLeftArm.transform.position + quaternion * new Vector3(0f, 0f, num * this.ragdoll.handLength);
			break;
		case HandMuscles.TargetingMode.Chest:
			worldPos = this.ragdoll.partChest.transform.position + quaternion2 * new Vector3(-0.2f, 0.15f, 0f) + quaternion * new Vector3(0f, 0f, num * this.ragdoll.handLength);
			break;
		case HandMuscles.TargetingMode.Hips:
			if (targetPitchAngle > 0f)
			{
				num3 = -0.3f * targetPitchAngle / 90f;
			}
			worldPos = this.ragdoll.partHips.transform.position + quaternion2 * new Vector3(-0.2f, 0.65f + num3, num4) + quaternion * new Vector3(0f, 0f, num * this.ragdoll.handLength);
			break;
		case HandMuscles.TargetingMode.Ball:
			if (targetPitchAngle > 0f)
			{
				num3 = -0.2f * targetPitchAngle / 90f;
			}
			if (this.ragdoll.partLeftHand.sensor.grabJoint != null)
			{
				num4 = ((!this.human.isClimbing) ? 0f : -0.2f);
			}
			worldPos = this.ragdoll.partBall.transform.position + quaternion2 * new Vector3(-0.2f, 0.7f + num3, num4) + quaternion * new Vector3(0f, 0f, num * this.ragdoll.handLength);
			break;
		}
		switch (targetingMode2)
		{
		case HandMuscles.TargetingMode.Shoulder:
			worldPos2 = this.ragdoll.partRightArm.transform.position + quaternion * new Vector3(0f, 0f, num2 * this.ragdoll.handLength);
			break;
		case HandMuscles.TargetingMode.Chest:
			worldPos2 = this.ragdoll.partChest.transform.position + quaternion2 * new Vector3(0.2f, 0.15f, 0f) + quaternion * new Vector3(0f, 0f, num2 * this.ragdoll.handLength);
			break;
		case HandMuscles.TargetingMode.Hips:
			if (targetPitchAngle > 0f)
			{
				num3 = -0.3f * targetPitchAngle / 90f;
			}
			worldPos2 = this.ragdoll.partHips.transform.position + quaternion2 * new Vector3(0.2f, 0.65f + num3, num4) + quaternion * new Vector3(0f, 0f, num2 * this.ragdoll.handLength);
			break;
		case HandMuscles.TargetingMode.Ball:
			if (targetPitchAngle > 0f)
			{
				num3 = -0.2f * targetPitchAngle / 90f;
			}
			if (this.ragdoll.partRightHand.sensor.grabJoint != null)
			{
				num4 = ((!this.human.isClimbing) ? 0f : -0.2f);
			}
			worldPos2 = this.ragdoll.partBall.transform.position + quaternion2 * new Vector3(0.2f, 0.7f + num3, num4) + quaternion * new Vector3(0f, 0f, num2 * this.ragdoll.handLength);
			break;
		}
		this.ProcessHand(this.leftMem, this.ragdoll.partLeftArm, this.ragdoll.partLeftForearm, this.ragdoll.partLeftHand, worldPos, num, grab, this.motion.legs.legPhase + 0.5f, false);
		this.ProcessHand(this.rightMem, this.ragdoll.partRightArm, this.ragdoll.partRightForearm, this.ragdoll.partRightHand, worldPos2, num2, grab2, this.motion.legs.legPhase, true);
	}
	private void ProcessHand(HandMuscles.ScanMem mem, HumanSegment arm, HumanSegment forearm, HumanSegment hand, Vector3 worldPos, float extend, bool grab, float animationPhase, bool right)
	{
		double num = 0.1 + (double)(0.14f * Mathf.Abs(this.human.controls.targetPitchAngle - mem.grabAngle) / 80f);
		double num2 = num * 2.0;
		if (CheatCodes.climbCheat)
		{
			num = (num2 = num / 4.0);
		}
		if (grab && !hand.sensor.grab)
		{
			if ((double)mem.grabTime > num)
			{
				mem.pos = arm.transform.position;
			}
			else
			{
				grab = false;
			}
		}
		if (hand.sensor.grab && !grab)
		{
			mem.grabTime = 0f;
			mem.grabAngle = this.human.controls.targetPitchAngle;
		}
		else
		{
			mem.grabTime += Time.fixedDeltaTime;
		}
		hand.sensor.grab = ((double)mem.grabTime > num2 && grab);
		if (this.human.mod_vo.isBoxing && extend < 0.2f)
		{
			hand.sensor.targetPosition = worldPos;
			mem.shoulder = arm.transform.position;
			mem.hand = hand.transform.position;
			this.maxPushForce *= 3f;
			if (hand.sensor.grabJoint == null)
			{
				worldPos = this.FindTarget(mem, worldPos, out hand.sensor.grabFilter);
			}
			this.PlaceHand(arm, hand, worldPos, true, hand.sensor.grabJoint != null, hand.sensor.grabBody);
			if (hand.sensor.grabBody != null)
			{
				this.LiftBody(hand, hand.sensor.grabBody);
			}
			hand.sensor.grabPosition = worldPos;
			return;
		}
		if (extend > 0.2f)
		{
			hand.sensor.targetPosition = worldPos;
			mem.shoulder = arm.transform.position;
			mem.hand = hand.transform.position;
			if (hand.sensor.grabJoint == null)
			{
				worldPos = this.FindTarget(mem, worldPos, out hand.sensor.grabFilter);
			}
			this.PlaceHand(arm, hand, worldPos, true, hand.sensor.grabJoint != null, hand.sensor.grabBody);
			if (hand.sensor.grabBody != null)
			{
				this.LiftBody(hand, hand.sensor.grabBody);
			}
			hand.sensor.grabPosition = worldPos;
			return;
		}
		hand.sensor.grabFilter = null;
		if (this.human.state == HumanState.Walk)
		{
			this.AnimateHand(arm, forearm, hand, animationPhase, 1f, right);
			return;
		}
		if (this.human.state == HumanState.FreeFall)
		{
			Vector3 targetDirection = this.human.targetDirection;
			targetDirection.y = 0f;
			HumanMotion2.AlignToVector(arm, arm.transform.up, -targetDirection, 2f);
			HumanMotion2.AlignToVector(forearm, forearm.transform.up, targetDirection, 2f);
			return;
		}
		if (this.human.mod_vo.isBoxing)
		{
			return;
		}
		Vector3 targetDirection2 = this.human.targetDirection;
		targetDirection2.y = 0f;
		HumanMotion2.AlignToVector(arm, arm.transform.up, -targetDirection2, 20f);
		HumanMotion2.AlignToVector(forearm, forearm.transform.up, targetDirection2, 20f);
	}

}