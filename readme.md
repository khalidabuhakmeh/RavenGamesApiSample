# RavenDB 5 with ASP.NET Core Sample

This is a demo project using ASP.NET Core with RavenDB data storage.


## Getting Started

You'll need a container instance of RavenDB 5+. You can run these commands in your terminal and follow the instructions to set up your server at http://localhost:8080.

```bash
docker pull ravendb/ravendb && docker run -d --name ravendb -p 8080:8080 ravendb/ravendb:latest
```

You may also need to get a developer license (FREE) from https://ravendb.net.

Once your server is up, you can run the following commands.

The first will load our documents into the database, and then apply the RavenDB indexes.

```bash
dotnet run --project ./GamesApi -lp "load csv data" && dotnet run --project ./GamesApi -lp "create indexes"
```

## HTTP Endpoints

Sample web requests

```http request
GET https://localhost:7112/games?search=Ninja
### 
GET https://localhost:7112/stats/top-100-publishers
###
GET https://localhost:7112/stats/platforms
###
GET https://localhost:7112/stats/platforms
### 
GET https://localhost:7112/stats/esrb
```

## License

Copyright (c) 2022 Khalid Abuhakmeh

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.




