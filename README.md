
# Demo
dotnet core training demos

logging requires seq

https://datalust.co/seq


| Chapter 			| Topics  	|
| -----------------------------	| -------------	|
| Introduction to .Net Core	| About .NET Core <br> .NET Core characteristics <br> Supported Languages <br> Install .NET SDK <br> Install the .NET Core Runtime <br> Introduction to .NET Core CLI <br> Get started with .NET Core using the .NET Core CLI <br> Startup class <br> Host	|
| Dependency Injection		| Dependency injection <br> Services	|
| Middleware 			| Middlewares <br> Custom Middlewares	|
| Configuration			| Configuration	<br> Options pattern in ASP.NET Core <br> Multiple Configuration Sources <br> Use multiple environments in ASP.NET Core	|
| Web APIs  			| Routing in ASP.NET Core <br> Swagger ui <br> Model binding and Model Validation	|
| Action Filters 		| Action Filters <br> Custom Action Filters	|
| Exception Handling		| Handle errors in ASP.NET Core	<br> Handle Exceptions in ASP.NET Core	|
| Logging			| Configuration and LogLevels <br> Providers <br> Seq   |
| Authentication with JWT	| JWT <br> Identity server intro <br> Authentication <br> Authorization	|
| Unit Test			| AAA <br> Mocking				|	
| Entity Framework Introduction | Entity Framework <br> Repository Pattern	|
| Razor Pages 			| Introduction					|

# Blazor

## Hosting models
- Server
- Client

## Routing
- @page "/route"
- @page "/route/{parameter}"

## Data Binding
- One way @bind-value
- Two way @bind-value:event="oninput"

## Components
- Flexible and lightweight self contained functionality with UI
- Parameter
- Cascading Parameter

# Virtualize
```C#
<Virtualize ItemsProvider="LoadEmployees" ItemSize="40" Context="employee">
    <ItemContent>
        <tr>
            <td>@employee.FirstName</td>
            <td>@employee.LastName</td>
            <td>@employee.JobTitle</td>
        </tr>
    </ItemContent>
    <Placeholder>
        <tr>
            <td>Loading...</td>
        </tr>
    </Placeholder>
</Virtualize>
```

## Component Render
- Component created
- Component events are triggered
- Component parameter changes
- Manual - StatusHasChanged()

## Events
- Parent - SetParameter
- Parent - OnInitialized
- Parent - OnParametersSet
  - Child - SetParameter
  - Child - OnInitialized
  - Child - OnParametersSet
- Parent - OnAfterRender
  - Child - OnAfterRender

## EventCallBack
- EventCallBack 
- EventCallBack of T

## Templated Components
- RenderFragment
- RenderFragment of T
  - TItem

## Dependency Injection
- inject

## More
- LocalStorage
- CSS Isolation
- JS Isolation
  
