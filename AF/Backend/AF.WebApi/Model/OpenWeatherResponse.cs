using System.Collections.Generic;
using System;
using Newtonsoft.Json;
public class OpenWeatherResponse
{
    [JsonProperty("city")]
    public City City { get; set; }
    [JsonProperty("list")]
    public IEnumerable<List> List { get; set; }

}

public class City
{
    [JsonProperty("name")]
    public string Name { get; set; }
}

public class List
{
    [JsonProperty("main")]
    public Main Main { get; set; }
    [JsonProperty("wind")]
    public Wind Wind { get; set; }

    [JsonIgnore]
    private DateTime? _dtxt;

    [JsonProperty("dt_txt")]
    public DateTime? DtTxt
    {
        get => _dtxt;
        set
        {
            _dtxt = value;
            this.Day = value.Value.Date.ToString("m");
            this.Time = value.Value.ToShortTimeString();
        }
    }
    [JsonIgnore]
    public String Day { get; set; }
    [JsonIgnore]
    public String Time { get; set; }
}
public class Main
{
    [JsonProperty("temp")]
    public string Temp { get; set; }
    [JsonProperty("humidity")]
    public string Humidity { get; set; }
}

public class Wind
{
    [JsonProperty("speed")]
    public string Speed { get; set; }
}