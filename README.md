# posterr
###### by Daniel Burghardt

## Running the app
### Using Docker
#### To run the container:
`docker-compose up`

Access it at: https://localhost:8080/swagger/index.html

#### To rebuild and run the container (in case of any modifications):
`docker-compose up --build`

#### To run the unit tests:
`docker build --target tests -t tests:latest .`

### Using Visual Studio
#### 1. Run the SQL Server
`docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=PosterrDB_123' -p 1433:1433 -d --name=standalone_sqlserver mcr.microsoft.com/mssql/server`
#### 2. Adjust the ConnectionString
In the `appsettings.json` change the `Server=db` to `Server=localhost`.

#### 3. Run the Posterr project
Access it at: https://localhost:7039/swagger/index.html

## General considerations
I have added Swagger for a basic interface to help with the testing of the endpoints. It can be accessed at `/swagger/index.html`. Otherwise the endpoints can be directly reached using the prefix `/api` (e.g.: `/api/posts`).

There are four users seeded in the database (ID's: `1`, `2`, `3`, `4`). The "current user" is hardcoded in the `UsersService.cs` to be user "user_one". That can be changed simply by changing the ID parameter there to be another one. Any new posts will take the "current user" as author.

For loading the posts both in the homepage and in the profile page, I have built the `/posts` endpoint with a *PageSize* and *CurrentPage* query parameters for fetching the desired amount of posts (10 or 5) and to continuously load more posts as the user scrolls. Additionally, this endpoint supports the query parameters *UserId*, *StartDate* and *EndDate*, allowing the client to load only posts of a specific user (Only mine toggle in the Homepage) and to filter them by post date.

In case of a big chain of *Repost* &rarr; *Quote* &rarr; *Repost* &rarr; *Quote* I have chosen to only load one level deep child posts. So, for a quote post, the quoted post is also included in the response. Like on Twitter, if the user wants to see deeper in the chain he would have to open the details of a post, potentially making use of an additional endpoint.

New posts can be made using the endpoints `/posts/post`, `/posts/quote` and `/posts/repost`, which checks on a bunch of constraints: max. 777 characters, max. 5 posts per day, no reposts of reposts, no quotes of quotes, no reposting or quoting own posts.

## Performance considerations
For fetching posts, the PostsRepository uses a `Skip` &rarr; `Take` approach for pagination. Such approach has its performance flaws, because the database still needs to process the skipped over entities. Even so, I choose to stick to this approach because of the very low amount of posts being loaded per request and making the assumption that the great majority of users will not be scrolling extremely far down the feed, mostly not going over page 10 (100 posts) before leaving the page or reloading, starting over at page 1. Given that, I believe this approach would be performant enough, but it would be healthy to re-evaluate it after collecting some user feedback and performance data (request response times, etc.) after the application has been live for a little while.

## Critique
The first thing I would like to address is that if the app was to actually go live, I think it would be crucial to have the database server hosted outside of the docker container. Better yet on a separate server than the app. I have only set it all up in a single container for the purposes of demoing this work in a more convenient way.

Having had more time though, I would have liked to setup a custom Model Binder that would work well with the derived DTO classes, so that it no longer would be necessary to have the `QuotedPost` and `RepostedPost` objects of the `QuotedPostDto` and `RepostDto` classes be of type `object`, but rather be of type `PostDto`. Nor would it be needed to perform the  `ToList<object>()` conversion in the `GetPosts` endpoint.

Furthermore, it would have been nice to setup some integration tests in addition to the unit tests, which provide far more reliable results than the unit tests alone.

If the app was to grow to greater proprotions, I believe that the biggest candidate for potentially causing issues would be the `GetPosts` endpoint, as discussed earlier in the **Performance considerations** section. With a bigger user base, that endpoint would be the one putting the biggest load on the server and the database.

To reduce the load on the server, an alternative would be to start horizontally scalling with the addition of new servers, taking off the load from the main server. Also, as pointed out above, ideally there would be dedicated servers for hosting the database and others for hosting the web server(s).

Additionally, looking specifically at the `GetPosts` endpoint (which would certainly be the most often called one), a good performance optimization could be done with the addition of indexes to de Posts table in the database, on the `CreatedAt` and `UserId` columns, used in the query for sorting and filtering the posts.
