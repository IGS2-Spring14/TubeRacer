using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eEndPointsMode { AUTO, AUTOCLOSED, EXPLICIT }
public enum eWrapMode { ONCE, LOOP }
public delegate void OnEndCallback();

public class SplineInterpolator : MonoBehaviour
{
	public bool StopShip = false;
	public bool ResetSpeed = false;
	public bool AllowChangeSpeed = true; 
	public bool IsStraightPath = false;
	public bool IsFlipped = false;
	public float SpeedUpValue = 250f;
	public float DefaultSpeed = 6.8f;
	public bool IsPlayer = true; 
	bool UserSet = false, boosting = false;
	eEndPointsMode mEndPointsMode = eEndPointsMode.AUTO;

	public float TimeScale = 1.0f;
	public float boostDecayRate = 1.5f;
	Quaternion TempRot; 
	
	public void SlowDown()
	{
		//Debug.Log ("I will speed you down to " + TimeScale);
		if(TimeScale-0.2f>1.0f)
			TimeScale = TimeScale - 0.2f;
	}
	public void SpeedUp()
	{
		//Debug.Log ("I will speed you up to " + TimeScale);
		if(TimeScale<6.0f)
			TimeScale = TimeScale + (TimeScale / 50f);
		//TimeScale = TimeScale + 0.2f;
	}
	public void SpeedUp(float speed)
	{
			TimeScale = TimeScale + speed;
	}
	public void SetSpeed(float Speed)
	{
		TimeScale = Speed;
		UserSet = true;
		}
	public void LockSpeedChange()
	{
				AllowChangeSpeed = false;
		}
	public void UnlockSpeedChange()
	{
				AllowChangeSpeed = true;
		}

	internal class SplineNode
	{
		internal Vector3 Point;
		internal Quaternion Rot;
		internal float Time;
		internal Vector2 EaseIO;

		internal SplineNode(Vector3 p, Quaternion q, float t, Vector2 io) { Point = p; Rot = q; Time = t; EaseIO = io; }
		internal SplineNode(SplineNode o) { Point = o.Point; Rot = o.Rot; Time = o.Time; EaseIO = o.EaseIO; }
	}

	List<SplineNode> mNodes = new List<SplineNode>();
	public string mState = "";
	bool mRotations;

	OnEndCallback mOnEndCallback;



	void Awake()
	{
		Reset();
	}

	public void StartInterpolation(OnEndCallback endCallback, bool bRotations, eWrapMode mode)
	{
		if (mState != "Reset")
			throw new System.Exception("First reset, add points and then call here");

		mState = mode == eWrapMode.ONCE ? "Once" : "Loop";
		mRotations = bRotations;
		mOnEndCallback = endCallback;

		SetInput();
	}

	public void Reset()
	{
		mNodes.Clear();
		mState = "Reset";
		mCurrentIdx = 1;
		mCurrentTime = 0;
		mRotations = false;
		mEndPointsMode = eEndPointsMode.AUTO;
	}

	public void AddPoint(Vector3 pos, Quaternion quat, float timeInSeconds, Vector2 easeInOut)
	{
		if (mState != "Reset")
			throw new System.Exception("Cannot add points after start");

		mNodes.Add(new SplineNode(pos, quat, timeInSeconds, easeInOut));
	}


	void SetInput()
	{
		if (mNodes.Count < 2)
			throw new System.Exception("Invalid number of points");

		if (mRotations)
		{
			for (int c = 1; c < mNodes.Count; c++)
			{
				SplineNode node = mNodes[c];
				SplineNode prevNode = mNodes[c - 1];

				// Always interpolate using the shortest path -> Selective negation
				if (Quaternion.Dot(node.Rot, prevNode.Rot) < 0)
				{
					node.Rot.x = -node.Rot.x;
					node.Rot.y = -node.Rot.y;
					node.Rot.z = -node.Rot.z;
					node.Rot.w = -node.Rot.w;
				}
			}
		}

		if (mEndPointsMode == eEndPointsMode.AUTO)
		{
			mNodes.Insert(0, mNodes[0]);
			mNodes.Add(mNodes[mNodes.Count - 1]);
		}
		else if (mEndPointsMode == eEndPointsMode.EXPLICIT && (mNodes.Count < 4))
			throw new System.Exception("Invalid number of points");
	}

