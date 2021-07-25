
# DWP Job Applicaiton Stage 2 Technical Excersise
## Osric Wilkinson

> Using the language of your choice please build your own API which calls the
> API at https://bpdts-test-app.herokuapp.com/, and returns people who are listed
> as either living in London, or whose current coordinates are within 50 miles of
> London.

# Instructions

0. Install [Dotnet](https://dotnet.microsoft.com/download) and [Git](https://git-scm.com/downloads)

1. Clone a copy of this repository

    `$ git clone https://github.com/Moosemorals/DWP.git`

2. Run tests

    `$ (cd DWP && dotnet test)`

3. Start server

    `$ (cd DWP/Web && dotnet run & )`

4. Retrieve list of people living in London, or whose coordinates are within 50 miles
   of London

   `$ curl http://localhost:5000/users`

# Notes

The solution is written in c# for .NET 5, so will complile and run under Windows,
Linux, or MacOS. 

This repository is [MIT](LICENCE.txt) licenced.