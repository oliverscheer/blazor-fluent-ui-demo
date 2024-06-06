@description('Location of solution')
param location string

@minLength(3)
param appName string

param linuxFxVersionDotNet string = 'DOTNETCORE|8.0'
param aspSku string = 'B3'

param sqlAdministratorName string

@secure()
param sqlAdministratorPassword string

var tags = {
}

// App Service Plan
var aspName = 'asp-${appName}'
module appServicePlan 'modules/app-service-plan-linux.bicep' = {
  name: 'deploy-${aspName}'
  params: {
    name: aspName
    location: location
    tags: tags
    sku: aspSku
  }
}

var connectionStrings = [
  {
      name: 'MusicDbContext'
      connectionString: sqlDatabase.outputs.connectionString
      type: 'SQLAzure' 
  }
]

// Web App
var appCopilotName = 'app-${appName}'
module appCopilot 'modules/web-app-linux.bicep' = {
  name: 'deploy-${appCopilotName}'
  params: {
    name: appName
    location: location
    tags: tags
    appServicePlanId: appServicePlan.outputs.id
    linuxFxVersion: linuxFxVersionDotNet  
    connectionStrings: connectionStrings
    appSettings: [
      {
        name: 'ASPNETCORE_ENVIRONMENT'
        value: 'Development'
      }
    ]
  }
}

// SQL Database
var sqlServerName = 'sqlserver${appName}'
var sqlDbName = appName

module sqlDatabase 'modules/sql-database-serverless.bicep' = {
  name: 'deploy-${appName}-sql'
  params: {
    location: location
    serverName : sqlServerName
    databaseName : sqlDbName
    administratorLogin: sqlAdministratorName
    administratorLoginPassword: sqlAdministratorPassword
  }
}

output appName string = appName
output hostName string = appCopilot.outputs.hostName
