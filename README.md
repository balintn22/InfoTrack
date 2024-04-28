# This solution demonstrates the following concepts

 - Separation of concerns and correct dependencies\
   Domain implements business logic and is known to all.\
   Repo.Dummy implements a simple data repository.\
   SearchConnector.Dummy implements a demonstrational wrapper around a search engine.\
   They are only known to the top level app, UI.MVC provided to libraries via DI.\
   UI.Mvc implements one possible UI, implemented as an ASP.Net MVC web app.

 - Error handling\
   Concept: libraries throw exceptions; unhandled exceptions are handled by the top level exception handler.\
   Top level exception handler implemented in the app, in this case in the ASP.Net processing pipeline as a middleware element.

 - Dependency injection\
   Each component (class or controller) uses constructor injection, where arguments are provided by the DI provider.\
   UI.Mvc.Program demonstrates the use of an alternative (to dotnet default) DI provider, Autofac, that could be used
   to implement aspects for any class (that dotnet DI has no supprt for this). Aspects not demonstrated.

 - Object lifecycle management\
   Only the singleton tpye demonstrated in the repo and search service implementation, see Autofac SingleInstance registration.

 - Libraries providing the means to use their DI services via Use... extension methods (see DiRegistrator classes)
   and their use in UI.Mvc.Program.cs.

 - The use of ASP.Net middlewares (see error handling)

 - OO concepts like classes, interfaces, classes implementing an interface

 - Minimal, html class-based .css targeting see site.css .searchresult

 - Thin application wrapper around the business logic\
   UI.Mvc is a thin wrapper around the business logic, whose responsibilities are
     - user interaction
     - interacting with the domain
     - error handling

   It would be possible to implement other application / access types with their own restrictions, i.e.
     - an API
     - a message-based service, using the domain directly, but providing other ways of interacting with its consumers
     - a mobile or a desktop app, or a JVM client using the API or the message based service

 # Concepts not demonstrated
  
  - OO concepts like inheritance, abstract classes

  - The separation of an API and an app\
    In real life an API would probably be implemented to provide access to business logic to other consumers
    in a regulated fashion. In this case, for the demo app it was not needed.

  - Multiple clients

  - CQRS (as in Command and Query Responsibility Segregation)
