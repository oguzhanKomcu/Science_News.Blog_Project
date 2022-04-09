# ASP.NET CORE BLOG PROJECT

  I made the UI part of my blog project using Asp.Net Core MVC design pattern. I created the design of my project with Domain Driven Design. I used Autofac to implement IOC in this project. I made the methods and functions I used in my project by applying the asynchronous programming technique.

##  DOMAIN DRIVEN DESIGN (DDD)

<img src="https://miro.medium.com/max/1400/0*28z_AXhf8mXoUihE.png" width="800" height="400">

  DDD is not an advanced technology or specific method. Domain Driven Design advocates a philosophy of how software should be modeled to adapt it to the digital world by creating real-world business models with a common language (Ubiquitous Language) that everyone can understand.

  According to DDD, one of the biggest problems in software projects is communication problem. The fact that the business unit and the technical team speak a different language creates long-term problems in determining the needs, developing and expanding the scope, and the main goal, the business problem that needs to be implemented with code, turns into a technology problem in an instant. DDD, on the other hand, promises to maximize the communication efficiency between the business unit and the technical team by following its principles and to model this process very well in terms of architecture.

DDD is created with 4-tier architecture.

- Domain Layer
- Application Layer
- Presentation Layer
- Infrastructure Layer

### Domain Layer

This is where the concepts of the business domain are. This layer has all the information about the business case and the business rules. Here’s where the entities are. As we mentioned earlier, entities are a combination of data and behavior, like a user or a product. You can have a look at how I use it in my project.[GitHub Pages](https://github.com/oguzhanKomcu/Science_News.Blog_Project/tree/master/Science_News.Domain)

### Application Layer

This layer doesn’t contain business logic. It’s the part that leads the user from one to another UI screen. It also interacts with application layers of other systems. It can perform simple validation but it contains no domain-related logic or data access. Its purpose is to organize and delegate domain objects to do their job. Moreover, it’s the only layer accessible to other bounded contexts. You can have a look at how I use it in my project.[GitHub Pages](https://github.com/oguzhanKomcu/Science_News.Blog_Project/tree/master/Science_News.Application)

### Presentation Layer

This layer is the part where interaction with external systems happens. This layer is the gateway to the effects that a human, an application or a message will have on the domain. Requests will be accepted from this layer and the response will be shaped in this layer and displayed to the user. You can have a look at how I use it in my project.[GitHub Pages](https://github.com/oguzhanKomcu/Science_News.Blog_Project/tree/master/Science_News.Presantation)

### Infrastructure Layer

This layer will be the layer that accesses external services such as database, messaging systems and email services. It supports communication between other layers and may contain supporting libraries for the UI layer.You can have a look at how I use it in my project.[GitHub Pages](https://github.com/oguzhanKomcu/Science_News.Blog_Project/tree/master/Science_News.Infrastructure)


## ASYNCHRONOUS PROGRAMMING

<img src="https://www.baeldung.com/wp-content/uploads/sites/4/2020/07/sync.png" width="800" height="400">

Asynchronous programming allows work to be split into parts and all processes to be continued at the same time. With Asynchronous Programming, while a code we wrote in our program is being run, other codes can be run within the same program. In this way, while the user is using a part of our program, he can also operate with another part.

Asynchronous programming should not be confused with multi-threading. The codes that we will write asynchronously can also work on a single thread. The feature of asynchronous programming is not to run in different threads, but to ensure that more than one job is executed on our program at the same time by dividing the work we give into parts.Asynchronous is about tasks. If the process executed by a thread requires waiting, instead of keeping the processor power idle, it switches to other jobs, does the work to be done in between, and continues the process that requires waiting from where it left off.

If we want to use asynchronous programming, we should use the "async" , "await" , "Task<T>","Task" keywords that come with C# 5.0.
  
The async keyword is used to activate the “await” keyword inside the function. Await provides a non-blocking feature that allows other processes to continue asynchronously while waiting for the task to be executed. await can only be used in cases where Task returns, C# compiler It will give an error when using await for methods that do not return Task. Our method, DownloadAndProcessFile, in which we use the await operator, started with async. The await operator is used in async otherwise it will throw a compilation error.

We will have methods that return a Task or Task<T> object. Defining these methods as asynchronous operations allows us to wait for them and continue to use the same execution sequence to run other operations that are unrelated to the expected task. We use the keywords "async" and "await" to process objects more easily.
  Examples ; 

  ```csharp
  public async Task Delete(int id)
        {
            var post = await _postRepo.GetDefault(x => x.Id == id);
            post.DeleteDate = DateTime.Now;
            post.Status = Status.Passive;
            await _postRepo.Delete(post);
        }
```
 
   ```csharp
  public async Task<List<GetPostsVM>> GetPosts()
        {
            var posts = await _postRepo.GetFilteredList(
                select: x => new GetPostsVM
                {
                    CategoryName = x.Category.Name,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                    CreateDate = x.CreateDate,
                    UserImagePath = x.Author.ImagePath,

                },
                where: x => x.Status != Status.Passive);

            return posts;
        }
```
## Expression in LINQ 
  
![alt text](https://www.tutorialsteacher.com/Content/images/linq/expression.png)

  
LINQ introduced the new type called Expression that represents strongly typed lambda expression. It means lambda expression can also be assigned to Expression<TDelegate> type. The .NET compiler converts the lambda expression which is assigned to Expression<TDelegate> into an Expression tree instead of executable code. This expression tree is used by remote LINQ query providers as a data structure to build a runtime query out of it .
  
This is how I created the method in the "BaseRepo" in the Science_News.Infrastructure layer. You can see more here.[GitHub Pages](https://github.com/oguzhanKomcu/Science_News.Blog_Project/blob/master/Science_News.Infrastructure/Repositories/BaseRepo.cs)
```csharp
  public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select,
                                                                  Expression<Func<T, bool>> where,
                                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                return  await orderBy(query).Select(select).ToListAsync();
            }
            else
            {
                return await query.Select(select).ToListAsync();
            }
        }
```
  
  
This is how I created the body of the method in the "PostServices.cs" class in the "PostService" in the "Services" folder in the Science_News.Application layer. You can see more here.[GitHub Pages](https://github.com/oguzhanKomcu/Science_News.Blog_Project/tree/master/Science_News.Application/Services)
  
   ```csharp
        public Task<List<PostVM>> GetPosts()
        {
            var posts = _postRepo.GetFilteredList(
                select: x => new PostVM
                {
                    Id = x.Id,
                    Title = x.Title,
                    CategoryName = x.Category.Name,
                    AuthorFirstName = x.Author.FirstName,
                    AuthorLastName = x.Author.LastName,
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Title),
                include: x => x.Include(x => x.Category).Include(x => x.Author));

            return posts;

        }
  
```
  
