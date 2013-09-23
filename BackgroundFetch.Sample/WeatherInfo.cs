using System;
using System.Collections.Generic;

namespace BackgroundFetchSample
{
	public class Coord
	{
		public double lon { get; set; }
		public double lat { get; set; }
	}

	public class Main
	{
		public double temp { get; set; }
		public int pressure { get; set; }
		public int humidity { get; set; }
		public double temp_min { get; set; }
		public double temp_max { get; set; }
	}

	public class Wind
	{
		public double speed { get; set; }
		public int deg { get; set; }
	}

	public class WeatherInfo
	{
		public Coord coord { get; set; }
		public Main main { get; set; }
		public Wind wind { get; set; }
		public int dt { get; set; }
		public int id { get; set; }
		public string name { get; set; }
		public int cod { get; set; }

		public int GetTempInCelsius()
		{
			//main.temp is in kelvin
			return (int)(main.temp - 273.15);
		}
	}
}

