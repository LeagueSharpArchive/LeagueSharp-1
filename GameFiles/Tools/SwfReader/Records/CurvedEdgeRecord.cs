#if SWFDUMPER
using System.CodeDom.Compiler;
#endif
namespace LeagueSharp.GameFiles.Tools.Swf
{
	internal class CurvedEdgeRecord : IShapeRecord
	{
		/*
			CURVEDEDGERECORD
			Field			Type			Comment
			TypeFlag		UB[1]			This is an edge record. Always 1.
			StraightFlag	UB[1]			Curved edge. Always 0.
			NumBits			UB[4]			Number of bits per value (2 less than the actual number).
			ControlDeltaX	SB[NumBits+2]	X control point change.
			ControlDeltaY	SB[NumBits+2]	Y control point change.
			AnchorDeltaX	SB[NumBits+2]	X anchor point change.
			AnchorDeltaY	SB[NumBits+2]	Y anchor point change.
		 */
		internal int ControlX;
		internal int ControlY;
		internal int AnchorX;
		internal int AnchorY;

		internal CurvedEdgeRecord(int ControlX, int ControlY, int AnchorX, int AnchorY)
		{
			this.ControlX = ControlX;
			this.ControlY = ControlY;
			this.AnchorX = AnchorX;
			this.AnchorY = AnchorY;
		}

		internal CurvedEdgeRecord(SwfReader r)
		{
			uint nbits = r.GetBits(4) + 2;

			this.ControlX = r.GetSignedNBits(nbits);
			this.ControlY = r.GetSignedNBits(nbits);
			this.AnchorX = r.GetSignedNBits(nbits);
			this.AnchorY = r.GetSignedNBits(nbits);
		}
#if SWFWRITER
		internal override void ToSwf(SwfWriter w)
		{
			throw new System.NotSupportedException("Use the overload: ToSwf(SwfWriter w, uint fillBits, uint lineBits, ShapeType shapeType)");
		}
		internal override void ToSwf(SwfWriter w, ref uint fillBits, ref uint lineBits, ShapeType shapeType)
		{
			w.AppendBit(true);
			w.AppendBit(false);

            uint bits = SwfWriter.MinimumBits(ControlX, ControlY, AnchorX, AnchorY);
            bits = bits < 2 ? 2 : bits; // min 2 bits
			w.AppendBits(bits - 2, 4);

			w.AppendSignedNBits(ControlX, bits);
			w.AppendSignedNBits(ControlY, bits);
			w.AppendSignedNBits(AnchorX, bits);
			w.AppendSignedNBits(AnchorY, bits);
		}
#endif
#if SWFDUMPER
		internal override void Dump(IndentedTextWriter w)
		{
			w.Write("Curve [");
			w.Write("cx: " + this.ControlX + ", ");
			w.Write("cy: " + this.ControlY + ", ");
			w.Write("ax: " + this.AnchorX + ", ");
			w.Write("ay: " + this.AnchorY);
			w.Write("]");
		}
		public override string ToString()
		{
			float scale = 400F;
			string s = "Curve [";
			s += "cx: " + (this.ControlX / scale) + ", ";
			s += "cy: " + (this.ControlY / scale) + ", ";
			s += "ax: " + (this.AnchorX / scale) + ", ";
			s += "ay: " + (this.AnchorY / scale);
			s += "]";
			return s;
        }
#endif
    }
}
