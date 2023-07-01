using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UndertaleModLib.Models;

namespace CrossChecker
{
    public class DifferenceChecker
    {

        public static bool DifferentCodes(UndertaleCode original, UndertaleCode other)
        {
            if (original.Length != other.Length || original.Instructions.Count != other.Instructions.Count)
                return true;
            
            //for (int i = 0; i < original.Instructions.Count; i++)
            //{
            //    var val1 = original.Instructions[i].Value;
            //    var val2 = other.Instructions[i].Value;
            //    if (original.Instructions[i].Kind != other.Instructions[i].Kind)
            //        return true;
            //}
            
            return false;
        }
        public static bool DifferentScripts(UndertaleScript original, UndertaleScript other)
        {
            return DifferentCodes(original.Code, other.Code);
        }

        public static bool DifferentRoom(UndertaleRoom original, UndertaleRoom other)
        {
            if (original.GameObjects.Count != other.GameObjects.Count)
                return true;


            return false;
        }
    }
}
