-Please change Log file path in nlog.config


1. Go to the Application folder and Run below command to restore dependencies:

    ```console
    dotnet restore
	```	

2. Run below command to build command 

    ```console
    dotnet build
    ```

3. Run your sample:

    ```console
    dotnet run
    ```
4. Access API.
   e.g. http://localhost:65241/news/15 will return 15 news

       
Things pending  :
- Swagger integration
