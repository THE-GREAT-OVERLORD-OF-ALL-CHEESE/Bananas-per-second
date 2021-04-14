using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;
using System.Reflection;
using Valve.Newtonsoft.Json;
using System.IO;

public class Bananaspersecond : VTOLMOD
{
    public class MeasurementDistUnit {
        public string name;
        public float conversion;
        public string label;

        public MeasurementDistUnit(string name, float conversion, string label) {
            this.name = name;
            this.conversion = conversion;
            this.label = label;
        }
    }

    public class MeasurementAltUnit
    {
        public string name;
        public float conversion;
        public string label;

        public MeasurementAltUnit(string name, float conversion, string label)
        {
            this.name = name;
            this.conversion = conversion;
            this.label = label;
        }
    }

    public class MeasurementSpeedUnit
    {
        public string name;
        public float conversion;
        public string label;

        public MeasurementSpeedUnit(string name, float conversion, string label)
        {
            this.name = name;
            this.conversion = conversion;
            this.label = label;
        }
    }

    public class MeasurementUnits
    {
        public List<MeasurementDistUnit> measurementDistUnits;
        public List<MeasurementAltUnit> measurementAltUnits;
        public List<MeasurementSpeedUnit> measurementSpeedUnits;
    }

    public static MeasurementUnits units;
    public static int distMode;
    public static int altMode;
    public static int speedMode;

    public override void ModLoaded()
    {
        HarmonyInstance harmony = HarmonyInstance.Create("cheese.bananas");
        harmony.PatchAll(Assembly.GetExecutingAssembly());

        Debug.Log("bananas");

        base.ModLoaded();

        units = new MeasurementUnits();

        units.measurementDistUnits = new List<MeasurementDistUnit>();
        units.measurementDistUnits.Add(new MeasurementDistUnit("Meters", 1, "m"));
        units.measurementDistUnits.Add(new MeasurementDistUnit("NautMiles", 0.000539957f, "nm"));
        units.measurementDistUnits.Add(new MeasurementDistUnit("Feet", 3.28084f, "ft"));
        units.measurementDistUnits.Add(new MeasurementDistUnit("Miles", 0.000621371f, "mi"));
        units.measurementDistUnits.Add(new MeasurementDistUnit("Bananas", 5.618f, "bns"));
        units.measurementDistUnits.Add(new MeasurementDistUnit("Ierdnas", 0.59880239521f, "i"));

        units.measurementAltUnits = new List<MeasurementAltUnit>();
        units.measurementAltUnits.Add(new MeasurementAltUnit("Meters", 1, "m"));
        units.measurementAltUnits.Add(new MeasurementAltUnit("Feet", 3.28084f, "ft"));
        units.measurementAltUnits.Add(new MeasurementAltUnit("Bananas", 5.618f, "bns"));
        units.measurementAltUnits.Add(new MeasurementAltUnit("Ierdnas", 0.59880239521f, "i"));

        units.measurementSpeedUnits = new List<MeasurementSpeedUnit>();
        units.measurementSpeedUnits.Add(new MeasurementSpeedUnit("MetersPerSecond", 1, "m/s"));
        units.measurementSpeedUnits.Add(new MeasurementSpeedUnit("KilometersPerHour", 3.6f, "KPH"));
        units.measurementSpeedUnits.Add(new MeasurementSpeedUnit("Knots", 1.94384f, "kt"));
        units.measurementSpeedUnits.Add(new MeasurementSpeedUnit("MilesPerHour", 2.23694f, "MPH"));
        units.measurementSpeedUnits.Add(new MeasurementSpeedUnit("FeetPerSecond", 3.28084f, "ft/s"));
        //units.measurementSpeedUnits.Add(new MeasurementSpeedUnit("Mach", 0.00291545f, "M"));
        units.measurementSpeedUnits.Add(new MeasurementSpeedUnit("BananasPerSecond", 5.618f, "BPS"));
        units.measurementSpeedUnits.Add(new MeasurementSpeedUnit("IerdnasPerSecond", 0.59880239521f, "I/S"));

        LoadFromFile();
    }

    public void LoadFromFile() {

        //string address = Directory.GetCurrentDirectory() + @"\VTOLVR_ModLoader\mods\Bananas_Per_Second";
        string address = ModFolder;
        Debug.Log("Checking for: " + address);

        if (Directory.Exists(address))
        {
            Debug.Log(address + " exists!");
            try
            {
                Debug.Log("Checking for: " + address + @"\units.json");
                string temp = File.ReadAllText(address + @"\units.json");

                units = JsonConvert.DeserializeObject<MeasurementUnits>(temp);
            }
            catch
            {
                Debug.Log("no json found, making one");
                File.WriteAllText(address + @"\units.json", JsonConvert.SerializeObject(units));
            }
        }
        else {
            Debug.Log("Mod folder not found? Make sure the name of the mod folder is \"Bananas_Per_Second\"");
        }
    }

	public static void UpdateDisplay(MeasurementManager measurements, MFDPage mfdPage, MFDPortalPage portalPage)
	{
		string text = units.measurementAltUnits[altMode].name;
		string text2 = units.measurementDistUnits[distMode].name;
		string text3 = units.measurementSpeedUnits[speedMode].name;
		if (mfdPage)
		{
			mfdPage.SetText("AltModeText", text);
			mfdPage.SetText("DistModeText", text2);
			mfdPage.SetText("SpeedModeText", text3);
			return;
		}
		if (portalPage)
		{
			portalPage.SetText("AltModeText", text);
			portalPage.SetText("DistModeText", text2);
			portalPage.SetText("SpeedModeText", text3);
		}
	}

    public static float ConvertedDistance(float distance)
    {
        return units.measurementDistUnits[distMode].conversion * distance;
    }

    public static float ConvertedAltitude(float altitude)
    {
        return units.measurementAltUnits[altMode].conversion * altitude;
    }

    public static float ConvertedSpeed(float speed)
    {
        return units.measurementSpeedUnits[speedMode].conversion * speed;
    }
}