FeatureBee
==========

![FeatureBee CI Build Status](https://www.myget.org/BuildSource/Badge/featurebee-ci?identifier=24682329-ee15-41cc-ad5e-922ae684f261)


##How to contribute

1. Check the [issues](https://github.com/AutoScout24/FeatureBee/issues) for stuff that needs to be done.
2. Fork the repository
3. Make your changes (in it´s own branch)
4. Send PullRequest

## Feature Bee Client

The client is the application where you implement the features. The features are toggled in an IF-Statement:

    if (Feature.IsEnabled("My Feature"))
    {
      ViewBag.Message = "Congratulations!";
    }

You can unit test this by injecting your own evaluator: 

    Feature.InjectEvaluator(featureName => true);
    if (Feature.IsEnabled("My Feature")) // always true
    {
      ViewBag.Message = "Congratulations!";
    }

## Feature Bee Server 

The Server distributes the feature state to all registered clients, and is the ui for however is editing the feature state.

![Feature Bee Server](https://github.com/AutoScout24/FeatureBee/raw/master/documentation/images/Feature%20Bee%20-%20Server.png)

## Feature Bee TrayIcon

The TrayIcon is displayed on the page, and can be used to turn features off and on for the current user.

![Feature Bee Tray](https://github.com/AutoScout24/FeatureBee/raw/master/documentation/images/Feature%20Bee%20-%20Tray%20Icon.png)


