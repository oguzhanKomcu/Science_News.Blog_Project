# ASP.NET CORE BLOG PROJECT

  I made the UI part of my blog project using Asp.Net Core MVC design pattern. I created the design of my project with Domain Driven Design. I used Autofac to implement IOC in this project. I made the methods and functions I used in my project by applying the asynchronous programming technique.

##  Domaın DRIVEN DESIGN (DDD)

  DDD is not an advanced technology or specific method. Domain Driven Design advocates a philosophy of how software should be modeled to adapt it to the digital world by creating real-world business models with a common language (Ubiquitous Language) that everyone can understand.

  According to DDD, one of the biggest problems in software projects is communication problem. The fact that the business unit and the technical team speak a different language creates long-term problems in determining the needs, developing and expanding the scope, and the main goal, the business problem that needs to be implemented with code, turns into a technology problem in an instant. DDD, on the other hand, promises to maximize the communication efficiency between the business unit and the technical team by following its principles and to model this process very well in terms of architecture.

DDD is created with 4-tier architecture.

- Domain Layer
- Application Layer
- Presentation Layer
- Infrastructure Layer


## ASYNCHRONOUS PROGRAMMING

Asynchronous programming allows work to be split into parts and all processes to be continued at the same time. With Asynchronous Programming, while a code we wrote in our program is being run, other codes can be run within the same program. In this way, while the user is using a part of our program, he can also operate with another part.

Asynchronous programming should not be confused with multi-threading. The codes that we will write asynchronously can also work on a single thread. The feature of asynchronous programming is not to run in different threads, but to ensure that more than one job is executed on our program at the same time by dividing the work we give into parts.
