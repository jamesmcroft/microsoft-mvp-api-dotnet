# Getting started

The Microsoft MVP API `ApiClient` class provides the one-stop shop for all interactions with the official Microsoft MVP API.

To get started, you will need to do the following:

- [Create a client application](https://apps.dev.microsoft.com/?mkt=en-us#/appList) and obtain the Client ID and secret
- Create a new application registration in the Microsoft MVP API developer portal using your Microsoft MVP account, and obtain the API subscription key

The Client ID, Client Secret, and API Subscription key will be used to initialize an instance of the `ApiClient` in your .NET application.

```csharp
this.API = new ApiClient(ClientId, ClientSecret, SubscriptionKey);
```

## Authenticating

The `ApiClient` offers a `RetrieveAuthenticationUri` method which will create a URI which can be used with a web authentication broker. 

We recommend providing the `Basic`, `Emails`, `OfflineAccess`, and `SignIn` scopes.

```csharp
var scopes = new List<MSAScope> { MSAScope.Basic, MSAScope.Emails, MSAScope.OfflineAccess, MSAScope.SignIn };

string authUri = App.API.RetrieveAuthenticationUri(scopes);
```

Once logged in, the auth code returned in the response can be exchanged for credentials with the `ApiClient.ExchangeAuthCodeAsync` method.

```csharp
string authCode = responseUri.GetQueryValue("code");

MSACredentials msaCredentials = await App.API.ExchangeAuthCodeAsync(authCode);
```

Once authenticated, the user can access the Microsoft MVP API if they are a registered, active Microsoft MVP.

## Profile

Every Microsoft MVP has a customizable profile that can be used to showcase the individual on the Microsoft MVP website. 

### Getting the user's profile

To retrieve the current authenticated user's profile, call the `ApiClient.GetMyProfileAsync` method. 

### Getting another user's profile

Another user's MVP profile can also be retrieved using the MVP ID for the specified user calling the `ApiClient.GetProfileAsync` method.

### Getting the user's profile image

The profile image for the authenticated user can be retrieved as base64 string by calling the `ApiClient.GetMyProfileImageAsync' method.

## Contributions

Every Microsoft MVP should provide their contributions for consideration in their annual renewals.

### Getting contributions

Your contributions can be retrieved using the `ApiClient.GetContributionsAsync` method providing an `offset` and `limit` parameters. 

This method supports pagination to improve the performance of your applications.

### Getting contribution areas

All contributions can be associated with specific contribution areas. These areas can be retrieved using the `ApiClient.GetContributionAreasAsync` method.

### Getting contribution types

As well as the area for a contribution, each require a specific type association, such as a blog post or open-source contribution. These types can be retrieved using the `ApiClient.GetContributionTypesAsync` method.

### Getting sharing preferences

Contributions have a visibility access which configures who can see the detail of the contributions. 

Calling the `ApiClient.GetSharingPreferencesAsync` method will retrieve the available sharing options.

### Adding a contribution

When submitting contributions, these can be added by constructing a `Contribution` object and submitting using the `ApiClient.AddContributionAsync` method.

### Updating a contribution

Submitted contributions can be later updated. The `ApiClient.UpdateContributionAsync` method requires the updated `Contribution` object to be provided.

### Deleting a contribution

Once a contribution has been submitted, it can also be deleted. Calling the `ApiClient.DeleteContributionAsync` method with the ID of the contribution will remove it from the authenticated user's MVP profile.

## Award Consideration Questions

Part of the re-award consideration process, the Microsoft MVP Award requires a set of questions to be answered.

### Getting award questions

The questions can be requested using the `ApiClient.GetCurrentAwardQuestionsAsync` method.

### Getting award question answers

The previously saved answers for questions can be requested using the `ApiClient.GetAwardQuestionAnswersAsync` method.

### Saving award question answers

Answers to questions can be saved to be submitted at a later date. This can be achieved using the `ApiClient.SaveAwardQuestionAnswersAsync` method.

### Submitting award question answers

Once answers are finalized and ready to be submitted for consideration, the `ApiClient.SubmitAwardQuestionAnswersAsync` method can be called. 

Note, once done, the answers cannot be changed under the current renewal cycle.