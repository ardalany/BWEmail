# Brightwheel Email Service
While I have experience writing code with Java and Python, I chose C# for this exercise because I am most familiar with the API framework of .Net compared to the other two languages. The Web API is a robust framework for writing microservices and it has a lot of features out of the box, such as data validation and dependency injection.

## Run the service
1. Download and install .Net 5.0 SDK from [here](https://dotnet.microsoft.com/download/dotnet/5.0).
2. Run the following command in the root folder:

        dotnet run --project .\BWEmail.Api\BWEmail.Api.csproj
3. The service will be running on ports 5000 and 5001. You can call the service directly (e.g. with Postman) or use swagger: [https://localhost:5001/swagger](https://localhost:5001/swagger)
4. If your browser does not trust the SSL certificate, run this command:

        dotnet dev-certs https --trust
5. You can change the email provider by changing the value of `EmailProvider` in [appsettings.json](.\BWEmail.Api\appsettings.json). The two acceptable values are:
    - Spendgrid
    - Snailgun

## Run the tests
I had time to only write one integration test, which was very helpful to identify errors and quirks of calling the email providers. You can run the test by running this command in the root folder:

    dotnet test

## Considerations
- Overall, I am happy with the structure of this project given the small scope of the exercise but in a real world scenario, I would create an Application project for the `EmailService` and client classes. Having all the classes as subfolders under `BWEmail.Api` can be hard to maintain once the code starts to grow.
- Including API keys in source code is a security risk and it is better to store them in a key-value store, such as AWS SSM. For the purpose of this exercise, I added them to the appsettings.json file. Because only the `Startup` class accesses the keys, the scope of change is minimal if the keys are to be imported from an external data source.
- Checking the status of the email in `SnailgunClient` is done 5 times with 4 second delays. It would be better to include these values in `appsettings.json` instead of hard-coding them. I also chose this numbers rather arbitrarily but they should be adjusted based on non-functional requirements which were absent in the PDF. For example, if this service were to be called asynchronously, I would be more generous with the delay between retries, and would perhaps consider exponential backoffs.
- Calling the Snailgun and Spendgrid APIs can be improved by using Polly with the HttpClientFactory to implement resilience policies such as retry and circuit breaker.
- I did not have time to add logging to the code but logging the errors would be very helpful in such a service.
- I did not have time to write unit tests but all the classes have mockable dependencies and the ground is set for writing unit tests.





