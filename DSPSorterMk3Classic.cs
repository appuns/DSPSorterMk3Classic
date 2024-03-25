using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Net;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Security;
using System.Security.Permissions;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]



namespace DSPSorterMk3Classic
{
    [BepInPlugin("Appun.DSP.plugin.SorterMk3Classic", "DSPSorterMk3Classic", "0.0.1")]
    [HarmonyPatch]
	public class Main : BaseUnityPlugin
	{

        public void Awake()
        {
            LogManager.Logger = Logger;
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());

		}

        [HarmonyPostfix, HarmonyPatch(typeof(VFPreload), "PreloadThread")]
        public static void VFPreload_PreloadThread_Patch(VFPreload __instance)
        {


            for (int i = 1; i < 4; i++)
            {
                TechProto techProto = LDB.techs.Select(3300 + i);
                techProto.IsObsolete = false;
                techProto.Position = new Vector2(techProto.Position.x + 28 , techProto.Position.y);
            }
            TechProto techProto2 = LDB.techs.Select(3304);
            techProto2.PreTechs[0] = 0;
        }

        //[HarmonyPostfix, HarmonyPatch(typeof(UITechTree), "_OnOpen")]
        public static void UUITechTree_OnOpen_PPostfix(UITechTree __instance)
        {

            GameMain.history.inserterStackCountObsolete = 6;
        }





        public class LogManager
        {
            public static ManualLogSource Logger;
        }
    }
}