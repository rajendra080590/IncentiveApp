// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.

export const environment = {
  production: false,
  name: 'dev',
  clientId: 'fe03b1ef-5d8d-4985-a6ee-aec8c69c825b', //this old one for testing please ignore
  
  authority: 'https://login.microsoftonline.com/8db90014-ca66-493d-b1b1-aa9ce0ba9aed/', 
  tenantId: '8db90014-ca66-493d-b1b1-aa9ce0ba9aed', 

  //redirectUrl: 'http://localhost:4200/#/login', //local
  //redirectUrl: 'https://incentivedevwebapp.azurewebsites.net/#/login', //DEV
  //redirectUrl: 'https://incentiveprodwebapp.azurewebsites.net/#/login', //PROD 
  redirectUrl: 'https://piecerateincentiveapp.fbmsales.com/#/login', //PROD Custom URL

  //postLogoutRedirectUri: 'https://incentivedevwebapp.azurewebsites.net/#/logout', //DEV
  postLogoutRedirectUri: 'https://incentiveprodwebapp.azurewebsites.net/#/logout', //PROD

  //scopeUri : "api://e45ca2ca-bfa2-48ae-bcd8-636004366f34/incentive-api-access", //dev
  scopeUri : "api://b50cae15-5f66-4c55-962e-dbd115ff8d23/incentive-api-access", //Prod

  //uiClienId: '39861754-f213-4c1a-9bdd-39391a3d6801', //this is created for testing on local
  //uiClienId: '998ea47f-0d49-4928-b767-f5e80edd53ac', // this is for Dev IncentiveDevWebApp
  uiClienId: '970ed1e2-f904-44a9-b654-2beeaa691b1c', // this is for Prod IncentiveWebApp
  
  mainUrl : 'https://incentiveprodwebapi.azurewebsites.net/'//PROD
  //mainUrl : 'https://IncentiveDEVWebApi.azurewebsites.net/'//POC
  //mainUrl : 'http://localhost:50869/' //Local
};
