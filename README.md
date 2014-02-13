FeatureBee
==========

FeatureBee helps you to release features without fears and pains. It´s the piece of puzzle which makes your Continuous Deplyoment perfect. FeatureBee decouples code releases from feature releases and makes it easy to rollout new features step-by-step.

For more details read [here](http://pgarbe.github.io/blog/2014/01/16/the-fear-of-new-features/)

![FeatureBee CI Build Status](https://www.myget.org/BuildSource/Badge/featurebee-ci?identifier=24682329-ee15-41cc-ad5e-922ae684f261)




## FeatureBee Server 

The Server distributes the feature state to all registered clients, and is the ui for however is editing the feature state.

![FeatureBee Server](https://github.com/AutoScout24/FeatureBee/raw/master/documentation/images/Feature%20Bee%20-%20Server.png)

## FeatureBee Client

The client is the application where you implement the features. The features are toggled in an IF-Statement:

    if (Feature.IsEnabled("My Feature"))
    {
      ViewBag.Message = "Congratulations!";
    }

You can unit test this by injecting your own evaluator: 

    Feature.InjectEvaluator(featureName => true);
    if (Feature.IsEnabled("My Feature")) // always true
    {
      ViewBag.Message = "Congratulations!";
    }

## FeatureBee TrayIcon

The TrayIcon is displayed on the page, and can be used to turn features off and on for the current user.

![Feature Bee Tray](https://github.com/AutoScout24/FeatureBee/raw/master/documentation/images/Feature%20Bee%20-%20Tray%20Icon.png)


##How to contribute

1. Check the [issues](https://github.com/AutoScout24/FeatureBee/issues) for stuff that needs to be done.
2. Fork the repository
3. Make your changes (in it´s own branch)
4. Send PullRequest


## Deploying a new release

When we're ready to deploy a new release, we need to do the following steps.

1. Create a branch named `release`.
2. Update [`ReleaseNotes.md`](ReleaseNotes.md). Note that the format is
important as we parse the version out and use that for the NuGet packages.
3. Push the branch to GitHub and create a pull request. This will kick off the
MyGet build of the NuGet package with this new version.
4. Run a local build to create the server package.
5. Test!
6. When you're satisfied with this release, push the package 
[from MyGet](https://www.myget.org/feed/featurebee-ci/package/FeatureBee) to NuGet.
7. Create a tag `git tag v#.#.#`. For example, to create a tag for 1.0.0 
`git tag v1.0.0`
8. Push the tag to the server. `git push --tags`
9. Accept the pull request.
10. Create a [new release](https://github.com/autoscout24/featurebee/releases/new)
using the tag you just created and pasting in the release notes you just wrote up. Don´t forget to attach the FeatureBee.Server.v#.#.#.zip file from your local `deploy` folder.
