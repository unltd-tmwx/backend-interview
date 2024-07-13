# Summary
- The endpoint can be tested by running up the TMW.Api application locally and running the http request from Tmw.Api.http.
- It can also be tested using Swagger [/swagger/index.html](https://localhost:7174/swagger/index.html)
- The result return 7 top scored items and 3 random customer with little behavioral data. 

## Other feedback
- My statistics knowledge is a bit rusty but hopefully the approach of normalising each category is not too far off.
- I first pulled the data into Excel and spent about half an hour looking at how to attempt the customer scoring.
- I assumer the locations are just random as they are all over the place.
- I used an InternalsVisibleToAttribute in Tmw.Lib to allow the test project to access internal services to speed up the dev. flow. I realise that it is not always the best approach but it was the only way that I could think of to break down the task and validate quickly. 
