## ValueConverter
You could use fastdeepcloner to Convert/Parse all internaltypes like string, int, datetime etc.
```csharp

int? data = DeepCloner.ValueConverter<int?>("120jm") // null
int data = DeepCloner.ValueConverter<int>("120jm") // 0
int data = DeepCloner.ValueConverter<int>("120") // 120
decimal? data = DeepCloner.ValueConverter<decimal>("12,5") // 12.5
decimal? data = DeepCloner.ValueConverter<decimal?>("552602.25") // 552602.25
DateTime? data = DeepCloner.ValueConverter<DateTime?>("2015-01-01sd") // null
DateTime? data = DeepCloner.ValueConverter<DateTime>("2015-01-01") // DateTime
string base64String =DeepCloner.ValueConverter<string>(new byte[] { 116, 101, 115, 116 }); // base64string
byte[] base64String = DeepCloner.ValueConverter<byte[]>("dGVzdA==") // array

```

See above how we easily could convert object to any object.
Of course the data have to be parsable to the desirable type, or else a default value will be loaded.
### List of converterble types
```csharp
Boolean
int
Decimal
Double
Float
Long
DateTime
TimeSpan
String
base64string(string) to bytes
bytes to base64string(string)
```
