using BepInEx;
using HarmonyLib;
using UnityEngine;
using AuthoritativeConfig;

namespace Adjust_Taming_Speed
{
    [BepInPlugin("zukane2.AdjustTamingSpeed", "Adjust Taming Speed", "2.0.0")]
    public class AdjustTamingSpeed : BaseUnityPlugin
    {
        public new AuthoritativeConfig.Config Config
        {
            get { return AuthoritativeConfig.Config.Instance; }
            set { }
        }

        private readonly Harmony harmony = new Harmony("zukane2.AdjustTamingSpeed");
        private static ConfigEntry<float> tamingTime;

        void Awake()
        {
            Config.init(this, true);
            tamingTime = Config.Bind<float>("General", "TamingTime", 1800f, "Taming Time");
            
            harmony.PatchAll();
        }

        void OnDestroy()
        {

            harmony.UnpatchSelf();
        }

        [HarmonyPatch(typeof(Tameable), "Awake")]
        static class Tame_Awake_Patch
        {
            static void Postfix(ref float ___m_tamingTime)
            {
                ___m_tamingTime = tamingTime.Value;
            }
        }
    }
}