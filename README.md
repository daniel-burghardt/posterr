# posterr


https://localhost:7039/swagger/index.html

docker build -t posterr .
docker run -d -p 8080:80 --name posterrapp posterr

docker-compose up --build



docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=PosterrDB_123" -p 1433:1433 -d mcr.microsoft.com/mssql/server
server localhost


Next:

setup user in db so we can create posts