	void SetExplicitMode()
	{
		if (mState != "Reset")
			throw new System.Exception("Cannot change mode after start");

		mEndPointsMode = eEndPointsMode.EXPLICIT;
	}

	public void SetAutoCloseMode(float joiningPointTime)
	{
		if (mState != "Reset")
			throw new System.Exception("Cannot change mode after start");

		mEndPointsMode = eEndPointsMode.AUTOCLOSED;

		mNodes.Add(new SplineNode(mNodes[0] as SplineNode));
		mNodes[mNodes.Count - 1].Time = joiningPointTime;

		Vector3 vInitDir = (mNodes[1].Point - mNodes[0].Point).normalized;
		Vector3 vEndDir = (mNodes[mNodes.Count - 2].Point - mNodes[mNodes.Count - 1].Point).normalized;
		float firstLength = (mNodes[1].Point - mNodes[0].Point).magnitude;
		float lastLength = (mNodes[mNodes.Count - 2].Point - mNodes[mNodes.Count - 1].Point).magnitude;

		SplineNode firstNode = new SplineNode(mNodes[0] as SplineNode);
		firstNode.Point = mNodes[0].Point + vEndDir * firstLength;

		SplineNode lastNode = new SplineNode(mNodes[mNodes.Count - 1] as SplineNode);
		lastNode.Point = mNodes[0].Point + vInitDir * lastLength;

		mNodes.Insert(0, firstNode);
		mNodes.Add(lastNode);
	}

	public float mCurrentTime;
	public int mCurrentIdx = 1;

