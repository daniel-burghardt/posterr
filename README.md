# posterr
###### by Daniel Burghardt

### To run the unit tests:
`docker build --target tests -t tests:latest .`

### To run the container:
`docker-compose up`

### To rebuild and run the container:
`docker-compose up --build`

Access it through: https://localhost:8080/swagger/index.html

## Pages
### Homepage
For loading the posts both in the homepage and in the profile page, I have built the `/posts` endpoint with a *PageSize* and *CurrentPage* query parameters for fetching the desired amount of posts (10 or 5) and to continuously load more posts as the user scrolls. Additionally, this endpoint has the query parameters *UserId*, *StartDate* and *EndDate*, allowing the client to load only posts of a specific user (Only mine toggle in the Homepage) and to filter them by post date.

In case of a big chain of *Repost* &rarr; *Quote* &rarr; *Repost* &rarr; *Quote* I have chosen to only load one level deep child posts. So, for a quote post for instance, the quoted post is also included in the response. Like on Twitter, if the user wants to see deeper in the chain he would have to open the details of a post, potentially making use of an additional endpoint.

New posts can be made using the endpoints `/posts/post`, `/posts/quote` and `/posts/repost`, which checks on a bunch of constraints: max. 777 characters, max. 5 posts per day, no reposts of reposts, no quotes of quotes, no reposting or quoting own posts.
