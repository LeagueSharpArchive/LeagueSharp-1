﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeagueSharp.GameFiles.Tools.LuaObjReader
{
    public class LNil : LObject
    {
        public static readonly LNil NIL = new LNil();

        public override bool Equals(object obj)
        {
            return (this == obj);
        }

        private LNil() { }
    }
}