	void Update()
	{
		if (AllowChangeSpeed)
		{
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.JoystickButton1))
			{
				if(!boosting)
				{
				SpeedUp(SpeedUpValue);
				boosting = true;
				}
				Debug.Log("boosting " + boosting);
			}
			else if ((Input.GetKey (KeyCode.UpArrow)) || Input.GetKey (KeyCode.JoystickButton4)){
							SpeedUp ();
				UserSet = false;
			} else if ((Input.GetKey (KeyCode.DownArrow))|| Input.GetKey (KeyCode.JoystickButton5)){
							SlowDown ();
				UserSet=false;
					} else
			{
				if((TimeScale>DefaultSpeed + 0.1f)||(TimeScale<DefaultSpeed - 0.1) && !UserSet)
				{
					if(TimeScale>DefaultSpeed + 0.1f)
					{
						TimeScale = TimeScale - (TimeScale/30);
						if (boosting)
							TimeScale -= boostDecayRate;
					}
					if(TimeScale<=DefaultSpeed)
					{
						TimeScale = TimeScale + (DefaultSpeed/20);
						if (boosting){
							boosting = false;
							ResetSpeed = true;
						}
					}
				}
					}
		}

		if (mState == "Reset" || mState == "Stopped" || mNodes.Count < 4)
			return;

		if (StopShip) {
			TimeScale = 0f;
				}
		if (ResetSpeed){
			StopShip = false;
			TimeScale = DefaultSpeed;
			ResetSpeed = false;
				}

		mCurrentTime += Time.deltaTime * TimeScale;

		// We advance to next point in the path
		if (mCurrentTime >= mNodes[mCurrentIdx + 1].Time)
		{
			if (mCurrentIdx < mNodes.Count - 3)
			{
				mCurrentIdx++;
			}
			else
			{
				if (mState != "Loop")
				{
					mState = "Stopped";

					// We stop right in the end point
					transform.position = mNodes[mNodes.Count - 2].Point;

					if (mRotations)
						transform.rotation = mNodes[mNodes.Count - 2].Rot;

					// We call back to inform that we are ended
					if (mOnEndCallback != null)
						mOnEndCallback();
						
					// Destroy enemy ships that reach the end of their path
					if (!IsPlayer) // if not the player
						Destroy(this.gameObject); 
				}
				else
				{
					mCurrentIdx = 1;
					mCurrentTime = 0;
				}

			}
		}

		if (mState != "Stopped")
		{
			// Calculates the t param between 0 and 1
			float param = (mCurrentTime - mNodes[mCurrentIdx].Time) / (mNodes[mCurrentIdx + 1].Time - mNodes[mCurrentIdx].Time);

			// Smooth the param
			param = MathUtils.Ease(param, mNodes[mCurrentIdx].EaseIO.x, mNodes[mCurrentIdx].EaseIO.y);

			transform.position = GetHermiteInternal(mCurrentIdx, param);

			//Debug.Log ("Node: " + mNodes[mCurrentIdx].Rot);
			//Debug.Log ("Squad: " + GetSquad(mCurrentIdx, param)); 

			//Debug.Log (mCurrentTime);
			if (mRotations)
			{
				if (IsStraightPath)
					transform.rotation = mNodes[mCurrentIdx].Rot;
				else
				{
					if (IsFlipped)
					{
						TempRot = GetSquad(mCurrentIdx, param);
						TempRot.y += 180;
						transform.rotation = TempRot;
					}
					else
						transform.rotation = GetSquad(mCurrentIdx, param);
				}
			}
		}
	}

	public Vector3 GetPoint(int idxFirstPoint)
	{
		Vector3 P0 = mNodes [idxFirstPoint].Point;
		return P0;
	}
	
	public Vector3 GetPointAtTime(float timeParam)
	{
		if (timeParam >= mNodes[mNodes.Count - 2].Time)
			return mNodes[mNodes.Count - 2].Point;

		int idx;
		float param;
		GetIndexAndParamAtTime(timeParam, out idx, out param);
		return GetPoint(idx);
	}

	Quaternion GetSquad(int idxFirstPoint, float t)
	{
		Quaternion Q0 = mNodes[idxFirstPoint - 1].Rot;
		Quaternion Q1 = mNodes[idxFirstPoint].Rot;
		Quaternion Q2 = mNodes[idxFirstPoint + 1].Rot;
		Quaternion Q3 = mNodes[idxFirstPoint + 2].Rot;

		Quaternion T1 = MathUtils.GetSquadIntermediate(Q0, Q1, Q2);
		Quaternion T2 = MathUtils.GetSquadIntermediate(Q1, Q2, Q3);

		return MathUtils.GetQuatSquad(t, Q1, Q2, T1, T2);
	}



	public Vector3 GetHermiteInternal(int idxFirstPoint, float t)
	{
		float t2 = t * t;
		float t3 = t2 * t;

		Vector3 P0 = mNodes[idxFirstPoint - 1].Point;
		Vector3 P1 = mNodes[idxFirstPoint].Point;
		Vector3 P2 = mNodes[idxFirstPoint + 1].Point;
		Vector3 P3 = mNodes[idxFirstPoint + 2].Point;

		float tension = 0.5f;	// 0.5 equivale a catmull-rom

		Vector3 T1 = tension * (P2 - P0);
		Vector3 T2 = tension * (P3 - P1);

		float Blend1 = 2 * t3 - 3 * t2 + 1;
		float Blend2 = -2 * t3 + 3 * t2;
		float Blend3 = t3 - 2 * t2 + t;
		float Blend4 = t3 - t2;

		return Blend1 * P1 + Blend2 * P2 + Blend3 * T1 + Blend4 * T2;
	}


	public Vector3 GetHermiteAtTime(float timeParam)
	{
		if (timeParam >= mNodes[mNodes.Count - 2].Time)
			return mNodes[mNodes.Count - 2].Point;

		int idx;
		float param;
		GetIndexAndParamAtTime(timeParam, out idx, out param);
		return GetHermiteInternal(idx, param);
	}

	public Quaternion GetSquadAtTime(float timeParam)
	{
		if (timeParam >= mNodes[mNodes.Count - 2].Time)
			return mNodes[mNodes.Count - 2].Rot;
		
		int idx;
		float param;
		GetIndexAndParamAtTime(timeParam, out idx, out param);
		return GetSquad(idx, param);
	}

	private void GetIndexAndParamAtTime(float timeParam, out int idx, out float param)
	{
		if (timeParam >= mNodes[mNodes.Count - 2].Time)
		{
			idx = mNodes.Count - 2;
			param = 1.0f;
			return;
		}
		
		int c;
		for (c = 1; c < mNodes.Count - 2; c++)
		{
			if (mNodes[c].Time > timeParam)
				break;
		}
		
		idx = c - 1;
		param = (timeParam - mNodes[idx].Time) / (mNodes[idx + 1].Time - mNodes[idx].Time);
		param = MathUtils.Ease(param, mNodes[idx].EaseIO.x, mNodes[idx].EaseIO.y);
	}
}