@description('Name of app service.')
param name string

@description('Location for all resources.')
param location string

param tags object

@description('Id of Apps Service Plan.')
param appServicePlanId string

param linuxFxVersion string

param connectionStrings array = []

@description('Location for all resources.')
param appSettings array = []

@description('Location for all resources.')
param startupCommand string = ''

resource webApp 'Microsoft.Web/sites@2022-03-01' = {
  name: name
  location: location
  properties: {
    serverFarmId: appServicePlanId
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      alwaysOn: true
      http20Enabled: true
      webSocketsEnabled: true
      autoHealEnabled: true
      detailedErrorLoggingEnabled: true
      appCommandLine: startupCommand
      ftpsState: 'Disabled'
      minTlsVersion: '1.2'
      appSettings: union(appSettings, [
        {
          name: 'WEBSITE_RUN_FROM_PACKAGE'
          value: '1'
        }
        {
          name: 'ApplicationInsightsAgent_EXTENSION_VERSION'
          value: '~3'
        }
        {
          name: 'XDT_MicrosoftApplicationInsights_Mode'
          value: 'default'
        }
        {
          name: 'XDT_MicrosoftApplicationInsights_PreemptSdk'
          value: '1'
        }
        {
          name: 'ASPNETCORE_DETAILEDERRORS'
          value: 'false'
        }
      ])
      connectionStrings: connectionStrings
    }    
    httpsOnly: true
    clientAffinityEnabled: false
  }
  identity: {
    type: 'SystemAssigned'
  }
  tags: tags
}

resource webAppLogSettings 'Microsoft.Web/sites/config@2022-03-01' = {
  parent: webApp
  name: 'logs'
  properties: {
    applicationLogs: {
      fileSystem: {
        level: 'Warning'
      }
    }
    httpLogs: {
      fileSystem: {
        retentionInMb: 40
        enabled: true
      }
    }
    failedRequestsTracing: {
      enabled: true
    }
    detailedErrorMessages: {
      enabled: true
    }
  }
}

output name string = webApp.name
output resourceId string = webApp.id
output hostName string = webApp.properties.defaultHostName
output principalId string = webApp.identity.principalId
