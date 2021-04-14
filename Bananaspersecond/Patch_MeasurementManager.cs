using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using UnityEngine;

[HarmonyPatch(typeof(MFDVehicleOptions), "ToggleDistMode")]
class Patch_MFDVehicleOptions_ToggleDistMode
{
    [HarmonyPrefix]
    static bool Prefix(MFDVehicleOptions __instance)
    {
		Traverse mfdTraverse = new Traverse(__instance);
		Debug.Log("measurements");
		MeasurementManager measurements = (MeasurementManager)mfdTraverse.Field("measurements").GetValue();

		Debug.Log("mfdPage");
		object obj1 = mfdTraverse.Field("mfdPage").GetValue();
		MFDPage mfdPage = null;
		if (obj1 != null) {
			mfdPage = (MFDPage)obj1;
		}

		Debug.Log("portalPage");
		object obj2 = mfdTraverse.Field("portalPage").GetValue();
		MFDPortalPage portalPage = null;
		if (obj2 != null)
		{
			portalPage = (MFDPortalPage)obj2;
		}

		Bananaspersecond.distMode = (Bananaspersecond.distMode + 1) % Bananaspersecond.units.measurementDistUnits.Count;
		Bananaspersecond.UpdateDisplay(measurements, mfdPage, portalPage);
		return false;
    }
}

[HarmonyPatch(typeof(MFDVehicleOptions), "ToggleAltMode")]
class Patch_MFDVehicleOptions_ToggleAltMode
{
	[HarmonyPrefix]
	static bool Prefix(MFDVehicleOptions __instance)
	{
		Traverse mfdTraverse = new Traverse(__instance);
		Debug.Log("measurements");
		MeasurementManager measurements = (MeasurementManager)mfdTraverse.Field("measurements").GetValue();

		Debug.Log("mfdPage");
		object obj1 = mfdTraverse.Field("mfdPage").GetValue();
		MFDPage mfdPage = null;
		if (obj1 != null)
		{
			mfdPage = (MFDPage)obj1;
		}

		Debug.Log("portalPage");
		object obj2 = mfdTraverse.Field("portalPage").GetValue();
		MFDPortalPage portalPage = null;
		if (obj2 != null)
		{
			portalPage = (MFDPortalPage)obj2;
		}

		Bananaspersecond.altMode = (Bananaspersecond.altMode + 1) % Bananaspersecond.units.measurementAltUnits.Count;
		Bananaspersecond.UpdateDisplay(measurements, mfdPage, portalPage);
		return false;
	}
}

[HarmonyPatch(typeof(MFDVehicleOptions), "ToggleSpeedMode")]
class Patch_MFDVehicleOptions_ToggleSpeedMode
{
	[HarmonyPrefix]
	static bool Prefix(MFDVehicleOptions __instance)
	{
		Traverse mfdTraverse = new Traverse(__instance);
		Debug.Log("measurements");
		MeasurementManager measurements = (MeasurementManager)mfdTraverse.Field("measurements").GetValue();

		Debug.Log("mfdPage");
		object obj1 = mfdTraverse.Field("mfdPage").GetValue();
		MFDPage mfdPage = null;
		if (obj1 != null)
		{
			mfdPage = (MFDPage)obj1;
		}

		Debug.Log("portalPage");
		object obj2 = mfdTraverse.Field("portalPage").GetValue();
		MFDPortalPage portalPage = null;
		if (obj2 != null)
		{
			portalPage = (MFDPortalPage)obj2;
		}

		Bananaspersecond.speedMode = (Bananaspersecond.speedMode + 1) % Bananaspersecond.units.measurementSpeedUnits.Count;
		Bananaspersecond.UpdateDisplay(measurements, mfdPage, portalPage);
		return false;
	}
}


[HarmonyPatch(typeof(MFDVehicleOptions), "UpdateDisplay")]
class Patch_MFDVehicleOptions_UpdateDisplay
{
	[HarmonyPrefix]
	static bool Prefix(MFDVehicleOptions __instance)
	{
		Traverse mfdTraverse = new Traverse(__instance);
		Debug.Log("measurements");
		MeasurementManager measurements = (MeasurementManager)mfdTraverse.Field("measurements").GetValue();

		Debug.Log("mfdPage");
		object obj1 = mfdTraverse.Field("mfdPage").GetValue();
		MFDPage mfdPage = null;
		if (obj1 != null)
		{
			mfdPage = (MFDPage)obj1;
		}

		Debug.Log("portalPage");
		object obj2 = mfdTraverse.Field("portalPage").GetValue();
		MFDPortalPage portalPage = null;
		if (obj2 != null)
		{
			portalPage = (MFDPortalPage)obj2;
		}

		Bananaspersecond.UpdateDisplay(measurements, mfdPage, portalPage);
		return false;
	}
}

[HarmonyPatch(typeof(MeasurementManager), "ConvertedDistance")]
class Patch_MeasurementManager_ConvertedDistance
{
	[HarmonyPrefix]
	static bool Prefix(MeasurementManager __instance, ref float __result, float distance)
	{
		__result = Bananaspersecond.ConvertedDistance(distance);
		return false;
	}
}

[HarmonyPatch(typeof(MeasurementManager), "ConvertedAltitude")]
class Patch_MeasurementManager_ConvertedAltitude
{
	[HarmonyPrefix]
	static bool Prefix(MeasurementManager __instance, ref float __result, float altitude)
	{
		__result = Bananaspersecond.ConvertedAltitude(altitude);
		return false;
	}
}

[HarmonyPatch(typeof(MeasurementManager), "ConvertedSpeed")]
class Patch_MeasurementManager_ConvertedSpeed
{
	[HarmonyPrefix]
	static bool Prefix(MeasurementManager __instance, ref float __result, float speed)
	{
		__result = Bananaspersecond.ConvertedSpeed(speed);
		return false;
	}
}

[HarmonyPatch(typeof(MeasurementManager), "ConvertedVerticalSpeed")]
class Patch_MeasurementManager_ConvertedVerticalSpeed
{
	[HarmonyPrefix]
	static bool Prefix(MeasurementManager __instance, ref float __result, float speed)
	{
		__result = Bananaspersecond.ConvertedDistance(speed);
		return false;
	}
}

[HarmonyPatch(typeof(MeasurementManager), "FormattedDistance")]
class Patch_MeasurementManager_FormattedDistance
{
	[HarmonyPrefix]
	static bool Prefix(MeasurementManager __instance, ref string __result, float distance)
	{
		__result = string.Format("{0:n}{1}", Bananaspersecond.ConvertedDistance(distance), Bananaspersecond.units.measurementDistUnits[Bananaspersecond.distMode].label);
		return false;
	}
}

[HarmonyPatch(typeof(MeasurementManager), "FormattedAltitude")]
class Patch_MeasurementManager_FormattedAltitude
{
	[HarmonyPrefix]
	static bool Prefix(MeasurementManager __instance, ref string __result, float altitude)
	{
		__result = string.Format("{0:n0}", Bananaspersecond.ConvertedAltitude(altitude));
		return false;
	}
}

[HarmonyPatch(typeof(MeasurementManager), "FormattedSpeed")]
class Patch_MeasurementManager_FormattedSpeed
{
	[HarmonyPrefix]
	static bool Prefix(MeasurementManager __instance, ref string __result, float speed)
	{
		__result = string.Format("{0:n0}", Bananaspersecond.ConvertedSpeed(speed));
		return false;
	}
}

[HarmonyPatch(typeof(MeasurementManager), "DistanceLabel")]
class Patch_MeasurementManager_DistanceLabel
{
	[HarmonyPrefix]
	static bool Prefix(MeasurementManager __instance, ref string __result)
	{
		__result = Bananaspersecond.units.measurementDistUnits[Bananaspersecond.distMode].label;
		return false;
	}
}

[HarmonyPatch(typeof(MeasurementManager), "AltitudeLabel")]
class Patch_MeasurementManager_AltitudeLabel
{
	[HarmonyPrefix]
	static bool Prefix(MeasurementManager __instance, ref string __result)
	{
		__result = Bananaspersecond.units.measurementAltUnits[Bananaspersecond.altMode].label;
		return false;
	}
}

[HarmonyPatch(typeof(MeasurementManager), "SpeedLabel")]
class Patch_MeasurementManager_SpeedLabel
{
	[HarmonyPrefix]
	static bool Prefix(MeasurementManager __instance, ref string __result)
	{
		__result = Bananaspersecond.units.measurementSpeedUnits[Bananaspersecond.speedMode].label;
		return false;
	}
}