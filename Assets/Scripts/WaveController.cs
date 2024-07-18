using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Deform
{
	[Deformer (Name = "Wave", Description = "Adds wave to mesh", XRotation = -90f, Type = typeof (WaveController))]
    //[HelpURL("https://github.com/keenanwoodall/Deform/wiki/RippleDeformer")]
    public class WaveController : Deformer, IFactor
	{
		public float Factor
		{
			get => Amplitude;
			set => Amplitude = value;
		}

		public float Frequency
		{
			get => frequency;
			set => frequency = value;
		}
		public float Amplitude
		{
			get => amplitude;
			set => amplitude = value;
		}
		public BoundsMode Mode
		{
			get => mode;
			set => mode = value; 
		}
		public float Falloff
		{
			get => falloff;
			set => falloff = Mathf.Clamp01 (value);
		}
		public float InnerRadius
		{
			get => innerRadius;
			set => innerRadius = Mathf.Max (0f, Mathf.Min (value, OuterRadius));
		}
		public float OuterRadius
		{
			get => outerRadius;
			set => outerRadius = Mathf.Max (value, InnerRadius);
		}
		public float Speed
		{
			get => speed;
			set => speed = value;
		}
		public float Offset
		{
			get => offset;
			set => offset = value;
		}
		public Transform Axis
		{
			get
			{
				if (axis == null)
					axis = transform;
				return axis;
			}
			set => axis = value;
		}

		//public float v;

		[SerializeField] private float frequency = 1f;
		[SerializeField] private float amplitude = 0f;
		[SerializeField] private BoundsMode mode = BoundsMode.Limited;
		[SerializeField] private float falloff = 1f;
		[SerializeField] private float innerRadius = 0f;
		[SerializeField] private float outerRadius = 1f;
		[SerializeField] private float speed = 0f;
		//[SerializeField, HideInInspector] private float offset = 0f;
		[SerializeField] public float offset = 0f;
		[SerializeField] private Transform axis;

		//[SerializeField] public float height = 0f;

		//[SerializeField] public AnimationCurve animation_curve;

		[SerializeField, HideInInspector]
		private float speedOffset;

		public override DataFlags DataFlags => DataFlags.Vertices;

		private void Update ()
		{
			speedOffset += Speed * Time.deltaTime;

			
		}

		public override JobHandle Process (MeshData data, JobHandle dependency = default)
		{
			if (Mathf.Approximately (Amplitude, 0f) || Mathf.Approximately (Frequency, 0f))
				return dependency;

			var meshToAxis = DeformerUtils.GetMeshToAxisSpace (Axis, data.Target.GetTransform ());

			switch (Mode)
			{
				default:
					return new UnlimitedRippleJob
					{
						frequency = Frequency,
						amplitude = Amplitude,
						offset = GetTotalOffset (),
						meshToAxis = meshToAxis,
						axisToMesh = meshToAxis.inverse,
						vertices = data.DynamicNative.VertexBuffer
					}.Schedule (data.Length, DEFAULT_BATCH_COUNT, dependency);
				case BoundsMode.Limited:
					return new LimitedRippleJob
					{
						frequency = Frequency,
						amplitude = Amplitude,
						falloff = Falloff,
						innerRadius = InnerRadius,
						outerRadius = OuterRadius,
						offset = GetTotalOffset (),
						meshToAxis = meshToAxis,
						axisToMesh = meshToAxis.inverse,
						vertices = data.DynamicNative.VertexBuffer,
						//animation_curve = animation_curve
					}.Schedule (data.Length, DEFAULT_BATCH_COUNT, dependency);
			}
		}

		bool complete;

		public float GetTotalOffset ()
		{

			//amplitude = (0.07f * (1f /(1f + Mathf.Pow((7f - Mathf.Abs(this.transform.position.x)),10))));

			if(Mathf.Abs(this.transform.position.x) < 20f){
				amplitude = 0.07f;
			}

			if(Mathf.Abs(this.transform.position.x) < 0.01f){
				complete = true;
			}

			if(amplitude < 0.01f || complete){
				amplitude = 0.01f;
			}

			
			return Offset + speedOffset;

			
			
			//return frequency;

		}

		

		[BurstCompile (CompileSynchronously = COMPILE_SYNCHRONOUSLY)]
		public struct UnlimitedRippleJob : IJobParallelFor
		{
			public float frequency;
			public float amplitude;
			public float offset;
			public float4x4 meshToAxis;
			public float4x4 axisToMesh;
			public NativeArray<float3> vertices;

			public void Execute (int index)
			{
				var point = mul (meshToAxis, float4 (vertices[index], 1f));

				var d = length (point.xy);

				point.z += sin ((offset + d * frequency) * (float)PI * 2f) * amplitude;

				vertices[index] = mul (axisToMesh, point).xyz;

				//Debug.Log("index" + )
			}
		}

		[BurstCompile (CompileSynchronously = COMPILE_SYNCHRONOUSLY)]
		public struct LimitedRippleJob : IJobParallelFor
		{
			public float frequency;
			public float amplitude;
			public float falloff;
			public float innerRadius;
			public float outerRadius;
			public float offset;
			public float4x4 meshToAxis;
			public float4x4 axisToMesh;
			public NativeArray<float3> vertices;

			//public AnimationCurve animation_curve;

			public void Execute (int index)
			{
				var range = outerRadius - innerRadius;

				var point = mul (meshToAxis, float4 (vertices[index], 1f));

				//var d = length (point.xy);
				var d = length (point.x);


				var clampedD = clamp (d, innerRadius, outerRadius);

				/*float border = 0.01f;

				float border2 =0.6f;

				float height= 0f;

				float p = 0.1f * (offset - Mathf.Floor(offset));

				float value =Mathf.Abs( p - (0.1f * (clampedD - Mathf.Floor(clampedD))));




				if(value < border){
					//height = 3f;
					height = 9f * sin ((value) * (float)PI * 4f);
				}

				float positionOffset = amplitude * height;*/

				//Debug.Log("value" + value);


			    var positionOffset = 1f * amplitude  *  Mathf.Pow(sin ((-offset + clampedD * 1f) * (float)PI * 2f) * 1f,1);

				//float value = ((1f)*(((-offset / 10f) + clampedD * frequency) * (float)PI * 2f  / 1f));

				

				//var positionOffset = Mathf.Pow(sin (1f * value) * amplitude,1);  // AAA


				//float aaa = 100f;


				/*if(Mathf.Abs(positionOffset) > aaa)
				{
					positionOffset = (positionOffset / Mathf.Abs(positionOffset)) * aaa;
				}*/

                

				if (range != 0f)
				{
					var pointBetweenBounds = clamp ((clampedD - innerRadius) / range, 0f, 1f);

                    float v = lerp (positionOffset, 0f, pointBetweenBounds * falloff);

					
                    

					if(v < 0){
						v = 0;
					}


					//point.z += lerp (positionOffset, 0f, pointBetweenBounds * falloff);

					point.z += v;

					/*if(d > frequency && d < frequency + 0.3f){ // 0.01f
						point.z += amplitude;
					}*/
					
                    

                    

					
				}
				else
				{
					if (d > outerRadius)
						point.z += lerp (positionOffset, 0f, falloff);
					else if (d < innerRadius)
						point.z += positionOffset;
				}

				vertices[index] = mul (axisToMesh, point).xyz;
			}
		}
	}
}
