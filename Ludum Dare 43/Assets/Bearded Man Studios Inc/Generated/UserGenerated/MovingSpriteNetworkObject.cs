using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15]")]
	public partial class MovingSpriteNetworkObject : NetworkObject
	{
		public const int IDENTITY = 6;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private Vector2 _Position;
		public event FieldEvent<Vector2> PositionChanged;
		public InterpolateVector2 PositionInterpolation = new InterpolateVector2() { LerpT = 0.15f, Enabled = true };
		public Vector2 Position
		{
			get { return _Position; }
			set
			{
				// Don't do anything if the value is the same
				if (_Position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_Position = value;
				hasDirtyFields = true;
			}
		}

		public void SetPositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_Position(ulong timestep)
		{
			if (PositionChanged != null) PositionChanged(_Position, timestep);
			if (fieldAltered != null) fieldAltered("Position", _Position, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			PositionInterpolation.current = PositionInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _Position);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_Position = UnityObjectMapper.Instance.Map<Vector2>(payload);
			PositionInterpolation.current = _Position;
			PositionInterpolation.target = _Position;
			RunChange_Position(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _Position);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (PositionInterpolation.Enabled)
				{
					PositionInterpolation.target = UnityObjectMapper.Instance.Map<Vector2>(data);
					PositionInterpolation.Timestep = timestep;
				}
				else
				{
					_Position = UnityObjectMapper.Instance.Map<Vector2>(data);
					RunChange_Position(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (PositionInterpolation.Enabled && !PositionInterpolation.current.UnityNear(PositionInterpolation.target, 0.0015f))
			{
				_Position = (Vector2)PositionInterpolation.Interpolate();
				//RunChange_Position(PositionInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public MovingSpriteNetworkObject() : base() { Initialize(); }
		public MovingSpriteNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public MovingSpriteNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
