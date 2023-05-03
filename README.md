# Expensier

Expensier can be run via Visual Studio by clicking the *Expensier.sln* solution file and setting *Expensier.WPF* as the startup project, or by running the .exe file found in *Expensier.WPF\bin\Debug\net7.0-windows*.

  
## API Key

Head over to the Financial Modeling Prep website, and create an account to obtain an API key. After doing so, launch the application through Visual Studio and follow the next steps:

- In the Solutions Explorer find *Expensier.WPF*.

- Right click the project and go to *Properties*.

- Go to the *Debug* tab of properties.

- Click *Open debug launch profiles UI*.

- Add a new *Environmental Variable* with the following information:

		    Name = *API_KEY*
		    Value = <Your API key from Financial Modeling Prep website>