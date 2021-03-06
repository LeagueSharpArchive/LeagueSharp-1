#if SWFASM || SWFDUMPER
using System.CodeDom.Compiler;
#endif
namespace LeagueSharp.GameFiles.Tools.Swf
{
    internal class WaitForFrame2 : IStackManipulator
	{
        internal ActionKind ActionId { get { return ActionKind.WaitForFrame2; } }
        internal uint Version { get { return 4; } }
        internal uint Length { get { return 4; } }

        internal uint StackPops { get { return 1; } }
        internal uint StackPushes { get { return 0; } }
        internal int StackChange { get { return -1; } }

        internal uint SkipCount;

        internal WaitForFrame2(SwfReader r)
		{
			SkipCount = (uint)r.GetByte();
		}
#if SWFASM
        internal override void ToFlashAsm(IndentedTextWriter w)
		{
			w.WriteLine("ifframeloadedexpr");
		}
#endif
#if SWFWRITER
		internal override void ToSwf(SwfWriter w)
		{
            w.AppendByte((byte)ActionKind.WaitForFrame2);
            w.AppendUI16(Length - 3); // don't incude def byte and len

            w.AppendByte((byte)SkipCount);

		}
#endif
#if SWFDUMPER
		internal override void Dump(IndentedTextWriter w)
		{
			w.WriteLine(System.Enum.GetName(typeof(ActionKind), this.ActionId));
		}
#endif
	}
}
