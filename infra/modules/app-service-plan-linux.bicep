param name string
param location string
param tags object
param sku string

resource appServicePlan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: name
  location: location

  properties: {
    reserved: true
  }
  sku: {
    name: sku
  }
  kind: 'linux'
  tags: tags
}

// resource salock 'Microsoft.Authorization/locks@2020-05-01' = {
//   name: 'salock'
//   scope: appServicePlan
//   properties: {
//     level: 'CanNotDelete'
//     notes: 'Should not be deleted.'
//   }
// }

output name string = appServicePlan.name
output id string = appServicePlan.id
