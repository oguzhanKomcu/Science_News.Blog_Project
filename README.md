# BLOG PROJECT

  I made the UI part of my blog project using Asp.Net Core MVC design pattern. I created the design of my project with Domain Driven Design. I used Autofac to implement IOC in this project. I made the methods and functions I used in my project by applying the asynchronous programming technique.

##  Domaın DRIVEN DESIGN (DDD)

  DDD is not an advanced technology or specific method. Domain Driven Design advocates a philosophy of how software should be modeled to adapt it to the digital world by creating real-world business models with a common language (Ubiquitous Language) that everyone can understand.

  According to DDD, one of the biggest problems in software projects is communication problem. The fact that the business unit and the technical team speak a different language creates long-term problems in determining the needs, developing and expanding the scope, and the main goal, the business problem that needs to be implemented with code, turns into a technology problem in an instant. DDD, on the other hand, promises to maximize the communication efficiency between the business unit and the technical team by following its principles and to model this process very well in terms of architecture.


When you decide to switch to DDD, the principles of DDD appear in two main stages. These stages are Strategic design and Tactical design.

### STRATEGIC DESIGN 

  This stage is the stage where conceptual decisions are taken for the project to be transferred to DDD and the common language between the collaboration and the technical team is established. Strategic design consists of Ubiquitous language and Bounded context phases.

### Ubiquitous Language

 It is the basic DDD principle that aims to ensure the use of a common language between the technical team and the business unit. It argues that each of the concepts and tasks used in the business unit should also have a counterpart on the code side. Thus, the teams better agree with each other on what needs to be done.

### Bounded Context
  DDD is a recommended approach for use in complex systems. A complex domain may contain sub-domains. Let's continue to consider these subdomains through implementation. Let's say you develop autonomous parts for each subdomain such as Membership, Subscription, Invoicing, Api Server, Api Processor in our application. Each of them serves as an application in itself. Each of these is a Bounded Context. They are independent autonomous units that can be developed and operated without other services and independent of them. Bounded Contexts has the mission of maintaining the ubiquitous language in general and determining the boundaries/responsibilities of the subdomain. Therefore, they are still members of the domain layer.


