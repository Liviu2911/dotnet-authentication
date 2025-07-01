# Dotnet authentication project

## About
This is a simple authentication project, for which I chose ASP.NET as backend, local running postgres as database and react with TanStack Router and React Query for frontend.

I used token based authetication, every access token is issued every 15 minutes after logging in or creating an account, whereas a refresh token lives as long as a week.

Since it is a minimal project I decided to not add more advanced functions like reset password or other functionality after logging in (the users can only see their data, email and username, on the /user page).
