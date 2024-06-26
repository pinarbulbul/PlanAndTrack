# PlanAndTrack


A system has been developed that will operate in a transparent way where a test unit can manage requests for different test types from many different sources, plan in an objective manner, monitor processes and their performance criteria, and report when necessary. Two different objectives are studied according to the purposes of meeting the largest number of requests and prioritizing the most important requests according to their importance level. In addition to the constraints of the Multi-dimensional Knapsack problem, the proposed planning model considers the prerequisite condition to be considered in the completion and planning of requests. This prerequisite situation is when some requests have another request that must be completed before they can be completed. Traceability through the system will provide transparency to requesters, team members, and managers. In this study, the test unit is considered, but job planning and tracking of processes are also valid concerns for other business units. The proposed system can also be used for other business units.


The article: [PBU_ Conference Article.pdf](https://github.com/pinarbulbul/PlanAndTrack/files/12406133/PBU_.Conference.Article.pdf)


# TO START

Commands for DB migrations:

---ApplicationIdentity

dotnet ef migrations add InitialApplicationIdentity -c ApplicationIdentityDbContext --startup-project PlanAndTrack.Web --project PlanAndTrack.Infrastructure -o EntityFrameworkCore/ApplicationIdentity/Migrations
 
dotnet ef database update -c ApplicationIdentityDbContext --startup-project PlanAndTrack.Web --project PlanAndTrack.Infrastructure

 
---TestRequestDbContext 

dotnet ef migrations add InitialTestRequest   -c TestRequestDbContext --startup-project PlanAndTrack.Web --project PlanAndTrack.Infrastructure  -o EntityFrameworkCore/TestRequest/Migrations

dotnet ef database update -c TestRequestDbContext --startup-project PlanAndTrack.Web --project PlanAndTrack.Infrastructure 


_Localhost Link_: https://localhost:7145/

_Application Users_:

| User                          | Pass          |
| ------------------------------|:-------------:| 
| admin@planandtrack.com        | Admin12*      | 
| manager@planandtrack.com      | Manager12*    | 
| requester1@planandtrack.com   | Requester12*  | 
| tester@planandtrack.com       | Tester12*     | 
