# cms-demo
a simple Content management system with Database first

### Keywords
- ASP.NET Core
- Content management system (CMS)
- Database first

---
#### Tutorial_08/1/2020
# Design a database that description your ideas

### What is the CMS?
- A content management system (CMS) is a software application that can be used to manage the creation and modification of digital content.
- A content management system (CMS) typically has two major components: a content management application (CMA), as the front-end user interface that allows a user, even with limited expertise, to add, modify, and remove content from a website without the intervention of a webmaster; and a content delivery application (CDA), that compiles the content and updates the website.

### I have an idea to create a structure CMS
#### If you a developer, you know about Object-oriented programming
- Object-oriented programming: is a programming paradigm based on the concept of "objects", which can contain data, in the form of fields (often known as attributes or properties), and code, in the form of procedures (often known as methods).

Here is a class object:
```
public class Person {
    public string Name { get; set; }
    public int Age { get; set; }
}
```
or common keywords like that
```
access_modifier object_type object_name {
    access_modifier field_type filed_name field_value;
}
```
#### Object as you know
- In computer science, an object can be a variable, a data structure, a function, or a method, and as such, is a value in memory referenced by an identifier.

- In the class-based object-oriented programming paradigm, object refers to a particular instance of a class, where the object can be a combination of variables, functions, and data structures.

- In relational database management, an object can be a table or column, or an association between data and a database entity (such as relating a person's age to a specific person.

#### So content in the layout, I mean CMS can be thought of as is an object

- From example about *Class object* above, assumption all keyword as tables is single without relation:
```
access_modifier
object_type
object_name
field_type
filed_name
field_value
```

- After think about relation, we should have two relation tables:
object_name is one instance of many objects, so we have *objects* table with properties: object_type, access_modifier, filed.
filed_name is one instance of many fileds, so we have *fields* table with properties field_type, access_modifier, field_value.

#### A sample schema for this idea
- You can see *cms.sql* at this project folder, you use this file to generate your local database.
- Or, I have created a schema template for this idea, you can use MySQL Workbench to connect with this connection infomation:
*Server=mysql-6037-0.cloudclusters.net;port=10001;Database=cms;user=admin;password=abc@1234;*

### You can think different! You should create new design is better!

### What is database first?
- This is an approach to create *Entity data model*, it creates model codes (classes, properties, DbContext etc.) from the *existing database* in the project.

---
## Now after have an existing database, we will use database first approach
### Setup your local development
- Net core 2.2
- Visual studio code
- MySQL

### Steps
1. Create new project ASP.NET Core
- Create a new folder for your project.
- At root folder add file global.json to use .netcore 2.2 
```
{
    "sdk": {
        "version": "2.2.100"
    }
}
```
- At the folder, open Command/Terminal then run this command to create a template webapi project:
```
dotnet new webapi
```

2. Add packageReferences EF core
- Add packageReference to file .csproj use CLI EF core:
```
  <ItemGroup>
     <PackageReference Include="MySql.Data.EntityFrameworkCore" Version="8.0.16" />
     <PackageReference Include="MySql.Data.EntityFrameworkCore.Design" Version="8.0.18" />
     <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.3"/>
  </ItemGroup>
```
- At the folder, open Command/Terminal then run this command to restore again:
```
dotnet restore
```

3. CLI generate Entities Models
- We have template CLI to scaffold:
```
dotnet ef dbcontext scaffold [ConnectionString] MySql.Data.EntityFrameworkCore -c [ContextName] -o [OutputFolder]
```

- At the folder, open Command/Terminal then run this command to create a models:
```
dotnet ef dbcontext scaffold "Server=mysql-6037-0.cloudclusters.net;port=10001;Database=cms;user=admin;password=abc@1234;CharSet=utf8;" MySql.Data.EntityFrameworkCore -c CmsContext -o Models
```

4. You will see Models folder be generated with class files
- A CmsContext class description all tables of schema and relation if have
- Classes corresponding to tables in the database schema

5. Next tutorial, we will add some layer and code to use this CmsContext to working with data

---
## Referecnces:
https://dev.mysql.com/doc/connector-net/en/connector-net-entityframework-core-scaffold-example.html
https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx