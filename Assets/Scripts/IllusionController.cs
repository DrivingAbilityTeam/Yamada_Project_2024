using UnityEngine;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace Deform
{
	[Deformer (Name = "Illusion", Description = "Adds illusion to mesh", XRotation = -90f, Type = typeof (IllusionController))]
    //[HelpURL("https://github.com/keenanwoodall/Deform/wiki/RippleDeformer")]
    public class IllusionController : Deformer, IFactor
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

		/*public AnimationCurve Curve
		{
			get => curve;
			set => curve = value;
		}*/
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

		
		[SerializeField] private float frequency = 1f;
		[SerializeField] private float amplitude = 0f;
		[SerializeField] private BoundsMode mode = BoundsMode.Limited;
		[SerializeField] private float falloff = 1f;
		[SerializeField] private float innerRadius = 0f;
		[SerializeField] private float outerRadius = 1f;
		[SerializeField] private float speed = 0f;
		[SerializeField] public float offset = 0f;
		[SerializeField] private Transform axis;

		//[SerializeField] private AnimationCurve curve;


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
						vertices = data.DynamicNative.VertexBuffer,
						//curve = Curve
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
						//curve = Curve
						
					}.Schedule (data.Length, DEFAULT_BATCH_COUNT, dependency);
			}
		}

		bool complete;

		public float GetTotalOffset ()
		{

			

			
			return 1f;

			
			
			

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
			//public AnimationCurve curve;

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
			//public AnimationCurve curve;

			

			public void Execute (int index)
			{
				var range = outerRadius - innerRadius;

				var point = mul (meshToAxis, float4 (vertices[index], 1f));

				
				var d = length (point.x);


				var clampedD = clamp (d, innerRadius, outerRadius);

				


				var positionOffset = (point.x /d) * (amplitude + (d * frequency));
                

				if (range != 0f)
				{
					var pointBetweenBounds = clamp ((clampedD - innerRadius) / range, 0f, 1f);

                    float v = lerp (positionOffset, 0f, pointBetweenBounds * falloff);

					
                    

					
					

					


                    //point.y = (point.y *  amplitude) + (frequency * ((point.y /length (point.y)) * d));


					point.y = (point.y *  amplitude) + (frequency * ((point.y /length (point.y)) * point.x));
					
                    

                    

					
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
