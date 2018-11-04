# Alexa.NET.Reminders
Small helper library for Alexa.NET based skills to access the reminders API

N.B. The reminders client requires that you have a skill with reminders persmission enabled, and the user must have given your skill reminders permission (even if its your development account)

## Creating a reminder

```csharp
using Alexa.NET.Response
using Alexa.NET.Reminders
....
var reminder = new Reminder
{
    RequestTime = DateTime.UtcNow,
    Trigger = new RelativeTrigger(12 * 60 * 60),
    AlertInformation = new AlertInformation(new[] { new SpokenContent("it's a test", "en-GB") }),
    PushNotification = PushNotification.Disabled
};
var client = new RemindersClient(skillRequest);
var alertDetail = await client.Create(reminder);
Console.WriteLine(alertDetail.AlertToken);
```

## Retrieving Current Reminders
```csharp
var alertList = await client.Get();
foreach(var alertInformation in alertList.Alerts)
{
  //Your logic here
}
```

## Deleting a Reminder
```csharp
await client.Delete(alertToken);
```