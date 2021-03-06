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
- [Parameter]
- [CascadingParameter]
- @typeparam TItem (typed component)

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

## EditForm and Validation
- EditForm
- DataAnnotationsValidator
- ValidationSummary
- ValidationMessage For
- ValidateComplexType
- ObjectGraphDataAnnotationsValidator

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

## Javascript
- JSInterop Namespace
- IJSRuntine
- IJSObjectReference
- [JSInvokable] Attribute
- _jsModule.InvokeAsync of T

## Authentication / Authorization
- [Authorize]
```C#
<CascadingAuthenticationState>
    <AuthorizeRouteView >
        <Authorized>                
        </Authorized>
        
        <NotAuthorized>                
        </NotAuthorized>
    </AuthorizeRouteView>
</CascadingAuthenticationState>
```

## More
- LocalStorage
- CSS Isolation
- JS Isolation
  
## Resources  
<a href="https://mudblazor.com/">mudblazor</a> <br>
<a href="https://matblazor.com">Matblazor</a> <br>
<a href="https://blazor.radzen.com/">Radzen</a>

