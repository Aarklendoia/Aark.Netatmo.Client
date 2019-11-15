# Aark.Netatmo.SDK

Aark.Netatmo.SDK is a library providing simple and complete access to Netatmo device data. It is based on the API provided by Netatmo through its [Netatmo Connect](https://dev.netatmo.com/) program.

## Current status

[![Board Status](https://dev.azure.com/aarklendoia/06099e08-821c-4a1e-8560-171b5a013fe6/281e0cef-528b-4d75-9990-220973046722/_apis/work/boardbadge/2da2fbba-f7ea-474d-8ff7-c4e80b463714?columnOptions=1)](https://dev.azure.com/aarklendoia/06099e08-821c-4a1e-8560-171b5a013fe6/_boards/board/t/281e0cef-528b-4d75-9990-220973046722/Microsoft.RequirementCategory/)
[![Build Status](https://dev.azure.com/aarklendoia/Aark.Netatmo.SDK/_apis/build/status/Aarklendoia.Aark.Netatmo.SDK?branchName=master)](https://dev.azure.com/aarklendoia/Aark.Netatmo.SDK/_build/latest?definitionId=26&branchName=master)

## Getting Started

To use this API, you must first [declare a new application to Netatmo](https://dev.netatmo.com/myaccount/createanapp) in order to obtain a Client ID and a Client Secret.

### Currently supported features

* Full access to weather station data (instant and historical data for base, outdoor, indoor, anenometer and raingauge modules).
* Partial access to energy data (reading instant data only, in progress).
* Full access security data (display of the timeline, reception of events, playback of videos and modification of people present or absent.).

## How to use

Install the [nuget package](https://www.nuget.org/packages/Aark.Netatmo.SDK/).

The entry point for using the API is the *NetatmoManager* class. Its constructor takes as parameters the Client ID and the Secret Client obtained via Netatmo Connect, as well as a Netatmo account identifier and the associated password.

```csharp
NetatmoManager netatmoManager = new NetatmoManager("YourClientId", "YourClientSecret", "NetatmoAccount", "NetatmoPassword");
```

You can then load one of the three main datasets provided by Netatmo:

* Weather Station data
* Energy data (thermostat and valves connected).
* Security data (cameras and smoke detectors).

For example, for the weather station, just call the following command:

```csharp
await netatmoManager.LoadWeatherDataAsync();
```

The loaded data will then be available in the *netatmoManager.WeatherStation* object. This object contains a list of Devices, each *Device* corresponding to a weather station referenced on the account of the connected Netatmo user. All stations are available, whether the user is the owner or just a guest.

To obtain a history of the measurements of a module, you can call the *LoadMeasuresAsync()* function of the desired module by specifying the type of data to be reported. You can request several types of data simultaneously.

The example below shows the historical temperature, humidity and noise data recorded by the base module of the first referenced weather station for the day of August 30, 2019:

```csharp
DateTime start = new DateTime(2019, 8, 30, 0, 0, 0);
DateTime end = new DateTime(2019, 8, 30, 23, 59, 59);
await netatmoManager.WeatherStation.Devices[0].Base.DefineDateRange(start, end).LoadMeasuresAsync(MeasuresFilters.Temperature | MeasuresFilters.Humidity | MeasuresFilters.Noise);
```

If you receive an error when connecting or recovering data, you can call *GetLastError()* to get the last error message returned by the API.

```csharp
netatmoManager.GetLastError();
```

## How to execute test

To be able to run the tests, you must create an appsettings.json file containing your access information and place it in the output directory of the test project:

```json
{
  "ConnectionStrings": {
    "ClientId": "xxxxxxxxxxxxxxxxxx",
    "ClientSecret": "xxxxxxxxxxxxxxxxxxxxxxxxx",
    "NetatmoAccount": "aarklendoia@example.com",
    "NetatmoPassword": "MyPassword"
  }
}
```

## Authors

* **Édouard Biton** - *Initial work* - [AArklendoïa](https://www.aarklendoia.com)

## License

This project is licensed under the MIT licence.