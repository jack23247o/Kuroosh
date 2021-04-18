﻿using System;
using System.Text.RegularExpressions;

namespace VenoX_Global_Systems._Core_
{
    public class Debug
    {
        public static bool DEBUG_MODE_ENABLED = true;
        public static void OutputDebugString(string text)
        {
            try
            {
                if (!DEBUG_MODE_ENABLED) { return; }
                Console.WriteLine(DateTime.Now.Hour + " : " + DateTime.Now.Minute + " | : " + text);
            }
            catch { }
        }

        public static void CatchExceptions(string FunctionName, Exception ex)
        {
            if (!DEBUG_MODE_ENABLED) { return; }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[EXCEPTION " + FunctionName + "] " + ex.Message);
            Console.WriteLine("[EXCEPTION " + FunctionName + "] " + ex.StackTrace);
            Console.ResetColor();
        }
        public static void OutputLog(string message, ConsoleColor color)
        {

            var pieces = Regex.Split(message, @"(\[[^\]]*\])");

            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];

                if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    Console.ForegroundColor = color;
                    piece = piece.Substring(1, piece.Length - 2);
                }

                Console.Write(piece);
                Console.ResetColor();

            }
            Console.WriteLine();
        }
    }
}
