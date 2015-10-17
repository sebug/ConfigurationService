### ConfigurationService
A small ASP.NET vNext API using IDataProtector to provide a configuration
dictionary to applications having a key.

To get started, create some files in the directory files, for example MySuperApp.json - you can verify that it found your file
by going to http://localhost:5000/api/configuration which lists the application codes.

The files have to contain a JSON object of key-value pairs:
     {
        "property1": "value1",
        "property2": "value2"
     }
